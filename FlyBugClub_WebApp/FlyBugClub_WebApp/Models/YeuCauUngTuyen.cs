using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class YeuCauUngTuyen
{
    public string StudentId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Major { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Faculty { get; set; }

    public string Position { get; set; } = null!;

    public string ReasonNote { get; set; } = null!;

    public string AdvantagesNote { get; set; } = null!;

    public string DisadvantagesNote { get; set; } = null!;
}
