// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FlyBugClub_WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlyBugClub_WebApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private FlyBugClubWebApplicationContext _ctx;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            FlyBugClubWebApplicationContext ctx)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _ctx = ctx;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [Display(Name = "FullName")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "UID")]
            public string UID { get; set; }

            [Required]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [EmailAddress]
            //[RegularExpression(@"^[\w-]+(\.[\w-]+)*@hoasen\.edu\.vn$|^[\w-]+(\.[\w-]+)*@sinhvien\.hoasen\.edu\.vn$", ErrorMessage = "Invalid email domain.")]
            [Display(Name = "Email")]
            public string Email { get; set; }




            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        //Thêm danh sách chức vụ
        //public SelectList PositionList { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Lấy danh sách chức vụ và chia sẻ với Razor View
            var positions = _ctx.Positions.ToList();
            var usse = _ctx.Users.ToList();

            // Chuyển danh sách chức vụ thành danh sách SelectListItem
            var positionList = positions.Select(position => new SelectListItem
            {
                Text = position.PositionName,
                Value = position.PositionId.ToString()
            });

            // Chia sẻ danh sách chức vụ với trang Razor
            ViewData["PositionList"] = positionList;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                /*var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);*/
                string otp = GenerateOTP();
                SendEmail(otp);
                List<string> data_user = new List<string> { Input.FullName, Input.UID, "Member", Input.PhoneNumber, Input.Address, Input.Email };
                var User_Json = JsonConvert.SerializeObject(data_user);

                return LocalRedirect($"/LoginSignUp/VerifyAccount?otp={otp}&user={User_Json}");



            }

            // If we got this far, something failed, redisplay form
            return Page();
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

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
