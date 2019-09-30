using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    public static class Helper
    {
        public static int Distance(this Point startPoint, Point endPoint)
        {
            int dx = endPoint.X - startPoint.X;
            int dy = endPoint.Y - startPoint.Y;
            return (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
        }
    }
}
