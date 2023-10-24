using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class CategoryDevice
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
