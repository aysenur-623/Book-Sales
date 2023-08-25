using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AuthController : Controller
    {
        private readonly IHttpApiService _apiService;

        public AuthController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> LogIn(LogInDto dto)
        //{
        //  // servisle habeleşecek ilgili endpointten böyle bir kullanıcı var mı diye kontrol ettirecek
        //  // SERVİSDEN GELEN YANITA BAKACAK 
        //  // client tarayıcısına, oradaki ajax çağrılan yere bir yanıt dönecek

        //  using (HttpClient client = new HttpClient())
        //  {
        //    client.BaseAddress = new Uri("http://localhost:5075/api");

        //    var responseMessage = await client.GetAsync($"{client.BaseAddress}/auth/login?userName={dto.UserName}&password={dto.Password}");

        //    var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

        //    var response = JsonSerializer.Deserialize<ResponseBody<AdminPanelUserItem>>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        //    if (responseMessage.IsSuccessStatusCode)
        //    {

        //      HttpContext.Session.SetString("ActiveAdminPanelUser", JsonSerializer.Serialize(response.Data));

        //      return Json(new { IsSuccess = true, Messages = "Kullanıcı Bulundu" });
        //    }
        //    else
        //    {
        //      return Json(new {IsSuccess=false,Messages=response.ErrorMessages });
        //    }
        //  }


        //}

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto dto)
        {
            string endPoint = $"/auth/login?userName={dto.UserName}&password={dto.Password}";

            var response =
              await _apiService.GetData<ResponseBody<AdminPanelUserItem>>(endPoint);



            if (response.StatusCode == 200)
            {
                HttpContext.Session.SetString("ActiveAdminPanelUser", JsonSerializer.Serialize(response.Data));

                // Servise gidip token alacağız, bu token ı session a kaydedeceğiz, sonra iç sayfalarda token lazım olduğuhda sessiondan token ı okuyup servise göndereceğiz

                await GetTokenAndSetInSession();

                return Json(new { IsSuccess = true, Messages = "Kullanıcı Bulundu" });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }

        }

        public async Task GetTokenAndSetInSession()
        {
            var response = await _apiService.GetData<ResponseBody<AccessTokenItem>>($"/auth/gettoken");

            HttpContext.Session.SetString("AccessToken", JsonSerializer.Serialize(response.Data));
        }
    }
}
