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
        private int angle = 0;
        private DashStyle dashStyle = DashStyle.Solid;
        private Color lineColor = Color.Black;

        private bool isFilled = true;
        private Color fillColor = Color.Transparent;

        private LineStyle LineStyle => new LineStyle(width, dashStyle, lineColor);

        private FillStyle FillStyle => new FillStyle(isFilled, fillColor);
                             
        private Point startPoint;
        //private Point endPoint;

        private List<IFigure> figures;
        private List<Point> points;
        private IFigure tempFigure;

        private MouseEventHandler[] clickEventHandlers;
        private MouseEventHandler[] upEventHandlers;
        private MouseEventHandler[] downEventHandlers;
        private MouseEventHandler[] moveEventHandlers;

        
        
        
       
        public Form1()
        {
            InitializeComponent();

            fillCheckBox.Checked = isFilled;
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


            clickEventHandlers = new MouseEventHandler[]
            {
                new MouseEventHandler(Point_MouseClick),
                new MouseEventHandler(PolyLine_MouseClick),
                null,
                null,
                null,
                null
            };

            moveEventHandlers = new MouseEventHandler[]
            {
                null,
                new MouseEventHandler(PolyLine_MouseMove),
                new MouseEventHandler(Rectangle_MouseMove),
                new MouseEventHandler(Square_MouseMove),
                new MouseEventHandler(Circle_MouseMove),
                new MouseEventHandler(Ellipse_MouseMove)
            };

            downEventHandlers = new MouseEventHandler[]
            {
                null,
                null,
                new MouseEventHandler(Rectangle_MouseDown),
                new MouseEventHandler(Square_MouseDown),
                new MouseEventHandler(Circle_MouseDown),
                new MouseEventHandler(Ellipse_MouseDown)
            };

            upEventHandlers = new MouseEventHandler[]
            {
                null,
                null,
                new MouseEventHandler(Rectangle_MouseUp),
                new MouseEventHandler(Square_MouseUp),
                new MouseEventHandler(Circle_MouseUp),
                new MouseEventHandler(Ellipse_MouseUp)
            };


            // debug
            points = new List<Point>();                       
        }


        private void UnsubscribeMouseEvents()
        {
            foreach (var handler in clickEventHandlers)
            {
                pictureBox1.MouseClick -= handler;
            }

            foreach (var handler in moveEventHandlers)
            {
                pictureBox1.MouseMove -= handler;
            }

            foreach (var handler in downEventHandlers)
            {
                pictureBox1.MouseDown -= handler;
            }

            foreach (var handler in upEventHandlers)
            {
                pictureBox1.MouseUp -= handler;
            }
        }

        private void clearDrawing(object sender, EventArgs e)
        {
            figures.Clear();
            pictureBox1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int figureIdx = Int32.Parse(button.Name.Substring(6, 1));
            currentFigType = (FigureType)figureIdx;

            UnsubscribeMouseEvents();

            pictureBox1.MouseClick += clickEventHandlers[figureIdx];
            pictureBox1.MouseMove += moveEventHandlers[figureIdx];
            pictureBox1.MouseDown += downEventHandlers[figureIdx];
            pictureBox1.MouseUp += upEventHandlers[figureIdx];
        }




        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);

            if (figures.Count() > 0)
            {
                foreach (IFigure figure in figures)
                {
                    figure.Draw(graphics);
                }
            }

            if (isDrawingInProgress)
            {
                if (tempFigure != null) tempFigure.Draw(graphics);
            }


            // debug
            //SolidBrush brush = new SolidBrush(Color.White); 
            //Pen pen = new Pen(lineColor);            
            //pen.Width = (float)comboBoxLineWidth.SelectedItem;

            //var a = new MyRectangle(new Point(300, 300), new Point(200, 500), 30, LineStyle, FillStyle);
            //a.Draw(graphics);
            
            //pen.Dispose();
            //brush.Dispose();            
        }

        



        #region ControlsEvents
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


        #endregion



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
                isDrawingInProgress = false;
            }
            else 
            {
                isDrawingInProgress = true;
                points.Add(e.Location);
            }
            
            pictureBox1.Refresh();
        }

        private void PolyLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingInProgress)
            {
                List<Point> tempPoints = new List<Point>(points);
                tempPoints.Add(e.Location);
                tempFigure = new PolyLine(LineStyle, tempPoints.ToArray());
                pictureBox1.Refresh();
            }            
        }
        #endregion


        #region RectangleMouseEvents
        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            tempFigure = new MyRectangle(startPoint, e.Location, angle, LineStyle, FillStyle);
            pictureBox1.Refresh();
        }

        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {
            figures.Add(new MyRectangle(startPoint, e.Location, angle, LineStyle, FillStyle));
            tempFigure = null;
            isDrawingInProgress = false;
        }
        #endregion


        #region SquareMouseEvents
        private void Square_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void Square_MouseMove(object sender, MouseEventArgs e)
        {
            //endPoint = e.Location;
            pictureBox1.Refresh();
        }

        private void Square_MouseUp(object sender, MouseEventArgs e)
        {
            //endPoint = e.Location;
            //figures.Add(new Circle(startPoint, startPoint.Distance(endPoint), LineStyle, FillStyle));
            isDrawingInProgress = false;
        }
        #endregion


        #region CircleMouseEvents
        private void Circle_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void Circle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingInProgress)
            {                
                tempFigure = new Circle(startPoint, startPoint.Distance(e.Location), LineStyle, FillStyle);
                pictureBox1.Refresh();
            }            
        }

        private void Circle_MouseUp(object sender, MouseEventArgs e)
        {
            figures.Add(new Circle(startPoint, startPoint.Distance(e.Location), LineStyle, FillStyle));
            isDrawingInProgress = false;            
        }
        #endregion

        
        #region EllipseMouseEvents
        private void Ellipse_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawingInProgress = true;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            tempFigure = new Ellipse(startPoint, e.Location, angle, LineStyle, FillStyle);
            pictureBox1.Refresh();
        }

        private void Ellipse_MouseUp(object sender, MouseEventArgs e)
        {
            figures.Add(new Ellipse(startPoint, e.Location, angle, LineStyle, FillStyle));
            isDrawingInProgress = false;
        }
        #endregion

        
    }
}
