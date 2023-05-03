namespace ExpenseTracking.Web.Extensions
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Services;
    using ExpenseTracking.Infrastructure.GenericRepository;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}