using BS.Business.Interfaces;
using BS.Model.Dtos.Genre;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BaseController
    {
        private readonly IGenreBs _genreBs;
        public GenresController(IGenreBs genreBs)
        {
            _genreBs = genreBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<GenreGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllGenres()
        {
            var response = await _genreBs.GetGenresAsync("Books");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<GenreGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byname")]
        public async Task<ActionResult> GetAllGenresByName([FromQuery] string name)
        {
            var response = await _genreBs.GetGenresByNameAsync(name, "Books");

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<GenreGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _genreBs.GetByIdAsync(id, "Books");

            return SendResponse(response);

        }


        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Genre>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(string))]
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveNewGenre([FromBody] GenrePostDto dto)
        {
            var response = await _genreBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.GenreId }, response.Data);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateGenre([FromBody] GenrePutDto dto)
        {
            var response = await _genreBs.UpdateAsync(dto);

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
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = await _genreBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}





