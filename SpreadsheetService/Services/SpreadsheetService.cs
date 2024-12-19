using SpreadsheetService.Dtos;
using SpreadsheetService.Services.Interfaces;
using ClosedXML.Excel;

namespace SpreadsheetService.Services
{
    public class SpreadsheetService : ISpreadsheetService
    {
        public SpreadsheetService()
        {

        }

        public byte[] InvestmentsReturnsSpreadsheet(IEnumerable<InstrumentBalanceExportDto> instrumentsBalanceExportDto)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Investments_Returns");

                worksheet.Cell(1, 1).Value = "Ticker";
                worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 3).Value = "Holdings";
                worksheet.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 4).Value = "AveragePurchasePrice";
                worksheet.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 5).Value = "Invested";
                worksheet.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 6).Value = "Current Amount";
                worksheet.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 7).Value = "Profit";
                worksheet.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;

                var row = 2;
                decimal totalInvested = 0;
                decimal totalCurrentAmount = 0;
                foreach (var instrument in instrumentsBalanceExportDto)
                {
                    var currentAmount = instrument.Holdings * instrument.CurrentPrice;
                    var profit = (instrument.CurrentPrice - instrument.AveragePurchasePrice) / instrument.AveragePurchasePrice;

                    worksheet.Cell(row, 1).Value = instrument.Ticker;
                    worksheet.Cell(row, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 2).Value = instrument.Description;
                    worksheet.Cell(row, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 3).Value = instrument.Holdings;
                    worksheet.Cell(row, 4).Value = instrument.AveragePurchasePrice;
                    worksheet.Cell(row, 5).Value = instrument.InvestedAmount;
                    worksheet.Cell(row, 6).Value = currentAmount;
                    worksheet.Cell(row, 7).Value = profit;

                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(row, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(row, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 6).Style.NumberFormat.Format = "$#,##0.00";
                    worksheet.Cell(row, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(row, 7).Style.NumberFormat.Format = "0,00%";
                    worksheet.Cell(row, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    var profitCell = worksheet.Cell(row, 7);
                    profitCell.Style.NumberFormat.Format = "0%";
                    if (profit >= 0)
                    {
                        profitCell.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else
                    {
                        profitCell.Style.Fill.BackgroundColor = XLColor.Coral;
                    }

                    row++;
                    totalInvested += instrument.InvestedAmount;
                    totalCurrentAmount += currentAmount;
                }

                worksheet.Cell(row + 3, 1).Value = "Total Invested";
                worksheet.Cell(row + 3, 1).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(row + 3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(row + 3, 2).Value = totalInvested;
                worksheet.Cell(row + 3, 2).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Cell(row + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(row + 4, 1).Value = "Current Amount";
                worksheet.Cell(row + 4, 1).Style.Fill.BackgroundColor = XLColor.Gold;
                worksheet.Cell(row + 4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(row + 4, 2).Value = totalCurrentAmount;
                worksheet.Cell(row + 4, 2).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Cell(row + 4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                if (totalCurrentAmount > totalInvested)
                {
                    worksheet.Cell(row + 4, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else
                {
                    worksheet.Cell(row + 4, 2).Style.Fill.BackgroundColor = XLColor.Coral;
                }

                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}