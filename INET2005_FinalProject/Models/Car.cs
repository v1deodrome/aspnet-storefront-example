namespace INET2005_FinalProject.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Foreign key
        public required CarType CarType { get; set; }
    }
}
