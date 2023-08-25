using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Author;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class AuthorController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public AuthorController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            //servise bağlan kategorileri çek view e model olarak gönder

            var response = await _apiService.GetData<ResponseBody<List<AuthorItem>>>("/authors", jwt.Token);

            return View(response.Data);

        }

        

        [HttpPost]
        public async Task<IActionResult> Save(NewAuthorDto dto)
        {
            var postData = new
            {
                AuthorId = dto.AuthorId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,

            };

            var response =
                await _apiService.PostData<ResponseBody<AuthorItem>>("/authors", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Yazar Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yazar Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Yazar Kaydedilemedi <br /> {errorMessages}"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorDto dto)
        {

            var putData = new
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var response =
              await _apiService.PutData<ResponseBody<AuthorItem>>("/authors", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Yazar Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yazar Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Yazar Güncellenemedi <br /> {errorMessages}"
            });


        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/authors/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Yazar Silindi" });

            return Json(new { IsSuccess = false, Message = "Yazar Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<AuthorItem>>($"/authors/{id}");

            return Json(new
            {
                FirstName = response.Data.FirstName,
                LastName = response.Data.LastName,

            });

        }


    }
}


