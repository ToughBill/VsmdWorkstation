using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
            //ConnectVsmd frm = new ConnectVsmd();
            //if(frm.ShowDialog() == DialogResult.OK && frm.IsConnected)
            //{
            //    Application.Run(new DripFrm());
            //}
            //ConfigurationManager.AppSettings[""]
        }
    }
}
