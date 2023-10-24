using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DashboardController(FlyBugClubWebApplicationContext ctx,
                                    SignInManager<ApplicationUser> signInManager)
        {
            _ctx = ctx;
            _signInManager = signInManager;
        }

        public IActionResult Dashboard()
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


            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/identity/account/login");
        }
    }
}
