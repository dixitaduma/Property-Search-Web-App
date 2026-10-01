using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Services;

namespace Property_Search_Web_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly SpaceService _spaceService;
        private readonly CommonMethods _commonMethods;
        public SpacesController(SpaceService spaceService , CommonMethods commonMethods) 
        {
            _commonMethods = commonMethods;
            _spaceService = spaceService;
        }

        [HttpGet("GetAllSpaces")]
        public async Task<IActionResult> GetAllSpaces([FromQuery] int propertyId, [FromQuery] string? type = null, [FromQuery] decimal? minSize = null, [FromQuery]  int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            ResponseModel<List<DTOSpaceResponse>> response = new ResponseModel<List<DTOSpaceResponse>>();

            try
            {
                var properties = await _spaceService.GetAllSpacesAsync(propertyId, type, minSize, pageNumber, pageSize);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;

                return BadRequest(response);
            }
        }

    }
}
