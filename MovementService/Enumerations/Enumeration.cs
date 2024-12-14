namespace MovementService.Enumerations
{
    public class Enumerations
    {
        public enum InstrumentType
        {
            CEDEARS = 1,
            LETRAS = 2
        }

        public enum MovementType
        {
            Buy = 'B',
            Sell = 'S'
        }

        public enum EventType
        {
            InstrumentPublished,
            Undetermined
        }
    }
}