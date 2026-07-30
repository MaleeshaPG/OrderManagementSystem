namespace OrderManagementSystem.Models
{
    public class StoreItemSupplier
    {
        public int StoreItemSupplierID { get; set; }
        public int StoreItemID { get; set; }
        public int SupplierID { get; set; }
        public int MaxQuantity { get; set; }
        public int MinQuantity { get; set; }
        public int Priority { get; set; }
        public Decimal BuyingPrice { get; set; }
        public StoreItem StoreItem { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;

    }
}
