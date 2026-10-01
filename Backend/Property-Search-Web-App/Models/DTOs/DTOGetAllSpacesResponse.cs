namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOGetAllSpacesResponse
    {
        public List<DTOSpaceResponse> Spaces { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
