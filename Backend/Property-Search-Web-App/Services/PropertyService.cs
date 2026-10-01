using System.Net;
using Microsoft.AspNetCore.Mvc;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Repository.Interface;

namespace Property_Search_Web_App.Services
{
    public class PropertyService
    {
        private readonly IPropertyInterface _propertyRepo;
        private readonly CommonMethods _commonMethods;
        public PropertyService(IPropertyInterface propertyRepo, CommonMethods commonMethods)
        {
            _propertyRepo = propertyRepo;
            _commonMethods = commonMethods;
        }

        public async Task<ResponseModel<DTOGetAllPropertiesResponse>> GetAllProperty(string? type = null, decimal? minPrice = null, decimal? maxPrice = null, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<DTOGetAllPropertiesResponse> response = new ResponseModel<DTOGetAllPropertiesResponse>();

            try
            {
                var result = await _propertyRepo.GetAllProperty(type, minPrice, maxPrice, pageNumber, pageSize);
                return result;

            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.OK;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;
                return response;

            }
        }


        public async Task<ResponseModel<DTOPropertyResponse>> GetPropertyById(int id)

        {
            ResponseModel<DTOPropertyResponse> response = new ResponseModel<DTOPropertyResponse>();

            try
            {
                if (id == null)
                {
                    response.statusCode = (int)HttpStatusCode.BadRequest;
                    response.messages = _commonMethods.PropertyIdRequired;
                    response.data = null;

                }
                var result = await _propertyRepo.GetPropertyById(id);
                return result;
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;
            }

            return response;
        }

        public async Task<ResponseModel<object>> AddPropertyAsync(DTOAddProperty propertyDto)
        {
            ResponseModel<object> response = new ResponseModel<object>();

            try
            {

                var result = await _propertyRepo.AddPropertyAsync(propertyDto);
                return result;
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;
            }

            return response;
        }


    }
}
