using Property_Search_Web_App.Models.DTOs;

namespace Property_Search_Web_App.Repository.Interface
{
    public interface IPropertyInterface
    {
        Task<ResponseModel<DTOGetAllPropertiesResponse>> GetAllProperty(string? type = null, decimal? minPrice = null,decimal? maxPrice = null, int pageNumber = 1,int pageSize = 10);
        Task<ResponseModel<DTOPropertyResponse>> GetPropertyById(int id);
        Task<ResponseModel<object>> AddPropertyAsync(DTOAddProperty propertyDto);


    }
}
