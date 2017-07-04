using HRP1679.BLL.FileOperation;
using HRP1679.BLL.NetCtrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HRP1679.BLL.BoardCtrl
{
  public class SlaveCtrl
    {

      private static SlaveCtrl instance = null;
      private static object obj = new object();
      public static SlaveCtrl Instance
      {
          get { 
          lock(obj)
          {
              instance = instance ?? new SlaveCtrl();
              return instance;
          }
         
          }
      }

      private TCPClient slaveClient;
      public  AutoResetEvent autoResetEvent = new AutoResetEvent(false);  
      public bool IsConnect
      {
          get { return slaveClient.IsConnect; }
      }
      public SlaveCtrl()
      {
          slaveClient = new TCPClient();
          slaveClient.DataHanleEvent += NetHandler.Instance.SlaveDepack;
       
      }
      public void Connect()
      {
          slaveClient.BeginConnect();
      }
      /// <summary>
      /// 下位机命令字下发
      /// </summary>
      /// <param name="data"></param>
      public bool SendMessageToSlave(byte[] data)
      {  //
          slaveClient.SendMessage(data);
        bool result=  autoResetEvent.WaitOne(10000); //最多等待10秒反馈结果
        return result;
      }
  


    }
}
