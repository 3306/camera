using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ListenerDLL;

namespace CameraServer
{
    public partial class CameraOptions : Form
    {

        public CameraOptions()
        {
            InitializeComponent();
        }

        private void StartListenBtn_Click(object sender, EventArgs e)
        {
            Listener.ConnectAndListen();
        }
    }
}
