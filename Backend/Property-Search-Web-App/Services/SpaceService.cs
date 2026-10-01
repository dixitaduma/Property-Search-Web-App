using Property_Search_Web_App.Common_Methods;
using System.Net;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Repository.Interface;

namespace Property_Search_Web_App.Services
{
    public class SpaceService
    {
        private readonly ISpaceInterface _spaceRepo;
        private readonly CommonMethods _commonMethods;

        public SpaceService(ISpaceInterface spaceRepo, CommonMethods commonMethods) 
        {
            _spaceRepo = spaceRepo;
            _commonMethods = commonMethods;
        }

        public async Task<ResponseModel<DTOGetAllSpacesResponse>> GetAllSpacesAsync(int propertyId , string? type = null, decimal? minSize = null, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<DTOGetAllSpacesResponse> response = new ResponseModel<DTOGetAllSpacesResponse>();

            try
            {
                if(propertyId == null || propertyId == 0)
                {
                    response.statusCode = (int)HttpStatusCode.BadRequest;
                    response.messages = _commonMethods.PropertyIdRequired;
                    return response;
                }
                var result = await _spaceRepo.GetAllSpacesAsync(propertyId, type, minSize, pageNumber, pageSize);
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
    }
}
