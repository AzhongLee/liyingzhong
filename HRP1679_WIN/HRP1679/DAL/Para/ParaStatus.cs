using HRP1679.BLL.BoardCtrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using HRP1679.UI;
using Hirain.Lib.Common;

namespace HRP1679.DAL.Para
{
    [Serializable]
    public class ParaStatus : INotifyPropertyChanged 
    {
        [field:NonSerializedAttribute]
        public event PropertyChangedEventHandler PropertyChanged;
           
        private  void RaisePropertyChanged(string propertyName)
        {
           
            var handler = this.PropertyChanged;
            if (handler != null)
            {
               handler(this , new PropertyChangedEventArgs(propertyName));
            }
        }

       public ParaStatus()
       {
           DRFMStatus = MicroStatus = dMAOne = dMATwo = dMAThree = MemStatus = GCStatus = slaveStatus = "异常";
        }

       private bool btnStartEnable = true;
       /// <summary>
       /// 开始按钮显示控制
       /// </summary>
       public bool BtnStartEnable
       {
           get { return btnStartEnable; }
           set
           {
               btnStartEnable = value; this.RaisePropertyChanged("BtnStartEnable");
           }
       }
       public  bool IBtnStartEnable
       {
           set { 
           //   FormMain.Instance.m_SyncContext.Post((x) => { BtnStartEnable = Boolean.Parse(x.ToString());  } , value);
             ParaUI.UIhandler.Invoke((Action)(() => { this.BtnStartEnable = value; }),null);
           }
       }
       
        private string drfmstatus;
        public string DRFMStatus { get { return drfmstatus; } set { drfmstatus = value; this.RaisePropertyChanged("DRFMStatus"); } }
        /// <summary>
        /// 基带板卡
        /// </summary>
        public bool IDRFMStatus
        {
            set {
                   ParaUI.UIhandler.Invoke((Action)(() => {   
                if (value == true)
                    DRFMStatus = "正常";
                else
                    DRFMStatus = "异常";
                   }) , null);
            }
        }
        /// <summary>
        /// 基带板卡自检状态
        /// </summary>
       public bool IDRFMCheck
        {
            set {
                ParaUI.UIhandler.Invoke((Action)(() => {
                    if (value == true)
                        DRFMStatus = "正常";
                    else
                        DRFMStatus = "自检异常";
                }), null);
            }
        }

        private string microstatus;
        public string MicroStatus { get { return microstatus; } set { microstatus = value; this.RaisePropertyChanged("MicroStatus"); } }
        /// <summary>
        /// 微波自检状态
        /// </summary>
        public bool IMicroStatus
        {
            
            set {
                ParaUI.UIhandler.Invoke((Action)(() => { 
                if (value == true)
                    MicroStatus = "正常";
                else
                    MicroStatus = "异常";
                }) , null);
            }
        }

        private string slaveStatus;
        /// <summary>
        /// 下位机连接状态
        /// </summary>
        public string SlaveStatus
      {
          get { return slaveStatus; }
          set
          {
              slaveStatus = value;
              this.RaisePropertyChanged("SlaveStatus");
          }
      }
        public bool ISlaveStatus
      {
          set
          {
                 ParaUI.UIhandler.Invoke((Action)(() => { 
              if (SlaveCtrl.Instance.IsConnect)
                  SlaveStatus = "正常";
              else
                  SlaveStatus = "异常";
                 }) , null);
          }
      }


      
        private string dMAOne;
        /// <summary>
        /// 存储板1
        /// </summary>
        public string DMAOne
        {
            get { return dMAOne; }
            set
            {
                dMAOne = value;
                this.RaisePropertyChanged("DMAOne");
            }
        }
        public bool IDMAOne
        {
            set
            {
                   ParaUI.UIhandler.Invoke((Action)(() => { 
                if (DMACtrl.Instance.tcpclient[0].IsConnect)
                    DMAOne = "正常";
                else
                    DMAOne = "异常";
                   }) , null);
            }
        }
       
        private string dMATwo;
        /// <summary>
        /// 存储板2
        /// </summary>
        public string DMATwo
        {
            get { return dMATwo; }
            set
            {
                dMATwo = value;
                this.RaisePropertyChanged("DMATwo");
            }
        }
        public bool IDMATwo
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() => {
                    if (DMACtrl.Instance.tcpclient[1].IsConnect)
                    DMATwo = "正常";
                else
                    DMATwo = "异常";}) , null);
               
            }
        }
       
        private string dMAThree;
        /// <summary>
        /// 存储板3
        /// </summary>
        public string DMAThree { 
            get{return dMAThree;} 
         set
            {
                dMAThree = value;
                this.RaisePropertyChanged("DMAThree");
            }
        }
        public bool IDMAThree
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() => {  
                    if (DMACtrl.Instance.tcpclient[2].IsConnect)
                    DMAThree = "正常";
                else
                    DMAThree = "异常";}) , null);
               
            }
        }
        public string GCStatus { get; set; }
        /// <summary>
        /// 通用控制板
        /// </summary>
        public bool IGCStatus
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() => {
                 if (value == true)
                    GCStatus = "正常";
                else
                    GCStatus = "自检异常";}) , null);
               
            }
        }
      
        private bool state;
       /// <summary>
       /// 操作执行状态
       /// </summary>
        public bool State
        {
            get { return state; }
            set { state = value;
            if (!value)
            {
                System.Windows.Forms.MessageBox.Show("下位机指令执行失败！");
            }
            }
        }

        private uint port;
       /// <summary>
       /// 端口号
       /// </summary>
        public uint Port
        {
          
            set { port = value;
                //if(state)
                //    LoggingService.LogToShow("端口"+port + "配置成功" , InformationType.Success , InformationDisplayMode.FormList);
                //else
                //    LoggingService.LogToShow("端口" + port + "配置失败" , InformationType.Success , InformationDisplayMode.FormList);
            }
        }
        public string MemStatus { get; set; }
       /// <summary>
       /// 反射内存卡
       /// </summary>
        public bool IMemSelfcheck
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() =>
                {
                  if (value == true)
                    MemStatus = "正常";
                else
                    MemStatus = "自检异常";}) , null);
              
            }
        }
      
        private bool initialized;
       /// <summary>
       /// 是否完成初始化
       /// </summary>
        public bool Initialized
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() =>
                {
                  if (value == false)
                {
                    GCStatus = "异常";
                    MemStatus = "异常";
                    DRFMStatus = "异常";
                }}) , null);
              
            }
        }
      
        private bool gCInit;
       /// <summary>
       /// 通用控制板初始化
       /// </summary>
        public bool IGCInit
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() =>
                {
                if (value == true)
                    GCStatus = "正常";
                else
                    GCStatus = "异常"; }) , null);
               
            }
        }
       
        private bool memInit;
       /// <summary>
       /// 反射内存卡初始化
       /// </summary>
        public bool IMemInit
        {
            set
            {
                ParaUI.UIhandler.Invoke((Action)(() =>
                {
                     if (value == true)
                    MemStatus = "正常";
                else
                    MemStatus = "异常";
                }) , null);
               
            }
        }

     
       
    }
}
