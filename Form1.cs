using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        }
    }
}
