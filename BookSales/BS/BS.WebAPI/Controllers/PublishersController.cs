using BS.Business.Interfaces;
using BS.Model.Dtos.Publisher;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BaseController
    {
        private readonly IPublisherBs _publisherBs;
        public PublishersController(IPublisherBs publisherBs)
        {
            _publisherBs = publisherBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<PublisherGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPublishers()
        {
            var response = await _publisherBs.GetPublishersAsync();

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<PublisherGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byname")]
        public async Task<ActionResult> GetAllPublishersByName([FromQuery] string name)
        {
            var response = await _publisherBs.GetPublishersByNameAsync(name);

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<PublisherGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("bycity")]
        public async Task<ActionResult> GetAllPublishersByCity([FromQuery] string city)
        {

            var response = await _publisherBs.GetPublishersByCityAsync(city);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<PublisherGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(string))]
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _publisherBs.GetByIdAsync(id);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Publisher>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveNewPublisher([FromBody] PublisherPostDto dto)
        {
            var response = await _publisherBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.PublisherId }, response.Data);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePublisher([FromBody] PublisherPutDto dto)
        {
            var response = await _publisherBs.UpdateAsync(dto);

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
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var response = await _publisherBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}
