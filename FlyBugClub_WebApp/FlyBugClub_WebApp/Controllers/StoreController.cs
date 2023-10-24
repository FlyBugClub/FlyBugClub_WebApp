using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBugClub_WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace FlyBugClub_WebApp.Controllers
{
    public class StoreController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;

        private readonly ILogger<StoreController> _logger;
        private IProductRepository _productRepository;
        private IGenreRepository _genreRepository;
        SignInManager<ApplicationUser> _signInManager;

        public StoreController(FlyBugClubWebApplicationContext ctx, 
                                ILogger<StoreController> logger, 
                                IProductRepository productRepository,
                                IGenreRepository genreRepository,
                                SignInManager<ApplicationUser> signInManager
            )
        {
            _ctx = ctx;
            _logger = logger;
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _signInManager = signInManager;
        }
        public static string noteBill { get; set; }
        /*public DateTime ReceiptDate { get; set; }*/

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }               

        public IActionResult Store(int page = 1)
        {
            /*==================== Pagination ====================*/

            int itemsPerPage = 9; // Số mục muốn hiển thị cho mỗi trang

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

            /*================== Get All Data Devices ==================*/

            //1. lay du lieu
            List<Device> listdevice = _productRepository.GetAllDevices().OrderBy(x => x.DeviceId).Skip(startIndex).Take(itemsPerPage).ToList();
            //2. gui du lieu cho view

            CardModel cartModel = new CardModel();
            cartModel.CardId = HttpContext.Session.Id;
            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                List<Item>? items = HttpContext.Session.Get<List<Item>>("store");
                cartModel.setAllItem(items);
            }

            MenuCard m = new MenuCard();
            m.GetDevices = listdevice;
            m.Card = cartModel;

            return View("Store", m);
        } // xử lý cửa hàng

        [HttpGet]
        public IActionResult Search(string keyword, int page = 1)
        {
            List<Device> products = _productRepository.SearchByName(keyword);

            /*==================== Pagination ====================*/

            int itemsPerPage = 9; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Tính toán vị trí bắt đầu và kết thúc của mục cho trang hiện tại
            int startIndex = (page - 1) * itemsPerPage;

            var paginatedProducts = products.Skip(startIndex).Take(itemsPerPage).ToList();

            int currentPage = page;
            int nextPage = currentPage + 1;
            int prevPage = currentPage - 1;

            ViewBag.CurrentPage = currentPage;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.NextPage = nextPage;
            ViewBag.PrevPage = prevPage;
            ViewBag.TotalDevices = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;

            CardModel cartModel = new CardModel();
            cartModel.CardId = HttpContext.Session.Id;
            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                List<Item>? items = HttpContext.Session.Get<List<Item>>("store");
                cartModel.setAllItem(items);
            }

            MenuCard m = new MenuCard();
            m.Card = cartModel;
            m.GetDevices = paginatedProducts;

            return View("Search", m);
        }  // tìm kiếm sản phẩm

        public IActionResult IncreaseBorrowRate(string deviceId)
        {
            var device = _ctx.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
            if (device != null)
            {
                device.BorrowRate++;
                _ctx.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult SortProduct(string sortOption, int page = 1)
        {
            var products = _productRepository.GetAllDevices();
            /*==================== Pagination ====================*/

            int itemsPerPage = 9; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Xác định số mục cần bỏ qua để hiển thị trang hiện tại
            int skipAmount = (page - 1) * itemsPerPage;

            switch (sortOption)
            {
                case "PriceAscending":
                    products = products.OrderBy(x=>x.Price).ToList(); 
                    break;
                case "PriceDescending":
                    products = products.OrderByDescending(x=>x.Price).ToList();
                    break;
                case "BestSelling":
                    products = products.OrderByDescending(x=>x.BorrowRate).ToList();
                    break;
            }

            // Áp dụng phân trang lên danh sách sản phẩm
            var paginatedProducts = products.Skip(skipAmount).Take(itemsPerPage).ToList();

            MenuCard m = new MenuCard();
            CardModel cartModel = new CardModel();
            cartModel.CardId = HttpContext.Session.Id;
            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                List<Item>? items = HttpContext.Session.Get<List<Item>>("store");
                cartModel.setAllItem(items);
            }

            m.GetDevices = paginatedProducts;
            m.Card = cartModel;

            // Trả về view và truyền thông tin phân trang
            ViewBag.TotalDevices = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.ItemPerPage = itemsPerPage;
            ViewBag.CurrentPage = page;
            ViewBag.SortOption = sortOption;

            return View("SortProduct", m);
        }  // Sắp xếp sản phẩm

        [HttpGet]
        public IActionResult FillProduct(string fillOption, int page = 1)
        {
            var filteredProducts = new List<Device>();

            // Lấy dữ liệu dựa trên fillOption
            var dataToPage = new List<Device>();


            if (fillOption == "all")
            {
                dataToPage = _productRepository.GetAllDevices();
            }
            else if(fillOption == "1")
            {
                dataToPage = _productRepository.GetSensorProduct(fillOption);
            }
            else if (fillOption == "2")
            {
                dataToPage = _productRepository.GetHardwareProduct(fillOption);
            }
            else if (fillOption == "3")
            {
                dataToPage = _productRepository.GetConnectionProduct(fillOption);
            }

            /*==================== Pagination ====================*/

            int itemsPerPage = 9; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = dataToPage.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Xác định số mục cần bỏ qua để hiển thị trang hiện tại
            int skipAmount = (page - 1) * itemsPerPage;

            var paginatedProducts = dataToPage.Skip(skipAmount).Take(itemsPerPage).ToList();

            MenuCard m = new MenuCard();
            CardModel cartModel = new CardModel();
            cartModel.CardId = HttpContext.Session.Id;
            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                List<Item>? items = HttpContext.Session.Get<List<Item>>("store");
                cartModel.setAllItem(items);
            }

            m.GetDevices = paginatedProducts;
            m.Card = cartModel;

            // Trả về view và truyền thông tin phân trang
            ViewBag.TotalDevices = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.ItemPerPage = itemsPerPage;
            ViewBag.CurrentPage = page;
            ViewBag.fillOption = fillOption;

            return View("FillProduct", m);
        }  // lọc sản phẩm theo loại

        public IActionResult Payment(string note)  //Xử lý giỏ hàng
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return LocalRedirect("/identity/account/logincustomer");
            }
            else
            {
                List<Device> top10BestSeller = _productRepository.Top10BestSeller();

                MenuCard m = new MenuCard();
                m.Top10Bestdevicces = top10BestSeller;

                ViewBag.sessionId = HttpContext.Session.Id;
                CardModel cartModel = new CardModel();
                cartModel.CardId = HttpContext.Session.Id;
                if (HttpContext.Session.Get<List<Item>>("store") != null)
                {
                    List<Item>? items = HttpContext.Session.Get<List<Item>>("store");
                    cartModel.setAllItem(items);
                }

                noteBill = note;

                m.Card = cartModel;

                /*cartModel.NoteBill = ViewBag.Note;
                StoreController.note = _note;
*/
                return View(m);
            }
        }

        [HttpPost]
        public IActionResult CheckOut()
        {
            //doc session va luu database
            List<Item>? items = HttpContext.Session.Get<List<Item>>("store");

            BillBorrow bill = new BillBorrow();

            string userEmail = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                userEmail = User.Identity.Name;
            }

            string format = "yyyy-MM-ddTHH:mm";
            var note = Request.Form["note"].ToString();
            var receiptDateStr = Request.Form["myDatetimeInput"];
            DateTime receiptDate;

            User u = _ctx.Users.OrderByDescending(x => x.Email == userEmail).Take(1).SingleOrDefault();
            if (DateTime.TryParseExact(receiptDateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out receiptDate))
            {
                bill.Sid = u.StudentId;
                bill.BorrowDate = DateTime.Now;
                bill.ReturnDate = null;
                bill.ReceiveDay = receiptDate;
                bill.Note = note;
                bill.SupplierId = 1;
                bill.FeeShip = null;
                bill.Status = 0;
                _ctx.BillBorrows.Add(bill);
                _ctx.SaveChanges();
            }

            //2
            var BillId = _ctx.BillBorrows.OrderByDescending(x => x.Bid).Take(1).SingleOrDefault().Bid;
            var TC = _ctx.Users.Where(x => x.PositionId == "TC").ToList();
            var MB = _ctx.Users.Where(x => x.PositionId == "MB").ToList();
            var STU = _ctx.Users.Where(x => x.PositionId == "STU").ToList();

            decimal DepositMoney = 0m;
            decimal TotalBill = 0m;
            decimal TotalBillDeposit = 0m;

            foreach(var item in items)
            {
                var device = _ctx.Devices.FirstOrDefault(x => x.DeviceId == item.Id);

                BorrowDetail bd= new BorrowDetail();
                bd.Bid = BillId;
                bd.DeviceId= item.Id;
                bd.Price = item.Price;
                bd.Quantity= item.Quantity;
                device.Quantity -= item.Quantity;
                bd.ReturnQuantity = 0;
                bd.QtyDamage = 0;
                
                // Kiểm tra nếu người dùng có vị trí là "TC"
                if (TC.Any(user => user.StudentId == u.StudentId))
                {
                    DepositMoney = (item.Price * 0m) * item.Quantity;
                }
                else if (MB.Any(x => x.StudentId == u.StudentId))
                {
                    // Áp dụng giảm giá 80% cho thành viên
                    DepositMoney = (item.Price * 0.2m) * item.Quantity; // 20% của giá gốc
                }
                else if (STU.Any(x => x.StudentId == u.StudentId))
                {
                    // Áp dụng giảm giá 60% cho thành viên
                    DepositMoney = (item.Price * 0.4m) * item.Quantity; // 40% của giá gốc
                }
                else
                {
                    // Không áp dụng giảm giá
                    DepositMoney = item.Price * item.Quantity;
                }
                TotalBill += DepositMoney;
                bd.SubTotal= DepositMoney;
 
                bd.DepositPrice = item.Price * item.Quantity;
                TotalBillDeposit += item.Price * item.Quantity;

                _ctx.Entry(device).State = EntityState.Modified;
                _ctx.BorrowDetails.Add(bd);
            }
            bill.Total = TotalBill;
            bill.DepositPriceOnBill = TotalBillDeposit;
            _ctx.SaveChanges();

            ClearAllCartItem();
            return RedirectToAction("Payment", "store");
        }  //Xử lý đơn hàng

        public IActionResult AddToCard(string id)
        {
            //1. Find product by id
            Device device = _productRepository.findById(id);
            CardModel cardModel = null;
            int Quantity = 1;

            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                //1
                cardModel = new CardModel();
                cardModel.CardId = HttpContext.Session.Id;
                cardModel.setAllItem(HttpContext.Session.Get<List<Item>>("store"));

                //2
                Item item = new Item()
                {
                    Id = device.DeviceId,
                    ImagePath = device.ImagePath,
                    Name = device.Name,
                    Price = device.Price,
                    Quantity = Quantity,
                    lineTotla = device.Price * Quantity
                };

                //3
                cardModel.addItem(item);

                //4
                HttpContext.Session.Set<List<Item>>("store", cardModel.getAllItem());
            }
            else
            {
                //1
                cardModel = new CardModel();
                cardModel.CardId = HttpContext.Session.Id;

                //2
                Item item = new Item()
                {
                    Id = device.DeviceId,
                    ImagePath = device.ImagePath,
                    Name = device.Name,
                    Price = device.Price,
                    Quantity = Quantity,
                    lineTotla = device.Price * Quantity,
                };
                //3
                cardModel.addItem(item);

                //4
                HttpContext.Session.Set<List<Item>>("store", cardModel.getAllItem());
                /*HttpContext.Session.SetInt32()*/
            }

            return RedirectToAction("Payment");
        }  //Thêm sản phẩm vào giỏ hàng

        public IActionResult UpdateQuantity()
        {
            var btn = Request.Form["btnUpdateQuantity"].ToString();
            var id = Request.Form["item.Id"].ToString();
            var qty = Request.Form["item.Quantity"].ToString();
            CardModel cartModel = null;

            if (HttpContext.Session.Get<List<Item>>("store") != null)
            {
                //1
                cartModel = new CardModel();
                cartModel.CardId = HttpContext.Session.Id;
                cartModel.setAllItem(HttpContext.Session.Get<List<Item>>("store"));
            }
            cartModel.UpdateQuantity(id, 1, btn);

            //4
            HttpContext.Session.Set<List<Item>>("store", cartModel.getAllItem());

            return RedirectToAction("Payment");
        }  //cập nhật số lượng sản phẩm

        public IActionResult RemoveItem(string id)  // Xóa sản phẩm trong giỏ hàng
        {
            //1
            List<Item> cartItems = HttpContext.Session.Get<List<Item>>("store");

            if (cartItems != null)
            {
                Item RemoveToProduct = cartItems.FirstOrDefault(x => x.Id == id);

                cartItems.Remove(RemoveToProduct);
            }
            HttpContext.Session.Set("store", cartItems);

            return RedirectToAction("Payment");
        }

        public IActionResult ClearAllCartItem() //xóa tất cả sản phẩm trong giỏ hàng
        {
            HttpContext.Session.Remove("store");

            return RedirectToAction("Payment", "store");
        }

        public IActionResult DetailCard(string Id)
        {
            //1. Find product by id
            var device = _productRepository.findById(Id);

            if (device == null)
            {
                ViewBag.note = "Không tìm thấy sp";
                return NotFound(); // Trong trường hợp không tìm thấy thiết bị
            }

            return View(device);
        } // Vào chi tiết của sản phẩm
    }
}
