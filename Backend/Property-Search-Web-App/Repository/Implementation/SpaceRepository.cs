using Microsoft.EntityFrameworkCore;
using System.Net;
using Property_Search_Web_App.Data;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Repository.Interface;

namespace Property_Search_Web_App.Repository.Implementation
{
    public class SpaceRepository : ISpaceInterface
    {
        private readonly AppDbContext _context;
        private readonly CommonMethods _commonMethods;

        public SpaceRepository(AppDbContext context, CommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;

        }

        public async Task<ResponseModel<DTOGetAllSpacesResponse>> GetAllSpacesAsync(int propertyId, string? type = null, decimal? minSize = null, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<DTOGetAllSpacesResponse> response = new ResponseModel<DTOGetAllSpacesResponse>();

            try
            {
                var query = _context.Spaces.AsQueryable();

                // Apply filters
                if (propertyId != null ||  propertyId != 0)
                {
                    query = query.Where(s => s.PropertyId == propertyId);
                }

                if (!string.IsNullOrEmpty(type))
                {
                    query = query.Where(s => s.Type.ToLower().Contains(type.ToLower()));
                }

                if (minSize.HasValue)
                {
                    query = query.Where(s => s.Size >= minSize.Value);
                }

               
                var totalSpaces = await query.CountAsync();

                // Pagination
                var spaces = await query
                    .Skip((pageNumber - 1) * pageSize)  
                    .Take(pageSize) 
                    .Select(s => new DTOSpaceResponse
                    {
                        Id = s.Id,
                        PropertyId = s.PropertyId,
                        Type = s.Type,
                        Size = s.Size,
                        Description = s.Description
                    })
                    .ToListAsync();

                
                var totalPages = (int)Math.Ceiling(totalSpaces / (double)pageSize);

                
                var result = new DTOGetAllSpacesResponse
                {
                    Spaces = spaces,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalSpaces,
                    TotalPages = totalPages
                };
                
                if (spaces.Any())
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.GetAllSpacesSuccess;
                    response.data = result;
                }
                else
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.SpacesNotFound;
                    response.data = new DTOGetAllSpacesResponse();  
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;
            }

            return response;
        }

    }
}
