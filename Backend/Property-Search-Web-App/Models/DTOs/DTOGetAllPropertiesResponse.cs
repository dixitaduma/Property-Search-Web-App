namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOGetAllPropertiesResponse
    {
        public List<DTOPropertyResponse> Properties { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
