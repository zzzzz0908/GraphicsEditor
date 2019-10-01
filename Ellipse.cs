using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class Ellipse : MyRectangle, IFigure
    {
        public Ellipse(Point startPoint, Point endPoint, int angle, LineStyle borderStyle, FillStyle fillStyle) : base(startPoint, endPoint, angle, borderStyle, fillStyle)
        {
        }

        public override void Draw(Graphics graphics)
        {
            Point center = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            Size halfSize = GetHalfSize();

            Rectangle rect = new Rectangle(center - halfSize, halfSize + halfSize);

            Matrix m = new Matrix();
            m.RotateAt(Angle, center);
            graphics.Transform = m;

            Pen pen = new Pen(BorderStyle.Color, BorderStyle.Width);
            pen.DashStyle = BorderStyle.Type;
            //установить паттерн
            if (BorderStyle.Type != DashStyle.Solid)
            {
                pen.DashPattern = DefaultDashPatterns.GetPattern(BorderStyle.Type);
            }

            if (FillStyle.isFilled)
            {
                SolidBrush brush = new SolidBrush(FillStyle.Color);
                graphics.FillEllipse(brush, rect);
                brush.Dispose();
            }

            graphics.DrawEllipse(pen, rect);

            graphics.ResetTransform();
            pen.Dispose();
        }
    }
}
