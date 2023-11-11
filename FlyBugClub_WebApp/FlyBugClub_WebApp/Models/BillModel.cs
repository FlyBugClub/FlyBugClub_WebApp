namespace FlyBugClub_WebApp.Models
{
    public class BillModel
    {
        public List<BillBorrow> getBills { get; set; }
        public List<BillBorrow> getWaitingBill { get; set; }
        public List<BillBorrow> getBorowingBill { get; set; }
        public List<BillBorrow> getDoneBill { get; set; }
        public List<HistoryUpdate> getAllHistory { get; set; }

    }
}
