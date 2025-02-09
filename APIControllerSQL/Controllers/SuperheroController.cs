using APIControllerSQL.Data;
using APIControllerSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControllerSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        private readonly DataContext _DbContext;

        public SuperheroController(DataContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> GetAllHeroes()
        {
            var heroes = await _DbContext.Superheroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> GetHeroById(int id)
        {
            var hero = await _DbContext.Superheroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound("Hero not found");
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<Superhero>> PostHero(Superhero newHero)
        {
            _DbContext.Superheroes.AddAsync(newHero);
            await _DbContext.SaveChangesAsync();
            return Ok(newHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Superhero>> Deletehero(int id)
        {
            var hero = await _DbContext.Superheroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound("Hero not found");
            }
            _DbContext.Superheroes.Remove(hero);
            _DbContext.SaveChangesAsync();
            return Ok($"Removed hero {hero.HeroName}");
        }
    }
}
