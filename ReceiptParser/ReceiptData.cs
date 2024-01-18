namespace ReceiptParser
{
    public class ReceiptData
    {
        public string Description { get; set; }
        public BoundingPoly BoundingPoly { get; set; }
    }

    public class BoundingPoly
    {
        public Vertice[] Vertices { get; set; }
    }

    public class Vertice
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
