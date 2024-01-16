using Group5_Management_Library.Models;
using Group5_Management_Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using X.PagedList;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace ManagementTravel_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductController : BaseController
    {
        /*ICustomersRepository CustomerRepository = null;*/
        private readonly HttpClient _httpClient;
        public ProductController()
        {
            _httpClient = new HttpClient();
           /* CustomerRepository = new CustomersRepository();*/
            // GET: CustomerController

        }
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

        }


            /*IProductsRepository productsRepository = null;
           // IProductsCategoryRepository productsCategoryRepository = null;
            IUsersRepository userRepository = null;
            private readonly IWebHostEnvironment webHostEnvironment;
            public IActionResult Index(string? searchString, int? page, string sortBy, int? categoryId)
            {
                //Enumerable<ProductCategory> productsCategory = productsRepository.GetAllProductsCategory();
                //IEnumerable<User> users = userRepository.GetAll();
                // Tạo SelectList từ danh sách quyền truy cập
                //SelectList selectList = new SelectList(productsCategory, "Id", "CategoryName");

                // Lưu SelectList vào ViewBag để sử dụng trong View
                //ViewBag.ProductCategory = selectList;
                TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                //var products = productsRepository.GetProductsByKeyword(searchString, sortBy, categoryId).OrderByDescending(n => n.DateUpdate);
                //IPagedList<Product> productsList = new PagedList<Product>(products, pageNumber, pageSize);
                var productsCategoryUser = new ProductsCategoryUsers
                {
                    //ProductsCategory = (ICollection<ProductCategory>)productsCategory,
                    //Users = (ICollection<User>)users,
                };
                //productsCategoryUser.Products = productsList;
                return View(productsCategoryUser);*/
        }
        //Get Admin/Roles.Create
        
       /* [HttpPost]
        public JsonResult Create(Product products)
        {
            try
            {
                // Lấy giá trị từ các trường thuộc tính của news trong form
                var model = new Product
                {
                    Name = products.Name,
                    Description = products.Description,
                    SubjectContent = products.SubjectContent,
                    CategoryId = products.CategoryId,
                    Avatar = products.Avatar,
                    Price = products.Price,
                    Quanlity = products.Quanlity,
                    Status = products.Status
                };
                //model.DateUpdate = Common.Common.GetServerDateTime();
                var user = User as ClaimsPrincipal;
                var userName = user?.FindFirstValue(ClaimTypes.Name);
                model.UserId = userRepository.GetByUserName(userName).UserId;
                productsRepository.InsertProduct(model);

                SetAlert("Insert Data is success!", "success");

                *//* }*//*
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product products = productsRepository.GetProductByID(id);
            //ViewBag.NewsCategory = new SelectList(productsRepository.GetAllProductsCategory(), "Id", "CategoryName");
            var data = new
            {
                ProductId = id,
                Name = products.Name,
                Description = products.Description,
                SubjectContent = products.SubjectContent,
                CategoryId = products.CategoryId,
                DateUpdate = products.DateUpdate.ToString("dd/MM/yyyy"),
                Avatar = products.Avatar,
                Status = products.Status,
                UserId = products.UserId,
                Price = products.Price,
                Quanlity = products.Quanlity,
            };
            ViewBag.Anh = products.Avatar;
            return new JsonResult(new { success = true, data = data });
        }

        [HttpPost]
        public JsonResult Edit(Product products)
        {
            try
            {
                // Lấy giá trị từ các trường thuộc tính của user trong form
                var newp = new Product
                {
                    ProductId = products.ProductId,
                    Name = products.Name,
                    Description = products.Description,
                    SubjectContent = products.SubjectContent,
                    CategoryId = products.CategoryId,
                    Avatar = products.Avatar,
                    Status = products.Status,
                    UserId = products.UserId,
                    Price = products.Price,
                    Quanlity = products.Quanlity,
                };
                if (products.DateUpdate != null)
                {
                    newp.DateUpdate = DateTime.ParseExact(products.DateUpdate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                }
                productsRepository.UpdateProduct(newp);
                SetAlert("Update Data is success!", "success");

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                string wwwRootPath = webHostEnvironment.WebRootPath;
                // Tạo thư mục nếu nó không tồn tại
                *//* if (!Directory.Exists(uploadsFolder))
                 {
                     Directory.CreateDirectory(uploadsFolder);
                 }*//*
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //var filePath = Path.Combine(uploadsFolder, file.FileName);
                string extension = Path.GetExtension(file.FileName);
                ViewBag.Anh = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Upload/Images/", fileName);
                // Lưu tệp tin vào thư mục uploads
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var data = new
                {
                    Avatar = fileName,
                };
                return new JsonResult(new { success = true, data = data });
                // Trả về phản hồi thành công (nếu cần)
                //return Ok();
            }

            // Trả về phản hồi lỗi (nếu có)
            return BadRequest();
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            //var result = productsRepository.ChangeStatus(id);
            return Json(new
            {
                //status = result
            });
        }

        [HttpPost]
        public JsonResult Delete(Product products)
        {
            try
            {
                //productsRepository.DeleteProduct(products);
                SetAlert("Delete Data is success!", "success");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }*/

    }
