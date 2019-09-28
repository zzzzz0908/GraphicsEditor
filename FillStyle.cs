using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    public struct FillStyle
    {
        public FillStyle(bool isFilled, Color color)
        {
            this.isFilled = isFilled;
            Color = color;
        }

        public bool isFilled { get; set; }
        public Color Color { get; set; }
    }
}
