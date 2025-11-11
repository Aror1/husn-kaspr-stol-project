using System;
using System.Collections.Generic;

namespace ApiTeamProject.Model;

public partial class Status
{
    public string? StatusName { get; set; }

    public int Statusid { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
