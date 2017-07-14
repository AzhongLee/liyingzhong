using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Hirain.Lib.Common;

namespace HRP1679.BLL.NetCtrl
{
    public class FtpW32
    {
        #region " 单例 "

        private static FtpW32 instance;
        private static object lockObj = new object();

        public static FtpW32 Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                        instance = new FtpW32();
                    return instance;
                };
            }
        }

        private FtpW32()
        {
            this.Enabled = false;
        }

        #endregion

        //变量
        private Process prcFtp;

        //属性
        public bool Enabled { get; set; }

        //开始
        public void Start()
        {
            try
            {
                //声明一个程序信息类
                ProcessStartInfo Info = new ProcessStartInfo();

                //设置外部程序名
                Info.FileName = "wftpd32.exe";
                Info.WorkingDirectory = Application.StartupPath;
                Info.CreateNoWindow = true;
                Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                Process currentProcess = Process.GetCurrentProcess();
                // Get all instances of Notepad running on the local computer.
                Process[] localByName = Process.GetProcessesByName("wftpd32");

                if (localByName.Length != 0)
                {
                    prcFtp = localByName[0];
                    Enabled = true;
                    return;
                }

                //启动外部程序
                prcFtp = System.Diagnostics.Process.Start(Info);
                Enabled = true;
            }
            catch (System.ComponentModel.Win32Exception o)
            {
                Enabled = false;
                LoggingService.LogToShow("启动FTP服务器异常：" + o.Message , InformationType.Error , InformationDisplayMode.FormList);
            }
        }

        //停止
        public void Stop()
        {
            if (prcFtp != null)
            {
                if (prcFtp.HasExited == false)
                {
                    prcFtp.Kill();
                    Enabled = false;
                }
            }
        }

    }
}
