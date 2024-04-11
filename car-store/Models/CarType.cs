namespace CarStore.Models
{
    public class CarType
    {
        public int CarTypeID { get; set; }
        public string TypeName { get; set; } = string.Empty;

        // One to many relationship
        List<Car>? Cars { get; set; }
    }
}
