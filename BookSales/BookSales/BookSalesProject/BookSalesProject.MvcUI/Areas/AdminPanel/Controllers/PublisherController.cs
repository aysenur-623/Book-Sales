using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Book;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Publisher;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class PublisherController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public PublisherController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);


            //servise bağlan kategorileri çek view e model olarak gönder

            var response = await _apiService.GetData<ResponseBody<List<PublisherItem>>>("/publishers", jwt.Token);

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewPublisherDto dto, IFormFile form)
        {
            var postData = new
            {
                PublisherName = dto.PublisherName,
                City = dto.City,

            };

            var response =
                await _apiService.PostData<ResponseBody<PublisherItem>>("/publishers", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Yayınevi Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yayınevi Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Yayınevi Kaydedilemedi <br /> {errorMessages}"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePublisherDto dto)
        {
            var putData = new
            {
                PublisherName = dto.PublisherName,
                City = dto.City,
            };

            var response =
              await _apiService.PutData<ResponseBody<PublisherItem>>("/publishers", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Yayınevi Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yayınevi Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Yayınevi Güncellenemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/publishers/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Yayınevi Silindi" });

            return Json(new { IsSuccess = false, Message = "Yyınevi Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<PublisherItem>>($"/publishers/{id}");

            return Json(new
            {
                PublisherName = response.Data.PublisherName,
                City = response.Data.City,

            });

        }
    }
}
