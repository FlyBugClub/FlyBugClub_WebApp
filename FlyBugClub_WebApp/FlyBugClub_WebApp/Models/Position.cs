using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class Position
{
    public string PositionId { get; set; } = null!;

    public string PositionName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
