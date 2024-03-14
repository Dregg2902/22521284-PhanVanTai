using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplithuyet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double result;
        double firstdigit;
        string abbb;
        bool isoptr = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox.Text == "0" || isoptr)
            {
                textBox.Clear();
            }
            isoptr = false;
            Button button = (Button)sender;
            if (textBox.Text == ".")
            {
                if (!textBox.Text.Contains("."))
                {
                    textBox.Text += button.Text;
                }
            }
            else
                textBox.Text += button.Text;
            //textBox.Text = button.Text;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = textBox.Text.Length;
            index--;
            textBox.Text = textBox.Text.Remove(index);
            if (textBox.Text == "")
            {
                textBox.Text = "0";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox.Text = "0";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            firstdigit = double.Parse(textBox.Text);
            abbb = "%";
            isoptr = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            switch (abbb)
            {
                case "+":
                    textBox.Text = (firstdigit + double.Parse(textBox.Text)).ToString();
                    break;
                case "-":
                    textBox.Text = (firstdigit - double.Parse(textBox.Text)).ToString();
                    break;
                case "*":
                    textBox.Text = (firstdigit * double.Parse(textBox.Text)).ToString();
                    break;
                case "/":
                    textBox.Text = (firstdigit / double.Parse(textBox.Text)).ToString();
                    break;
                case "%":
                    result = double.Parse(textBox.Text);
                    result = (firstdigit % double.Parse(textBox.Text));
                    textBox.Text = result.ToString();
                    break;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            firstdigit = double.Parse(textBox.Text);
            Button Optr = (Button)sender;
            abbb = Optr.Text;
            isoptr = true;
        }
    }
}
