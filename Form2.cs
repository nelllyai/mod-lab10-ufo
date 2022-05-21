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

namespace ufo
{
    public partial class Form2 : Form
    {
        public Form2(List<double> points)
        {
            InitializeComponent();
            chart.Series.Add(new Series());

            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[1].IsVisibleInLegend = false;

            Font myFont = new Font("Verdana", 11);
            chart.ChartAreas[0].AxisX.TitleFont = myFont;
            chart.ChartAreas[0].AxisY.TitleFont = myFont;

            chart.ChartAreas[0].AxisX.Title = "Количество членов ряда";
            chart.ChartAreas[0].AxisX.Minimum = 2;
            chart.ChartAreas[0].AxisX.Maximum = 9;
            chart.ChartAreas[0].AxisY.Title = "Радиус области попадания";

            chart.Series[0].Color = Color.Blue;
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Series[1].ChartType = SeriesChartType.Point;

            for (int i = 0; i < points.Count; i++)
            {
                chart.Series[0].Points.AddXY(i + 2, Math.Round(points[i], 2));
                chart.Series[1].Points.AddXY(i + 2, Math.Round(points[i], 2));
            }
        }
    }
}
