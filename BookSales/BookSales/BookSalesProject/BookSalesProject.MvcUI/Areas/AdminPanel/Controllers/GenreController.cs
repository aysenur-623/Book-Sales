using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Genre;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class GenreController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public GenreController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            //servise bağlan kategorileri çek view e model olarak gönder

            var response = await _apiService.GetData<ResponseBody<List<GenreItem>>>("/genres", jwt.Token);

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewGenreDto dto)
        {
            var postData = new
            {
                GenreId =dto.GenreId,
                GenreName = dto.GenreName,

            };

            var response =
                await _apiService.PostData<ResponseBody<GenreItem>>("/genres", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Tür Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Tür Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Tür Kaydedilemedi <br /> {errorMessages}"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGenreDto dto)
        {
            var putData = new
            {
                GenreName = dto.GenreName,
            };

            var response =
              await _apiService.PutData<ResponseBody<GenreItem>>("/genres", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Tür Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Tür Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Tür Güncellenemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/genres/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Tür Silindi" });

            return Json(new { IsSuccess = false, Message = "Tür Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetGenre(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<GenreItem>>($"/genres/{id}");

            return Json(new
            {
                GenreName = response.Data.GenreName,

            });

        }

    }
}
