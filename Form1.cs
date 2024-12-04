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

        private void button1_Click(object sender, EventArgs e) //Кнопка "Вычислить"
        {
            richTextBox2.Text = "";
            chart1.Series[0].Points.Clear();
            z.ClearLists();
            try
            {
                int chislo = Convert.ToInt32(richTextBox1.Text);
                z.function(chislo);
                for (int i = 0; i < z.Xm.Count; i++)
                {
                    richTextBox2.Text += $"X={z.Xm[i]}, Y={z.Ym[i]}\n";
                    chart1.Series[0].Points.AddXY(z.Xm[i], z.Ym[i]);
                }
                chart1.ChartAreas[0].AxisX.MaximumAutoSize = 90;
            }
            catch (FormatException ex)
            {
                richTextBox1.Text = ex.Message + "Впишите любое число больше нуля.";
            }
        }
    }

    public interface Service //Интерфейс с функциями
    {
        void ClearLists();
        int algorithm(int n);
        void function(int chislo);
    }

    public class Zadacha : Service //Класс с алгоритмом
    {
        bool error = false;
        public List<int> Xm = new List<int>();
        public List<int> Ym = new List<int>();
        public virtual void ClearLists() //Очистка списков
        {
            Xm.Clear();
            Ym.Clear();
        }

        //3N+1
        public virtual int algorithm(int n) =>  3 * n + 1;

        public void function(int chislo) //Основные вычисления
        {
            int y = chislo;
            int i = 1; // Индекс шагов начинается с 1
            Xm.Add(i); // Добавляем первый шаг (индекс)
            Ym.Add(y); // Добавляем первое введённое число

            while (y != 1) // Выполняем, пока число не станет 1
            {
                if (y % 2 == 1) // Если число нечетное
                {
                    y = algorithm(y); // Применяем алгоритм 3n+1
                }
                else // Если число четное
                {
                    y = y / 2; // Делим на 2
                }

                i++; // Увеличиваем индекс шагов
                Xm.Add(i); // Добавляем новый шаг (индекс)
                Ym.Add(y); // Добавляем новое значение числа
            }
        }

        public string Error() //Ошибка ввода
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
        private void RemoveZeroValues() //Удаление нулей
        {
            Xm.RemoveAll(item => item == 0);
            Ym.RemoveAll(item => item == 0);
        }
    }
}