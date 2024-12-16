namespace MarketService.Dtos
{
    public class CurrentPriceResponseDto
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal MarketChange { get; set; }
        public string MarketChangePercent { get; set; } = string.Empty;
    }
}