using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ElevatorApp;

/// <summary>Draws one elevator shaft: floors, floor doors and the car with its sliding doors.</summary>
public class ShaftView : Control
{
    public const int Floor1Y = 120;
    public const int Floor0Y = 310;
    private const int CarHeight = 85;

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Position { get; set; }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double DoorOpenness { get; set; }

    [DefaultValue("")]
    public string Title { get; set; } = "";

    public static int FloorY(int floor) => floor == 0 ? Floor0Y : Floor1Y;

    public ShaftView()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.FromArgb(235, 235, 245);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(BackColor);

        int shaftLeft = 15, shaftRight = Width - 15;
        int carWidth = shaftRight - shaftLeft - 30;
        int carX = shaftLeft + 15;

        // shaft
        using (var shaftPen = new Pen(Color.DimGray, 2))
            g.DrawRectangle(shaftPen, shaftLeft, 30, shaftRight - shaftLeft, Floor0Y - 30 + 10);

        // floors and floor doors
        for (int floor = 0; floor <= Elevator.TopFloor; floor++)
        {
            int y = FloorY(floor);
            using var floorPen = new Pen(Color.Black, 3);
            g.DrawLine(floorPen, 0, y, Width, y);

            double distance = Math.Abs(Position - floor);
            bool carHere = distance < 0.02;
            double open = carHere ? DoorOpenness : 0;
            DrawDoors(g, carX, y - CarHeight, carWidth, CarHeight, open, Color.Gray, Color.Gainsboro);
            g.DrawString("Floor " + floor, Font, Brushes.Black, 4, y + 3);
        }

        // car
        int carBottom = (int)Math.Round(Floor0Y - Position * (Floor0Y - Floor1Y));
        int carTop = carBottom - CarHeight;
        using (var body = new SolidBrush(Color.FromArgb(75, 0, 70)))
            g.FillRectangle(body, carX - 4, carTop - 4, carWidth + 8, CarHeight + 4);
        DrawDoors(g, carX, carTop, carWidth, CarHeight, DoorOpenness, Color.FromArgb(120, 60, 120), Color.FromArgb(250, 245, 220));

        using var bold = new Font(Font, FontStyle.Bold);
        g.DrawString(Title, bold, Brushes.Black, 4, 4);
    }

    private static void DrawDoors(Graphics g, int x, int y, int width, int height, double openness, Color doorColor, Color insideColor)
    {
        using var inside = new SolidBrush(insideColor);
        using var door = new SolidBrush(doorColor);
        using var edge = new Pen(Color.Black, 1);

        g.FillRectangle(inside, x, y, width, height);
        int half = (int)Math.Round(width / 2.0 * (1 - Math.Clamp(openness, 0, 1)));
        g.FillRectangle(door, x, y, half, height);
        g.FillRectangle(door, x + width - half, y, half, height);
        g.DrawRectangle(edge, x, y, width, height);
    }
}
