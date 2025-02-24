using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingManager.MVC.Security
{
    public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? role = context.HttpContext.Session.GetString("ROLE");
            if(role != "Admin")
            {
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.Redirect("/Customer/Login");
                // throw new BadHttpRequestException("", 403);
            }
        }
    }
}
