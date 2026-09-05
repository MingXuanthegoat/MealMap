using Microsoft.EntityFrameworkCore;
using MealMap.Api.Models;

namespace MealMap.Api.Data
{
    public class MealMapDbContext : DbContext
    {
        public MealMapDbContext(DbContextOptions<MealMapDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>(); //DbSet<> is creating table for this entity/class
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<MealPlanEntry> MealPlanEntries => Set<MealPlanEntry>();
    }
}