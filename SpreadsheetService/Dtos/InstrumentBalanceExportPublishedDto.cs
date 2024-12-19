namespace SpreadsheetService.Dtos
{
    public class InstrumentBalanceExportPublishedDto
    {
        public IEnumerable<InstrumentBalanceExportDto>? InstrumentsBalanceExportDto { get; set; }
        public string Event { get; set; } = string.Empty;
    }
}