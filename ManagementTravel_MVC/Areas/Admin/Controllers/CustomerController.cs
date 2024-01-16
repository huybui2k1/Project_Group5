using Azure;
using Group5_Management_Library.Models;
using Group5_Management_Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using X.PagedList;

namespace ManagementTravel_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CustomerController : BaseController
    {
        //ICustomersRepository CustomerRepository = null;
        private readonly HttpClient _httpClient;
        public CustomerController()
        {
            _httpClient = new HttpClient();
            //CustomerRepository = new CustomersRepository();
            // GET: CustomerController

        }


        [HttpGet]
        public async Task<ActionResult> Index(string searchString, string CityName, int? page, string sortBy)
        {
            // API endpoint URL
            string apiUrl = "http://localhost:5000/api/Products";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);


            if (response.IsSuccessStatusCode)
            {
                // Read and process the response content
                string responseData = await response.Content.ReadAsStringAsync();
                // Do something with the responseData

                // Return a view or display the data in some way
                return View();
            }
            else
            {
                // Handle the error (e.g., log, display an error message)
                return View("Error");
            }

            /*var CustomerList = CustomerRepository.GetCustomers(sortBy).ToPagedList(page ?? 1, 5);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                CustomerList = CustomerRepository.GetCustomerByName(searchString,sortBy).ToPagedList(page ?? 1, 5);
            }*/
            /*  TempData["searchString"] = searchString;*/

            /*var CustomerList = CustomerRepository.GetCustomerByName(searchString is null ? null : searchString, CityName is null ? null : CityName.ToLower(), sortBy).ToPagedList(page ?? 1, 5);*//*

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
            return View(*//*CustomerList*//*);*/
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
        // GET: CustomerController/Create
       /* public ActionResult Create()
        {
            return View();
        }
*/
        // POST: CustomerController/Create
        /*[HttpPost]
        *//* [ValidateAntiForgeryToken]*//*
        public ActionResult Create(Customer kh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerRepository.InsertCustomer(kh);
                    SetAlert("Tạo mới thành công", "error");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo mới khách hàng không thành công");
                }
            }
            catch
            {

            }
            return View(kh);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var kh = CustomerRepository.GetCustomerByID(id);
            return View(kh);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer kh)
        {
            try
            {
                CustomerRepository.UpdateCustomer(kh);
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        *//*public ActionResult Delete(int id)
        {
            return View();
        }*//*

        [HttpPost]
        public JsonResult DeleteId(int id)
        {
            try
            {
                var record = CustomerRepository.GetCustomerByID(id);
                if (record == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                CustomerRepository.DeleteCustomer(id);
                SetAlert("Xoá thành công", "error");
                *//*return Json(new { success = true, id = id});*//*
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

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                CustomerRepository.DeleteCustomer(id);
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
            CustomerRepository.DeleteSelectedCustomer(SelectedCatDelete);
            TempData["Message"] = $"Xoá {SelectedCatDelete.Count()} hàng thành công";
            return RedirectToAction("Index");
        }

        *//*public JsonResult ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var data = CustomerRepository.GetCustomerByName(q.ToLower(), "", "name");
                var responseData = data.Select(kh => kh.CustomerIdName).ToList();
                return Json(new
                {
                    data = responseData,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });*//*
        }
    }*/

