using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class Ellipse : MyRectangle, IFigure
    {
        public Ellipse(Point startPoint, Point endPoint, double angle, LineStyle borderStyle, FillStyle fillStyle) : base(startPoint, endPoint, angle, borderStyle, fillStyle)
        {
        }

        public override void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
