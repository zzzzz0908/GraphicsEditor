using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    class Circle : IFigure
    {
        public int Radius { get; }
        public LineStyle BorderStyle { get; set; }
        public FillStyle FillStyle { get; set; }
        public void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
