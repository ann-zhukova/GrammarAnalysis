using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhukova_AV_6202_V17
{
    public struct Entry // структура для хранения данных о семантике
    {
        public string value; // значение константы или идентификатора
        public string type; // тип 
    }
    public partial class Zhukova_A_V17 : Form
    {
        private string chain; // исходная строка
        private LinkedList<Entry> idConsts; // список данных о идентификаторах
        private LinkedList<Entry> numeralsConsts; // список данных о целых константах
        private string startConst; // начальное значение цикла 
        private string endConst; // конечное значение цикла 
        private string step; // значение шага
        public Zhukova_A_V17()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // ввод новой строки
        {
            textBox1.Text = "";
            chain = "";
            label1.Visible = false;
            button3.Visible = false;
            pictureBox1.Visible = false;

        }
        private void button2_Click(object sender, EventArgs e) // анализ введенной строки 
        {
            button3.Visible = false;
            chain = textBox1.Text.ToLower();
            label1.Visible = true;
            label1.Text = "Анализ цепочки: ";
            int errorPos = -1;
            string message = "";
            if(Analyzer.CheckLoopFor(chain, out message, out errorPos, out startConst, out endConst, out step, out idConsts, out numeralsConsts )) // проверка симантики и синтаксиса 
            {
                label1.Text += "цепочка принадлежит языку";
                button3.Visible = true;
                pictureBox1.Visible = false;
            }
            else
            {
                label1.Text += message; // вывод ошибки 
                pictureBox1.Visible = true;
                pictureBox1.Location = new Point((errorPos + 1) * 6, 105); // подчеркивание ошибки 
                textBox1.Focus(); // перевод курсора на позицию ошибки 
                if(errorPos != -1)
                {
                    textBox1.SelectionStart = errorPos;
                }
                else
                {
                    textBox1.SelectionStart = errorPos+1;
                }
               
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e) //  сокрытие семантики при изменении цепочки
        {
            button3.Visible = false;
            label2.Visible = false;
            label3.Visible = false; 
        }
        private void button3_Click(object sender, EventArgs e) // вывод данных о семантике
        {
            label2.Visible = true;
            label3.Visible = true;
            label3.Text = "\nСписок констант:\n";
            if(step == "")
            {
                step += "1";
            }
            foreach (Entry entry in numeralsConsts) // вывод констант 
            {
                label3.Text += String.Format($"{{0,10}} {{1,10}}", entry.value, entry.type);
                label3.Text +=  "\n";
            }
            if(Convert.ToInt32(step) > 0) // вывод числа итераций 
            {
                label3.Text += "\nЧисло итераций цикла: " + (1+(Math.Abs(-Convert.ToInt32(startConst) + Convert.ToInt32(endConst)) / Convert.ToInt32(step)));
            }
            else
            {
                if(Convert.ToInt32(step) < 0)
                {
                    label3.Text += "\nЧисло итераций цикла: " + (1 +( Math.Abs(Convert.ToInt32(startConst) - Convert.ToInt32(endConst)) / Math.Abs(Convert.ToInt32(step))));
                }
            }
           
            label2.Text = "\nСписок индентификаторов\n"; // вывод идентификаторов 
            foreach ( Entry entry in idConsts)
            {
                label2.Text += $"{entry.value,30} {entry.type,10}";
                label2.Text += " \n";
                
            }
        }
    }
}
