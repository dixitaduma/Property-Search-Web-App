namespace Property_Search_Web_App.Models.Entity
{
    public class Property
    {
        public int Id { get; set; } 
        public string Address { get; set; } 
        public string Type { get; set; }
        public decimal Price { get; set; } 
        public string Description { get; set; } 

        public ICollection<Space> Spaces { get; set; }
    }
}
