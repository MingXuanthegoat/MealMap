using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MealMap.Api.Data;
using MealMap.Api.Models;

namespace MealMap.Api.Controllers
{   

    // Makes every endpoint live under /api/recipes
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly MealMapDbContext _context;

        public RecipesController(MealMapDbContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();
        }

        // GET: api/recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return NotFound();
            return recipe;
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // PUT: api/recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id) return BadRequest();

            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}