using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Sale;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class SaleController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public SaleController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            //servise bağlan kategorileri çek view e model olarak gönder

            var response = await _apiService.GetData<ResponseBody<List<SaleItem>>>("/sales", jwt.Token);

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewSaleDto dto)
        {
            var postData = new
            {
                bookId = dto.BookId,
                memberId = dto.MemberId,
                numberOfSales = dto.NumberOfSales,
                saleTime = dto.SaleTime,

            };

            var response =
                await _apiService.PostData<ResponseBody<SaleItem>>("/sales", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Satış Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Satış Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Satış Kaydedilemedi <br /> {errorMessages}"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSaleDto dto)
        {
            var putData = new
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                NumberOfSales = dto.NumberOfSales,
                SaleTime = dto.SaleTime,
            };

            var response =
              await _apiService.PutData<ResponseBody<SaleItem>>("/sales", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Satış Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Satış Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Satış Güncellenemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/sales/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Satış Silindi" });

            return Json(new { IsSuccess = false, Message = "Satış Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetSale(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<SaleItem>>($"/sales/{id}");

            return Json(new
            {
                BookId = response.Data.BookId,
                MemberId = response.Data.MemberId,
                NumberOfSales = response.Data.NumberOfSales,
                SaleTime = response.Data.SaleTime,

            });

        }
    }
}
