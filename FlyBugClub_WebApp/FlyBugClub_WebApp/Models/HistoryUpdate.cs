using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class HistoryUpdate
{
    public int? Bid { get; set; }

    public int? BorrowDetailId { get; set; }

    public string? Uid { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual BillBorrow? BorrowDetail { get; set; }

    public virtual BorrowDetail? BorrowDetailNavigation { get; set; }

    public virtual User? UidNavigation { get; set; }
}
