using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class OrderDeviceDetail
{
    public string Odid { get; set; } = null!;

    public string Oid { get; set; } = null!;

    public string DeviceId { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double SubTotal { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual OrderDevice OidNavigation { get; set; } = null!;
}
