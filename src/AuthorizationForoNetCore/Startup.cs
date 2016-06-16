using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using AuthorizationForoNetCore.Policy;
using AuthorizationForoNetCore.IRepository;
using AuthorizationForoNetCore.Repository;

namespace AuthorizationForoNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
   
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
                options.AddPolicy("Over21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
                options.AddPolicy("CanLogin", policy => policy.Requirements.Add(new LoginRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, HasPasswordHandler>();
            services.AddSingleton<IAuthorizationHandler, HasAccessTokenHandler>();
            services.AddSingleton<IDocumentRepository, FakeDocumentRepository>();
            services.AddSingleton<IAuthorizationHandler, DocumentEditHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
               AuthenticationScheme = "MyCookieMiddlewareInstance",
               LoginPath = new PathString("/Account/Unauthorized"),
               AccessDeniedPath = new PathString("/Account/Forbidden"),
               AutomaticAuthenticate = true,
               AutomaticChallenge = true
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
