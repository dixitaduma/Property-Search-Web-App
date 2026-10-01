using Property_Search_Web_App.Models.DTOs;

namespace Property_Search_Web_App.Repository.Interface
{
    public interface ISpaceInterface 
    {
        Task<ResponseModel<DTOGetAllSpacesResponse>> GetAllSpacesAsync(int propertyId, string? type = null, decimal? minSize = null, int pageNumber = 1, int pageSize = 10);

    }
}
