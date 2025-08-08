namespace DAL.Entities
{
    public class Violation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ViolationMessage { get; set; } = string.Empty;
        
        public ViolationType Type { get; set; }

        public string PlateNumber { get; set; } = string.Empty;
        public double Speed { get; set; }
        public DateTime Timestamp { get; set; }
        public int Lane { get; set; }
        public string Direction { get; set; } = string.Empty;
        public double LocationX { get; set; }
        public double LocationY { get; set; }
    }
}
