namespace CEDEARsTracker.Models;

public class BalancesAndPositionsResponse
{
    public List<GroupedAvailability> GroupedAvailability { get; set; } = new();
    public List<GroupedInstrument> GroupedInstruments { get; set; } = new();
}

public class GroupedAvailability
{
    public string Currency { get; set; } = string.Empty;
    public List<Availability> Availability { get; set; } = new();
}

public class Availability
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Settlement { get; set; } = string.Empty;
}

public class GroupedInstrument
{
    public string Name { get; set; } = string.Empty;
    public List<InstrumentBalance> Instruments { get; set; } = new();
    public decimal GroupedValue { get; set; }
}

public class InstrumentBalance
{
    public string Ticker { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
}