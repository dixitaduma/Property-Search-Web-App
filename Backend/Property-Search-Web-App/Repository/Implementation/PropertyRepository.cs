using System.Net;
using Microsoft.EntityFrameworkCore;
using Property_Search_Web_App.Common_Methods;
using Property_Search_Web_App.Data;
using Property_Search_Web_App.Models.DTOs;
using Property_Search_Web_App.Models.Entity;
using Property_Search_Web_App.Repository.Interface;

namespace Property_Search_Web_App.Repository.Implementation
{
    public class PropertyRepository : IPropertyInterface
    {

        private readonly AppDbContext _context;
        private readonly CommonMethods _commonMethods;
        public PropertyRepository(AppDbContext context, CommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }
        public async Task<ResponseModel<DTOGetAllPropertiesResponse>> GetAllProperty(string? type = null, decimal? minPrice = null, decimal? maxPrice = null, int pageNumber = 1, int pageSize = 10)
        {
            ResponseModel<DTOGetAllPropertiesResponse> response = new ResponseModel<DTOGetAllPropertiesResponse>();

            try
            {
                
                var query = _context.Properties
                                     .Include(p => p.Spaces)
                                     .AsQueryable();

                if (!string.IsNullOrEmpty(type))
                {
                    query = query.Where(p => p.Type.ToLower().Contains(type.ToLower()));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }
                query = query.OrderByDescending(p => p.Id);

                var totalProperties = await query.CountAsync();

                //Pagination
                var properties = await query
                    .Skip((pageNumber - 1) * pageSize)  
                    .Take(pageSize)  
                    .Select(p => new DTOPropertyResponse
                    {
                        Id = p.Id,
                        Price = p.Price,
                        Type = p.Type,
                        Address = p.Address,
                        Description = p.Description,
                        SpaceAverage = p.Spaces.Any() ? p.Spaces.Average(s => s.Size) : 0,
                        NumberOfSpaces = p.Spaces.Count(),
                        Spaces = p.Spaces.Select(s => new DTOSpaceResponse
                        {
                            Id = s.Id,
                            Size = s.Size,
                            Type = s.Type,
                            Description = s.Description,
                            PropertyId = s.PropertyId
                        }).ToList()
                    })
                    .ToListAsync();

                
                var totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);

             
                var result = new DTOGetAllPropertiesResponse
                {
                    Properties = properties,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalProperties,
                    TotalPages = totalPages
                };

               
                if (properties.Count > 0)
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.GetAllPropertiesSuccess;
                    response.data = result;
                }
                else
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.GetAllPropertiesFailed;
                    response.data = new DTOGetAllPropertiesResponse();  
                }
            }
            catch (Exception ex)
            {              
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = _commonMethods.InternalServerError;
                response.data = null;
            }
            return response;
        }



        public async Task<ResponseModel<DTOPropertyResponse>> GetPropertyById(int id)
        {
            ResponseModel<DTOPropertyResponse> response = new ResponseModel<DTOPropertyResponse>();
            try
            {
                var result = await _context.Properties
                    .Include(p => p.Spaces)
                    .Where(p => p.Id == id)
                    .Select(p => new DTOPropertyResponse
                    {
                        Id = p.Id,
                        Price = p.Price,
                        Type = p.Type,
                        Address = p.Address,
                        Description = p.Description,
                        Spaces = p.Spaces.Select(s => new DTOSpaceResponse
                        {
                            Id = s.Id,
                            Size = s.Size,
                            Type = s.Type,
                            Description = s.Description,
                            PropertyId = s.PropertyId
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.GetPropertySuccess;
                    response.data = result;
                }
                else
                {
                    response.statusCode = (int)HttpStatusCode.OK;
                    response.messages = _commonMethods.GetPropertyNotFound;
                    response.data = new DTOPropertyResponse();
                }

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

            using (var transaction = await _context.Database.BeginTransactionAsync())  
            {
                try
                {  
                    var newProperty = new Property
                    {
                        Address = propertyDto.Address,
                        Type = propertyDto.Type,
                        Price = propertyDto.Price,
                        Description = propertyDto.Description,
                    };
                  
                    if (propertyDto.Spaces != null && propertyDto.Spaces.Any())
                    {
                        newProperty.Spaces = propertyDto.Spaces.Select(s => new Space
                        {
                            Type = s.Type,
                            Size = s.Size,
                            Description = s.Description,
                        }).ToList();
                    }
                  
                    await _context.Properties.AddAsync(newProperty);
                    await _context.SaveChangesAsync();
                 
                    await transaction.CommitAsync();
             
                    response.statusCode = (int)HttpStatusCode.Created;
                    response.messages =  _commonMethods.PropertyCreatedSuccess ;                    
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    response.statusCode = (int)HttpStatusCode.InternalServerError;
                    response.messages = _commonMethods.InternalServerError;
                    response.data = null;
                }
            }
            return response;
        }
    }
}