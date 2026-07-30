namespace OrderManagementSystem.Models
{
    public class ForecastData
    {
        public int ForecastDataID { get; set; }
        public int StoreItemID { get; set; }
        public DateTime ForecastDate { get; set; }
        public Decimal ForecastQuantity { get; set; }
        public Decimal BufferQuantity { get; set; }
        public Decimal PromotionQuantity { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public StoreItem StoreItem { get; set; } = null!;
    }
}
