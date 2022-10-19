using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_4
{
    public class Signal
    {
        internal int n;
        internal double[] signal, restSignal, nfSignal;
        internal double[] sinSp, cosSp;
        internal double[] amplSp, faseSp;
        internal int numHarm = 100;

        double A, phi;
        public double[] parabolicS, medianS, slidingS, asp, fsp;

        public double[] signVal { get { return signal; } }
        public double[] amplSpectrum { get { return amplSp; } }
        public double[] phaseSpectrum { get { return faseSp; } }
        public double[] restoredSignal { get { return restSignal; } }
        public double[] restorednonphasedSignal { get { return nfSignal; } }

        public Signal(double amplitude, double phase, int discrPoints)
        {
            A = amplitude;
            n = discrPoints;
            phi = phase;

            signal = GenerateSignal();
            parabolicS = ParabolicSmoothing();
            medianS = MedianSmoothing(5);
            slidingS = SlidingSmoothing(3);
            sinSp = GetSinSpectrum(signal);
            cosSp = GetCosSpectrum(signal);
            amplSp = GetASpectrum(sinSp, cosSp);
            faseSp = GetFaseSpectrum(sinSp, cosSp);
            restSignal = RestoreSignal();
            nfSignal = RestoreNFSignal();
        }

        public void Operate(int ft)
        {
            double[] fs = null;
            switch (ft)
            {
                case 0:
                    fs = parabolicS;
                    break;
                case 1:
                    fs = medianS;
                    break;
                case 2:
                    fs = slidingS;
                    break;
                default:
                    break;
            }
            double[] sinSp = GetSinSpectrum(fs);
            double[] cosinSp = GetCosSpectrum(fs);
            asp = GetASpectrum(sinSp, cosinSp);
            fsp = GetFaseSpectrum(sinSp, cosinSp);
        }

        internal double[] GenerateSignal()
        {
            double[] sign = new double[n];
            Random rnd = new Random();
            double B = A / 70;
            for (int i = 0; i <= n - 1; i++)
            {
                sign[i] = A * Math.Sin(2 * Math.PI * i / n + phi);
                double noise = 0;
                for (int j = 50; j <= 70; j++)
                {
                    noise += (rnd.Next(100000) % 2 == 0) ? (B * Math.Sin(2 * Math.PI * i * j / n + phi)) : (-B * Math.Sin(2 * Math.PI * i * j / n + phi));
                }
                sign[i] += noise;
            }
            return sign;
        }

        public double[] ParabolicSmoothing()
        {
            double[] rest = new double[n];
            for (int i = 7; i <= rest.Length - 8; i++)
            {
                rest[i] = (
                    -3 * signal[i - 7]
                    - 6 * signal[i - 6]
                    - 5 * signal[i - 5]
                    + 3 * signal[i - 4]
                    + 21 * signal[i - 3]
                    + 46 * signal[i - 2]
                    + 67 * signal[i - 1]
                    + 74 * signal[i]
                    - 3 * signal[i + 7]
                    - 6 * signal[i + 6]
                    - 5 * signal[i + 5]
                    + 3 * signal[i + 4]
                    + 21 * signal[i + 3]
                    + 46 * signal[i + 2]
                    + 67 * signal[i + 1]
                    ) / 320;
            }
            return rest;
        }

        public double[] SlidingSmoothing(int windowSize)
        {
            double[] rest = (double[])signal.Clone();
            List<double> window = new List<double>();
            for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
            {
                window.Clear();
                for (int j = i; j <= i + windowSize - 1; j++)
                {
                    window.Add(signal[j]);
                }
                double avg = window.Sum() / windowSize;
                rest[i + windowSize / 2] = avg;
            }
            return rest;
        }

        public double[] MedianSmoothing(int windowSize)
        {
            double[] rest = (double[])signal.Clone();
            List<double> window = new List<double>();
            for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
            {
                window.Clear();
                for (int j = i; j <= i + windowSize - 1; j++)
                {
                    window.Add(signal[j]);
                }
                window.Sort();
                rest[i + windowSize / 2] = window[windowSize / 2 + 1];
            }
            return rest;
        }

        internal double[] GetSinSpectrum(double[] signal)
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                double val = 0;
                for (int i = 0; i <= n - 1; i++)
                {
                    val += signal[i] * Math.Sin(2 * Math.PI * i * j / n);
                }
                values[j] = 2 * val / n;
            }
            return values;
        }

        internal double[] GetCosSpectrum(double[] signal)
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                double val = 0;
                for (int i = 0; i <= n - 1; i++)
                {
                    val += signal[i] * Math.Cos(2 * Math.PI * i * j / n);
                }
                values[j] = 2 * val / n;
            }
            return values;
        }

        internal double[] GetASpectrum(double[] sineSp, double[] cosineSp)
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                values[j] = Math.Sqrt(Math.Pow(sinSp[j], 2) + Math.Pow(cosSp[j], 2));
            }
            return values;
        }

        internal double[] GetFaseSpectrum(double[] sineSp, double[] cosineSp)
        {
            double[] values = new double[numHarm];
            for (int j = 0; j <= numHarm - 1; j++)
            {
                values[j] = Math.Atan(sinSp[j] / cosSp[j]);
            }
            return values;
        }

        internal double[] RestoreSignal()
        {
            double[] values = new double[n];
            for (int i = 0; i <= n - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n - faseSp[j]);
                }
                values[i] = val;
            }
            return values;
        }

        internal double[] RestoreNFSignal()
        {
            double[] values = new double[n];
            for (int i = 0; i <= n - 1; i++)
            {
                double val = 0;
                for (int j = 0; j <= numHarm - 1; j++)
                {
                    val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n);
                }
                values[i] = val;
            }
            return values;
        }
    }
}
