using MOSU_LAB1.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOSU_LAB1
{
    public partial class MainForm : Form
    {
        private double dt = 1;
        private Tank tank;
        private double time = 0;

        // Мінімальні та максимальні значення
        private const double MinValue = 0.0;
        private const double MaxValue = 10.0;

        public MainForm()
        {
            InitializeComponent();
            tank = new Tank(dt);
        }

        private void bt_start_Click(object sender, EventArgs e)
        {
            modelTimer.Start();
            model_chart.Series[0].Points.Clear();  // очищення графіка
            model_chart.Series[1].Points.Clear();  // очищення графіка
            model_chart.Series[2].Points.Clear();  // очищення графіка
            model_chart.Series[3].Points.Clear();  // очищення графіка
            time = 0;
        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            modelTimer.Stop();
        }

        private void modelTimer_Tick(object sender, EventArgs e)
        {
            // Обчислюємо рівень на основі поточних значень
            double level = tank.Step(tank.X1, tank.X2, tank.X3);

            model_chart.Series[0].Points.AddXY(time, level);
            model_chart.Series[1].Points.AddXY(time, tank.X2);
            model_chart.Series[2].Points.AddXY(time, tank.X3);
            model_chart.Series[3].Points.AddXY(time, tank.X1);

            time += dt;
        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            if (modelTimer.Interval == 100)
            {
                modelTimer.Interval = 1000;
                btnSpeed.Text = "x10";
            }
            else
            {
                modelTimer.Interval = 100;
                btnSpeed.Text = "x1";
            }
        }

        // Зменшення x2
        private void btnDn2_Click(object sender, EventArgs e)
        {
            if (tank.X2 > MinValue)
            {
                tank.X2 -= 0.1;
                tbx2.Text = tank.X2.ToString("F3");
            }
        }

        // Збільшення x2
        private void btnUp2_Click(object sender, EventArgs e)
        {
            if (tank.X2 < MaxValue)
            {
                tank.X2 += 0.1;
                tbx2.Text = tank.X2.ToString("F3");
            }
        }

        // Зменшення x3
        private void btnDn3_Click(object sender, EventArgs e)
        {
            if (tank.X3 > MinValue)
            {
                tank.X3 -= 0.1;
                tbx3.Text = tank.X3.ToString("F3");
            }
        }

        // Збільшення x3
        private void btnUp3_Click(object sender, EventArgs e)
        {
            if (tank.X3 < MaxValue)
            {
                tank.X3 += 0.1;
                tbx3.Text = tank.X3.ToString("F3");
            }
        }

        // Зменшення x1 (початковий вхід)
        private void btnDn1_Click(object sender, EventArgs e)
        {
            if (tank.X1 > MinValue)
            {
                tank.X1 -= 0.1;
                tbx1.Text = tank.X1.ToString("F3");
            }
        }

        // Збільшення x1 (початковий вхід)
        private void btnUp1_Click(object sender, EventArgs e)
        {
            if (tank.X1 < MaxValue)
            {
                tank.X1 += 0.1;
                tbx1.Text = tank.X1.ToString("F3");
            }
        }

        // Обробник події для кліку на label3
        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("label3 was clicked!");
        }

        private void model_chart_Click(object sender, EventArgs e)
        {
            // залишаємо порожнім
        }
    }
}