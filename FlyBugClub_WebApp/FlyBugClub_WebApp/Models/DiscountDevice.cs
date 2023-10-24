using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class DiscountDevice
{
    public string DiscountDeviceId { get; set; } = null!;

    public string DeviceId { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double Percentage { get; set; }

    public virtual Device Device { get; set; } = null!;
}
