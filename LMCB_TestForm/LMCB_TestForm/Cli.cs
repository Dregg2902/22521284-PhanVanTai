using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LMCB_TestForm
{
    public partial class Cli : Form
    {

        private TcpClient tcpClient;
        private StreamReader sReader;
        private StreamWriter sWriter;
        private Thread clientThread;
        private int serverPort = 8000;
        private bool stopTcpClient = true;
        public Cli()
        {
            InitializeComponent();

        }

        private void ClientRecv()
        {
            StreamReader sr = new StreamReader(tcpClient.GetStream());
            try
            {
                while (!stopTcpClient)
                {
                    Application.DoEvents();
                    string data = sr.ReadLine();
                    if (data!=null)
                    {
                        string formattedData = $"[{DateTime.Now:MM/dd/yyyy h:mm tt}] {data}\n";
                        UpdateChatHistoryThreadSafe($"{data}\n");
                    }
                  
                }
            }
            catch (SocketException sockEx)
            {
                tcpClient.Close();
                sr.Close();

            }
        }

        private delegate void SafeCallDelegate(string text);

        private void UpdateChatHistoryThreadSafe(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
              
                var d = new SafeCallDelegate(UpdateChatHistoryThreadSafe);
                richTextBox1.Invoke(d, new object[] {text});
            }
            else
            {
                richTextBox1.Text += text;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sWriter.WriteLine(sendMsgTextBox.Text);
                sendMsgTextBox.Text = "";
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Close(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                stopTcpClient = false;

                this.tcpClient = new TcpClient();
                this.tcpClient.Connect(new IPEndPoint(IPAddress.Parse(textBox2.Text), serverPort));
                this.sWriter = new StreamWriter(tcpClient.GetStream());
                this.sWriter.AutoFlush = true;
                sWriter.WriteLine(this.textBox1.Text);
                clientThread = new Thread(this.ClientRecv);
                clientThread.Start();
                MessageBox.Show("Connected");
            }
            catch (SocketException sockEx)
            {
                MessageBox.Show(sockEx.Message, "Network error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đảm bảo ngắt kết nối trước khi đóng form
            DisconnectFromServer();
        }

        private void DisconnectFromServer()
        {
            try
            {
                stopTcpClient = true; // Dừng vòng lặp nhận tin nhắn
                if (tcpClient != null)
                {
                    tcpClient.Close(); // Ngắt kết nối với server
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disconnecting from server: {ex.Message}");
            }
        }

    }
}
