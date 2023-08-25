using BS.Business.Interfaces;
using BS.Model.Dtos.Book;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly IBookBs _bookBs;
        public BooksController(IBookBs bookBs)
        {
            _bookBs = bookBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllBooks()
        {
            var response = await _bookBs.GetBooksAsync("Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("bygenre")]
        // .../api/books/bygenre?genreId=2   (querystring)
        public async Task<ActionResult> GetAllBooksByGenre([FromQuery] int genreId)
        {
            var response = await _bookBs.GetBooksByGenreAsync(genreId, "Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byauthor")]
        
        public async Task<ActionResult> GetAllBooksByNumberOfPages([FromQuery] int numberOfPages)
        {

            var response = await _bookBs.GetBooksByNumberOfPagesAsync(numberOfPages, "Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("bypublisher")]
        public async Task<ActionResult> GetAllBooksByISBN([FromQuery] string isbn)
        {

            var response = await _bookBs.GetBooksByISBNAsync(isbn, "Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byname")]
        public async Task<ActionResult> GetAllBooksByName([FromQuery] string name)
        {
            var response = await _bookBs.GetBooksByNameAsync(name,"Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byprice")]
        public async Task<ActionResult> GetAllBooksByPrice([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var response = await _bookBs.GetBooksByPriceAsync(min, max, "Genre");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _bookBs.GetByIdAsync(id, "Genre");

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Book>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveNewBook([FromBody] BookPostDto dto)
        {
            var response = await _bookBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.BookId }, response.Data);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromBody] BookPutDto dto)
        {
            var response = await _bookBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _bookBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}

