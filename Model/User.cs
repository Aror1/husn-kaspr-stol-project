using System;
using System.Collections.Generic;

namespace ApiTeamProject.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; } 

    public string? PasswordHash { get; set; } 

    public string? FullName { get; set; }

    public string Role { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
