using ApiTeamProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTeamProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly RestoranContext _context = new RestoranContext();


        [HttpGet("GetAll")]
        public async Task<IEnumerable<OrderDto>> GetAll() =>
            await _context.Orders.Select(o => new OrderDto
            {
                Id = o.Id,
                TableId = o.TableId,
                Status = GetStatusString(o.Status),
                TotalAmount = (decimal)o.TotalAmount,
                Created = (DateTime)o.Created,
                ConfirmedByWaiterId = o.ConfirmedByWaiterId,
                DeliveredAt = o.DeliveredAt
            }).ToListAsync();

        [HttpGet("GetById")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var o = await _context.Orders.FindAsync(id);
            if (o == null) return NotFound();
            return new OrderDto
            {
                Id = o.Id,
                TableId = o.TableId,
                Status = GetStatusString(o.Status),
                TotalAmount = (decimal)o.TotalAmount,
                Created = (DateTime)o.Created,
                ConfirmedByWaiterId = o.ConfirmedByWaiterId,
                DeliveredAt = o.DeliveredAt
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(OrderCreateDtom dto)
        {
            var o = new Order
            {
                TableId = dto.order.TableId,
                Status = 1,
                TotalAmount = dto.order.TotalAmount,
                Created = dto.order.Created == default ? DateTime.UtcNow : dto.order.Created,
                ConfirmedByWaiterId = dto.order.ConfirmedByWaiterId,
                DeliveredAt = dto.order.DeliveredAt
            };

           

            _context.Orders.Add(o);
            await _context.SaveChangesAsync();
            int id = o.Id;

            foreach (OrderitemDto orderitem in dto.orderItems ?? Enumerable.Empty<OrderitemDto>())
            {
                var menuItem = await _context.MenuItems.FindAsync(orderitem.iditem);
                if (menuItem == null)
                {
                    return BadRequest($"MenuItem not found: {orderitem.iditem}");
                }

                var a = new OrderItem
                {
                    OrderId = id,
                    MenuItemId = orderitem.iditem,
                    Quantity = orderitem.quantity,
                    PriceAtOrder = orderitem.quantity * menuItem.Price
                };
                _context.OrderItems.Add(a);
            }

            await _context.SaveChangesAsync();

            var resultDto = new OrderDto
            {
                Id = o.Id,
                TableId = o.TableId,
                Status = "Оформлен",
                TotalAmount = (decimal)(o.TotalAmount ?? 0m),
                Created = o.Created ?? DateTime.UtcNow,
                ConfirmedByWaiterId = o.ConfirmedByWaiterId,
                DeliveredAt = o.DeliveredAt
            };

            return CreatedAtAction(nameof(Get), new { id = o.Id }, resultDto);
        }

        [HttpPut("UpdateById")]
        public async Task<ActionResult> Update(int id, OrderDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var o = await _context.Orders.FindAsync(id);
            if (o == null) return NotFound();

            o.TableId = dto.TableId;
            o.Status = GetStatusNumber(dto.Status);
            o.TotalAmount = dto.TotalAmount;
            o.Created = dto.Created;
            o.ConfirmedByWaiterId = dto.ConfirmedByWaiterId;
            o.DeliveredAt = dto.DeliveredAt;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult> Delete(int id)
        {
            var o = await _context.Orders.FindAsync(id);
            if (o == null) return NotFound();
            _context.Orders.Remove(o);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        public static string GetStatusString(int status)
        {
            switch (status)
            {
                case 1:
                    return "Оформлен";
                case 2:
                    return "Готов к выдаче";
                case 3:
                    return "Выдан";
                default:
                    return "Неизвестный статус"; // или бросить исключение, если нужно
            }
        }
        public static int GetStatusNumber(string status)
        {
            switch (status)
            {
                case "Оформлен":
                    return 1;
                case "Готов к выдаче":
                    return 2;
                case "Выдан":
                    return 3;
                default:
                    // Можно вернуть значение по умолчанию или бросить исключение
                    throw new ArgumentException($"Неизвестный статус: {status}");
            }
        }
    }


}
