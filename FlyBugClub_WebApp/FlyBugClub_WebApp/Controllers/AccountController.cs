using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Migrations;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using System.Linq;
using System.Security;
using System.Text;

namespace FlyBugClub_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;
        private IOrderProcessingRepository _orderProcessingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private static readonly Random random = new Random();
        private const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        
        private readonly IEmailSender _emailSender;

       

        public AccountController(FlyBugClubWebApplicationContext ctx, 
            IOrderProcessingRepository orderProcessingRepository,
            UserManager<ApplicationUser> userManager, 
            IEmailSender emailSender)
        {
            _ctx = ctx;
            _orderProcessingRepository = orderProcessingRepository;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult VerifyAccount()
        {
            
            string otp = HttpContext.Request.Query["otp"];
            string usersJson = HttpContext.Request.Query["user"];
            string ForgotPassword = HttpContext.Request.Query["ForgotPassword"];
            string email_forgotpas = HttpContext.Request.Query["email"];
            if (ForgotPassword != null)
            {
                HttpContext.Session.SetString("ForgotPassword", ForgotPassword);
            }
            if (email_forgotpas != null)
            {
                HttpContext.Session.SetString("email_forgotpas", email_forgotpas);
            }

            if (otp != null)
            {
                HttpContext.Session.SetString("otp", otp);
            }    
            if (usersJson != null)
            {
                HttpContext.Session.SetString("user_json", usersJson);
            }    
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> cuongAsync(string otp0, string otp1, string otp2, string otp3, string otp4, string otp5)
        {
            //var email = "cuong.dq12897@sinhvien.hoasen.edu.vn";
            //var usersWithEmail = _ctx.Users.Where(u => u.Email == "cuong.dq12897@sinhvien.hoasen.edu.vn").ToList();
            string otp = HttpContext.Session.GetString("otp");
            string user_json = HttpContext.Session.GetString("user_json");
            string ForgotPassword = HttpContext.Session.GetString("ForgotPassword");
            string email_forgotpas = HttpContext.Session.GetString("email_forgotpas");
            string validate_otp = otp0 + otp1 + otp2 + otp3 + otp4 + otp5;
            
 

            //var query = _ctx.Users.FromSqlRaw("SELECT Email FROM Users WHERE Email = {0}", email);
            //var userWithEmail = query.FirstOrDefault();

            if (otp == validate_otp)
            {
                if (ForgotPassword == "yes")
                {
                    //return RedirectToAction("ResetPassword", "Account", new { email = email_forgotpas });
                    HttpContext.Session.SetString("email23", email_forgotpas);
                    return View($"~/Views/Account/ResetPassword.cshtml");
                }
                else
                {
                    string uid = GenerateUID(20);

                    // Phân tách chuỗi JSON thành danh sách
                    List<string> Data_User = JsonConvert.DeserializeObject<List<string>>(user_json);
                    var user = new ApplicationUser { UserName = Data_User[5], Email = Data_User[5] };
                    user.FullName = Data_User[0];
                    user.UID = Data_User[1];
                    user.PhoneNumber = Data_User[3];
                    user.Address = Data_User[4];


                    var result = await _userManager.CreateAsync(user, Data_User[6]);
                    User usr = new User()
                    {
                        Name = Data_User[0],
                        StudentId = Data_User[1],
                        PositionId = Data_User[2],
                        Phone = Data_User[3],
                        Address = Data_User[4],
                        Email = Data_User[5],


                    };

                    _ctx.Users.Add(usr);
                    _ctx.SaveChanges();
                    return LocalRedirect("~/Identity/Account/LoginCustomer");
                }
            }
            else
            {
               
                return View("~/Views/Account/VerifyAccount.cshtml");

            }
            
            // Xử lý yêu cầu POST ở đây

        }
        [HttpPost]
        public IActionResult ResendEmail()
        {
            string otp = GenerateOTP();
            string user_json = HttpContext.Session.GetString("user_json");
            string forgot_email = HttpContext.Session.GetString("email_forgotpas");
            if (user_json!=null)
            {
                List<string> Data_User = JsonConvert.DeserializeObject<List<string>>(user_json);
                SendEmail(otp, Data_User[5]);
            }
            else if(forgot_email!=null)
            {
                SendEmail(otp, forgot_email);
            }

            HttpContext.Session.SetString("otp", otp);
            return LocalRedirect($"/Account/VerifyAccount");
        }
        public void SendEmail(string otp, string email)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                //client.Authenticate("flybug@hoasen.edu.vn", "#FlyBugClub@hoasen.edu.vn");
                client.Authenticate("cuong.dq12897@sinhvien.hoasen.edu.vn", "75R22UYT");
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<p>hello anh, otp: {otp}</p>",
                    TextBody = "Xin chao"
                };
                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };
                //message.From.Add(new MailboxAddress("FlyBug thông báo", "flybug@hoasen.edu.vn"));
                message.From.Add(new MailboxAddress("FlyBug thông báo", "cuong.dq12897@sinhvien.hoasen.edu.vn"));
                message.To.Add(new MailboxAddress("Test", email));
                message.Subject = "FlyBug thông báo nhẹ";
                client.Send(message);
                client.Disconnect(true);

            }

        }
        public string GenerateUID(int length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }
            return result.ToString();
        }

        public string GenerateOTP()
        {
            // Độ dài của mã OTP (6 số)
            int otpLength = 6;

            // Dãy số từ 0 đến 9
            string numbers = "0123456789";

            // Random số ngẫu nhiên để tạo OTP
            Random random = new Random();

            // Tạo chuỗi OTP bằng cách lựa chọn ngẫu nhiên các số từ dãy numbers
            char[] otpChars = new char[otpLength];
            for (int i = 0; i < otpLength; i++)
            {
                otpChars[i] = numbers[random.Next(0, numbers.Length)];
            }

            // Chuyển mảng thành chuỗi và trả về OTP
            string otp = new string(otpChars);
            return otp;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    
        public IActionResult ChangePassword()
        {
            string email = HttpContext.Session.GetString("email_user");
            if (email != null)
            {
                HttpContext.Session.SetString("email", email);
            }
            return View();
        }


        public async Task<IActionResult> ResetPassword(string newPassword, string confirmPassword)
        {
            string email = HttpContext.Session.GetString("email23");
            if (newPassword != null)
            {

                HttpContext.Session.SetString("reset_newpassword", newPassword);
            }
            if (confirmPassword != null)
            {
                HttpContext.Session.SetString("reset_conformpassword", confirmPassword);
            }


            if (newPassword == null)
            {
                ViewBag.ResetNewPassword = "Chưa nhập mật khẩu mới";
                return View("~/Views/Account/ResetPassword.cshtml");
            }
            else
            {
                var NewPassword = HttpContext.Session.GetString("reset_newpassword");
                if (NewPassword != null)
                {
                    ViewBag.ValueResetNewPassword = NewPassword;
                }
                ViewBag.ResetNewPassword = "";
            }

            if (confirmPassword == null)
            {
                ViewBag.ResetConformPassword = "Chưa nhập xác nhận lại mật khẩu ";
                return View("~/Views/Account/ResetPassword.cshtml");
            }
            else
            {
                var ConformPassword = HttpContext.Session.GetString("reset_conformpassword");
                if (ConformPassword != null)
                {
                    ViewBag.ValueResetConformPassword = ConformPassword;
                }
                ViewBag.ResetConformPassword = "";
            }


            if (newPassword != confirmPassword)
            {
                ViewBag.ResetconfirmPassword = "xác nhận mk ko giống với mk mới ";
                // Xử lý khi mật khẩu và xác nhận mật khẩu không khớp
                return View("~/Views/Account/ResetPassword.cshtml");
            }

            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    return LocalRedirect("~/Identity/Account/LoginCustomer");
                }
                else
                {
                    // Xử lý khi việc đặt lại mật khẩu không thành công
                    return View();
                }
            }
            else
            {
                // Xử lý khi không tìm thấy người dùng
                return View();
            }
        }
        public async Task<IActionResult> EnterChangePassword(string oldPassword,string newPassword, string confirmPassword)
        {

            if (oldPassword != null) {
                HttpContext.Session.SetString("oldpassword", oldPassword);
            }
            if (newPassword != null)
            {
                HttpContext.Session.SetString("newpassword", newPassword);
            }
            if (confirmPassword != null)
            {
                HttpContext.Session.SetString("conformpassword", confirmPassword);
            }


            if (oldPassword == null )
            {
                ViewBag.OldPassword = "Chưa nhập mật khẩu cũ";
                return View("~/Views/Account/ChangePassword.cshtml");
            }
            else
            {
                var OldPassword = HttpContext.Session.GetString("oldpassword");
                if (OldPassword !=null)
                {
                    ViewBag.valueOldPassword = OldPassword;
                }    
                ViewBag.OldPassword = "";
            }

            if (newPassword == null)
            {
                ViewBag.NewPassword = "Chưa nhập mật khẩu mới";
                return View("~/Views/Account/ChangePassword.cshtml");
            }
            else 
            {
                var NewPassword = HttpContext.Session.GetString("newpassword");
                if (NewPassword != null)
                {
                    ViewBag.valueNewPassword = NewPassword;
                }
                ViewBag.NewPassword = "";
            } 
            
            if(confirmPassword == null)
            {
                ViewBag.ConformPassword = "Chưa nhập xác nhận lại mật khẩu ";
                return View("~/Views/Account/ChangePassword.cshtml");
            }
            else
            {
                var ConformPassword = HttpContext.Session.GetString("conformpassword");
                if (ConformPassword != null)
                {
                    ViewBag.valueConformPassword = ConformPassword;
                }
                ViewBag.ConformPassword = "";
            } 
            
            if (newPassword != confirmPassword)
            {
                // Xử lý khi mật khẩu và xác nhận mật khẩu không khớp
                return View("~/Views/Account/ChangePassword.cshtml");
            }
            string email_user = HttpContext.Session.GetString("email");
            var user = await _userManager.FindByNameAsync(email_user);

            if (user != null)
            {
                string hashedPassword = user.PasswordHash;
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, hashedPassword, oldPassword);
                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    // Mật khẩu cũ đúng, bạn có thể thực hiện thay đổi mật khẩu
                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    if (result.Succeeded)
                    {
                        return LocalRedirect("~/Identity/Account/LoginCustomer");
                    }
                    else
                    {
                        return View("~/Views/Account/ChangePassword.cshtml");
                    }
                }
                else
                {
                    return View("~/Views/Account/ChangePassword.cshtml");
                }

            }
            else
            {
                // Xử lý khi không tìm thấy người dùng
                return View("~/Views/Account/ChangePassword.cshtml");
            }
        }

        public IActionResult UserPage()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                HttpContext.Session.SetString("email_user", email);
            }
            return View();
        }

        public IActionResult ChangeInfoUser()
        {
            return View();
        }

        public async Task<IActionResult> Receiption()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return LocalRedirect("/Identity/Account/LoginCustomer");
            }
            else
            {
                var billsByUID = _ctx.BillBorrows
                    .Where(b => b.Sid == currentUser.UID)
                    .Include(b => b.BorrowDetails)
                    .OrderByDescending(b => b.BorrowDate)
                    .OrderBy(b => b.Status)
                    .ToList();

                foreach (var bill in billsByUID)
                {
                    foreach (var detail in bill.BorrowDetails)
                    {
                        detail.DeviceId = _orderProcessingRepository.GetDeviceName(detail.DeviceId);
                    }
                }

                if (billsByUID.Count == 0)
                {
                    billsByUID = null;
                }

                return View(billsByUID);
            }
        }

        public IActionResult DeleteBill(string id)
        {
            _orderProcessingRepository.Delete(id);
            return RedirectToAction("Receiption", "Account");
        }

    }
}
