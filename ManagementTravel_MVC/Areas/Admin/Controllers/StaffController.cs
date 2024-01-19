using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ManagementTravel_MVC.Areas.Admin.Controllers
{
  
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public class StaffController : BaseController
        {
            /*ICustomersRepository CustomerRepository = null;*/
            private readonly HttpClient _httpClient = null;
            public StaffController()
            {
                _httpClient = new HttpClient();
                /* CustomerRepository = new CustomersRepository();*/
                // GET: CustomerController

            }
            public async Task<ActionResult> Index()
            {
                // API endpoint URL
                string apiUrl = "http://localhost:5000/api/Basket";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Basket> listBasket = JsonSerializer.Deserialize<List<Basket>>(strData, option);

                return View(listBasket);
            

        }

    }
}
