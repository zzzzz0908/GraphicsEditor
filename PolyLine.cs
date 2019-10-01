using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class PolyLine : IFigure
    {
        public PolyLine(LineStyle lineStyle, Point[] points)
        {
            LineStyle = lineStyle;
            Points = points;
        }

        public LineStyle LineStyle { get; set; } 
        public Point[] Points { get; }

        public void Draw(Graphics graphics)
        {
            Pen pen = new Pen(LineStyle.Color, LineStyle.Width);
            pen.DashStyle = LineStyle.Type;
            //установить паттерн
            if (LineStyle.Type != DashStyle.Solid)
            {
                pen.DashPattern = DefaultDashPatterns.GetPattern(LineStyle.Type);
            }

            graphics.DrawLines(pen, Points);

        }
    }
}
