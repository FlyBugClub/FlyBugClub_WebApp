using System;
using System.Collections.Generic;

namespace FlyBugClub_WebApp.Models;

public partial class Device
{
    public string DeviceId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Describe { get; set; }

    public string ImagePath { get; set; } = null!;

    public int Status { get; set; }

    public string CategoryId { get; set; } = null!;

    public int QtyBorrowed { get; set; }

    public decimal Price { get; set; }

    public int? BorrowRate { get; set; }

    public virtual ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();

    public virtual ICollection<BorrowRate> BorrowRates { get; set; } = new List<BorrowRate>();

    public virtual CategoryDevice Category { get; set; } = null!;

    public virtual ICollection<DeviceSheetPrice> DeviceSheetPrices { get; set; } = new List<DeviceSheetPrice>();

    public virtual ICollection<DiscountDevice> DiscountDevices { get; set; } = new List<DiscountDevice>();

    public virtual ICollection<OrderDeviceDetail> OrderDeviceDetails { get; set; } = new List<OrderDeviceDetail>();
}
