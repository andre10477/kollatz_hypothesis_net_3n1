using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3N_1
{
    public struct ST //шаблон структуры(для будущего улучшения кода)
    {
        string G { get; set; }
    }
    public partial class Form1 : Form
    {
        Zadacha z = new Zadacha();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int chislo = Convert.ToInt32(richTextBox1.Text);
            z.function(chislo);
            for (int i = 0; i < z.Xm.Length; i++)
            {
                richTextBox2.Text += $"X={z.Xm[i]}, Y={z.Ym[i]}\n";
                chart1.Series["Series1"].Points.AddXY(z.Xm[i], z.Ym[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            chart1.Series["Series1"].Points.Clear();
            z.ClearArrays();
        }
    }

    public interface Service //шаблон интерфейса(для будущего улучшения кода)
    {
        void ClearArrays();
        int algorithm(int n);
        void function(int chislo);
    }

    public class Zadacha : Service
    {
        bool error = false;
        public int[] Xm = new int[99999999];
        public int[] Ym = new int[99999999];
        public virtual void ClearArrays()
        {
            Array.Clear(Xm, 0, Xm.Length);
            Array.Clear(Ym, 0, Ym.Length);
        }

        public virtual int algorithm(int n)
        {
            int func = 3 * n + 1;
            return func;
        }

        public void function(int chislo)
        {
            int y = chislo;
            int i = 0;
            try
            {
                while (true)
                {
                    if (y == 1)
                    {
                        break;
                    }
                    Ym[i] = y;
                    Xm[i] = i;
                    if (y % 2 == 1)
                    {
                        y = algorithm(y);
                    }
                    else
                    {
                        y = y / 2;
                    }
                    i++;
                    if (i >= Ym.Length)
                    {
                        throw new IndexOutOfRangeException("Слишком много данных");
                    }
                    Ym[i] = y;
                    Xm[i] = i;
                }
            }
            catch (IndexOutOfRangeException e) //сделать работу с формой
            {
                MessageBox.Show(e.Message);
                ClearArrays();
            }
            RemoveZeroValues();
        }

        public string Error()
        {
            if (error == true)
            {
                string a = "Введите другое число";
                return a;
            }
            else
            {
                return "";
            }
        }
        private void RemoveZeroValues()
        {
            List<int> nonZeroIndices = new List<int>();

            for (int i = 0; i < Xm.Length; i++)
            {
                if (!(Xm[i] == 0 && Ym[i] == 0))
                {
                    nonZeroIndices.Add(i);
                }
            }

            int[] newXm = new int[nonZeroIndices.Count];
            int[] newYm = new int[nonZeroIndices.Count];

            for (int i = 0; i < nonZeroIndices.Count; i++)
            {
                newXm[i] = Xm[nonZeroIndices[i]];
                newYm[i] = Ym[nonZeroIndices[i]];
            }

            Xm = newXm;
            Ym = newYm;
        }
    }
}