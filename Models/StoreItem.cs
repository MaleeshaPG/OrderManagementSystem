namespace OrderManagementSystem.Models
{
    public class StoreItem
    {
        public int StoreItemID{ get; set; }
        public int StoreID { get; set; }
        public int ItemID { get; set; }

        public Store Store { get; set; } = null!;
        public Item Item { get; set; } = null!;

        public ICollection<StoreItemSupplier> StoreItemSuppliers { get; set; }= new List<StoreItemSupplier>();

        public ICollection<ForecastData> ForecastData { get; set; } = new List<ForecastData>();
    }
}
