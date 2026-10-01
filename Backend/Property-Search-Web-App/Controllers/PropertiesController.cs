using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Repository.Implementation;
using Property_Search_Web_App.Services;

namespace Property_Search_Web_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertyService;
        private readonly CommonMethods _commonMethods;

        public PropertiesController(PropertyService propertyService, CommonMethods commonMethods)
        {
            _propertyService = propertyService;
            _commonMethods = commonMethods;
        }

        [HttpGet("GetProperties")]
        public async Task<IActionResult> GetProperties( [FromQuery] string? type = null, [FromQuery]  decimal? minPrice = null, [FromQuery]  decimal? maxPrice = null, [FromQuery]  int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            ResponseModel<DTOGetAllPropertiesResponse> response = new ResponseModel<DTOGetAllPropertiesResponse>();
            try
            {
                var properties = await _propertyService.GetAllProperty(type, minPrice, maxPrice, pageNumber, pageSize);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                response.statusCode = (int) HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;

                return BadRequest(response);
            }
        }

        [HttpGet("GetPropertyById")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            ResponseModel<DTOPropertyResponse> response = new ResponseModel<DTOPropertyResponse>();

            try
            {   
                var properties = await _propertyService.GetPropertyById(id);
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


        [HttpPost("AddProperty")]
        public async Task<IActionResult> AddProperty([FromBody]  DTOAddProperty propertyDto)
        {
            ResponseModel<object> response = new ResponseModel<object>();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.statusCode = (int)HttpStatusCode.BadRequest;
                    response.messages = _commonMethods.ModelStateNotValid;
                    response.data = ModelState;
                    return BadRequest(response);
                }
                
                var properties = await _propertyService.AddPropertyAsync(propertyDto);
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
