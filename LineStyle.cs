using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    public struct LineStyle
    {
        public LineStyle(int width, DashStyle type, Color color)
        {
            Width = width;
            Type = type;
            Color = color;
        }

        public int Width { get; set; }
        public DashStyle Type { get; set; }
        public Color Color { get; set; }
    }
}
