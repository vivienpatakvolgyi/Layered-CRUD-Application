using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Logic.Services;
using D2P0JX_SG1_21_22_2.Repository.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace D2P0JX_SG1_21_22_2.Logic.Infrastructure
{
    public static class BLInitialization
    {
        public static void InitBlServices(IServiceCollection services)
        {
            RepoInitialization.InitRepoServices(services);

            services.AddScoped<PizzaDbContext>((sp) => new PizzaDbContext());
            services.AddScoped<IPizzaLogic, PizzaLogic>();
            services.AddScoped<ICustomerLogic, CustomerLogic>();
            services.AddScoped<IRestaurantLogic, RestaurantLogic>();
        }
    }
}
