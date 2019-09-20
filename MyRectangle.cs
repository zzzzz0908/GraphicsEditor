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
        public LineStyle BorderStyle { get; set; }
        public FillStyle FillStyle { get; set; }

        public Point[] Corners { get; }
        public double Angle { get; }

        public virtual void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
