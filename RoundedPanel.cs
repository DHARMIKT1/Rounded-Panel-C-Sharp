using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RoundedPanel
{
    // This class defines a custom UserControl with rounded corners
    public partial class RoundedPane : UserControl
    {
        public RoundedPanel()
        {
            InitializeComponent();
        }

        private int _radius = 10; // Private field to store the radius of the corners

        // Public property to get or set the radius of the corners
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                Invalidate(); // Redraw the control when the radius changes
            }
        }

        // Private field to store the background color
        private Color _backgroundColor = SystemColors.Control;

        // Public property to get or set the background color
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                Invalidate(); // Redraw the control when the background color changes
            }
        }

        // Private field to store the border color
        private Color _borderColor = SystemColors.Control;

        // Public property to get or set the border color
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate(); // Redraw the control when the border color changes
            }
        }

        // Private field to store the border width
        private float _borderWidth = 1.0f;

        // Public property to get or set the border width
        public float BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                Invalidate(); // Redraw the control when the border width changes
            }
        }

        // Override the OnPaint method to customize the painting behavior
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Enable anti-aliasing for smoother graphics

            // Create brushes and pens with the specified colors and widths
            using (SolidBrush backgroundBrush = new SolidBrush(_backgroundColor))
            using (Pen borderPen = new Pen(_borderColor, _borderWidth))
            {
                // Fill and draw the rounded rectangle
                g.FillRoundedRectangle(backgroundBrush, 0, 0, Width - 1, Height - 1, _radius);
                g.DrawRoundedRectangle(borderPen, 0, 0, Width - 1, Height - 1, _radius);
            }
        }
    }

    // Extension methods for the Graphics class to support drawing and filling rounded rectangles
    public static class GraphicsExtensions
    {
        // Method to get the path of a rounded rectangle
        public static GraphicsPath GetRoundedRectanglePath(RectangleF rect, float radius)
        {
            float diameter = radius * 2;
            SizeF size = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(rect.Location, size);
            GraphicsPath path = new GraphicsPath();

            // Add arcs for the corners
            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure(); // Close the path
            return path;
        }

        // Method to draw a rounded rectangle
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, float x, float y, float width, float height, float radius)
        {
            RectangleF rect = new RectangleF(x, y, width, height);
            using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
            {
                graphics.DrawPath(pen, path); // Draw the path
            }
        }

        // Method to fill a rounded rectangle
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, float x, float y, float width, float height, float radius)
        {
            RectangleF rect = new RectangleF(x, y, width, height);
            using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
            {
                graphics.FillPath(brush, path); // Fill the path
            }
        }
    }
}
