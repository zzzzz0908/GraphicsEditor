using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

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

        public static Point GetSquareOppositeCorner(Point startPoint, Point endPoint,int angle)
        {
            double angleRad = angle / 180.0 * Math.PI;
            int dx = endPoint.X - startPoint.X;
            int dy = endPoint.Y - startPoint.Y;

            double Width = dy * Sin(angleRad) + dx * Cos(angleRad);
            double Height = dy * Cos(angleRad) - dx * Sin(angleRad);

            double squareSize = Min(Abs(Width), Abs(Height));

            // угол от начальной точки к конечной в повернутой системе координат
            int angle1 = (int)Round((Atan2(Height, Width) / PI * 180 + 360) % 360);

            int angle2 = (angle1 / 90) * 90 + 45 + angle;

            Point targetPoint = new Point();
            targetPoint.X = (int)(startPoint.X + squareSize * Sqrt(2) * Cos(angle2 / 180.0 * PI));
            targetPoint.Y = (int)(startPoint.Y + squareSize * Sqrt(2) * Sin(angle2 / 180.0 * PI));

            return targetPoint;
        }
    }
}
