using System;
using System.Collections.Generic;

namespace ApiTeamProject.Model;

public partial class Order
{
    public Order()
    {
    }

    public Order(int tableId, int status, decimal? totalAmount, DateTime? created, int? confirmedByWaiterId, DateTime? deliveredAt)
    {
        TableId = tableId;
        Status = status;
        TotalAmount = totalAmount;
        Created = created;
        ConfirmedByWaiterId = confirmedByWaiterId;
        DeliveredAt = deliveredAt;
    }


    public int Id { get; set; }

    public int TableId { get; set; }

    public int Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? Created { get; set; }

    public int? ConfirmedByWaiterId { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public virtual User? ConfirmedByWaiter { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Status? StatusNavigation { get; set; } = null!;
}
