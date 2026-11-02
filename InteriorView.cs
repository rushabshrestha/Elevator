using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ElevatorApp;

/// <summary>Shows the photo of the elevator interior (Assets\lift.jpg) with animated sliding doors,
/// a floor indicator and a flashing SOS alarm.</summary>
public class InteriorView : Control
{
    // door and indicator positions inside the photo, as fractions of its width/height
    private static readonly RectangleF DoorFrac = new(0.3333f, 0.2375f, 0.3333f, 0.5781f);
    private static readonly RectangleF DisplayFrac = new(0.70f, 0.234f, 0.0667f, 0.081f);

    private static Image? _image;
    private static bool _triedToLoad;

    [DefaultValue("")]
    public string Title { get; set; } = "";

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Floor { get; set; }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Arrow { get; set; }              // 1 = up, -1 = down, 0 = none

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double DoorOpenness { get; set; }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Alarm { get; set; }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Blink { get; set; }

    public InteriorView()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.FromArgb(40, 35, 32);
    }

    private static Image? LoadImage()
    {
        if (_triedToLoad) return _image;
        _triedToLoad = true;

        // 1. the photo embedded in the program (works at run time and in Visual Studio's Design view)
        try
        {
            using var stream = typeof(InteriorView).Assembly.GetManifestResourceStream("lift.jpg");
            if (stream != null)
            {
                var memory = new MemoryStream();
                stream.CopyTo(memory);
                _image = Image.FromStream(memory);      // memory stays alive with the image
                return _image;
            }
        }
        catch { /* try the file next */ }

        // 2. Assets\lift.jpg next to the .exe (relative path), walking up a few folders
        try
        {
            string? folder = Settings.BaseFolder;
            for (int i = 0; i < 6 && folder != null; i++, folder = Path.GetDirectoryName(folder))
            {
                string path = Path.Combine(folder, "Assets", "lift.jpg");
                if (File.Exists(path)) { _image = Image.FromFile(path); break; }
            }
        }
        catch { /* the view falls back to a plain background */ }
        return _image;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.Clear(BackColor);

        var img = LoadImage();
        if (img == null)
        {
            g.DrawString("Assets\\lift.jpg not found", Font, Brushes.White, 10, 40);
            return;
        }

        g.DrawImage(img, new Rectangle(0, 0, Width, Height));

        var door = new RectangleF(DoorFrac.X * Width, DoorFrac.Y * Height, DoorFrac.Width * Width, DoorFrac.Height * Height);
        var srcDoor = new RectangleF(DoorFrac.X * img.Width, DoorFrac.Y * img.Height, DoorFrac.Width * img.Width, DoorFrac.Height * img.Height);

        if (DoorOpenness > 0.001)
        {
            // the lobby of the current floor is visible behind the opening doors
            using (var lobby = new LinearGradientBrush(door, Color.FromArgb(246, 241, 226), Color.FromArgb(200, 192, 172), 90f))
                g.FillRectangle(lobby, door);
            using (var big = new Font("Segoe UI", Height * 0.03f, FontStyle.Bold))
            using (var center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                g.DrawString("Floor " + Floor, big, Brushes.DimGray, door, center);

            float half = door.Width / 2f;
            float srcHalf = srcDoor.Width / 2f;
            float shift = (float)(DoorOpenness * half);

            g.SetClip(door);
            g.DrawImage(img, new RectangleF(door.X - shift, door.Y, half, door.Height),
                new RectangleF(srcDoor.X, srcDoor.Y, srcHalf, srcDoor.Height), GraphicsUnit.Pixel);
            g.DrawImage(img, new RectangleF(door.X + half + shift, door.Y, half, door.Height),
                new RectangleF(srcDoor.X + srcHalf, srcDoor.Y, srcHalf, srcDoor.Height), GraphicsUnit.Pixel);
            g.ResetClip();
        }

        // floor indicator above the button panel
        var display = new RectangleF(DisplayFrac.X * Width, DisplayFrac.Y * Height, DisplayFrac.Width * Width, DisplayFrac.Height * Height);
        g.FillRectangle(Brushes.Black, display);
        string arrow = Arrow > 0 ? "▲" : Arrow < 0 ? "▼" : "";
        using (var f = new Font("Consolas", Math.Max(7f, Height * 0.032f), FontStyle.Bold))
        using (var green = new SolidBrush(Color.FromArgb(110, 255, 130)))
        using (var center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            g.DrawString(arrow + Floor, f, green, display, center);

        // title bar
        using (var bar = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
            g.FillRectangle(bar, 0, 0, Width, 24);
        using (var bold = new Font(Font, FontStyle.Bold))
            g.DrawString(Title, bold, Brushes.White, 6, 4);

        if (Alarm)
        {
            if (Blink)
                using (var red = new SolidBrush(Color.FromArgb(95, 255, 0, 0)))
                    g.FillRectangle(red, ClientRectangle);
            using (var pen = new Pen(Color.Red, 5))
                g.DrawRectangle(pen, 2, 2, Width - 5, Height - 5);
            using (var sos = new Font("Segoe UI", Height * 0.07f, FontStyle.Bold))
            using (var center = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("SOS", sos, Blink ? Brushes.White : Brushes.Red, new RectangleF(0, Height * 0.06f, Width, Height * 0.15f), center);
        }
    }
}
