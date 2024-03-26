using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class ClientForm : Form
    {
        private Socket clientSocket = null;
        private int _buff_size = 2048;

        public ClientForm()
        {
            InitializeComponent();
            clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress serverIp = IPAddress.Parse(textBox1.Text);
                int serverPort = int.Parse(textBox2.Text);
                IPEndPoint serverEp = new IPEndPoint(serverIp, serverPort);
                clientSocket.Connect(serverEp);
                richTextBox1.Text += "Connected to " + serverEp.ToString() + "\n";
                Task.Factory.StartNew(ReceiveData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string message = textBox1.Text; // Sử dụng richTextBox2 để nhập tin nhắn
                clientSocket.Send(Encoding.UTF8.GetBytes(message));
                textBox1.Text = ""; // Xóa nội dung sau khi gửi tin nhắn
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceiveData()
        {
            try
            {
                while (clientSocket.Connected)
                {
                    byte[] buffer = new byte[_buff_size];
                    int readBytes = clientSocket.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer, 0, readBytes);
                    UpdateChatHistoryThreadSafe(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Disconnect()
        {
            try
            {
                if (clientSocket.Connected)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    richTextBox1.Text += "Disconnected from server." + "\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Disconnect();
        }

        private void UpdateChatHistoryThreadSafe(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateChatHistoryThreadSafe);
                richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                richTextBox1.Text += text + "\n";
            }
        }

        private delegate void SafeCallDelegate(string text);
        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

       

    }
}
