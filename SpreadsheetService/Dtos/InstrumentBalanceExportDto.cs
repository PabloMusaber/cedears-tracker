namespace SpreadsheetService.Dtos
{
    public class InstrumentBalanceExportDto
    {
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Holdings { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal InvestedAmount { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}