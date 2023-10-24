using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class BorrowRate
{
    public string DeviceId { get; set; } = null!;

    public string Uid { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual User UidNavigation { get; set; } = null!;
}
