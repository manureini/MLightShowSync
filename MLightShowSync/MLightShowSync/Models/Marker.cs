namespace MLightShowSync.Models
{
    public class Marker
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public double PositionPercent { get; set; }
    }
}
