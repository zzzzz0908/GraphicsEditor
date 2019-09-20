using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class PolyLine : IFigure
    {
        public LineStyle LineStyle { get; set; } 
        public Point[] Points { get; }

        public void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
