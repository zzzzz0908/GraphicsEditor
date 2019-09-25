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


        public Form1()
        {
            InitializeComponent();

            points = new List<Point>();
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
            Rectangle rect = new Rectangle(100, 100, 300, 200);
            Matrix m = new Matrix();
            m.RotateAt(45, new PointF(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2)));

            graphics.Transform = m;
            graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
            graphics.ResetTransform();
            graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
        }
    }
}
