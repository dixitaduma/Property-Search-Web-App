namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOSpaceResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Type { get; set; }
        public decimal Size { get; set; }
        public string Description { get; set; }
    }
}
