using BookSalesProject.MvcUI.ApiServices;
using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Book;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class BookController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHost;
        public BookController(IHttpApiService apiService, IWebHostEnvironment webHost)
        {
            _apiService = apiService;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            //aşağıdaki endpoint bir token istiyor, GetData metodu üzerinden bu token ı endpoint e göndermemiz lazım

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");
            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response =
              await _apiService.GetData<ResponseBody<List<BookItem>>>("/books", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewBookDto dto, IFormFile bookImage)
        {

            if (bookImage == null)
                return Json(new { IsSuccess = false, Message = "Kitap resmi seçilmelidir" });

            if (!bookImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir" });

            if (bookImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya büyüklüğü en fazla 250 KB olaiblir" });


            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(bookImage.FileName)}";
            var uploadPath = $@"{_webHost.WebRootPath}/adminPanel/bookImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await bookImage.CopyToAsync(fs);
            }

            var postData = new
            {
                PicturePath = $@"/adminPanel/bookImages/{randomFileName}",
                bookName = dto.BookName,
                numberOfPages = dto.NumberOfPages,
                isbn = dto.ISBN,
                price = dto.Price,
                publishDate = DateTime.UtcNow

            };



            var response =
              await _apiService.PostData<ResponseBody<BookItem>>("/books", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Kitap Başarıyla Kaydedildi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kitap Başarıyla Kaydedildi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Kitap Kaydedilemedi <br /> {errorMessages}"
            });


        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBookDto dto, IFormFile bookImage)
        {

            if (bookImage == null)
                return Json(new { IsSuccess = false, Message = "Kitap resmi seçilmelidir" });

            if (!bookImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir" });

            if (bookImage.Length > 1024 * 250 )
                return Json(new { IsSuccess = false, Message = "Dosya büyüklüğü en fazla 250 KB olaiblir" });

            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(bookImage.FileName)}";
            var uploadPath = $@"{_webHost.WebRootPath}/adminPanel/bookImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await bookImage.CopyToAsync(fs);
            }

            var putData = new
            {
                BookName = dto.BookName,
                NumberOfPages = dto.NumberOfPages,
                ISBN = dto.ISBN,
                Price = dto.Price,
                GenreName = dto.GenreName,
                PicturePath = $@"/adminPanel/bookImages/{randomFileName}",
            };


            var response =
              await _apiService.PutData<ResponseBody<BookItem>>("/books", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 201)
                return Json(new { IsSuccess = true, Message = "Kitap Başarıyla Güncellendi" });

            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kitap Başarıyla Güncellendi" });


            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br />";
            }
            return Json(new
            {
                IsSuccess = false,
                Message = $"Kitap Güncellenemedi <br /> {errorMessages}"
            });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response =
              await _apiService.DeleteData($"/books/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Kitap Silindi" });

            return Json(new { IsSuccess = false, Message = "Kitap Silinemedi" });
        }

        [HttpPost]
        public async Task<IActionResult> GetBook(int id)
        {
            var response =
              await _apiService.GetData<ResponseBody<BookItem>>($"/books/{id}");

            return Json(new
            {
                BookName = response.Data.BookName,
                NumberOfPages = response.Data.NumberOfPages,
                ISBN = response.Data.ISBN,
                Price = response.Data.Price,
                PicturePath = response.Data.PicturePath,
                GenreName = response.Data.GenreName,
            });

        }
    }
}
