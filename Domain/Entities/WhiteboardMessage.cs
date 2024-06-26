namespace Domain.Entities;

public struct Point2D
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class WhiteboardMessage
{
    public required string Type { get; set; }
    public required string Color { get; set; }
    public List<Point2D>? Points { get; set; }
    public Point2D StartPos { get; set; }
    public Point2D EndPos { get; set; }
}