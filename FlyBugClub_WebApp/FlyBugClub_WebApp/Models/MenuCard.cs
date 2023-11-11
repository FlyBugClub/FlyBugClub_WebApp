﻿namespace FlyBugClub_WebApp.Models
{
    public class MenuCard
    {
        public List<Device> Top10Bestdevicces { get; set; }
        public List<Device> GetDevices { get; set; }
        public List<Device> GetSearch { get; set; }
        public List<Device> GetBillBorrowed { get; set; }
        public List<Device> GetDetailBillBorrowed { get; set; }
        public List<BorrowRate> borrowRate { get; set; }
        public Device device { get; set; }
        public AspNetUser GetUidUser { get; set; }
        public CardModel Card { get; set; }
        public List<Device> GetDeviceFavorite {  get; set; }
        public BillBorrow BillBorrow { get; set; }
        public BorrowDetail borrowDetail { get; set; }        

    }
}
