namespace MealMap.Api.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int PrepTimeMinutes { get; set; }
        public List<string> Tags { get; set; } = new();

        public List<Ingredient> Ingredients { get; set; } = new();
    }
}