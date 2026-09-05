namespace MealMap.Api.Models
{
    public class User
    {
        public int Id { get; set; } // Get/Set is a property that allows us read/write access
        public string Username { get; set; } = string.Empty; // string.Empty is default value
        public string PasswordHash { get; set; } = string.Empty;
        public BudgetTier BudgetTier { get; set; }
        public string? Postcode { get; set; } // question mark after string means it is allowed to be null
        public double? Latitude { get; set; }
        public double? Longitude { get; set; } 

        public List<MealPlanEntry> MealPlanEntries { get; set; } = new();
    }
}