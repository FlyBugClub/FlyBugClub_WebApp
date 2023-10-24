using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? SupplierPhone { get; set; }

    public string SupplierAddress { get; set; } = null!;

    public string? Room { get; set; }

    public virtual ICollection<BillBorrow> BillBorrows { get; set; } = new List<BillBorrow>();
}
