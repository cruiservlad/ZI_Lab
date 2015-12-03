using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ZI_Labs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                String text = textBox1.Text;
                int key = Convert.ToInt16(textBox2.Text);
                char newText;
                char keyText;
                int keyTexti;
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    newText = Convert.ToChar(text[i]);
                    if (Convert.ToInt32(newText) == 32) { textBox3.Text += " "; }
                    else
                    {
                        keyTexti = Convert.ToInt32(newText + key);
                        if (keyTexti > 1103) { keyTexti = (keyTexti - 1104) + 1072; }
                        keyText = Convert.ToChar(keyTexti);
                        textBox3.Text += keyText;
                    }
                }
            }
            else MessageBox.Show("Одно из полей пустое, кодирование невозможно!");
        }

        private void шифрПростойЗаменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void шифрПерестановкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false ;
            panel2.Visible = true;
            textBox5.Text = "453120";
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "") { MessageBox.Show("Одно или несколько полей пустые!"); return; }
            int аnswer = 0;
            string text = textBox4.Text;
            Random rnd = new Random();

            string key = textBox5.Text;
            int row_count = (int)Math.Floor((decimal)(text.Length / 6) + 1);
            char[,] mass = new char[6, row_count];
            char[,] rez = new char[6, row_count];
            if (textBox1.Text.Length <= 40)
            {
                // - - - - - - - - - ИНИЦИАЛИЗАЦИЯ - - - - - -- - - - -- - - -- - - - -- - - - -- - - //
                for (int x = 0; x < row_count; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        mass[y, x] = ' ';
                        //rez[y, x] = ' ';

                    }
                }
                int q = 0;

                // - - - - - - - - - ЗАПОЛНЕНИЕ МАТРИЦЫ - - - - - -- - - - -- - - -- - - - -- - - - -- - - //              
                for (int x = 0; x < row_count; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        if (q < text.Length)
                        {
                            mass[y, x] = text[q];
                            q++;
                        }
                    }
                }
                // - - - - - - - - - ПРЕОБРАЗОВАНИЕ МАТРИЦЫ - - - - - -- - - - -- - - -- - - - -- - - - -- - - 
                аnswer = key[0];



                int c;

                for (int x = 0; x < row_count; x++)
                {
                    c = 0;
                    for (int y = 0; y < 6; y++)
                    {


                        rez[y, x] = mass[int.Parse(key[c].ToString()), x];
                        richTextBox1.Text += rez[y, x];
                        c++;

                    }
                    richTextBox1.Text += Environment.NewLine;

                }

            }
            else
            {
                MessageBox.Show("Очень много символов. Хочу меньше.");

            }
        }

        private void зашифроватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Путь к файлу
            string enpath = richTextBox2.Text;
            uint k = 1; //Переменная выбора шифрования/дешифрования
            string s = richTextBox2.Text; //Строка, к которой применяется шифрованияе/дешифрование
            string result = ""; //Строка - результат шифрования/дешифрования
            string key = textBox6.Text; //Строка - ключ шифра
            string key_on_s = ""; //Ключ длиной строки
            int x = 0, y = 0; //Координаты нового символа из таблицы Виженера
            int registr = 0; //Регистр символа
            char dublicat; //Дубликат прописной буквы

            //Формирование таблицы Виженера на алфавите кирилицы
            int shift = 0;
            char[,] tabula_recta = new char[32, 32]; //Таблица Виженера
            string alfabet = "абвгдежзийклмнопрстуфхцчшщьыъэюя";
            //Формирование таблицы
            for (int i = 0; i < 32; i++)
                for (int j = 0; j < 32; j++)
                {
                    shift = j + i;
                    if (shift >= 32) shift = shift % 32;
                    tabula_recta[i, j] = alfabet[shift];
                }
            //Если выбрано шифрование
            if (k == 1)
            {
                bool flag = true;
                //Выполение шифрования
                //Формирование строки, длиной шифруемой, состоящей из повторений ключа
                for (int i = 0; i < s.Length; i++)
                {
                    key_on_s += key[i % key.Length];
                }
                //Шифрование при помощи таблицы
                for (int i = 0; i < s.Length; i++)
                {
                    //Если не кириллица
                    if (((int)(s[i]) < 1040) || ((int)(s[i]) > 1103))
                        result += s[i];
                    else
                    {
                        //Поиск в первом столбце строки, начинающейся с символа ключа
                        int l = 0;
                        flag = false;
                        //Пока не найден символ
                        while ((l < 32) && (flag == false))
                        {
                            //Если символ найден
                            if (key_on_s[i] == tabula_recta[l, 0])
                            {
                                //Запоминаем в х номер строки
                                x = l;
                                flag = true;
                            }
                            l++;
                        }
                        //Уменьшаем временно регистр прописной буквы
                        if ((Convert.ToInt16(s[i]) < 1072) && (Convert.ToInt16(s[i]) >= 1040))
                        {
                            dublicat = Convert.ToChar(Convert.ToInt16(s[i]) + 32);
                            registr = 1;
                        }
                        else
                        {
                            registr = 0;
                            dublicat = s[i];
                        }
                        l = 0;
                        flag = false;
                        //Пока не найден столбец в первой строке с символом строки
                        while ((l < 32) && (flag == false))
                        {
                            //Проверка совпадения
                            if (dublicat == tabula_recta[0, l])
                            {
                                //Запоминаем номер столбца
                                y = l;
                                flag = true;
                            }
                            l++;
                        }
                        //Увеличиваем регистр буквы до прописной
                        if (registr == 1)
                        {
                            //Изменяем символ на первоначальный регистр
                            dublicat = Convert.ToChar(Convert.ToInt16(tabula_recta[x, y]) - 32);
                            result += dublicat;
                        }
                        else
                            result += tabula_recta[x, y];
                    }
                }
                //Вывод на экран зашифрованной строки
                Console.WriteLine("Строка успешно зашифрована!");
                richTextBox3.Text = result;
                /*short[] masX = new short[alfabet.Length];
                for(int i = 0; i < alfabet.Length; i++)
                {
                    masX[i]++;
                    //this.chart1.Series["Series1"].Points[i].YValues[i] = masX[i];
                    this.chart1.Series["Series1"].Points[i].AxisLabel = alfabet[i].ToString();
                    MessageBox.Show(i.ToString());
                }
                this.chart1.Series["Series1"].Points.DataBindXY(alfabet, masX);*/
                //this.chart1.Series["Series1"].Points.AddY(masX);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
