using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class OrderDevice
{
    public string Oid { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string? SupplierPhone { get; set; }

    public string? SupplierAddress { get; set; }

    public DateTime? DateOrder { get; set; }

    public double Total { get; set; }

    public virtual ICollection<OrderDeviceDetail> OrderDeviceDetails { get; set; } = new List<OrderDeviceDetail>();
}
