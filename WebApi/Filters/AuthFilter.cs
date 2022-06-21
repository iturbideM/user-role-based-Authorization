using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Security;
using Security.Entities;

namespace WebApi.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private Permission _permission;
        private ISessionService? _authService;

        public AuthFilter(Permission permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _authService = context.HttpContext.RequestServices.GetService<ISessionService>();
            var token = context.HttpContext.Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "You need to be logged in. Missing token"
                };
            }
            else
            {
                var userId = _authService.ValidateToken(token);
                if (userId == null)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 400,
                        Content = "Wrong token"
                    };
                }
                else
                {
                    if (!_authService.HasPermission((int)userId, _permission))
                    {
                        context.Result = new ContentResult()
                        {
                            StatusCode = 403,
                            Content = "User logged in dont have permitions to this request."
                        };
                    };
                }
            }
        }
    }
}