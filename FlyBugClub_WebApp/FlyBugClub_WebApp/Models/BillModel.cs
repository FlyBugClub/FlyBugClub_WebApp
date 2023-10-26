namespace FlyBugClub_WebApp.Models
{
    public class BillModel
    {
        public List<BillBorrow> getWaitingBill { get; set; }
        public List<BillBorrow> getBorowingBill { get; set; }
        public List<BillBorrow> getDoneBill { get; set; }
    }
}
