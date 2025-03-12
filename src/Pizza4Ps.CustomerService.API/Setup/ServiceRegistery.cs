using Microsoft.AspNetCore.Identity;
using Pizza4Ps.CustomerService.API.DependencyInjection.Extentions;
using Pizza4Ps.CustomerService.Application.DependencyInjection.Extentions;
using Pizza4Ps.CustomerService.Domain.DependencyInjection.Extentions;
using Pizza4Ps.CustomerService.Domain.Entities.Identity;
using Pizza4Ps.CustomerService.Persistence;
using Pizza4Ps.CustomerService.Persistence.DependencyInjection.Extentions;

namespace Pizza4Ps.CustomerService.API.Setup
{
    public static class ServiceRegistery
    {
        public static IServiceCollection ServiceRegisteryMethod(this IServiceCollection services)
        {
            // Configure CORS to allow everything
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            // Add HttpContextAccessor
            services.AddHttpContextAccessor();

            // Register Application Layer Services
            RegisterApplicationServices(services);

            // Register Persistence Layer Services
            RegisterPersistenceServices(services);

            // Register Domain Layer Services
            RegisterDomainServices(services);

            // Register API Layer Services
            RegisterApiServices(services);

            // Register Infrastructure Services
            RegisterInfrastructureServices(services);
            return services;
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddServicesFromAssembly();
            services.AddAutoMapperService();
            services.AddMediatRService();
        }

        private static void RegisterPersistenceServices(IServiceCollection services)
        {
            services.AddInterceptorPersistence();
            services.AddSQLServerPersistence();
            services.AddRepositoryAssembly();
            services.AddIdentityServices();

        }
        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddDomainServices();
        }
        private static void RegisterApiServices(IServiceCollection services)
        {
            services.AddSwaggerAuthUI();
        }

        private static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<ApplicationDBContext>()
                    .AddDefaultTokenProviders();
        }

        private static void RegisterInfrastructureServices(IServiceCollection services)
        {
        }
    }
}
