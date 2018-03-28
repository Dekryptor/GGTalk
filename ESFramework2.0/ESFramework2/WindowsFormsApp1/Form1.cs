using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESFramework;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            EsfTcpClient client = new EsfTcpClient("127.0.0.1", 8000);
            NetworkStream stream = client.GetNetworkStream();
            if (stream.CanWrite)
            {
                byte[] buffer = new byte[] { 1,1,1,1,1,1,1,1,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                stream.Write(buffer, 0, 20);
            }
        }
    }
}
