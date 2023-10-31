using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class HistoryUpdate
{
    public string? Bid { get; set; }

    public string? BorrowDetailId { get; set; }

    public string? Uid { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual BillBorrow? BidNavigation { get; set; }

    public virtual BorrowDetail? BorrowDetail { get; set; }

    public virtual User? UidNavigation { get; set; }
}
