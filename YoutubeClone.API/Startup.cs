using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using YoutubeClone.API.Middleware;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Application.Mapping;
using YoutubeClone.Application.Posts.Handler;
using YoutubeClone.Application.Profiles;
using YoutubeClone.Application.Videos;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Infrastructure.Email;
using YoutubeClone.Infrastructure.FileUpload;
using YoutubeClone.Infrastructure.Security;
using YoutubeClone.Interfaces;
using YoutubeClone.Persistence.Data;
using YoutubeClone.Persistence.Repositories;

namespace YoutubeClone.API
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
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddCors(opt =>
                        opt.AddPolicy("CorsPolicy", policy => {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            }));

            services.AddMediatR(typeof(GetPostsHandler).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();

            var builder = services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.SignIn.RequireConfirmedEmail = true;
            });
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
            identityBuilder.AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IFileStorage, FileStorage>();
            services.AddScoped<IProfileReader, ProfileReader>();
            services.AddScoped<IUploadVideoService, UploadVideoService>();
            services.Configure<SendGridSettings>(Configuration.GetSection("SendGrid"));
            services.Configure<VideoSettings>(Configuration.GetSection("AcceptedVideoFileTypes"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
