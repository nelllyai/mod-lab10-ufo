using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ufo
{
    public partial class Form1 : Form
    {
        double x1 = 50, y1 = 100;
        double x2 = 650, y2 = 800;
        double step = 1;
        int power = 10;
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(DrawLine);
        }

        void DrawLine(object sender, PaintEventArgs e)
        {
            double x = x1;
            double y = y1;

            Graphics g = e.Graphics;
            g.ScaleTransform(0.5f, 0.5f);

            Pen blackPen = new Pen(Color.Black, 3);
            Pen boldBlackPen = new Pen(Color.Black, 6);

            g.DrawEllipse(boldBlackPen, (int)x, (int)y, 5, 5);
            g.DrawEllipse(boldBlackPen, (int)x2, (int)y2, 5, 5);

            GraphicsState gs = g.Save();

            double value = Math.Abs(x - x2) + Math.Abs(y - y2);
            double angle = Arctg((y2 - y) / (x - x2), power);
            double distance = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));

            while (distance <= value)
            {
                x += step * Cos(angle, power);
                y -= step * Sin(angle, power);
                g.DrawEllipse(blackPen, (int)x, (int)y, 1, 1);

                distance = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));

                if (distance < value)
                    value = distance;
            }

            string error = "Погрешность: " + value.ToString();
            Font myFont = new Font("Verdana", 22);
            SolidBrush myBrush = new SolidBrush(Color.Black);
            g.DrawString(error, myFont, myBrush, (int)x2 + 30, (int)y2 + 30);

            g.Restore(gs);

            List<double> points = new List<double>();
            for (int i = 2; i < 10; i++)
            {
                x = x1;
                y = y1;
                distance = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));
                value = Math.Abs(x - x2) + Math.Abs(y - y2);
                angle = Arctg((y2 - y) / (x - x2), i);

                while (distance <= value)
                {
                    x += step * Cos(angle, i);
                    y -= step * Sin(angle, i);
                    distance = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));

                    if (distance < value)
                        value = distance;
                }
                points.Add(value);
                Console.WriteLine(value);
            }

            Form2 graphic = new Form2(points);
            graphic.Show();
        }

        double Arctg(double x, int power)
        {
            double arctg = 0;
            if (x >= -1 && x <= 1)
            {
                for (int i = 1; i <= power; i++)
                {
                    arctg += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
                }
            }

            else
            {
                if (x >= 1)
                    arctg += Math.PI / 2;
                else
                    arctg -= Math.PI / 2;

                for (int i = 0; i < power; i++)
                {
                    arctg -= Math.Pow(-1, i) / ((2 * i + 1) * Math.Pow(x, 2 * i + 1));
                }
            }

            return arctg;
        }

        int Factorial(int x)
        {
            if (x <= 0)
                return 1;
            return x * Factorial(x - 1);
        }

        double Sin(double x, int power)
        {
            double sin = 0;

            for (int i = 1; i <= power; i++)
            {
                sin += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / Factorial(2 * i - 1);
            }

            return sin;
        }

        double Cos(double x, int power)
        {
            double cos = 0;

            for (int i = 1; i <= power; i++)
            {
                cos += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / Factorial(2 * i - 2);
            }

            return cos;
        }
    }
}
