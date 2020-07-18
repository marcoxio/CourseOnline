using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Courses;
using Dominio.Entities;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
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
                services.AddControllers()
                .AddFluentValidation(cfg => 
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<New>();

                    //Configure Core Identity
                    var builder = services.AddIdentityCore<User>();
                    var identityBuilder = new IdentityBuilder( builder.UserType,builder.Services);
                    identityBuilder.AddEntityFrameworkStores<CoursesOnlineContext>();
                    identityBuilder.AddSignInManager<SignInManager<User>>();
                    services.TryAddSingleton<ISystemClock, SystemClock>();
                });
            #endregion

           
            
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
