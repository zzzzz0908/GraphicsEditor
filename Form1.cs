using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsEditor
{
    public partial class Form1 : Form
    {
        private FigureType currentFigType;
        private List<Point> points;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawingInProgress = false;

        private List<Rectangle> rectangles;

        public Form1()
        {
            InitializeComponent();

            points = new List<Point>();

            rectangles = new List<Rectangle>();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int buttonNumber = Int32.Parse(button.Name.Substring(6, 1));
            currentFigType = (FigureType)buttonNumber;

            // подписать события

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);

            int height = pictureBox1.Height;
            int width = pictureBox1.Width;

            graphics.FillRectangle(brush, new Rectangle(0, 0, width, height));

            Pen pen = new Pen(Color.Magenta);

            if (rectangles.Count() > 0)
            {
                graphics.DrawRectangles(pen, rectangles.ToArray());
            }

            if (isDrawingInProgress)
            {
                graphics.DrawRectangle(pen, new Rectangle(
                   Math.Min(startPoint.X, endPoint.X),
                   Math.Min(startPoint.Y, endPoint.Y),
                   Math.Abs(startPoint.X - endPoint.X),
                   Math.Abs(startPoint.Y - endPoint.Y)));
            }

            //Rectangle rect = new Rectangle(100, 100, 300, 200);
            //Matrix m = new Matrix();
            //m.RotateAt(45, new PointF(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2)));

            //graphics.Transform = m;
            //graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
            //graphics.ResetTransform();
            //graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            isDrawingInProgress = false;
            rectangles.Add(new Rectangle(
                   Math.Min(startPoint.X, endPoint.X),
                   Math.Min(startPoint.Y, endPoint.Y),
                   Math.Abs(startPoint.X - endPoint.X),
                   Math.Abs(startPoint.Y - endPoint.Y)));
            pictureBox1.Refresh();
        }

        private void clearDrawing(object sender, EventArgs e)
        {
            rectangles.Clear();
            pictureBox1.Refresh();
        }
    }
}
