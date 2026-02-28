using ApiTeamProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTeamProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuCategoriesController : ControllerBase
    {
        private readonly RestoranContext _context = new RestoranContext();

        [HttpGet("GetAll")]
        public async Task<IEnumerable<MenuCategoryDto>> GetAll() =>
            await _context.MenuCategories.Select(mc => new MenuCategoryDto
            {
                Id = mc.Id,
                Name = mc.Name,
                Description = mc.Description,
            }).ToListAsync();

        [HttpGet("GetById")]
        public async Task<ActionResult<MenuCategoryDto>> Get(int id)
        {
            var mc = await _context.MenuCategories.FindAsync(id);
            if (mc == null) return NotFound();
            return new MenuCategoryDto { Id = mc.Id, Name = mc.Name, Description = mc.Description };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(MenuCategoryDto dto)
        {
            var mc = new MenuCategory { Name = dto.Name, Description = dto.Description };
            _context.MenuCategories.Add(mc);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = mc.Id }, mc);
        }

        [HttpPut("UpdateById")]
        public async Task<ActionResult> Update(int id, MenuCategoryDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var mc = await _context.MenuCategories.FindAsync(id);
            if (mc == null) return NotFound();
            mc.Name = dto.Name;
            mc.Description = dto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult> Delete(int id)
        {
            var mc = await _context.MenuCategories.FindAsync(id);
            if (mc == null) return NotFound();
            _context.MenuCategories.Remove(mc);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
