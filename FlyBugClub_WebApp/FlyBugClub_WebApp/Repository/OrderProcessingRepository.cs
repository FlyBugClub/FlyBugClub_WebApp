using FlyBugClub_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyBugClub_WebApp.Repository
{
    public interface IOrderProcessingRepository
    {
        public bool Create(BillBorrow billBorrow);
        public bool Update(BillBorrow billBorrow);
        public bool Delete(int bill);
        public List<BillBorrow> GetAllBill();
        public List<BillBorrow> GetAllBillsWithDetails();
        public List<BorrowDetail> GetDetailBillByBID(int bid);
        public List<BorrowDetail> GetBorrowDetailsByBillBorrowId(int billBorrowId);
        public List<Supplier> GetAllSuppliers();
        string GetDeviceName(string deviceId);
        string GetUserName(string Sid);
        public BillBorrow findById(int id);
        public BorrowDetail findBillDetailById(int billId, int detailId);
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

        public bool Create(BillBorrow billBorrow)
        {
            throw new NotImplementedException();
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

        public bool Delete(int bill)
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

        public BillBorrow findById(int id)
        {
            return _ctx.BillBorrows.FirstOrDefault(x => x.Bid == id);
        }

        public List<BorrowDetail> GetDetailBillByBID(int bid)
        {
            return _ctx.BorrowDetails.Where(x => x.Bid == bid).ToList();
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _ctx.Suppliers.ToList();
        }

        public List<BorrowDetail> GetBorrowDetailsByBillBorrowId(int billBorrowId)
        {
            return _ctx.BillBorrows
                .Where(bill => bill.Bid == billBorrowId)
                .Include(bill => bill.BorrowDetails)
                .SelectMany(bill => bill.BorrowDetails)
                .ToList();
                }

        public BorrowDetail findBillDetailById(int billId, int detailId)
        {
            return _ctx.BorrowDetails.FirstOrDefault(x => x.Bid == billId && x.BorrowDetailId == detailId);
        }
    }
}
