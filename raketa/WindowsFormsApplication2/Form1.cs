
using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
            double dt = 0.01;
            double M = 6 * Math.Pow(10, 24);
            double x = 0;
            double y= 6.4 * Math.Pow(10, 6);
        double graviconst = 6.672 * Math.Pow(10,-11);
        
       

        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();

            chart1.Series.Add("Ракета со скоростью 7500 м/c");

            chart1.Series.Add("Ракета со скоростью 7923 м/c");

            chart1.Series.Add("Ракета2 со скоростью 10000 м/c");
            chart1.Series.Add("Ракета 3со скоростью 11206 м/c");
            Axis ax = new Axis();
            ax.Title = "X";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Y";
            chart1.ChartAreas[0].AxisY = ay;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            
            chart1.Series[0].BorderWidth = 4;

            chart1.Series[1].ChartType = SeriesChartType.Line;

            chart1.Series[1].BorderWidth = 3;

            chart1.Series[2].ChartType = SeriesChartType.Line;

            chart1.Series[2].BorderWidth = 2;
            chart1.Series[2].ChartType = SeriesChartType.Line;

            chart1.Series[3].BorderWidth = 1;
            chart1.Series[3].ChartType = SeriesChartType.Line;

            Rocket v1 = new Rocket();
            v1.velocity = 7500;
            Rocket v2 = new Rocket();
            v2.velocity = 7923;
            Rocket v3 = new Rocket();
            v3.velocity = 10000;
            Rocket v4 = new Rocket();
            v4.velocity = 11206;
            GetData(v1.velocity);
            GetData(v2.velocity);
            GetData(v3.velocity);
            GetData(v4.velocity);
            

        }

        public void GetData(double velocity)
        {
            string FileName = "";
            if (velocity == 7500) { FileName = "Rocket1.txt"; }
            else if (velocity == 7923) { FileName = "Rocket2.txt"; }
            else if (velocity == 10000) { FileName = "Rocket3.txt"; }
            else if (velocity == 11206) { FileName = "Rocket4.txt"; }
            StreamWriter writedata = new StreamWriter(FileName);
            double coeff = graviconst * dt * M;
            double xx = x;
            double yy = y;
            int datax = 0;
            string data="";
            double n = 100;
            double xt;
            double yt;
            double vxt=velocity;
            double vyt=velocity/1000;
            double r;
            double[,] datamass=new double[Convert.ToInt32(n/dt+1),2];
            datamass[0, 0] = x;
            datamass[0, 1] = y;
            for (double t=0;t<=n;t+=dt)
            {
                datax++;
                xt = xx + vxt * dt;
                yt = yy + vyt * dt;
                //try { 
                datamass[datax, 0] = yt;
                datamass[datax, 1] = xt;
                //}
                //catch { DrawGraph(datamass, n, velocity); }
                data = data + "\n" + Convert.ToString(xt) + "   " + Convert.ToString(yt);

                r = Math.Sqrt(xt * xt + yt * yt);
                vxt = vxt - (coeff*xt/(r*r*r));
                vyt = vyt + (coeff* yt/ (r * r * r));
                xx = xt;
                yy = yt;
            }
            writedata.Write(data);
            writedata.Close();
            DrawGraph(datamass,n,velocity);
        }
        public void DrawGraph(double[,] mass, double n, double velocity)
        {

            int z = 0;
            if (velocity == 7500) { z = 0; }
            else if (velocity == 7923) { z = 1; }
            else if (velocity == 10000) { z = 2; }
            else if (velocity == 11206) { z = 3; }
            for (int i = 1; i < mass.GetLength(0); i++)

            {
                chart1.Series[z].Points.AddXY(mass[i,0], mass[i, 1]);
            }
            
        }
    }
    class Rocket
    {
        private double _velocity;
        public double velocity
        { 
            get { return _velocity; }
            set { _velocity = value; }
        }
    }
        
}

