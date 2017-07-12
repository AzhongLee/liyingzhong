using Hirain.Lib.Common;
using Hirain.Lib.IO;
using HRP1679.BLL.Other;
using HRP1679.DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using HRP1679.DAL.Para;

namespace HRP1679.BLL.NetCtrl
{
    /// <summary>
    /// TCP异步通信客户端
    /// 2017.5.2
    /// 李迎忠
    /// </summary>
    public class TCPClient:INotifyPropertyChanged
    {
        #region"下位机单例"
        private static object obj = new object();
        private static TCPClient instance=null;
        public static TCPClient Instance
        {
            get
            {
                lock (obj)
                {
                    instance = instance ?? new TCPClient();
                    return instance;
                }
            }
        }
        #endregion

        #region"网络接收数据处理"
        /// <summary>
        /// 下位机数据包解析委托
        /// </summary>
        /// <param name="array"></param>
        public delegate void dataHandleDelegate(byte[] array);
        public event dataHandleDelegate DataHanleEvent;
        private void DataHandle(byte[] array)
        {
            if (DataHanleEvent != null)
                DataHanleEvent(array);
        }
        /// <summary>
        /// 存储板数据包解析委托
        /// </summary>
        /// <param name="array"></param>
        /// <param name="ip"></param>
        public delegate void dataHandleDelegateDMA(byte[] array,IPAddress ip);
        public event dataHandleDelegateDMA DataHanleEventDMA;
        private void DataHandle(byte[] array, IPAddress ip)
        {
            if (DataHanleEventDMA != null)
                DataHanleEventDMA(array,ip);
        }
        #endregion
        /// <summary>
        /// 链接上服务器时需要做的事情
        /// </summary>
        public delegate void connectedEventDelegate();
        public event connectedEventDelegate ConnectedEvent;
        private void OnConnected()
        {
            if (ConnectedEvent != null)
                ConnectedEvent();
        }


        #region"INotifypropertychanged接口实现"
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this , new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region"初始化TCPClient配置"
        public TCPClient():this(ConstData.NetSlaveConfigPath)  //用于下位机
        {
            
        }
        public TCPClient(string NetConfigPath)       //用于
        {
         //读取配置文件,获取服务器IP地址、端口号
            NetConfig netConfig = (NetConfig)XmlFileOperator.XmlDeSerializer(NetConfigPath , typeof(NetConfig));
            serverIP = IPAddress.Parse(netConfig.Ip);
            serverPort = netConfig.Port;
            BGWorker_Receive.DoWork += BGWorker_Receive_DoWork;
            BGWorker_Receive.RunWorkerCompleted += BGWorker_Receive_RunWorkerCompleted;
        }
        #endregion

        #region"变量"
        private int serverPort;                       //端口号
        private IPAddress serverIP;                   //服务器端IP地址
        private Socket client;                        //客户端套接字
        private bool isConnect = false;               //是否连接
        private bool isRead = false;                  //是否从网络流读取数据
        private int retryTimes = 0;                   //重连次数
        private NetHandler netHandler = new NetHandler();  //网络通信处理器
        NetworkStream netStream;                      //数据流
        byte[] netData;                               //接收数据
        private uint simCtrl;
        System.ComponentModel.BackgroundWorker BGWorker_Receive = new System.ComponentModel.BackgroundWorker();
        #endregion

        #region"属性"
        public bool IsConnect
        {
            get { return isConnect; }
            set
            {
                if (value != isConnect)
                {
                    isConnect = value;
                    ParaUI.Instance.paraStatus.IDMAOne = ParaUI.Instance.paraStatus.IDMATwo = ParaUI.Instance.paraStatus.IDMAThree = ParaUI.Instance.paraStatus.ISlaveStatus = value;
                }
            }
        }
        public IPAddress ServerIP
        {
            get { return this.serverIP; }
        }
        #endregion


        /// <summary>
        /// 获取本地IPV4
        /// </summary>
        private IPAddress GetLocalIPV4()
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
            return ipl;
        }
        /// <summary>
        /// tcp连接
        /// </summary>
        public bool BeginConnect()
        {
            
            IPEndPoint ipe = new IPEndPoint(serverIP , serverPort);
            client = new Socket(AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp);
            if (!IsConnect)
                client.BeginConnect(ipe , new AsyncCallback(Connect) , client);
            else
            {
                LoggingService.logShowed("已经与服务器连接！" , InformationType.Success , InformationDisplayMode.FormList);
                BGWorker_Receive.RunWorkerAsync();
            }
            return client.Connected;
        }

        private void Connect(IAsyncResult iar)
        {
            Socket socket = iar.AsyncState as Socket;
            try
            {
                socket.EndConnect(iar);
                LoggingService.logShowed("网络连接成功！" , InformationType.Success , InformationDisplayMode.FormList);
                retryTimes = 0;             //重连次数重新计数
                IsConnect = true;
                netStream = new NetworkStream(client);
                OnConnected();
                BGWorker_Receive.RunWorkerAsync();
            }
            catch (Exception)
            {
                IsConnect = false;
                LoggingService.logShowed("连接服务器失败！" , InformationType.Error , InformationDisplayMode.FormList);
                if (retryTimes++ > 20) return;
                LoggingService.logShowed("开始重连！" , InformationType.Warning , InformationDisplayMode.FormList);
                this.BeginConnect();
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGWorker_Receive_DoWork(object sender , System.ComponentModel.DoWorkEventArgs e)
        {
          
            try
            {
                while (!client.Poll(10 , SelectMode.SelectRead));
                if (IsConnect = client.Available > 0)
                {
                    netData = new byte[client.Available];
                    lock (netStream)
                    {
                        netStream.Read(netData , 0 , netData.Length);
                        isRead = true;
                    }
                }
            }
            catch(Exception)
            {
                IsConnect = false;
            }
       
        }
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGWorker_Receive_RunWorkerCompleted(object sender , System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (IsConnect)
            {
                if (isRead)
                {
                    DataHandle(netData,this.serverIP);
                    DataHandle(netData);
             }
            }
            else
            {
                if (retryTimes++ < 1) 
                LoggingService.logShowed("连接断开，开始重连！" , InformationType.Error , InformationDisplayMode.FormList);
                this.BeginConnect();
                return;
            }
            BGWorker_Receive.RunWorkerAsync();
        }

        public void Close()
        {
            if (IsConnect)
            {
                try
                {
                    BGWorker_Receive.CancelAsync();
                    client.Close();
                    IsConnect = isRead = false;
                    netData = null;
                    retryTimes = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        public void SendMessage(byte[] senddata)
        {

            if (IsConnect && netStream.CanWrite)
            {
                lock (netStream)
                {
                    netStream.Write(senddata , 0 , senddata.Length);
                }
            }
            else 
            {
             //   LoggingService.logShowed("网络未连接！" , InformationType.Error , InformationDisplayMode.FormList);
                System.Windows.Forms.MessageBox.Show("网络未连接！");
            }
        }
    }

    public class NetConfig
    {
        /// <summary>
        /// 下位机IP地址
        /// </summary>
        public string Ip;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port;
    }
}

