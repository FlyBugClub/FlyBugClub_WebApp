using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class ReceivingFacility
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BillBorrow> BillBorrows { get; set; } = new List<BillBorrow>();
}
