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

namespace DSP1
{
    public partial class Form1 : Form
    {
        private int graphCount = 5;
        private readonly List<HarmonicalSignal> signals = new List<HarmonicalSignal>()
        {
            new HarmonicalSignal(0, 0, 0, ""),
            new HarmonicalSignal(0, 0, 0, ""),
            new HarmonicalSignal(0, 0, 0, ""),
            new HarmonicalSignal(0, 0, 0, ""),
            new HarmonicalSignal(0, 0, 0, "")
        };
        private readonly List<double> polyharmonicalSignal = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Draw(IEnumerable<double> points, string legend)
        {
            List<Series> seriesList = new List<Series>();

            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "ChartArea",
                Legend = legend
            };

            var k = 0;
            foreach (var t in points)
            {
                series.Points.AddXY(k, t);
                k++;
            }
            seriesList.Add(series);
            Chart1.Series.Add(series);

            Chart1.Legends.Add(new Legend(legend));

            Chart1.Legends[legend].DockedToChartArea = "ChartArea";

            Chart1.Series.Last().Legend = legend;
            Chart1.Series.Last().LegendText = legend;
            Chart1.Series.Last().IsVisibleInLegend = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                Chart1.Series[0].Points.Clear();
                Chart1.Legends.Clear();
                Chart1.Series.Clear();
            }
            catch { }


            var n = Convert.ToInt32(numberInput.SelectedItem);

            switch (signalTypeInput.SelectedIndex)
            {
                case 0:
                    DrawHarmonicalSignals(n);
                    break;
                case 1:
                    DrawPolyharmonicalSignal(n);
                    break;
                case 2:
                    DrawPolyharmonicalWithLinearDiff(n);
                    break;
                default:
                    DrawPolyharmonicalSignal(n);
                    break;
            }
        }

        private void DrawPolyharmonicalWithLinearDiff(int N)
        {
            var polyAndLinear = new List<double>();
            double A = 7;
            double[] fis = { 100.0, 150.0 };
            double f = 1;
            var legend = $"Init val: A={A} fi=[{fis[0]}, {fis[1]}] f={f}\n";
            for (var i = 0; i < N; i++)
            {
                double sum = 0;
                var ntmp = i % N;
                A += 0.2 * i / N;
                f -= 0.1 * i / N;
                fis[0] += 0.05 * i / N;
                fis[1] -= 0.05 * i / N;
                for (var j = 0; j < 2; j++)
                {
                    sum += A * Math.Sin(2 * Math.PI * f * ntmp / N + fis[j]);
                }
                polyAndLinear.Add(sum);
            }

            Draw(polyAndLinear, legend + $"End val: A={A} fi=[{fis[0]}, {fis[1]}] f={f}");
        }

        private void DrawPolyharmonicalSignal(int n)
        {
            Polyharmonical(n);
            for (var i = 0; i < n; i++)
            {
                var x = signals.Sum(s => s.Points[i]);
                polyharmonicalSignal.Add(x);
            }
            Draw(polyharmonicalSignal, "polyharm");
        }

        private void DrawHarmonicalSignals(int n)
        {
            switch (constValues.SelectedIndex)
            {
                case 0:
                    ConstFandA(n);
                    break;
                case 1:
                    ConstFiAndA(n);
                    break;
                case 2:
                    ConstFandFi(n);
                    break;
                default:
                    ConstFandA(n);
                    return;
            }

            foreach (var signal in signals)
            {
                signal.N = n;
                signal.Calculate();
                Draw(signal.Points, signal.Legend);
            }
        }

        private void Polyharmonical(int n)
        {
            var fiValues = new[] { Math.PI, Math.PI/4, 0, 3 * Math.PI / 4, Math.PI / 2 };
            for (var i = 0; i < graphCount; i++)
            {
                signals[i].A = 7;
                signals[i].f = i + 1;
                signals[i].fi = fiValues[i];
                signals[i].N = n;
                signals[i].Calculate();
            }
        }

        private void ConstFandA(int n)
        {
            var fiValues = new[] { Math.PI / 3, 3 * Math.PI / 4, 2 * Math.PI, Math.PI, Math.PI / 6 };
            var legends = new[] { "pi/3", "3pi/4", "2*pi", "pi", "pi/6" };
            for (var i = 0; i < graphCount; i++)
            {
                signals[i].A = 9;
                signals[i].f = 4;
                signals[i].fi = fiValues[i];
                signals[i].Legend = $"fi = {legends[i]}";
                signals[i].N = n;
            }

        }

        private void ConstFandFi(int n)
        {
            var aValues = new[] { 4, 5, 3, 1, 7 };
            for (var i = 0; i < graphCount; i++)
            {
                signals[i].A = aValues[i];
                signals[i].f = 7;
                signals[i].fi = Math.PI / 6;
                signals[i].Legend = $"A = {aValues[i]}";
                signals[i].N = n;
            }
        }

        private void ConstFiAndA(int n)
        {
            var fValues = new[] { 4, 8, 2, 1, 9 };
            for (var i = 0; i < graphCount; i++)
            {
                signals[i].A = 7;
                signals[i].f = fValues[i];
                signals[i].fi = Math.PI / 6;
                signals[i].Legend = $"f = {fValues[i]}";
                signals[i].N = n;
            }
        }
    }


    public class HarmonicalSignal
    {
        public int A { get; set; }

        public int f { get; set; }

        public double fi { get; set; }

        public int N { get; set; }

        public List<double> Points { get; set; } = new List<double>();

        public string Legend { get; set; }

        public HarmonicalSignal(int a, int f, double fi, string str, int n = 512)
        {
            A = a;
            this.f = f;
            this.fi = fi;
            N = n;
            Legend = str;
        }

        public void Calculate()
        {
            Points.Clear();
            for (int i = 0; i < N; i++)
            {
                var x = A * Math.Sin(2 * Math.PI * f * i / N + fi);
                Points.Add(x);
            }
        }
    }
}

