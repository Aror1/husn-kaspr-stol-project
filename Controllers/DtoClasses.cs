using ApiTeamProject.Model;

namespace ApiTeamProject.Controllers
{
    public class MenuCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class MenuItemDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public string? Notes { get; set; }
    }



    public class UserDto
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class RegisDto
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
    }
    public class LoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class OrderitemDto
    {
        public int iditem { get; set; }
        public int quantity { get; set; }
    }
    public class OrderCreateDtom
    {
        public Order order { get; set; }
        public List<OrderitemDto> orderItems { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Created { get; set; }
        public int? ConfirmedByWaiterId { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
