using Microsoft.Extensions.Options;
using Moor.Core.Constant;
using Moor.Core.Session;
using Moor.Core.Utilities;
using Moor.Model.Authorize;
using Moor.Service.Exceptions;
using Moor.Service.Utilities.AppSettings;
using Moor.Service.Utilities.AuthorizeHelpers;

namespace Moor.API.Middlewares
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<MoorSettings> _moorSettings;

        public CustomAuthMiddleware(RequestDelegate next, IOptions<MoorSettings> moorSettings)
        {
            _next = next;
            _moorSettings = moorSettings;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();

            if (endpoint is not null)
            {
                var hasPerm = endpoint.Metadata.OfType<HasPermissionAttribute>().ToList();

                if (hasPerm is not null && hasPerm.Count != 0)
                {
                    var token = httpContext.Request.Headers["Authorization"].ToString();

                    if (string.IsNullOrEmpty(token))
                        throw new NotFoundException(TokenConstant.NOT_FOUND_TOKEN);

                    try
                    {
                        var tokenHelper = new TokenHelper(_moorSettings);
                        var loginUser = tokenHelper.ValidateToken(token);

                        if (loginUser is null)
                            throw new ClientSideException(TokenConstant.INVALID_TOKEN);

                        new SessionManager(httpContext)
                        {
                            User = new UserSessionModel
                            {
                                Username = loginUser.Username,
                                PersonnelId = loginUser.PersonnelId,
                                Roles = loginUser.Roles
                            }
                        };

                    }
                    catch (ClientSideException customException)
                    {
                        throw customException;
                    }
                    catch (Exception)
                    {
                        throw new ClientSideException(TokenConstant.NOT_FOUND_TOKEN);
                    }
                }

            }

            await _next(httpContext);
        }
    }
    public static class CustomAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthMiddleware>();
        }
    }
}
