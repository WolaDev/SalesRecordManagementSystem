﻿using Microsoft.AspNetCore.Identity;
using SalesRecordManagementSystem.Models;

namespace AimaanRegistrationSystem.Utility;

public static class Helper
{
    public static async Task<(string userId, string userName)> GetCurrentUserIdAsync(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext!.User?.Identity?.IsAuthenticated == true)
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            return (user!.Id, user.UserName!);
        }

        return (string.Empty, string.Empty); // Return empty strings if not authenticated
    }
}
