using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class BillBorrow
{
    public int Bid { get; set; }

    public string Sid { get; set; } = null!;

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public DateTime? ReceiveDay { get; set; }

    public string? Note { get; set; }

    public int SupplierId { get; set; }

    public double? FeeShip { get; set; }

    public decimal? Total { get; set; }

    public int? Status { get; set; }

    public decimal? DepositPriceOnBill { get; set; }

    public virtual ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();

    public virtual User SidNavigation { get; set; } = null!;  //SID

    public virtual Supplier Supplier { get; set; } = null!;
}
