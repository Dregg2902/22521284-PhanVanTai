using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;
namespace TCPclient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        string ip;
        private void Form2_Load(object sender, EventArgs e)
        {
            ip = textBox1.Text;
            client = new SimpleTcpClient(ip, 5555);
            client.Events.Connected += Events_Connected;
            client.Events.Disconnected += Events_Disconnected;
            client.Events.DataReceived += Events_DataReceived;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() != "")
            {
                client.Send(textBox2.Text);
                richTextBox1.Text += $"{Environment.NewLine} ME: {textBox2.Text}";
        }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            client.Connect();
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {


                richTextBox1.Text += $"{Environment.NewLine} {e.IpPort}: {Encoding.UTF8.GetString(e.Data.Array)}";

            });
            }

        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {



                richTextBox1.Text += $"{Environment.NewLine} Disconnected!";
            });
        }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {



                richTextBox1.Text = $"{Environment.NewLine} Connected";
            });
        }
    }
}
