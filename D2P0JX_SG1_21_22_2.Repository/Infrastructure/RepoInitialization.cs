using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using D2P0JX_SG1_21_22_2.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace D2P0JX_SG1_21_22_2.Repository.Infrastructure
{
    public static class RepoInitialization
    {
        public static void InitRepoServices(IServiceCollection services)
        {
            services.AddScoped<PizzaDbContext>((sp) => new PizzaDbContext());
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
