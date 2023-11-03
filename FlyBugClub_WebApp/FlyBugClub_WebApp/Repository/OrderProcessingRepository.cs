using FlyBugClub_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyBugClub_WebApp.Repository
{
    public interface IOrderProcessingRepository
    {
        public bool UpdatetBillDetail(BorrowDetail borrowDetail);
        public bool Update(BillBorrow billBorrow);
        public bool AddHistory(HistoryUpdate billBorrow);
        public bool Delete(string bill);
        public List<BillBorrow> GetAllBill();
        public List<BillBorrow> GetAllBillsWithDetails();
        public List<BillBorrow> GetWaitingBillsWithDetails();
        public List<BillBorrow> GetBorrowingBillsWithDetails();
        public List<BillBorrow> GetDoneBillsWithDetails();
        public List<BorrowDetail> GetDetailBillByBID(string bid);
        public List<BorrowDetail> GetBorrowDetailsByBillBorrowId(string billBorrowId);
        public List<Supplier> GetAllSuppliers();
        string GetDeviceName(string deviceId);
        string GetUserName(string Sid);
        public BillBorrow findById(string id);
        public BorrowDetail findBillDetailById(string billId, string detailId);
    }
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public OrderProcessingRepository(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public List<BillBorrow> GetAllBill()
        {
            return _ctx.BillBorrows.ToList();
        }

        public string GetDeviceName(string deviceId)
        {
            return _ctx.Devices.FirstOrDefault(x => x.DeviceId == deviceId)?.Name;
        }

        public string GetUserName(string Sid)
        {
            return _ctx.Users.FirstOrDefault(x => x.StudentId == Sid)?.Name;
        }

        public List<BillBorrow> GetAllBillsWithDetails()
        {
            return _ctx.BillBorrows
                .Include(b=>b.BorrowDetails)
                .Include(b=>b.SidNavigation)
                .ToList();
        }

        public bool Update(BillBorrow billBorrow)
        {
            BillBorrow bill = _ctx.BillBorrows.FirstOrDefault(x=>x.Bid == billBorrow.Bid);
            if (bill != null)
            {
                _ctx.Entry(bill).CurrentValues.SetValues(billBorrow);
                _ctx.SaveChanges();
            }
            return true;
        }

        public bool Delete(string bill)
        {
            // Lấy hóa đơn cùng với chi tiết hóa đơn
            BillBorrow b = _ctx.BillBorrows.Include(x => x.BorrowDetails).FirstOrDefault(x => x.Bid == bill);

            if (b != null)
            {
                // Lấy danh sách các sản phẩm chi tiết trong hóa đơn
                var detailsToRemove = b.BorrowDetails.ToList();

                // Xóa các sản phẩm chi tiết
                _ctx.BorrowDetails.RemoveRange(detailsToRemove);

                // Xóa hóa đơn
                _ctx.BillBorrows.Remove(b);

                // Lưu các thay đổi vào cơ sở dữ liệu
                _ctx.SaveChanges();

                return true;
            }

            return false; // Trả về false nếu không tìm thấy hóa đơn
        }

        public BillBorrow findById(string id)
        {
            return _ctx.BillBorrows.FirstOrDefault(x => x.Bid == id);
        }

        public List<BorrowDetail> GetDetailBillByBID(string bid)
        {
            return _ctx.BorrowDetails.Where(x => x.Bid == bid).ToList();
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _ctx.Suppliers.ToList();
        }

        public List<BorrowDetail> GetBorrowDetailsByBillBorrowId(string billBorrowId)
        {
            return _ctx.BillBorrows
                .Where(bill => bill.Bid == billBorrowId)
                .Include(bill => bill.BorrowDetails)
                .SelectMany(bill => bill.BorrowDetails)
                .ToList();
                }

        public BorrowDetail findBillDetailById(string billId, string detailId)
        {
            return _ctx.BorrowDetails.FirstOrDefault(x => x.Bid == billId && x.BorrowDetailId == detailId);
        }

        public List<BillBorrow> GetWaitingBillsWithDetails()
        {
            return _ctx.BillBorrows.Where(x=>x.Status == 0)
                .OrderByDescending(b=>b.BorrowDate)
                .Include(b => b.BorrowDetails)
                .Include(b => b.SidNavigation)
                .ToList();
        }

        public List<BillBorrow> GetBorrowingBillsWithDetails()
        {
            return _ctx.BillBorrows.Where(x => x.Status == 1)
                .OrderByDescending(b => b.BorrowDate)
                .Include(b => b.BorrowDetails)
                .Include(b => b.SidNavigation)
                .ToList();
        }

        public List<BillBorrow> GetDoneBillsWithDetails()
        {
            return _ctx.BillBorrows.Where(x => x.Status == 2)
                .OrderByDescending(b => b.BorrowDate)
                .Include(b => b.BorrowDetails)
                .Include(b => b.SidNavigation)
                .ToList();
        }

        public bool UpdatetBillDetail(BorrowDetail borrowDetail)
        {
            BorrowDetail detail = _ctx.BorrowDetails.FirstOrDefault(x => x.BorrowDetailId == borrowDetail.BorrowDetailId);
            if (detail != null)
            {
                _ctx.Entry(detail).CurrentValues.SetValues(borrowDetail);
                _ctx.SaveChanges();
            }
            return true;
        }

        public bool AddHistory(HistoryUpdate history)
        {
            HistoryUpdate histories = _ctx.HistoryUpdates.FirstOrDefault(x => x.HistoryUpdateId == history.HistoryUpdateId);
            if (histories != null)
            {
                _ctx.HistoryUpdates.Add(histories);
                _ctx.SaveChanges();
            }
            return true;

            // Thêm đối tượng lịch sử cập nhật vào cơ sở dữ liệu
           
            
        }
    }
}
