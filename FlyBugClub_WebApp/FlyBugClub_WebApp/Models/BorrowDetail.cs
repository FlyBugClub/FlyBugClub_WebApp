using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class BorrowDetail
{
    public int BorrowDetailId { get; set; }

    public int Bid { get; set; }

    public string DeviceId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal SubTotal { get; set; }

    public int? ReturnQuantity { get; set; }

    public int? QtyDamage { get; set; }

    public decimal? DepositPrice { get; set; }

    public virtual BillBorrow BidNavigation { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;
}
