using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FlyBugClub_WebApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private FlyBugClubWebApplicationContext _ctx;

        public UsersController(FlyBugClubWebApplicationContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getuserbyuid")]
        public async Task<IActionResult> GetUserByUID(string uid, string deviceid,string status)
        {
            var asp_user = await _userManager.FindByIdAsync(uid);
            if (asp_user != null)
            {
                var result =  _ctx.BorrowRates.Where(u => u.Uid == asp_user.UID).ToList();

                int flag = 0;
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if (item.DeviceId == deviceid)
                        {
                            flag = 1;
                        }
                    }
                }

                    if (flag == 1)
                    {
                    BorrowRate borrowRateToUpdate = _ctx.BorrowRates.FirstOrDefault(rate => rate.Uid == asp_user.UID && rate.DeviceId == deviceid); // Thay someId bằng ID của bản ghi bạn muốn cập nhật
                    borrowRateToUpdate.Status = bool.Parse(status);
                    _ctx.BorrowRates.Update(borrowRateToUpdate);
                            _ctx.SaveChanges();


                            return Ok(asp_user);
                    }
                else
                {
                    BorrowRate borrowRate = new BorrowRate()
                    {
                        Uid = asp_user.UID,
                        DeviceId = deviceid,
                        Status = bool.Parse(status)
                    };
                    _ctx.BorrowRates.Add(borrowRate);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _ctx.SaveChanges();


                    return Ok(asp_user);
                }    
            }
            else
            {

                return NotFound();
            }


        }

        [HttpGet]
        [Route("getbillbydate")]
        public async Task<List<BillBorrow>> GetBillByDate(string date)
        {
            string[] two_date = date.Split("@@");
            DateTime startDate = DateTime.ParseExact(two_date[0], "yyyy-MM-dd", CultureInfo.InvariantCulture); // Chỉnh lại định dạng ngày nếu cần
            DateTime endDate = DateTime.ParseExact(two_date[1], "yyyy-MM-dd", CultureInfo.InvariantCulture).AddHours(23); // Chỉnh lại định dạng ngày nếu cần

            var result = _ctx.BillBorrows.Where(u => u.ReceiveDay >= startDate && u.ReceiveDay <= endDate).ToList();
            result.Reverse();

            
            return result;

        }
        [HttpGet]
        [Route("get10bill")]
        public async Task<List<BillBorrow>> Get10Billl ()
        {
            var result = _ctx.BillBorrows
             .OrderByDescending(b => b.BorrowDate) // Sắp xếp theo BorrowDate giảm dần
             .Take(10) // Lấy 10 phần tử
             .ToList();

           

            return result;

        }

    }
}
