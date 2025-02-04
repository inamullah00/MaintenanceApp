using Maintenance.Application.Wrapper;

namespace Maintenance.Web.AuthenticationAuthorization
{
    public static class AuthentiationAndAuthorizationConfig
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;

                options.Events.OnRedirectToAccessDenied = async evnt =>
                {
                    if (evnt.Request.Path.StartsWithSegments("/api"))
                    {
                        evnt.Response.StatusCode = 403;
                        evnt.Response.ContentType = "application/json";
                        var data = Result<object>.Failure(
                            "You are not authorized here",
                            StatusCodes.Status403Forbidden
                     );
                        await evnt.Response.WriteAsJsonAsync(data);
                    }
                    else
                    {
                        if (evnt.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        {
                            evnt.Response.StatusCode = 403;
                            evnt.Response.ContentType = "application/json";
                            var data = Result<object>.Failure(
                                "Unauthorized",
                                "You are not authorized here",
                                StatusCodes.Status403Forbidden
                            );

                            await evnt.Response.WriteAsJsonAsync(data);
                        }
                        else
                        {
                            evnt.Response.Redirect("/Error/AccessDenied");
                        }
                    }
                };

                options.Events.OnRedirectToLogin = async evnt =>
                {
                    if (evnt.Request.Path.StartsWithSegments("/api"))
                    {
                        evnt.Response.StatusCode = 401;
                        evnt.Response.ContentType = "application/json";
                        var data = Result<object>.Failure(
                            "Unauthenticated",
                            "You are not authorized here",
                            StatusCodes.Status401Unauthorized
                        );

                        await evnt.Response.WriteAsJsonAsync(data);
                    }
                    else
                    {
                        var returnUrl = evnt.Request.Path + evnt.Request.QueryString;
                        evnt.Response.Redirect($"/Account/Login?ReturnUrl={Uri.EscapeDataString(returnUrl)}");
                    }
                };
            });
        }

    }
}
