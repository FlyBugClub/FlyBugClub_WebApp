namespace FlyBugClub_WebApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal lineTotla { get; set; }
        public string ImagePath { get; set; }
        public decimal DepositPrice { get; set; }
    }
}
