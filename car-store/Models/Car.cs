namespace CarStore.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageName {  get; set; } = string.Empty;

        // Foreign key
        public required CarType CarType { get; set; }
    }
}
