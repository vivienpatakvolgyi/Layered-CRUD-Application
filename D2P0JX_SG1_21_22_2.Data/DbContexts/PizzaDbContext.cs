using D2P0JX_SG1_21_22_2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace D2P0JX_SG1_21_22_2.Data.DbContexts
{
    public class PizzaDbContext : DbContext
    {
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public PizzaDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\D2P0JXDb.mdf;Integrated Security=true;MultipleActiveResultSets=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set relation
            modelBuilder.Entity<Pizza>(e =>
            e.HasOne(c => c.Restaurant)
            .WithMany(b => b.Pizzas)
            .HasForeignKey(c => c.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Customer>(e =>
            e.HasOne(c => c.Pizza)
            .WithMany(b => b.Customers)
            .HasForeignKey(c => c.PizzaId)
            .OnDelete(DeleteBehavior.Cascade));

            //Seed
            var donpepe = new Restaurant() { Id = 1, Name = "Don Pepe" };
            var pizzahut = new Restaurant() { Id = 2, Name = "Pizza Hut" };
            var pizzaforte = new Restaurant() { Id = 3, Name = "Pizza Forte" };


            var margherita = new Pizza() { Id = 1, Name = "Margherita Pizza", Price = 1100, Size = 32, RestaurantId = donpepe.Id, IsGlutenFree = true };
            var hawaiian = new Pizza() { Id = 2, Name = "Hawaiian Pizza", Price = 1500, Size = 45, RestaurantId = donpepe.Id, IsGlutenFree = false };
            var cheese = new Pizza() { Id = 3, Name = "Cheese Pizza", Price = 1400, Size = 32, RestaurantId = pizzahut.Id, IsGlutenFree = false };
            var buffalo = new Pizza() { Id = 4, Name = "Buffalo Pizza", Price = 1700, Size = 45, RestaurantId = pizzahut.Id, IsGlutenFree = false };
            var pepperoni = new Pizza() { Id = 5, Name = "Pepperoni Pizza", Price = 1300, Size = 32, RestaurantId = pizzaforte.Id, IsGlutenFree = false };
            var veggie = new Pizza() { Id = 6, Name = "Veggie Pizza", Price = 1900, Size = 45, RestaurantId = pizzaforte.Id, IsGlutenFree = true };

            var customer1 = new Customer() { Id = 1, Name = "Kovács István", PizzaId = hawaiian.Id };
            var customer2 = new Customer() { Id = 2, Name = "Németh Béla", PizzaId = hawaiian.Id };
            var customer3 = new Customer() { Id = 3, Name = "Kiss Anna", PizzaId = cheese.Id };
            var customer4 = new Customer() { Id = 4, Name = "Horváth Krisztina", PizzaId = cheese.Id };
            var customer5 = new Customer() { Id = 5, Name = "Benkő Péter", PizzaId = margherita.Id };
            var customer6 = new Customer() { Id = 6, Name = "Lázár Rebeka", PizzaId = buffalo.Id };
            var customer7 = new Customer() { Id = 7, Name = "Szabó Anna", PizzaId = cheese.Id };
            var customer8 = new Customer() { Id = 8, Name = "Horváth Krisztina", PizzaId = veggie.Id };
            var customer9 = new Customer() { Id = 9, Name = "Medgyes Ervin", PizzaId = hawaiian.Id };
            var customer10 = new Customer() { Id = 10, Name = "Tolvai Krisztián", PizzaId = pepperoni.Id };
            var customer11 = new Customer() { Id = 11, Name = "Kiss Anna", PizzaId = cheese.Id };
            var customer12 = new Customer() { Id = 12, Name = "Horváth Krisztina", PizzaId = pepperoni.Id };
            modelBuilder.Entity<Restaurant>().HasData(donpepe, pizzahut, pizzaforte);
            modelBuilder.Entity<Pizza>().HasData(margherita, hawaiian, cheese, buffalo, pepperoni, veggie);
            modelBuilder.Entity<Customer>().HasData(customer1, customer2, customer3, customer4, customer5, customer6, customer7, customer8, customer9, customer10, customer11, customer12);

        }
    }
}
