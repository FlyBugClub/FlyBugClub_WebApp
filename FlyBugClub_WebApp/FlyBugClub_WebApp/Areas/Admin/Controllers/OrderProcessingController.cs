using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class OrderProcessingController : Controller
    {
        private FlyBugClubWebApplicationContext _ctx;
        private IOrderProcessingRepository _orderProcessingRepository;
        private IProductRepository _productRepository;
        public OrderProcessingController(FlyBugClubWebApplicationContext ctx, 
                                        IOrderProcessingRepository orderProcessingRepository,
                                        IProductRepository productRepository)
        {
            _ctx = ctx;
            _orderProcessingRepository = orderProcessingRepository;
            _productRepository = productRepository;
        }
        public IActionResult Bill()
        {
            List<BillBorrow> getAllBillWDetail = _orderProcessingRepository.GetAllBillsWithDetails();

            foreach (var bill in getAllBillWDetail)
            {
                /*bill.Sid = _orderProcessingRepository.GetUserName(bill.Sid);*/
                var userName = bill.SidNavigation.Name;

                foreach(var detail in bill.BorrowDetails)
                {
                    detail.DeviceId = _orderProcessingRepository.GetDeviceName(detail.DeviceId);
                }
            }

            int countWaiting = _ctx.BillBorrows.Count(x => x.Status == 0);
            int countBorrowing = _ctx.BillBorrows.Count(x => x.Status == 1);
            int countDone = _ctx.BillBorrows.Count(x => x.Status == 2);
            ViewBag.countWaiting = countWaiting;
            ViewBag.countBorrowing = countBorrowing;
            ViewBag.countDone = countDone;

            return View("Bill", getAllBillWDetail);
        }

        public enum BorrowStatus
        {
            Waiting = 0,
            Borrowing = 1,
            Done = 2
        }

        public IActionResult EditBill(int id)
        {
            /*var supplierList = _orderProcessingRepository.GetAllSuppliers();
            ViewBag.SupplierId = new SelectList(supplierList, "SupplierID", "SupplierName");*/
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

            return View("EditBill", _orderProcessingRepository.findById(id));
        }

        public IActionResult EditBillDetail(int billId, int detailId)
        {
            return View("EditBillDetail", _orderProcessingRepository.findBillDetailById(billId, detailId));
        }

        [HttpPost]
        public IActionResult UpdateBill(BillBorrow billBorrow)
        {
            _orderProcessingRepository.Update(billBorrow);
            return RedirectToAction("Bill", "OrderProcessing");
        }

        public IActionResult DeleteBill(int id)
        {
            _orderProcessingRepository.Delete(id);
            return RedirectToAction("Bill", "OrderProcessing");
        }
    }
}
