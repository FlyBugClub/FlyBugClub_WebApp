<<<<<<< HEAD
﻿using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Migrations;
using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> fbcf1544c9c1f6d4d62702688f8cad2b1ebf6382

namespace FlyBugClub_WebApp.Controllers
{
    public class LoginSignUp : Controller
    {
<<<<<<< HEAD
        private FlyBugClubWebApplicationContext _ctx;

        public LoginSignUp(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }
        /
        public IActionResult VerifyAccount()
        {

            string otp = HttpContext.Request.Query["otp"];
            string usersJson = HttpContext.Request.Query["user"];
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
        public IActionResult cuong(string otp0, string otp1, string otp2, string otp3, string otp4, string otp5)
        {
            //var email = "cuong.dq12897@sinhvien.hoasen.edu.vn";
            //var usersWithEmail = _ctx.Users.Where(u => u.Email == "cuong.dq12897@sinhvien.hoasen.edu.vn").ToList();
            string otp = HttpContext.Session.GetString("otp");
            string user_json = HttpContext.Session.GetString("user_json");
            string validate_otp = otp0 + otp1 + otp2 + otp3 + otp4 + otp5;


            //var query = _ctx.Users.FromSqlRaw("SELECT Email FROM Users WHERE Email = {0}", email);
            //var userWithEmail = query.FirstOrDefault();

            if (otp == validate_otp)
            {
                // Phân tách chuỗi JSON thành danh sách
                List<string> Data_User = JsonConvert.DeserializeObject<List<string>>(user_json);
                var user = new ApplicationUser { UserName = Data_User[5], Email = Data_User[5] };
                user.FullName = Data_User[0];
                user.UID = Data_User[1];
                user.PhoneNumber = Data_User[3];
                user.Address = Data_User[4];
                User usr = new User()
                {
                    Name = Data_User[0],
                    StudentId = Data_User[1],
                    PositionId = Data_User[2],
                    Phone = Data_User[3],
                    Address = Data_User[4],
                    Email = Data_User[5],
                    Account = Data_User[5]
                };

                //_ctx.Users.Add(usr);
                //_ctx.SaveChanges();
                return LocalRedirect("~/Identity/Account/LoginCustomer");

            }
            else
            {

                return View("~/Views/LoginSignUp/VerifyAccount.cshtml");

            }

            // Xử lý yêu cầu POST ở đây

        }
        [HttpPost]
        public IActionResult ResendEmail()
        {

            string otp = GenerateOTP();
            SendEmail(otp);
            HttpContext.Session.SetString("otp", otp);
            return LocalRedirect($"/LoginSignUp/VerifyAccount");
        }
        public void SendEmail(string otp)
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
                message.To.Add(new MailboxAddress("Test", "tr6r20@gmail.com"));
                message.Subject = "FlyBug thông báo nhẹ";
                client.Send(message);
                client.Disconnect(true);

            }

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
=======
        public IActionResult VerifyAccount()
        {
            return View();
        }
>>>>>>> fbcf1544c9c1f6d4d62702688f8cad2b1ebf6382
    }
}
