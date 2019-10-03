using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Math;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace GraphicsEditor
{
    class MyRectangle : IFigure
    {
        public MyRectangle(Point startPoint, Point endPoint, int angle, LineStyle borderStyle, FillStyle fillStyle)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Angle = angle;
            BorderStyle = borderStyle;
            FillStyle = fillStyle;
        }

        public Point StartPoint { get; }
        public Point EndPoint { get; }
        public int Angle { get; }

        public LineStyle BorderStyle { get; set; }
        public FillStyle FillStyle { get; set; }

        public virtual void Draw(Graphics graphics)
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
                graphics.FillRectangle(brush, rect);
                brush.Dispose();
            }

            graphics.DrawRectangle(pen, rect);

            graphics.ResetTransform();
            pen.Dispose();

        }

        public Size GetHalfSize()
        {
            Point center = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            double angle = Angle / 180.0 * Math.PI;            

            int dx = EndPoint.X - center.X;
            int dy = EndPoint.Y - center.Y;

            int halfWidth = (int)Abs(Math.Round(dy * Sin(angle) + dx * Cos(angle)));
            int halfHeight = (int)Abs(Math.Round(dy * Cos(angle) - dx * Sin(angle)));

            return new Size(halfWidth, halfHeight);
        }
    }
}
