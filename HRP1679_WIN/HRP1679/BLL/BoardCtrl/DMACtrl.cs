using HRP1679.BLL.NetCtrl;
using HRP1679.DAL.Common;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRP1679.BLL.BoardCtrl
{
    public class DMACtrl
    {
        private static DMACtrl instance = null;
        private static object obj = new object();
        public static DMACtrl Instance
        {
            get
            {
                lock (obj)
                {
                    instance = instance ?? new DMACtrl();
                    return instance;
                }
            }
        }

       public  TCPClient[] tcpclient = new TCPClient[3];
       
        public DMACtrl()
        {
            tcpclient[0] = new TCPClient(ConstData.NetDMAConfigPath1);
            tcpclient[1] = new TCPClient(ConstData.NetDMAConfigPath2);
            tcpclient[2] = new TCPClient(ConstData.NetDMAConfigPath3);
          
            foreach (TCPClient client in tcpclient)
            {
                client.DataHanleEventDMA+= NetHandler.Instance.DMADepack;
            }
        }
       ///// <summary>
       // /// /命令字下发
       ///// </summary>
       ///// <param name="data"></param>
       // public void SendMessageToDMA(uint[] data)
       // {
       //     byte[] arrbyte =new byte[sizeof(int)*data.Length];
       //     unsafe
       //     {
       //         fixed (uint* pUint = data)
       //         {
       //             byte* pByte = (byte*)pUint;
       //             for (int i = 0; i < data.Length; i++)
       //             {
       //                 arrbyte[i] = pByte[i];
       //             }
       //         }
       //     }

       //     foreach (TCPClient client in tcpclient)
       //     {
       //         client.SendMessage(arrbyte);
       //     }
       // }
        /// <summary>
        /// /命令字下发
        /// </summary>
        /// <param name="data"></param>
        public void SendMessageToDMA(byte[] data)
        {
            tcpclient[0].SendMessage(data);
            tcpclient[1].SendMessage(data);
            tcpclient[2].SendMessage(data);
        }
        
    }

}
