﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CameraServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。ssssdssd
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmServer());
        }
    }
}