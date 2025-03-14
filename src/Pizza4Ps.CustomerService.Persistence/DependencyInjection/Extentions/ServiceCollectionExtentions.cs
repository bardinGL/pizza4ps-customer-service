using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pizza4Ps.CustomerService.Persistence.Intercepter;
using Pizza4Ps.CustomerService.Domain.Abstractions.Repositories.RepositoryBase;
using Pizza4Ps.CustomerService.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using Pizza4Ps.CustomerService.Domain.Entities.Identity;

namespace Pizza4Ps.CustomerService.Persistence.DependencyInjection.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddSQLServerPersistence(this IServiceCollection services)
        {
            services.AddDbContextPool<DbContext, ApplicationDBContext>((provider, builder) =>
            {
                // dùng để lấy configuration từ appsetting và map vào object
                //var passwordValidatorOptions =
                //    services.BuildServiceProvider().GetRequiredService<IOptionsMonitor<PasswordValidatorOptions>>();
                //var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();
                var configuration = provider.GetRequiredService<IConfiguration>();
                // Lấy AuditSaveChangesInterceptor từ DI container
                var auditSaveChangesInterceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();
                builder
                    .UseSqlServer(
                    connectionString: configuration.GetConnectionString("MyDbContext"),
                    sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.GetName().Name))
                .AddInterceptors(auditSaveChangesInterceptor);
            });
        }
        public static IServiceCollection AddRepositoryAssembly(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryBase<,>)))  // Điều kiện lọc các repository
                .AsImplementedInterfaces()  // Đăng ký chúng với interface tương ứng
                .WithTransientLifetime());
            // Các đăng ký khác
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            return services;
        }

        public static void AddInterceptorPersistence(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<AuditSaveChangesInterceptor>();
        }

    }
}
