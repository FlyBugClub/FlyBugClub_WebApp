using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class OrderProcessingController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;
        private IOrderProcessingRepository _orderProcessingRepository;
        private IProductRepository _productRepository;
        private IFacilityRepository _facilityRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderProcessingController(FlyBugClubWebApplicationContext ctx, 
                                        IOrderProcessingRepository orderProcessingRepository,
                                        IProductRepository productRepository,
                                        IFacilityRepository facilityRepository,
                                        UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _orderProcessingRepository = orderProcessingRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _facilityRepository = facilityRepository;
        }
        public IActionResult Bill()
        {
            List<BillBorrow> getAllBill = _orderProcessingRepository.GetAllBillsWithDetails();
            List<HistoryUpdate> getAllHistory = _orderProcessingRepository.GetDAllHistory();

            foreach (var bill in getAllBill)
            {
                /*bill.Sid = _orderProcessingRepository.GetUserName(bill.Sid);*/
                var userName = bill.SidNavigation.Name;

                foreach(var detail in bill.BorrowDetails)
                {
                    detail.DeviceId = _orderProcessingRepository.GetDeviceName(detail.DeviceId);
                }
            }
            
            BillModel billModel = new BillModel();
            billModel.getBills = getAllBill;
            billModel.getAllHistory = getAllHistory;

            int countWaiting = _ctx.BillBorrows.Count(x => x.Status == 0);
            int countBorrowing = _ctx.BillBorrows.Count(x => x.Status == 1);
            int countDone = _ctx.BillBorrows.Count(x => x.Status == 2);

            ViewBag.countWaiting = countWaiting;
            ViewBag.countBorrowing = countBorrowing;
            ViewBag.countDone = countDone;

            return View("Bill", billModel);
        }

        [HttpGet]
        public IActionResult FilterBills(string filterBills, int page = 1)
        {
            List<HistoryUpdate> getAllHistory = _orderProcessingRepository.GetDAllHistory();

            var billList = new List<BillBorrow>();

            if (filterBills == "all")
            {
                billList = _orderProcessingRepository.GetAllBillsWithDetails();
            }
            else if (filterBills == "waiting_bill")
            {
                billList = _orderProcessingRepository.findBillByStatus(0);
            }
            else if (filterBills == "borrowing_bill")
            {
                billList = _orderProcessingRepository.findBillByStatus(1);
            }
            else if (filterBills == "done_bill")
            {
                billList = _orderProcessingRepository.findBillByStatus(2);
            }

            List<BillBorrow> getAllBill = _orderProcessingRepository.GetAllBillsWithDetails();
            foreach (var bill in getAllBill)
            {
                /*bill.Sid = _orderProcessingRepository.GetUserName(bill.Sid);*/
                var userName = bill.SidNavigation.Name;

                foreach (var detail in bill.BorrowDetails)
                {
                    detail.DeviceId = _orderProcessingRepository.GetDeviceName(detail.DeviceId);
                }
            }

            /*==================== Pagination ====================*/

            int itemsPerPage = 1; // Số mục muốn hiển thị cho mỗi trang

            // Lấy tổng số mục từ nguồn dữ liệu của bạn (ví dụ: cơ sở dữ liệu)
            int totalItems = billList.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Xác định số mục cần bỏ qua để hiển thị trang hiện tại
            int skipAmount = (page - 1) * itemsPerPage;

            var paginatedProducts = billList.Skip(skipAmount).Take(itemsPerPage).ToList();

            BillModel billModel = new BillModel();
            billModel.getBills = paginatedProducts;
            billModel.getAllHistory = getAllHistory;

            // Trả về view và truyền thông tin phân trang
            ViewBag.TotalDevices = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.ItemPerPage = itemsPerPage;
            ViewBag.CurrentPage = page;
            ViewBag.fillOption = filterBills;

            int countWaiting = _ctx.BillBorrows.Count(x => x.Status == 0);
            int countBorrowing = _ctx.BillBorrows.Count(x => x.Status == 1);
            int countDone = _ctx.BillBorrows.Count(x => x.Status == 2);

            ViewBag.countWaiting = countWaiting;
            ViewBag.countBorrowing = countBorrowing;
            ViewBag.countDone = countDone;

            return View("FilterBills", billModel);
        }

        public enum BorrowStatus
        {
            Waiting = 0,
            Borrowing = 1,
            Done = 2
        }

        public IActionResult EditBill(string id)
        {
            var suppliers = _orderProcessingRepository.GetAllSuppliers();
            var supplierItems = suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierId.ToString(), // Giá trị của mỗi nhà cung cấp là SupplierId.
                Text = s.SupplierName // Tên của nhà cung cấp là SupplierName.
            }).ToList();

            ViewBag.SupplierList = supplierItems; // Lưu danh sách các nhà cung cấp vào ViewBag.

            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(BorrowStatus))
                            .Cast<BorrowStatus>()
                            .Select(v => new SelectListItem
                            {
                                Text = v.ToString(),
                                Value = ((int)v).ToString()
                            }), "Value", "Text");

            BillBorrow bill = _orderProcessingRepository.findById(id);
            ViewBag.FacilityName = _orderProcessingRepository.GetFacilityNameById(bill.FacilityId);

            return View("EditBill", _orderProcessingRepository.findById(id));
        }

        public IActionResult EditBillDetail(string billId, string detailId)
        {
            return View("EditBillDetail", _orderProcessingRepository.findBillDetailById(billId, detailId));
        }

        [HttpPost]
        public async Task< IActionResult> UpdateBill(BillBorrow billBorrow)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            // Cập nhật billBorrow trong cơ sở dữ liệu
            _orderProcessingRepository.Update(billBorrow);

            var historyUpdate = new HistoryUpdate
            {
                // Gán giá trị từ borrowDetail

                Bid = billBorrow.Bid,
                BorrowDetailId = null,// Gán giá trị từ borrowDetail
                Uid = currentUser.UID, // Gán giá trị của UID (nếu có)
                UpdateDate = DateTime.Now // Hoặc ngày cập nhật mong muốn
            };
            _orderProcessingRepository.AddHistory(historyUpdate);
            return RedirectToAction("Bill", "OrderProcessing");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBillDetail(BorrowDetail borrowDetail)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            BorrowDetail existingBorrowDetail = _orderProcessingRepository.findBorrowDetailById(borrowDetail.BorrowDetailId);

            if (existingBorrowDetail != null)
            {
                int returnQuantity = borrowDetail.ReturnQuantity;
                Device device = _ctx.Devices.FirstOrDefault(d => d.DeviceId == existingBorrowDetail.DeviceId);
                
                if(device != null)
                {
                    device.Quantity += returnQuantity;

                    _ctx.SaveChanges();
                }
            }

                // Tìm BorrowDetail trong cơ sở dữ liệu dựa vào categoryId hoặc Bid
                _orderProcessingRepository.UpdatetBillDetail(borrowDetail);

            var historyUpdate = new HistoryUpdate
            {
                // Gán giá trị từ borrowDetail
                Bid = borrowDetail.Bid,
                BorrowDetailId = borrowDetail.BorrowDetailId, // Gán giá trị từ borrowDetail
                Uid = currentUser.UID, // Gán giá trị của UID (nếu có)
                UpdateDate = DateTime.Now // Hoặc ngày cập nhật mong muốn
            };

            _orderProcessingRepository.AddHistory(historyUpdate);


            return RedirectToAction("Bill", "OrderProcessing");
        }
            

        public IActionResult DeleteBill(string id)
        {
            List<BorrowDetail> bill = _orderProcessingRepository.GetBorrowDetailsByBillBorrowId(id);

            if (bill != null)
            {
                foreach (var item in bill)
                {
                    var device = _productRepository.findById(item.DeviceId);

                    if (device != null)
                    {
                        int currentQuantity = device.Quantity;
                        currentQuantity += item.Quantity;
                        device.Quantity = currentQuantity;
                        _ctx.SaveChanges();
                    }
                }

                _orderProcessingRepository.Delete(id);
            }
 
            return RedirectToAction("Bill", "OrderProcessing");
        }
    }
}
