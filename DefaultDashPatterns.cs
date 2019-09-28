using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor
{
    public static class DefaultDashPatterns
    {
        private static float[] Dash = { 10, 5 };
        private static float[] Dot = { 1, 2 };
        private static float[] DashDot = { 10, 5, 1, 5 };

        public static float[] GetPattern(DashStyle dashStyle)
        {
            switch (dashStyle)
            {                
                case DashStyle.Dash:
                    return Dash;
                case DashStyle.Dot:
                    return Dot;
                case DashStyle.DashDot:
                    return DashDot;
                default:
                    return Dash;
            }

        }
    }
}