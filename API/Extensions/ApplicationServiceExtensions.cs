using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DbContexts;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config){
                 services.AddDbContext<DatingAppContext>(opt =>{
               opt.UseSqlite(config.GetConnectionString("DefaultConnection")); 
            });
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<ITokenService,TokenService>();

            return services;
        }
    }
}