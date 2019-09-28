using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class Circle : IFigure
    {
        public Point Center { get; }
        public int Radius { get; }
        public LineStyle BorderStyle { get; set; }
        public FillStyle FillStyle { get; set; }

        public Circle(Point center, int radius, LineStyle borderStyle, FillStyle fillStyle)
        {
            Radius = radius;
            BorderStyle = borderStyle;
            FillStyle = fillStyle;
            Center = center;
        }


        public void Draw(Graphics graphics)
        {
            Pen pen = new Pen(BorderStyle.Color, BorderStyle.Width);
            pen.DashStyle = BorderStyle.Type;
            //установить паттерн
            if (BorderStyle.Type != DashStyle.Solid)
            {
                pen.DashPattern = DefaultDashPatterns.GetPattern(BorderStyle.Type);
            }
            
            //Point start = new Point(Center.X - Radius, Center.Y - Radius);
            //Point end = new Point(Center.X + Radius, Center.Y + Radius);

            Rectangle rect = new Rectangle(Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);

            if (FillStyle.isFilled)
            {
                SolidBrush brush = new SolidBrush(FillStyle.Color);
                graphics.FillEllipse(brush, rect);
                brush.Dispose();
            }

            graphics.DrawEllipse(pen, rect);

            pen.Dispose();
        }
    }
}
