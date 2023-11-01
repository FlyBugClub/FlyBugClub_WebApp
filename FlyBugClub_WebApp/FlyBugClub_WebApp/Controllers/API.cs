using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
       
    }
}
