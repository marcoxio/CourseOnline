using System.Text;
using Aplicacion.Courses;
using Aplicacion.Interfaces;
using Dominio.Entities;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Security;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Context DB
            services.AddDbContext<CoursesOnlineContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Courses"));
            });
            #endregion
      

            #region Configuration MediatR
            services.AddMediatR(typeof(Consult.Handler).Assembly);
                
            #endregion

            #region AddFluentValidation
                //Configuration Unatorized 401 for all methods
                services.AddControllers(opt => {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(cfg => 
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<New>();
                });
            #endregion

            #region Core Identity
                   //Configure Core Identity
                    var builder = services.AddIdentityCore<User>();
                    var identityBuilder = new IdentityBuilder( builder.UserType,builder.Services);
                    identityBuilder.AddEntityFrameworkStores<CoursesOnlineContext>();
                    identityBuilder.AddSignInManager<SignInManager<User>>();
                    services.TryAddSingleton<ISystemClock, SystemClock>();
            #endregion

            // Config Security Controller
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is muy secret word"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    //Changes this for Company private IP
                    ValidateAudience = false,
                    // for some ips
                    ValidateIssuer = false
                }; 
            
            });

            //Jwt Generator
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserSession, UserSession>();

           
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Middleware HandlerError
                app.UseMiddleware<HandlerErrorMiddleware>();
            #endregion

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            //Jwt Authentication
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
