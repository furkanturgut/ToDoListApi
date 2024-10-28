using Microsoft.AspNetCore.Mvc;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Text;
using System.Web.Mvc;

namespace TodoListApi.auth
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters; // Gerekli using ifadesi
    using System.Linq; // Linq için gerekli
    using System.Text;

    namespace TodoListApi.auth
    {
        public class BasicAuthAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (authHeader == null || !authHeader.StartsWith("Basic "))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Base64 kodunu çöz
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');

                var username = decodedCredentials[0];
                var password = decodedCredentials[1];

                // Auth servisiyle doğrulama
                var authService = (AuthService)context.HttpContext.RequestServices.GetService(typeof(AuthService));
                if (!authService.AuthenticateUser(username, password))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }


}
