namespace MealMap.Api.Models
{
    public enum MealSlot { Breakfast, Lunch, Dinner }

    public class MealPlanEntry
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public MealSlot Slot { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}