using Hirain.Lib.Common;
using Hirain.Lib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.BLL.NetCtrl
{
    /// <summary>
    /// TCP异步通信服务器
    /// </summary>
    public  class TCPServer
    {
        private static TCPServer instance = null;
        private static object obj = new object();
        public static TCPServer Instance
        {
            get 
            {
                lock (obj)
                {
                    instance = instance ?? new TCPServer();
                    return instance;
                }
            }
        }

        #region"字段"
        private Socket server;
        private int serverPort;                       //服务器端口号
        private IPAddress serverIP;                   //服务器端IP地址
        #endregion
        /// <summary>
        /// 获取本地IPV4
        /// </summary>
        private string GetLocalIPV4()
        {
            IPAddress[] ipLoccal = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipl = null;
            foreach (IPAddress ip in ipLoccal)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipl = ip;
                    break;
                }
            }
            return ipl.ToString();
        }
        //public bool BeginConnect()
        //{

        //    //读取配置文件,获取服务器IP地址、端口号
        //    NetConfig netConfig = (NetConfig)XmlFileOperator.XmlDeSerializer(Application.StartupPath + "\\NetConfig.xml" , typeof(NetConfig));
        //    serverIP = IPAddress.Parse(netConfig.Ip);
        //    serverPort = netConfig.Port;
        //    if (GetLocalIPV4() != netConfig.Ip)
        //    {
        //        LoggingService.logShowed("本机IP地址不是" + netConfig.Ip , InformationType.Error , InformationDisplayMode.FormList);
        //        return false;
        //    }
        //    IPEndPoint ipe = new IPEndPoint(serverIP , serverPort);
        //    return true;
        //}

    }
}
