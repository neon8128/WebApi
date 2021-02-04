using System;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using server_new_try.DTOs;
using Server_Try02.Services;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services.Location;
using WebApi.Validators;

namespace Server_Try02
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
            var mysqlconnection = Configuration.GetConnectionString("DefaultConnection"); // get db connection from json file
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IAuditService,AuditService>();
            services.AddScoped<ILocationService,LocationService>();

            services.AddControllersWithViews()
                    .AddFluentValidation();
       
            services.AddTransient<IValidator<RegisterUserDto>,RegisterUserValidator>();
            services.AddTransient<IValidator<LocationModel>,LocationValidator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AFhu9aVrbdUN3G4Vv87jWA7gCSuJG8c8D7cp7XrUMayEtcryWpGJzDQMnV7")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            
            services.AddDbContext<DataContext>(options => 
                options.UseMySql(mysqlconnection,
                    new MySqlServerVersion(new Version(10, 4, 13)))); // use MariaDbServerVersion for MariaDB

            //services.AddTransient<AppDb>(_ => new AppDb(Configuration["ConnectionStrings:DefaultConnection"]));
                    
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
