using ApiTeamProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTeamProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : Controller
    {
        private readonly RestoranContext _context = new RestoranContext();


        [HttpGet("GetAll")]
        public async Task<IEnumerable<OrderItemDto>> GetAll() =>
            await _context.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity,
                PriceAtOrder = oi.PriceAtOrder,
                Notes = oi.Notes
            }).ToListAsync();

        [HttpGet("GetById")]
        public async Task<ActionResult<OrderItemDto>> Get(int id)
        {
            var oi = await _context.OrderItems.FindAsync(id);
            if (oi == null) return NotFound();
            return new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity,
                PriceAtOrder = oi.PriceAtOrder,
                Notes = oi.Notes
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(OrderItemDto dto)
        {
            var oi = new OrderItem
            {
                OrderId = dto.OrderId,
                MenuItemId = dto.MenuItemId,
                Quantity = dto.Quantity,
                PriceAtOrder = dto.PriceAtOrder,
                Notes = dto.Notes
            };
            _context.OrderItems.Add(oi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = oi.Id }, oi);
        }

        [HttpPut("UpdateById")]
        public async Task<ActionResult> Update(int id, OrderItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var oi = await _context.OrderItems.FindAsync(id);
            if (oi == null) return NotFound();

            oi.OrderId = dto.OrderId;
            oi.MenuItemId = dto.MenuItemId;
            oi.Quantity = dto.Quantity;
            oi.PriceAtOrder = dto.PriceAtOrder;
            oi.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult> Delete(int id)
        {
            var oi = await _context.OrderItems.FindAsync(id);
            if (oi == null) return NotFound();
            _context.OrderItems.Remove(oi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }



}
