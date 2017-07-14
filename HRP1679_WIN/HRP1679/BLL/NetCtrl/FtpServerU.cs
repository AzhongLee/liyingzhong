using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Security;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management;
using System.Threading;

// ******************************************
// 
// FileName:FtpServerOperator.cs
// Description:
// 
// Author:许亚娟
// CreateTime:3/19/2012 6:02:19 PM 
// ModifiedRecords：
//
// ******************************************
namespace HRP1679.BLL.NetCtrl
{
    /// <summary>
    /// FTP服务控制类
    /// </summary>
    public class FtpServerU
    {
        private  Process procFtp = null;

        private string workDirectory = Application.StartupPath + "\\Serv-U";

        //private INIFilesOperate iniFilesOperate;
        private string iniFilePath = "C:\\Users\\";
        private string defaultFilePath = "C:\\WINDOWS\\wftpd32.INI";

        #region " 属性 " 
        //FTPSever所在路径
        public  string WorkDirectory
        {
            get
            {
                return workDirectory;
            }
            set
            {
                workDirectory = value;
            }
        }
        //启动标志
        public bool Enabled { get; set; }


        #endregion

        #region " 单例 "

        private static FtpServerU instance = null;
        protected static object SyscnRoot = new object();
        public static FtpServerU Instance
        {
            get
            {
                lock (SyscnRoot)
                {
                    if (null == instance)
                        return instance = new FtpServerU();
                }
                return instance;
            }
        }

        #endregion

        /// <summary>
        /// 启动FTP服务
        /// </summary>
        public void Start()        
        {
            procFtp = new Process();
            
            if (string.IsNullOrEmpty(workDirectory))
            {
                throw new Exception("请先设置FTPServer所在目录！");
            }
            //声明一个程序信息类
            ProcessStartInfo ftpInfo = new ProcessStartInfo();

            ftpInfo.FileName = "ServUAdmin.exe";
           // ftpInfo.FileName = " ServUDaemon.exe";

            ftpInfo.WorkingDirectory = workDirectory;

            ftpInfo.CreateNoWindow = true;
           // ftpInfo.WindowStyle = ProcessWindowStyle.Hidden;

            #region 修改wftpd32.exe用户名\密码\路径信息
            //Version ver = System.Environment.OSVersion.Version;
            //if (ver.Major == 6 && ver.Minor == 1)  
            //{//Windows7
            //    iniFilePath += GetUserName() + "\\AppData\\Local\\VirtualStore\\Windows\\wftpd32.INI";
            //    //iniFilePath = "C:\\Users\\xian.wang\\AppData\\Local\\VirtualStore\\Windows\\wftpd32.INI";
            //    if (!File.Exists(iniFilePath))
            //        iniFilePath = defaultFilePath;
            //}
            //else if (ver.Major == 5 && ver.Minor == 1)
            //{//WindowsXP
            //    iniFilePath = defaultFilePath;
            //}
            //else if (ver.Major == 5 && ver.Minor == 2)
            //{//WindowsServer2003
            //    iniFilePath = defaultFilePath;
            //}
            //else
            //{
            //    MessageBox.Show("仅支持WindowsXP、WindowsServer2003、Windows7系统","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //    return;
            //}
            //iniFilesOperate = new INIFilesOperate(iniFilePath);
            ////读取对应Section的信息
            //StringCollection strColetion=new StringCollection();
            //iniFilesOperate.ReadSection("Passwd", strColetion);   
            ////向指定Section写入信息
            //iniFilesOperate.WriteString("Passwd", "hirain", "D;Y05S4g80ul.");
            //iniFilesOperate.WriteString("Homes", "hirain", "C:\\");
            #endregion

            Process currentProc = Process.GetCurrentProcess();
            Process[] localByNameProc = Process.GetProcessesByName("ServUAdmin");                     
            if (localByNameProc.Length != 0)
            {
                procFtp = localByNameProc[0];             
                return;
            }
            try
            {
                
                procFtp = Process.Start(ftpInfo);
                
                Enabled = true;
                
                //Enabled = true;
                Thread.Sleep(2000);
               // ftpInfo.WindowStyle = ProcessWindowStyle.Minimized;
               // ftpInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procFtp.CloseMainWindow();
            }
            catch
            {
                Enabled = false;
                throw new Win32Exception(string.Format("在{0}目录下\r\n找不到指定的程序文件：{1}", ftpInfo.WorkingDirectory, ftpInfo.FileName));
            }
        }

        /// <summary>
        /// 终止ftp
        /// </summary>
        public void Stop()
        {
            if (null == procFtp)
                return;
            if (procFtp.HasExited == false)           
            {
                procFtp.Kill();
                Enabled = false;
            }
                
        }
        /// <summary>
        /// 获取系统用户名
        /// </summary>
        /// <returns></returns>
        //private string GetUserName()
        //{
        //    try
        //    {
        //        string st = "";
        //        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            st = mo["UserName"].ToString();
        //        }
        //        moc = null;
        //        mc = null;
        //        return st;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //    finally
        //    {

        //    }
        //}

        /// <summary>
        /// 获取系统登录名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            try
            {
                return System.Environment.UserName;
                
            }
            catch
            {
                return "unknow";
            }
        }
    }
}
