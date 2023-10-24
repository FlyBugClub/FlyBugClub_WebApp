using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class DeviceSheetPrice
{
    public string SheetPriceId { get; set; } = null!;

    public double Price { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string DeviceId { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;
}
