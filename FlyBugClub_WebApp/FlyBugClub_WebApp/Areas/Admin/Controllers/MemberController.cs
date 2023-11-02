using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MemberController : Controller
    {
        private readonly FlyBugClubWebApplicationContext _ctx;
       
        
        public MemberController(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Member()
        {
            // Sử dụng LINQ để lấy tất cả người dùng từ DbContext
            List<User> lst = _ctx.Users.ToList();

            return View("Member", lst);
        }
        
    }
}
