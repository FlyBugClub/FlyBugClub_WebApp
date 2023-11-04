using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ShopController : Controller
    {
        private readonly FlyBugClubWebApplicationContext _ctx;
        private IProductRepository _productRepository;
        private IGenreRepository _genreRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ShopController(IProductRepository productRepository, 
                                IGenreRepository genreRepository,
                                SignInManager<ApplicationUser> signInManager,
                                FlyBugClubWebApplicationContext ctx)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _signInManager = signInManager;
            _ctx = ctx;
        }
        public IActionResult Devices(int page = 1)
        {
            /*==================== Pagination ====================*/

            int itemsPerPage = 12; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = _ctx.Devices.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Tính toán vị trí bắt đầu và kết thúc của mục cho trang hiện tại
            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage;

            int currentPage = page;
            int nextPage = currentPage + 1;
            int prevPage = currentPage - 1;

            ViewBag.CurrentPage = currentPage;
            ViewBag.NextPage = nextPage;
            ViewBag.PrevPage = prevPage;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.TotalDevices = totalItems;
            ViewBag.TotalPages = totalPages;


            List<Device> lst = _productRepository.GetAllDevices().OrderBy(x=>x.DeviceId).Skip(startIndex).Take(itemsPerPage).ToList();

            return View("Devices", lst);
        }
         
        [HttpPost]
        public IActionResult saveDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                bool isProductIdExist = _productRepository.CheckId(device.DeviceId);
                bool isProductNameExist = _productRepository.CheckNameDevice(device.Name);
                if (isProductIdExist)
                {
                    ModelState.AddModelError(string.Empty, "Device Id is exist!!!");
                    var genreList = _genreRepository.GetAll();
                    ViewBag.GenreId = new SelectList(genreList, "CategoryId", "CategoryName");
                    return View("CreateDevice");
                }
                else
                {
                    if (isProductNameExist)
                    {
                        ModelState.AddModelError(string.Empty, "Device name is exist!!!");
                        var genreList = _genreRepository.GetAll();
                        ViewBag.GenreId = new SelectList(genreList, "CategoryId", "CategoryName");
                        return View("CreateDevice");
                    }
                    else
                    {
                        _productRepository.Create(device);
                        return RedirectToAction("Devices");
                    }
                }
            }
            else
            {
                /*================== Get All Data Genre ==================*/

                var genreList = _genreRepository.GetAll();
                ViewBag.GenreId = new SelectList(genreList, "CategoryId", "CategoryName");
                return View("CreateDevice");
            }
        }

        public IActionResult CreateDevice()
        {
            var lastTwoDigitsOfYear = DateTime.Now.Year % 100;
            var currentMonth = DateTime.Now.Month;
            /*================== Get All Data Genre ==================*/
            var billCounter = _ctx.BillBorrows.Count() + 1;
            var genreList = _genreRepository.GetAll();
            ViewBag.GenreId = new SelectList(genreList, "CategoryId", "CategoryName");
            ViewBag.BillCounter = billCounter;
            ViewBag.Month =currentMonth;
            ViewBag.Year = lastTwoDigitsOfYear;
            return View("createDevice", new Device());
        }

        [HttpPost]
        public IActionResult UpdateDevice(Device device)
        {
            _productRepository.Update(device);
            return RedirectToAction("Devices", "Shop");
        }

        public IActionResult EditDevice(string Id)
        {
            /*================== Get All Data Genre ==================*/

            var genreList = _genreRepository.GetAll();
            ViewBag.GenreId = new SelectList(genreList, "CategoryId", "CategoryName");

            return View("EditDevice", _productRepository.findById(Id));
        }

        public IActionResult DeleteDevice(string id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Devices", "Shop");
        }
    }
}
