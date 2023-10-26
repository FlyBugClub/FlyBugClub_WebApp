using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using NuGet.Protocol.Core.Types;

using Org.BouncyCastle.Asn1.X509;


using Org.BouncyCastle.Asn1.X509;

using System.Data;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;
        private IDashboardRepository _dashboard;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DashboardController(FlyBugClubWebApplicationContext ctx,
                                    SignInManager<ApplicationUser> signInManager,
                                    IDashboardRepository dashboard)
        {
            _ctx = ctx;
            _signInManager = signInManager;
            _dashboard = dashboard;
        }

        public IActionResult Dashboard(int day, int month, int year)  //Trang Dashboard chính của admin
        {
              int currentYear = DateTime.Now.Year;

              // Tính tổng số lượng thuê thiết bị qua tất cả các đơn hàng
              int totalQuantity = _ctx.BillBorrows.SelectMany(x => x.BorrowDetails).Sum(d => d.Quantity);
              ViewBag.TotalQuantity = totalQuantity;

              // Tính tổng tiền từ các billBorrow có Status là true
              var totalAmount = _ctx.BillBorrows.Where(x => x.Status == 1 && x.BorrowDate.Year == currentYear).Sum(b => b.Total);
              ViewBag.TotalAmount = totalAmount;

              // Đếm số lượng BillBorrow có Status là true
              int countTrue = _ctx.BillBorrows.Count(b => b.Status == 1);
              ViewBag.countTrue = countTrue;


            // xử lý bill borrow
            List<BillBorrow> billBorrows = new List<BillBorrow>();

            if (day > 0 && month > 0 && year > 0)
            {
                // Tạo một đối tượng kiểu DateTime từ dữ liệu ngày, tháng, năm nhận được
                var targetDate = new DateTime(year, month, day);

                // Truy vấn dữ liệu theo ngày
                billBorrows = _ctx.BillBorrows
                    .Where(b => b.BorrowDate.Date == targetDate.Date)
                    .ToList();
            }
            else
            {
                // Xử lý trường hợp không có ngày, tháng, năm được cung cấp
                billBorrows = _ctx.BillBorrows.ToList();
            }





            return View();
        }

        public IActionResult Logout() // Trả về trang đăng nhập khi đăng xuất
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/identity/account/login");
        }

       
    }
}
