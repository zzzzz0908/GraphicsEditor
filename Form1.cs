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
        private bool isDrawingInProgress = false;

        private int width = 1;
        private DashStyle dashStyle = DashStyle.Solid;
        private Color lineColor = Color.Black;

        private bool isFilled = true;
        private Color fillColor = Color.Transparent;

        private LineStyle LineStyle => new LineStyle(width, dashStyle, lineColor);

        private FillStyle FillStyle => new FillStyle(isFilled, fillColor);




        private List<Point> points;
        private Point startPoint;
        private Point endPoint;
        // debug
        private List<Rectangle> rectangles;
        private List<IFigure> figures;
       
        public Form1()
        {
            InitializeComponent();
            
            // debug
            points = new List<Point>();
            rectangles = new List<Rectangle>();
            figures = new List<IFigure>();


            Dictionary<string, DashStyle> comboSource1 = new Dictionary<string, DashStyle>();
            comboSource1.Add("Сплошная", DashStyle.Solid);
            comboSource1.Add("Штриховая", DashStyle.Dash);
            comboSource1.Add("Пунктирная", DashStyle.Dot);
            comboSource1.Add("Штрихпунктир", DashStyle.DashDot);

            comboBoxDashStyle.DataSource = new BindingSource(comboSource1, null);
            comboBoxDashStyle.DisplayMember = "Value";
            comboBoxDashStyle.ValueMember = "Key";

            float[] widthArray = { 1, 2, 3, 4, 5, 7, 8, 9, 10, 12, 15, 20 };
            comboBoxLineWidth.DataSource = new BindingSource(widthArray, null);

        }

        private void UnsubscribeMouseEvents()
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int buttonNumber = Int32.Parse(button.Name.Substring(6, 1));
            currentFigType = (FigureType)buttonNumber;

            // подписать события  
            // создать массив handlers с индексами от фигур
            // подпиcывать отписывать по индексам currentFigure
            //this.pictureBox1.MouseDown += new MouseEventHandler(this.pictureBox1_MouseDown);
            //this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            //this.pictureBox1.MouseUp += new MouseEventHandler(this.pictureBox1_MouseUp);

            //this.pictureBox1.MouseDown += new MouseEventHandler(this.Circle_MouseDown);
            //this.pictureBox1.MouseMove += new MouseEventHandler(this.Circle_MouseMove);
            //this.pictureBox1.MouseUp += new MouseEventHandler(this.Circle_MouseUp);

            //this.pictureBox1.MouseClick += new MouseEventHandler(Point_MouseClick);
            this.pictureBox1.MouseClick += new MouseEventHandler(PolyLine_MouseClick);

            //Delegate del = (Delegate)this.pictureBox1.MouseUp;
        }




        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush brush = new SolidBrush(Color.White);

            int height = pictureBox1.Height;
            int width = pictureBox1.Width;

            //graphics.FillRectangle(brush, new Rectangle(0, 0, width, height));
            graphics.Clear(Color.White);

            Pen pen = new Pen(lineColor);
            
            pen.Width = (float)comboBoxLineWidth.SelectedItem;
            //if (rectangles.Count() > 0)
            //{
            //    graphics.DrawRectangles(pen, rectangles.ToArray());
            //}

            if (figures.Count() > 0)
            {
                foreach(IFigure figure in figures)
                {
                    figure.Draw(graphics);
                }
            }


            if (isDrawingInProgress)
            {
                brush.Color = Color.Transparent;
                var circle = new Circle(startPoint, startPoint.Distance(endPoint), LineStyle, FillStyle);
                circle.Draw(graphics);
                //graphics.FillRectangle(brush, new Rectangle(
                //   Math.Min(startPoint.X, endPoint.X),
                //   Math.Min(startPoint.Y, endPoint.Y),
                //   Math.Abs(startPoint.X - endPoint.X),
                //   Math.Abs(startPoint.Y - endPoint.Y)));

                //graphics.DrawRectangle(pen, new Rectangle(
                //   Math.Min(startPoint.X, endPoint.X),
                //   Math.Min(startPoint.Y, endPoint.Y),
                //   Math.Abs(startPoint.X - endPoint.X),
                //   Math.Abs(startPoint.Y - endPoint.Y)));


            }

            //Rectangle rect = new Rectangle(100, 100, 300, 200);
            //Matrix m = new Matrix();
            //m.RotateAt(45, new PointF(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2)));

            //graphics.Transform = m;
            //graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
            //graphics.ResetTransform();
            //graphics.DrawRectangle(pen, new Rectangle(100, 100, 300, 200));
            pen.Dispose();
            brush.Dispose();
            
        }

        private void clearDrawing(object sender, EventArgs e)
        {
            figures.Clear();
            pictureBox1.Refresh();
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

        

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lineColor = colorDialog1.Color;
            }
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.fillColor = colorDialog1.Color;
            }
        }


        #region PointMouseEvents
        private void Point_MouseClick(object sender, MouseEventArgs e)
        {
            figures.Add(new MyPoint(e.Location, lineColor));
            pictureBox1.Refresh();
        }
        #endregion


        #region PolyLineMouseEvents
        private void PolyLine_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                figures.Add(new PolyLine(LineStyle, points.ToArray()));
                points.Clear();
            }
            else 
            {
                points.Add(e.Location);
            }
            
            pictureBox1.Refresh();
        }
        #endregion


        #region RectangleMouseEvents

        #endregion


        #region SquareMouseEvents

        #endregion


        #region CircleMouseEvents
        private void Circle_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void Circle_MouseMove(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            pictureBox1.Refresh();
        }

        private void Circle_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            figures.Add(new Circle(startPoint, startPoint.Distance(endPoint), LineStyle, FillStyle));
            isDrawingInProgress = false;            
        }
        #endregion



        #region EllipseMouseEvents

        #endregion



    }
}
