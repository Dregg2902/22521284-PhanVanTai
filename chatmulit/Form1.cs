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
namespace chatmulit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string ip;
        SimpleTcpServer server;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ip = textBox1.Text;
                server = new SimpleTcpServer(ip, 5555);
                server.Events.ClientConnected += Events_ClientConnected;
                server.Events.DataReceived += Events_DataReceived;
                server.Events.ClientDisconnected += Events_ClientDisconnected;

                richTextBox1.Text += $"{Environment.NewLine} Started...";
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += $"{Environment.NewLine}{e.IpPort}Disconnected.....!{e.Reason}";
                listBox1.Items.Remove(e.IpPort);
            });
        }


        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

                richTextBox1.Text += $"{Environment.NewLine}{e.IpPort}:{Encoding.UTF8.GetString(e.Data)}";
            });
        }

        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += $"{Environment.NewLine}{e.IpPort}Client connected!";
                listBox1.Items.Add(e.IpPort);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                string message = textBox2.Text;
                server.Send(listBox1.SelectedItem.ToString(), message);
                richTextBox1.Text += $"{Environment.NewLine}YOU: {message}";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
