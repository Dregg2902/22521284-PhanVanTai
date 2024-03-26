using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                }
                else
                {
                    serverThread = new Thread(this.listen);
                    serverThread.Start();
                   
                }
            }
            catch(Exception ex)
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
                UpdateChatHistoryThreadSafe("Server stopped listening.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void readFromSocket(Socket clientSocket)
        {
            while (started && clientSocket != null)
            {
                int readbytes = clientSocket.Receive(_buffer);
                string s = Encoding.UTF8.GetString(_buffer);
                UpdateChatHistoryThreadSafe(s + "\n");
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

    }
}
