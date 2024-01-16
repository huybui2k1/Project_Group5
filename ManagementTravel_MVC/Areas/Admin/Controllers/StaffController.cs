using Group5_Management_Library.Models;
using Group5_Management_Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ManagementTravel_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class StaffController : BaseController
    {
       IStaffRepository staffRepository = null;
        public StaffController() => staffRepository = new StaffRepository();
        // GET: StaffController
        [HttpGet]
        public ActionResult Index(string searchString, string CityName, int? page, string sortBy)
        {
            var staffList = staffRepository.GetStaff(sortBy).ToPagedList(page ?? 1, 5);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                staffList = staffRepository.GetStaffByName(searchString, sortBy).ToPagedList(page ?? 1, 5);
            }
            /*TempData["searchString"] = searchString;*/

            //Hiển thị thành phố
            var citys = new List<SelectListItem>
            {
                new SelectListItem{Value = "1", Text ="Hà Tĩnh"},
                new SelectListItem{Value = "2", Text ="Huế"},
                new SelectListItem{Value = "3", Text ="Quảng Bình"},
                new SelectListItem{Value = "4", Text ="Nghệ An"},
                new SelectListItem{Value = "5", Text ="Quảng Trị"},
                new SelectListItem{Value = "6", Text ="Quảng Nam"}
            };
            ViewBag.City = citys;
            return View(/*StaffList*/);
        }

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaffController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff kh)
        {
            try
            {
                    staffRepository.InsertStaff(kh);
                    TempData["Message"] = "Tạo mới thành công";
                    return RedirectToAction("Index");
            }
            catch
            {

            }
            return View(kh);
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            var kh = staffRepository.GetStaffByID(id);
            return View(kh);
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Staff kh)
        {
            try
            {
                staffRepository.UpdateStaff(kh);
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }*/

        [HttpPost]
        public JsonResult DeleteId(int id)
        {
            try
            {
                var record = staffRepository.GetStaffByID(id);
                if (record == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                staffRepository.DeleteStaff(id);
                SetAlert("Xoá thành công", "error");
                /*return Json(new { success = true, id = id});*/
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: StaffController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                staffRepository.DeleteStaff(id);
                TempData["Message"] = "Xoá thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult DeleteMultiple(IEnumerable<int> SelectedCatDelete)
        {
            staffRepository.DeleteSelectedStaff(SelectedCatDelete);
            TempData["Message"] = $"Xoá {SelectedCatDelete.Count()} hàng thành công";
            return RedirectToAction("Index");
        }
    }
}
