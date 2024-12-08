using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthECAPI.Controllers
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", GetUserProfile);
            app.MapGet("/getUserRole", GetUserRole);
            app.MapGet("/Users", GetAllUsers);
            app.MapGet("/Roles", GetRoles); // New endpoint for fetching roles
            app.MapPost("/AssignRole", AssignRole);
            app.MapPut("/Users/{id}", EditUser);
            app.MapDelete("/Users/{id}", DeleteUser);
            return app;
        }

        [Authorize]
        private static async Task<IResult> GetUserProfile(
            ClaimsPrincipal user,
            UserManager<AppUser> userManager)
        {
            string userID = user.Claims.First(x => x.Type == "userID").Value;
            var userDetails = await userManager.FindByIdAsync(userID);
            return Results.Ok(
                new
                {
                    Email = userDetails?.Email,
                    FullName = userDetails?.FullName,
                });
        }
        [Authorize]
        private static async Task<IResult> GetUserRole(
            ClaimsPrincipal user,
            UserManager<AppUser> userManager)
        {
            // Extract the user's ID from claims
            string userId = user.Claims.First(x => x.Type == "userID").Value;

            // Find the user by ID
            var appUser = await userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                return Results.NotFound("User not found.");
            }

            // Fetch the roles assigned to the user
            var roles = await userManager.GetRolesAsync(appUser);

            // Return the first role or a default message if no role is assigned
            var role = roles.FirstOrDefault() ?? "No Role Assigned";

            return Results.Ok(new
            {
                UserId = userId,
                Role = role
            });
        }

        [Authorize]
        private static async Task<IResult> GetAllUsers(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Fetch all users
            var users = await userManager.Users.ToListAsync();

            // Fetch roles for each user
            var userDetails = new List<object>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDetails.Add(new
                {
                    user.Id, // Include UserId
                    user.FullName,
                    user.Email,
                    user.Gender,
                    Role = roles.FirstOrDefault() ?? "No Role Assigned"
                });
            }

            return Results.Ok(userDetails);
        }

        [Authorize]
        private static async Task<IResult> GetRoles(RoleManager<IdentityRole> roleManager)
        {
            // Fetch all roles from the database
            var roles = await roleManager.Roles.Select(r => new
            {
                r.Id,
                r.Name
            }).ToListAsync();

            return Results.Ok(roles);
        }

        [Authorize]
        private static async Task<IResult> AssignRole(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AssignRoleRequest request)
        {
            // Find user
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return Results.NotFound($"User with ID {request.UserId} not found.");

            // Validate role
            var roleExists = await roleManager.RoleExistsAsync(request.RoleName);
            if (!roleExists)
                return Results.BadRequest($"Role {request.RoleName} does not exist.");

            // Remove existing roles (optional, depends on business rules)
            var currentRoles = await userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await userManager.RemoveFromRolesAsync(user, currentRoles);

            // Assign new role
            var result = await userManager.AddToRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
                return Results.BadRequest($"Failed to assign role: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            return Results.Ok($"Role {request.RoleName} assigned to user {user.Email}.");
        }

        [Authorize]
        private static async Task<IResult> EditUser(
            UserManager<AppUser> userManager,
            string id,
            EditUserRequest request)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return Results.NotFound($"User with ID {id} not found.");

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Gender = request.Gender;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Results.BadRequest($"Failed to update user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            return Results.Ok($"User {user.FullName} updated successfully.");
        }

        [Authorize]
        private static async Task<IResult> DeleteUser(
            UserManager<AppUser> userManager,
            string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return Results.NotFound($"User with ID {id} not found.");

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return Results.BadRequest($"Failed to delete user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            return Results.Ok($"User {user.Email} deleted successfully.");
        }
    }

    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }

    public class EditUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
