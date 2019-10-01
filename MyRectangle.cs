using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class MyRectangle : IFigure
    {
        public MyRectangle(Point startPoint, Point endPoint, double angle, LineStyle borderStyle, FillStyle fillStyle)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Angle = angle;
            BorderStyle = borderStyle;
            FillStyle = fillStyle;
        }

        public Point StartPoint { get; }
        public Point EndPoint { get; }
        public double Angle { get; }

        public LineStyle BorderStyle { get; set; }
        public FillStyle FillStyle { get; set; }

        public virtual void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
