using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceProvider_BLL.Authentication;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_BLL.Reposatories;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System.Reflection;
using System.Text;

namespace SeeviceProvider_PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

                
            services.AddCors(options =>
                    options.AddDefaultPolicy(builder =>
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                    )
            );

            services
                .AddMapsterConfiguration()
                .AddFluentValidationConfiguration()
                .AddAuthConfiguration(configuration);

            var connectionString = configuration.GetConnectionString("Default Connection") ??
             throw new InvalidOperationException("connection string 'Default Connection' not found.");

            services.AddDbContext<AppDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(connectionString));

            //services.AddIdentity<Vendor, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IAuthRepositry, AuthRepositry>();



            return services;
        }

        private static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;

            mappingConfig.Scan(typeof(VendorRepository).Assembly);

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }

        private static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(typeof(VendorRepository).Assembly);

            return services;
        }

        private static IServiceCollection AddAuthConfiguration(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddIdentity<Vendor, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(o =>
           {
               o.SaveToken = true;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                   ValidIssuer = jwtSettings?.Issuer,
                   ValidAudience = jwtSettings?.Audience
               };
           });

            return services;
        }
    }
}
