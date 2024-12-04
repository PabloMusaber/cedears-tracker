namespace CEDEARsTracker.Services.Interfaces
{
    public interface ISpreadsheetService
    {
        Task<byte[]> InvestmentsReturnsSpreadsheet();
    }
}