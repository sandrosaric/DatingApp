using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using API.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config= config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

        
            
           services.AddApplicationServices(_config);
            services.AddControllers();
             services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

         services.AddIdentityServices(_config);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
           
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          app.UseMiddleware<Middleware.ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ApiCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
