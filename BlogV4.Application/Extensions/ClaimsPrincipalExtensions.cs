﻿using System.Security.Claims;

namespace BlogV4.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.Name)?.Value;

        public static string GetUserId(this ClaimsPrincipal user)
           => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    
    
       
    
}
