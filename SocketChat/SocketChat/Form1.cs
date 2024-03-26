using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketChat
{
    public partial class Form1 : Form
    {
        private Socket listener = null;
        private bool started = false;
        private int _port = 11000;
        private static int _buff_size = 2048;
        private byte[] _buffer = new byte[_buff_size];
        private Thread serverThread = null;
        private delegate void SafeCallDelegate(string text);
        private List<Socket> connectedClients = new List<Socket>();

        public Form1()
        {
            InitializeComponent();
            listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (started)
                {
                    started = false;
                    button2.Text = "Listen";
                    serverThread = null;
                    listener.Close();
                    stopListening();
                }
                else
                {
                    serverThread = new Thread(this.listen);
                    serverThread.Start();
                    button2.Text = "Stop";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listen()
        {
            try
            {
                listener.Bind(new IPEndPoint(IPAddress.Any, _port));
                listener.Listen(10);
                started = true;
                UpdateChatHistoryThreadSafe("Start listening");

                while (started)
                {
                    Socket clientSocket = listener.Accept();
                    UpdateChatHistoryThreadSafe("Accept connection from " + clientSocket.RemoteEndPoint.ToString());

                    if (clientSocket != null && clientSocket.Connected)
                    {
                        Task.Factory.StartNew(() => HandleClient(clientSocket));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleClient(Socket clientSocket)
        {
            try
            {
                while (started && clientSocket.Connected)
                {
                    byte[] buffer = new byte[_buff_size];
                    int readBytes = clientSocket.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer, 0, readBytes);
                    UpdateChatHistoryThreadSafe(message);

                    // Gửi lại tin nhắn đã nhận được cho tất cả client khác
                    foreach (Socket connectedClient in connectedClients)
                    {
                        if (connectedClient != clientSocket && connectedClient.Connected)
                        {
                            connectedClient.Send(Encoding.UTF8.GetBytes(message));
                        }
                    }
                }
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopListening()
        {
            try
            {
                started = false;
                listener.Close();
                UpdateChatHistoryThreadSafe("Server stopped listening." + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Không cần thêm mã vào đây, vì chúng ta không cần xử lý sự kiện này
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            try
            {
                started = false;
                listener.Close();
                UpdateChatHistoryThreadSafe("Server stopped listening." + "\n");

                // Đóng tất cả các kết nối của client
                foreach (Socket connectedClient in connectedClients)
                {
                    if (connectedClient != null && connectedClient.Connected)
                    {
                        connectedClient.Shutdown(SocketShutdown.Both);
                        connectedClient.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}



