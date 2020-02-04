using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace a2_s3673712_s3719368.Attributes
{
    public class AuthorizeAdmin: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var Admin = context.HttpContext.Session.GetString("Admin");
            if (Admin == null)
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }
        }

    }
}
