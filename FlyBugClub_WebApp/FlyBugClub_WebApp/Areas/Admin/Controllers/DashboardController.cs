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

        public IActionResult Dashboard( int month, int year)
        {
            int currentYear = DateTime.Now.Year;
            ViewBag.TotalQuantity = _ctx.BillBorrows.SelectMany(x => x.BorrowDetails).Sum(d => d.Quantity);
            ViewBag.TotalAmount = _ctx.BillBorrows.Where(x => x.Status == 2 && x.BorrowDate.Year == currentYear).Sum(b => b.Total);
            ViewBag.CountTrue = _ctx.BillBorrows.Count(b => b.Status == 1);
            int day = 1;
            if (month > 0 && year > 0)
            {
                if (DateTime.TryParse($"{year}-{month}-{day}", out var targetDate))
                {
                    // Tìm ngày đầu tháng
                    var firstDayOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);

                    // Tìm ngày cuối tháng
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    var currentDate = DateTime.Now; // Ngày hiện tại

                    List<BillBorrow> billBorrows = _ctx.BillBorrows
                    .Where(b => b.BorrowDate >= firstDayOfMonth && b.BorrowDate <= lastDayOfMonth && b.Status == 2)
<<<<<<< HEAD
                    .OrderBy(b => b.BorrowDate) // Sắp xếp theo ngày tăng dần
=======
                    .OrderByDescending(b => b.BorrowDate) // Sắp xếp theo ngày tăng dần
>>>>>>> main
                    .ToList();

                    decimal totalAmount = (decimal)billBorrows.Sum(b => b.Total);
                    ViewBag.TotalAmount1 = totalAmount;
                    return View("Dashboard", billBorrows);
                }
                else
                {
                    // Xử lý trường hợp ngày tháng năm không hợp lệ
                    ViewBag.ErrorMessage = "Ngày tháng năm không hợp lệ.";
                }
            }

            List<BillBorrow> allBillBorrows = _ctx.BillBorrows.Where(b=>b.Status == 2).ToList();
            decimal totalAmount1 = (decimal)allBillBorrows.Sum(b => b.Total);
            ViewBag.TotalAmount1 = totalAmount1;
            return View("Dashboard", allBillBorrows);
        }



        public IActionResult Logout() // Trả về trang đăng nhập khi đăng xuất
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/identity/account/login");
        }

       
    }
}
