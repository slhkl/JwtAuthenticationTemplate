using Business.Concrete.Model;
using Business.Concrete.Services;
using Business.Discrete;
using Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Authorization;

namespace Presentation.Configuration
{
    public static class Extensions
    {
        public static void UseServices(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseMiddleware<RequestMiddleware>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddIocImports()
                .AddSwaggerForJwt()
                .AddAuthentication()
                .AddAuthorization()
                .AddCors();
        }

        private static IServiceCollection AddIocImports(this IServiceCollection services)
        {
            services.AddScoped<IService<Customer>, CustomerService>();
            services.AddScoped<IService<Client>, ClientService>();

            services.AddScoped<RequestMiddlewareModel>();

            return services;
        }

        private static IServiceCollection AddSwaggerForJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
             {
                 c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                 {
                     Name = "Authentication",
                     Type = SecuritySchemeType.Http,
                     Scheme = JwtBearerDefaults.AuthenticationScheme,
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authentication"
                 });
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] { }
                    }
                });
             });

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = TokenProvider.Instance.issuer,
                        ValidAudience = TokenProvider.Instance.audience,
                        IssuerSigningKey = new SymmetricSecurityKey(TokenProvider.Instance.key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    };
                    opt.SaveToken = true;
                });

            return services;
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomAccessPolicy", policy =>
                {
                    policy.RequireRole("Client", "Customer");
                });
            });

            return services;
        }

        private static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(p =>
            {
                p.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
