namespace MovementService.Dtos
{
    public class MovementReadDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid InstrumentId { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public char MovementType { get; set; }
    }
}