using BS.Business.Interfaces;
using BS.Model.Dtos.Author;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        private readonly IAuthorBs _authorBs;
        public AuthorsController(IAuthorBs authorBs)
        {
            _authorBs = authorBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllAuthors()
        {
            var response = await _authorBs.GetAuthorsAsync();

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byname")]
        public async Task<ActionResult> GetAllAuthorsByName([FromQuery] string name)
        {
            var response = await _authorBs.GetAuthorsByNameAsync(name);

            return SendResponse(response);
        }


        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _authorBs.GetByIdAsync(id);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Author>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveNewAuthor([FromBody] AuthorPostDto dto)
        {
            var response = await _authorBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.AuthorId }, response.Data);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorPutDto dto)
        {
            var response = await _authorBs.UpdateAsync(dto);

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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var response = await _authorBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}
