using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlyBugClub_WebApp.Controllers
{
    [ViewComponent(Name = "_BillBorrow")]
    public class _BillBorrowViewComponent : ViewComponent
    {
        private readonly FlyBugClubWebApplicationContext _ctx;

        public _BillBorrowViewComponent(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke(int day, int month, int year)
        {
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

            return View("_BillBorrow", billBorrows);
        }
    }
}
