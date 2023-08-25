
using BS.Business.Interfaces;
using BS.Model.Dtos.Member;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        private readonly IMemberBs _memberBs;
        public MembersController(IMemberBs memberBs)
        {
            _memberBs = memberBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MemberGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllMembers()
        {
            var response = await _memberBs.GetMembersAsync();

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MemberGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byname")]
        public async Task<ActionResult> GetAllMembersByName([FromQuery] string name)
        {
            var response = await _memberBs.GetMembersByNameAsync(name);

            return SendResponse(response);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MemberGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byemail")]
        public async Task<ActionResult> GetAllMembersByEmail([FromQuery] string email)
        {

            var response = await _memberBs.GetMembersByEmailAsync(email);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MemberGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("byaddress")]
        public async Task<ActionResult> GetAllMembersByAddress([FromQuery] string address)
        {

            var response = await _memberBs.GetMembersByAddressAsync(address);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MemberGetDto>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _memberBs.GetByIdAsync(id);

            return SendResponse(response);

        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Member>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(string))]
        #endregion

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveNewMember([FromBody] MemberPostDto dto)
        {
            var response = await _memberBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.MemberId }, response.Data);
        }

        #region SWAGGER DOC
        [Produces("application/json", "Text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(string))]
        #endregion

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMember([FromBody] MemberPutDto dto)
        {
            var response = await _memberBs.UpdateAsync(dto);

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
        public async Task<IActionResult> DeleteMember(int id)
        {
            var response = await _memberBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}
