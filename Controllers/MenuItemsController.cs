using ApiTeamProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTeamProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : Controller
    {
        private readonly RestoranContext _context = new RestoranContext();


        [HttpGet("GetAll")]
        public async Task<IEnumerable<MenuItemDto>> GetAll() =>
            await _context.MenuItems.Select(mi => new MenuItemDto
            {
                Id = mi.Id,
                CategoryId = mi.CategoryId,
                Name = mi.Name,
                Description = mi.Description,
                Price = mi.Price,
                IsAvailable = (bool)mi.IsAvailable,
                ImageUrl = mi.ImageUrl,
                CreatedAt = (DateTime)mi.CreatedAt
            }).ToListAsync();

        [HttpGet("GetById")]
        public async Task<ActionResult<MenuItemDto>> Get(int id)
        {
            var mi = await _context.MenuItems.FindAsync(id);
            if (mi == null) return NotFound();
            return new MenuItemDto
            {
                Id = mi.Id,
                CategoryId = mi.CategoryId,
                Name = mi.Name,
                Description = mi.Description,
                Price = mi.Price,
                IsAvailable = (bool)mi.IsAvailable,
                ImageUrl = mi.ImageUrl,
                CreatedAt = (DateTime)mi.CreatedAt
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(MenuItemDto dto)
        {
            var mi = new MenuItem
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable,
                ImageUrl = dto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };
            _context.MenuItems.Add(mi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = mi.Id }, mi);
        }

        [HttpPut("UpdateById")]
        public async Task<ActionResult> Update(int id, MenuItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var mi = await _context.MenuItems.FindAsync(id);
            if (mi == null) return NotFound();
            mi.CategoryId = dto.CategoryId;
            mi.Name = dto.Name;
            mi.Description = dto.Description;
            mi.Price = dto.Price;
            mi.IsAvailable = dto.IsAvailable;
            mi.ImageUrl = dto.ImageUrl;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult> Delete(int id)
        {
            var mi = await _context.MenuItems.FindAsync(id);
            if (mi == null) return NotFound();
            _context.MenuItems.Remove(mi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
