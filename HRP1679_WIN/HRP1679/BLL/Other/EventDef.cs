using Hirain.Lib.Common;
using Hirain.Lib.HRDRFM800M;
using HRP1679.BLL.BoardCtrl;
using HRP1679.BLL.NetCtrl;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HRP1679.BLL.Other
{
    public class EventDef
    {
        private static EventDef instance = null;
        private static object obj = new object();
        public static EventDef Instance
        {
            get
            {
                lock (obj)
                {
                    instance = instance ?? new EventDef();
                    return instance;
                }
            }
        }
        public static void TransferData()
        {
            new Thread(
                new ThreadStart((Action)(() =>
                {
                    //发送上传文件命令字
                    //获取blocks对应的cell号与长度
                    //for
                    // List<ParaDMA>=( from a in ParaUI.Instance.paraCollect.ManageFiles.Files where a.CellNum==)
                    //var lengthlist = (from a in ParaUI.Instance.paraCollect.ManageFiles.Files
                    //                  where ParaUI.Instance.paraCollect.TransferCell.Contains(a.CellNum)
                    //                  select new KeyValuePair<uint , ulong>(a.CellNum , a.FileLength)).ToList();
                    //foreach (var item in lengthlist)
                    //{
                    //    ParaUI.Instance.paraCollect.UploadCell = item.Key;
                    //    ParaUI.Instance.paraCollect.UpLoadLongLength = (long)item.Value;
                    //    DMACtrl.Instance.SendMessageToDMA(NetCtrl.NetHandler.Instance.DMAPack(NetCtrl.DMASCmdType.DataTransfer));
                    //}
                }))
                ) { IsBackground = true }.Start();
        }
        public static void EraseData()
        {
            new Thread(
              new ThreadStart((Action)(() =>
              {
                  DMACtrl.Instance.SendMessageToDMA(NetCtrl.NetHandler.Instance.DMAPack(NetCtrl.DMASCmdType.DataErase));
              }))
              ) { IsBackground = true }.Start();
        }
        public static void SetDataSource()
        {
            new Thread(
               new ThreadStart((Action)(() =>
               {
                   DMACtrl.Instance.SendMessageToDMA(NetCtrl.NetHandler.Instance.DMAPack(NetCtrl.DMASCmdType.SetDataSource));
               }))
               )
            { IsBackground = true }.Start();
        }

        public static void SefCheck()
        {
            //基带
            //下位机  --反射内存板、通用控制板
            //存储板
            new Thread(
             new ThreadStart((Action)(() =>
             {
                

                 try
                 {
                     bool result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.SelfCheck));
                     if (result)
                     {
                         LoggingService.LogToShow("下位机控制板卡自检操作完成！" , InformationType.Success , InformationDisplayMode.FormList);
                     }
                     else
                         LoggingService.LogToShow("下位机控制板卡自检操作超时！" , InformationType.Error , InformationDisplayMode.FormList);
                     //    ParaUI.Instance.paraStatus.IMicroStatus = ParaUI.Instance.paraStatus.State;
                 }
                 catch (Exception ex)
                 {
                     System.Windows.Forms.MessageBox.Show("Slave:" + ex.Message);
                     return;
                 }
                 try
                 {
                     // DMACtrl.Instance.SendMessageToDMA(NetHandler.Instance.DMAPack(DMASCmdType.SelfCheck));
                     DMACtrl.Instance.tcpclient[0].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.SelfCheck));
                     ParaUI.Instance.paraStatus.DMAOne = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常"; //自检、复位结果
                     DMACtrl.Instance.tcpclient[1].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.SelfCheck));
                     ParaUI.Instance.paraStatus.DMATwo = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常";
                     DMACtrl.Instance.tcpclient[2].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.SelfCheck));
                     ParaUI.Instance.paraStatus.DMAThree = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常";
                 }
                 catch (Exception ex)
                 {
                     System.Windows.Forms.MessageBox.Show("DMA:" + ex.Message);
                     return;
                 }
             }))
             ) { IsBackground = true }.Start();
        }

        public static void Reset()
        {
            new Thread(
              new ThreadStart((Action)(() =>
              {              
                  try
                  {
                      SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.SoftReset));
                  }
                  catch (Exception ex)
                  {
                      System.Windows.Forms.MessageBox.Show("Slave:" + ex.Message);
                      return;
                  }
                  try
                  {
                      //   DMACtrl.Instance.SendMessageToDMA(NetHandler.Instance.DMAPack(DMASCmdType.Reset));
                      DMACtrl.Instance.tcpclient[0].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.Reset));
                      ParaUI.Instance.paraStatus.DMAOne = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常"; //自检、复位结果
                      DMACtrl.Instance.tcpclient[1].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.Reset));
                      ParaUI.Instance.paraStatus.DMATwo = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常";
                      DMACtrl.Instance.tcpclient[2].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.Reset));
                      ParaUI.Instance.paraStatus.DMAThree = Convert.ToBoolean(ParaUI.Instance.paraCollect.ResetResult) ? "正常" : "异常";
                  }
                  catch (Exception ex)
                  {
                      System.Windows.Forms.MessageBox.Show("DMA:" + ex.Message);
                      return;
                  }

              }))
              ) { IsBackground = true }.Start();
        }

        public static void Start()
        {
            new Thread(
              new ThreadStart((Action)(() =>
              {
                  ParaUI.Instance.paraStatus.IBtnStartEnable = false;
                  #region
                
                  try
                  {
                      bool result = false;
                      if (ParaUI.Instance.paraSignal.SignalType == 0)//只有回放模式发
                      {
                          result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.DownloadSignalFile));
                          if (result)
                              LoggingService.LogToShow("信号产生文件下发成功", InformationType.Success, InformationDisplayMode.FormList);
                          else
                          {
                              LoggingService.LogToShow("信号产生文件下发超时", InformationType.Warning, InformationDisplayMode.FormList);
                              ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                              return;
                          }
                      }
                      result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.DownloadCeofFile));
                      if (result)
                          LoggingService.LogToShow("滤波器系数文件下发成功", InformationType.Success, InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("滤波器系数文件下发超时", InformationType.Warning, InformationDisplayMode.FormList);
                          ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                          return;
                      }
                      result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.SetDRFMCtrlWord));
                      if (result)
                          LoggingService.LogToShow("基带命令字下发成功", InformationType.Success, InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("基带命令字下发超时", InformationType.Warning, InformationDisplayMode.FormList);
                          ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                          return;
                      }
                      result =  SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.SetMicroWavePara));
                      if (result)
                          LoggingService.LogToShow("微波参数设置成功", InformationType.Success, InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("微波参数设置超时", InformationType.Warning, InformationDisplayMode.FormList);
                          ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                          return;
                      }
                      if (ParaUI.Instance.paraTrajector.TrajectorType==1) //模拟仿真机时发送
                      {
                          HRP1679.BLL.FileOperation.ExcelOperate.Instance.ReadInTrajSheet();//读取
                          result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.BoudParaPackPortOne));
                          if (result)
                              LoggingService.LogToShow("端口1参数设置成功", InformationType.Success, InformationDisplayMode.FormList);
                          else
                          {
                              LoggingService.LogToShow("端口1参数设置超时", InformationType.Warning, InformationDisplayMode.FormList);
                              ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                              return;
                          }
                          result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.BoudParaPackPortTow));
                          if (result)
                              LoggingService.LogToShow("端口2参数设置成功", InformationType.Success, InformationDisplayMode.FormList);
                          else
                          {
                              LoggingService.LogToShow("端口2参数设置超时", InformationType.Warning, InformationDisplayMode.FormList);
                              ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                              return;
                          }
                      }
                      result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.Start));
                      if (result)
                          LoggingService.LogToShow("启动试验成功", InformationType.Success, InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("启动试验超时", InformationType.Warning, InformationDisplayMode.FormList);
                          ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                          return;
                      }
                  }
                  catch (Exception ex)
                  {
                      LoggingService.LogToShow("Slave:" + ex.Message, InformationType.Error, InformationDisplayMode.FormList);
                      ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                      return;
                  }

                  #endregion
                  //   DMACtrl.Instance.tcpclient[0].SendMessage(NetHandler.Instance.DMAPack(DMASCmdType.GetFileFolder));

              }))
              ) { IsBackground = true }.Start();
        }
        public static void Stop()
        {
         
            new Thread(
              new ThreadStart((Action)(() =>
              {
                  
                  try
                  {
                      ParaUI.Instance.paraStatus.IBtnStartEnable = true;
                      bool result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.Stop));
                      if (result)
                          LoggingService.LogToShow("停止试验操作成功" , InformationType.Success , InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("停止试验操作超时" , InformationType.Warning , InformationDisplayMode.FormList);
                          return;
                      }
                      result = SlaveCtrl.Instance.SendMessageToSlave(NetHandler.Instance.SlavePack(SlaveSCmdType.SoftReset));
                      if (result)
                          LoggingService.LogToShow("软复位成功" , InformationType.Success , InformationDisplayMode.FormList);
                      else
                      {
                          LoggingService.LogToShow("软复位超时" , InformationType.Warning , InformationDisplayMode.FormList);
                          return;
                      }
                  }
                  catch (Exception ex)
                  {
                      LoggingService.LogToShow("Slave:" + ex.Message , InformationType.Error , InformationDisplayMode.FormList);
                  }

              }))
              ) { IsBackground = true }.Start();
        }
        public static void Initialation()
        {
            new Thread(
                 new ThreadStart((Action)(() =>
                 {
                    
                     //下位机
                     SlaveCtrl.Instance.Connect();
                  //   DMACtrl.Instance.tcpclient[0].BeginConnect();
                 }))
                 ) { IsBackground = true }.Start();
            // SlaveCtrl.Instance
        }
    }
}
