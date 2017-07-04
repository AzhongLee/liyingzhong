using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hirain.Lib.IO;
using Hirain.Lib.Common;
using HRP1679.BLL;
using HRP1679.BLL.CtrlWord;
using HRP1679.UI;
using HRP1679.BLL.Other;

namespace HRP1679
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            FormState.Instance.FormLoad();
            //用户权限正常启动
            Application.Run(FormMain.Instance);
            
            ////管理员权限启动
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    Application.Run(FormMain.Instance);
            //}
            //else
            //{
            //    //创建启动对象
            //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //  //  startInfo.UseShellExecute = true;
            //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
            //    //设置启动文件
            //    startInfo.FileName = Application.ExecutablePath;
            //    //设置启动动作,确保以管理员身份运行
            //    startInfo.Verb = "runas";
            //    try
            //    {
            //        //启动UAC
            //        System.Diagnostics.Process.Start(startInfo);
            //    }
            //    catch
            //    {
            //        return;
            //    }

            //    Application.Exit();
            //}
        }
    }
}
