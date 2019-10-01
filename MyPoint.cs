using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    public class MyPoint : IFigure
    {
        public MyPoint(Point location, Color color)
        {
            Location = location;
            Color = color;
        }

        public Point Location { get; }
        public Color Color { get; }
        
        public void Draw(Graphics graphics)
        {
            int size = 5;
            Pen pen = new Pen(Color);
            graphics.DrawEllipse(pen, Location.X - size, Location.Y - size, size * 2, size * 2);
            graphics.DrawLine(pen, Location.X - size - 2, Location.Y, Location.X + size + 2, Location.Y);
            graphics.DrawLine(pen, Location.X, Location.Y - size - 2, Location.X, Location.Y + size + 2);

            pen.Dispose();
        }
    }
}
