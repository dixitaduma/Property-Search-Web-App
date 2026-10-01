namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOPropertyResponse
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal SpaceAverage { get; set; }
        public int NumberOfSpaces { get; set; }

        public List<DTOSpaceResponse> Spaces { get; set; }
    }
}
