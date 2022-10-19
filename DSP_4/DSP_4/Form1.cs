using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSP_4
{
    public partial class Form1 : Form
    {
        private Signal hs = new Signal(10, 0, 512);
        private double[] fs;
        private int Fr;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Chart.Series[0].Points.Clear();
                Chart.Series[1].Points.Clear();
                Chart.Series.Clear();
            }
            catch { }

            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "ChartArea",
            };

            var seriesRest = new Series
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "ChartArea",
            };

          //  Signal hs = new Signal(10, 0, 512);
            fs = null;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Fr = 0;
                    fs = hs.parabolicS;
                    break;
                case 1:
                    Fr = 1;
                    fs = hs.medianS;
                    break;
                case 2:
                    Fr = 2;
                    fs = hs.slidingS;
                    break;
                default: return;
            }

            for (int i = 0; i <= 359; i++)
            {
                series.Points.AddXY(2 * Math.PI * i / 360, hs.signVal[i]);
                seriesRest.Points.AddXY(2 * Math.PI * i / 360, fs[i]);
            }

            Chart.Series.Add(series);
            Chart.Series.Add(seriesRest);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Chart.Series[0].Points.Clear();
                Chart.Series[1].Points.Clear();
                Chart.Series.Clear();
            }
            catch { }

            var series = new Series
            {
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea",
            };

            var seriesRest = new Series
            {
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea",
            };


            hs.Operate(Fr);

            for (int i = 0; i <= 49; i++)
            {
                series.Points.AddXY(i, hs.faseSp[i]);
                seriesRest.Points.AddXY(i, hs.fsp[i]);
            }

            Chart.Series.Add(series);
            Chart.Series.Add(seriesRest);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Chart.Series[0].Points.Clear();
                Chart.Series[1].Points.Clear();
                Chart.Series.Clear();
            }
            catch { }

            var series = new Series
            {
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea",
            };

            var seriesRest = new Series
            {
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea",
            };


            hs.Operate(Fr);

            for (int i = 0; i <= 49; i++)
            {
                series.Points.AddXY(i, hs.amplSp[i]);
                seriesRest.Points.AddXY(i, hs.asp[i]);
            }

            Chart.Series.Add(series);
            Chart.Series.Add(seriesRest);

        }
    }
}
