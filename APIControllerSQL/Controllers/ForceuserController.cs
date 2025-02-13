using APIControllerSQL.Data;
using APIControllerSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControllerSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForceuserController : ControllerBase
    {
        private readonly DataContext _DbContext;

        public ForceuserController(DataContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Forceuser>>> GetAllForceusers()
        {
            var forceusers = await _DbContext.Forceusers.ToListAsync();
            return Ok(forceusers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forceuser>> GetForcesserById(int id)
        {
            var forceuser = await _DbContext.Forceusers.FindAsync(id);
            if (forceuser == null)
            {
                return NotFound("Hero not found");
            }

            return Ok(forceuser);
        }

        [HttpPost]
        public async Task<ActionResult<Forceuser>> PostForceuser(Forceuser newForceuser)
        {
            _DbContext.Forceusers.AddAsync(newForceuser);
            await _DbContext.SaveChangesAsync();
            return Ok(newForceuser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Forceuser>> DeleteForceuser(int id)
        {
            var forceuser = await _DbContext.Forceusers.FindAsync(id);
            if (forceuser == null)
            {
                return NotFound("Hero not found");
            }
            _DbContext.Forceusers.Remove(forceuser);
            _DbContext.SaveChangesAsync();
            return Ok($"Removed hero {forceuser.Name}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Forceuser>> UpdateForceuser(Forceuser newForceuser, int id)
        {
            var oldForceuser = await _DbContext.Forceusers.FindAsync(id);
            oldForceuser.Name = newForceuser.Name;
            oldForceuser.Side = newForceuser.Side;
            oldForceuser.Species = newForceuser.Species;
            _DbContext.SaveChangesAsync();
            return Ok(newForceuser);
        }
    }
}
