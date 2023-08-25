using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Book;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Member;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class MemberController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public MemberController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            //servise bağlan kategorileri çek view e model olarak gönder

            var response = await _apiService.GetData<ResponseBody<List<MemberItem>>>("/members", jwt.Token);

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(NewMemberDto dto)
        {
            var postData = new
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                Phone = dto.Phone,
                Address =dto.Address,


            };

            var response =
                await _apiService.PostData<ResponseBody<MemberItem>>("/members", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Üye Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Üye Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Üye Kaydedilemedi <br /> {errorMessages}"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMemberDto dto)
        {

            var putData = new
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                
            };

            var response =
              await _apiService.PutData<ResponseBody<MemberItem>>("/members", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Üye Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Üye Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Üye Güncellenemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/members/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Üye Silindi" });

            return Json(new { IsSuccess = false, Message = "Üye Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetMember(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<MemberItem>>($"/members/{id}");

            return Json(new
            {
                FirstName = response.Data.FirstName,
                LastName = response.Data.LastName,
                BirthDate = response.Data.BirthDate,
                Email = response.Data.Email,
                Phone = response.Data.Phone,
                Address = response.Data.Address,
            });

        }
    }
}
