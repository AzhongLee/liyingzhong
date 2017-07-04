using Hirain.Lib.Common;
using Hirain.Lib.HRDRFM800M;
using Hirain.Lib.HwDriver;
using Hirain.Lib.IO;
using HRP1679.BLL.FileOperation;
using HRP1679.DAL.Common;
using HRP1679.DAL.Para;
using Jungo.pci_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HRP1679.BLL.BoardCtrl
{
   public class DRFMCtrl
    {
        #region 单例
       private static DRFMCtrl instance = null;
       public static DRFMCtrl Instance
        {
            get
            {
                    instance =instance?? new DRFMCtrl();
                return instance;
            }
        }
        #endregion


        #region 变量定义
        /// <summary>
        /// 基带板卡
        /// </summary>
        public myDevice DevBaseBorad = new myDevice();
        /// <summary>
        /// 设备初始化结果字串
        /// </summary>
        public List<DevIniResultInfo> strDevIniResult = new List<DevIniResultInfo>();
        /// <summary>
        /// 800M板卡
        /// </summary>
        public HRDRFM800M Dev800M = new HRDRFM800M();
        /// <summary>
        /// 800M操作的基址空间
        /// </summary>
        public const Hirain.Lib.HwDriver.Bar operateBAR = Hirain.Lib.HwDriver.Bar.Bar3;
        /// <summary>
        /// 通用控制板设备句柄
        /// </summary>
        public static PCI_Device DevHrCtrlBoard = null;

        private static List<uint> StaticCtrlWords = null;

        #endregion

        #region 初始化目标设备
        /// <summary>
        /// 初始化目标设备
        /// </summary>
        public void DeviceIniBaseBorad(uint vid , uint did , string devdescription)
        {
            string strError = string.Empty;


            //初始化设备信息
            DevBaseBorad.deviceinfo.DeviceID = did;
            DevBaseBorad.deviceinfo.VendorID = vid;
            DevBaseBorad.deviceinfo.deviceInstruction = devdescription;
            //打开指定板卡
            try
            {
                //打开基带板卡
                DRFMCtrl.Instance.Dev800M.PciDev = WinDriverHelper.Instance.OpenSpecificDevice(DevBaseBorad.deviceinfo.DeviceID , DevBaseBorad.deviceinfo.VendorID , DevBaseBorad.deviceinfo.deviceInstruction)[0];
                DRFMCtrl.instance.Dev800M.Is800MBoard = true;
                DRFMCtrl.instance.Dev800M.device = DRFMCtrl.instance.Dev800M.PciDev;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "警告" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                DRFMCtrl.instance.Dev800M.Is800MBoard = false;
            }

        }
        public void DeviceIniMvBoard(uint vid , uint did , string devdescription)
        {
            string strError = string.Empty;
            try
            {
                //打开指定板卡
                DRFMCtrl.DevHrCtrlBoard = WinDriverHelper.Instance.OpenSpecificDevice(did , vid , devdescription)[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "警告" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }

        }


        #endregion

        #region 获取测频信息
        /// <summary>
        /// 获取测频信息
        /// </summary>
        /// <returns></returns>
        public FreqMeasureInfo GetFreqMeasureInfo()
        {
            FreqMeasureInfo mout = new FreqMeasureInfo();
            //测频值
            Dev800M.ReadAddrData(operateBAR , (uint)enumBaseBoradRegs.fr , ref mout.freq);
            //pri
            Dev800M.ReadAddrData(operateBAR , (uint)enumBaseBoradRegs.pri , ref mout.pri);
            //pw
            Dev800M.ReadAddrData(operateBAR , (uint)enumBaseBoradRegs.pw , ref mout.pw);
            //频段/角度

            Dev800M.ReadAddrData(operateBAR , (uint)enumBaseBoradRegs.Azi , ref mout.Azi);
            Dev800M.ReadAddrData(operateBAR , (uint)enumBaseBoradRegs.Pitch , ref mout.Pitch);
            //mout.freqRange = freqrangetemp;
            return mout;
        }
        #endregion

        #region "发送基带控制字"
        /// <summary>
        /// 发送基带控制字
        /// </summary>
        /// <returns></returns>
        public void SendBaseBoradCtrlPackage(List<uint> sendData)
        {
            Dev800M.WriteAddrBlock(operateBAR , (uint)enumBaseBoradRegs.ctrlpackagestart , sendData);
            //  Dev800M.WriteAddrBlock(operateBAR, (uint)enumBaseBoradRegs.ctrlpackagestartDA, sendData);
        }


        #endregion

        #region "发送基带复位控制字"
        /// <summary>
        /// 发送基带复位控制字
        /// </summary>
        /// <returns></returns>
        public void SendResetCtrlPackage()
        {
            if (Dev800M.PciDev != null)
            {
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)1);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
                Dev800M.WriteAddrData(operateBAR, (uint)0x000000, (uint)0);
             
            }
        }
        #endregion

        #region "发送滤波器系数"
        public void SendCoef(List<uint> sendData , uint mbitsel)
        {

            Dev800M.WriteAddrBlock(operateBAR , (uint)enumBaseBoradRegs.coefstart , sendData);
        }
        /// <summary>
        /// 反补偿滤波器系数
        /// </summary>
        public void CoefDownLoad()
        {
            try
            {
                LoggingService.LogToShow("开始加载滤波器系数……" , InformationType.Normal , InformationDisplayMode.FormList);
                #region 读取滤波器系数               
                List<uint> senddata = GetCirDataPackage(ConstData.CoefFile);

                #endregion

                #region 发送滤波器系数
                if (DRFMCtrl.Instance.Dev800M.PciDev != null)
                {
                    DRFMCtrl.Instance.SendCoef(senddata , senddata[senddata.Count - 1]);
                    LoggingService.LogToShow("滤波器系数成功" , InformationType.Success , InformationDisplayMode.FormList);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                LoggingService.LogToShow(ex.Message , InformationType.Error , InformationDisplayMode.FormList);
            }
        }
        /// <summary>
        /// 得到反补偿滤波器系数  
        /// </summary>
        /// <returns></returns>
        public static List<uint> GetCirDataPackage(string CirFileName)
        {
            List<uint> reslutList = new List<uint>();
            string tempData = string.Empty;
            using (FileStream fileStream = new FileStream(CirFileName , FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        tempData = reader.ReadLine();
                        reslutList.Add(Convert.ToUInt32(tempData.Trim() , 10));
                    }
                }
            }
            return reslutList;
        }
        #endregion

        #region "FPGA程序加载"
        public void FPGABinDownLoad(string filePathAD , string filePathDA)
        {
            LoggingService.logShowed("开始下载FPGA程序..." , InformationType.Normal , InformationDisplayMode.FormList);
            Dev800M.PciDev = DRFMCtrl.Instance.Dev800M.PciDev;
            Dev800M.AdcFpgaDownLoad(filePathAD);
            Dev800M.DacFpgaDownLoad(filePathDA);
            LoggingService.logShowed("FPGA程序下载成功！" , InformationType.Success , InformationDisplayMode.FormList);
            Thread.Sleep(1000);

        }
        #endregion

        #region "发送信号产生文件"
        public void SendSignalData(List<uint> sendData)
        {
            Dev800M.WriteAddrData(operateBAR, (uint)enumBaseBoradRegs.SignalDataLength, (ulong)sendData.Count);

            Dev800M.WriteAddrBlock(operateBAR, (uint)enumBaseBoradRegs.SignalData0, sendData);



        }


        #endregion



        /// <summary>
        /// 滤波器系数加载执行函数
        /// </summary>
        public void CoefDownLoad(string coeFilePath)
        {
            try
            {

                #region 读取滤波器系数

                string CoefFilePath = coeFilePath;
                ExcelOperator.IsContainedHead = true;

                List<string> lststrCoef = new List<string>();
                FileStream fs = new FileStream(coeFilePath , FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() >= 0)
                {
                    string strTemp = sr.ReadLine();
                    strTemp.Trim();
                    lststrCoef.Add(strTemp);
                }
                //滤波器系数转换格式
                List<uint> senddata = ConvertLstStringToLstUnit(lststrCoef);

                #endregion

                #region 发送滤波器系数
                DRFMCtrl.Instance.SendCoef(senddata , senddata[senddata.Count - 1]);
                #endregion

            }
            catch (System.Exception ex)
            {
                LoggingService.LogToShow(ex.Message , InformationType.Error , InformationDisplayMode.FormList);
            }
        }


        /// <summary>
        /// 信号产生文件加载执行函数
        /// </summary>
        public void SignalFileDownLoad(string signalFilePath)
        {
            try
            {

                #region 读取信号产生文件
                
                List<string> lststrCoef = new List<string>();
                FileStream fs = new FileStream(signalFilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() >= 0)
                {
                    string strTemp = sr.ReadLine();
                    strTemp.Trim();
                    lststrCoef.Add(strTemp);
                }
                //滤波器系数转换格式
                List<uint> senddata = ConvertLstStringToLstUnit(lststrCoef);

                #endregion

                #region 发送信号产生文件
                DRFMCtrl.Instance.SendSignalData(senddata);
                #endregion

            }
            catch (System.Exception ex)
            {
                LoggingService.LogToShow(ex.Message, InformationType.Error, InformationDisplayMode.FormList);
            }
        }

        #region stringList转uintList
        public static List<uint> ConvertLstStringToLstUnit(List<string> mIn)
        {
            List<uint> mOut = new List<uint>();
            for (int i = 0; i < mIn.Count; i++)
            {
                string temp = Convert.ToString(Convert.ToInt32(mIn[i]) , 16);//先转换成16进制的字符串
                mOut.Add(Convert.ToUInt32(temp , 16));//再转换成uint32
            }
            return mOut;
        }
        #endregion

        #region"基带命令字打包"
        public static List<uint> CalcCtrlWord(ParaUI paraUI)
        {
            List<uint> CtrlW = new List<uint>();
            //第一个命令字
            uint ctrlWord = paraUI.paraSignal.SignalType | paraUI.paraCollect.WorkModel << 4 | paraUI.paraSignal.SignalDataType << 8 | paraUI.paraDebug.PRFModel << 9 | paraUI.paraCollect.FrequencyModel << 10 | paraUI.paraCollect.ADch1 << 11 | paraUI.paraCollect.ADch2 << 12 | paraUI.paraCollect.ADch3 << 13 | paraUI.paraCollect.WorkStart << 14 | paraUI.paraCollect.CollectStart << 15;
            CtrlW.Add(ctrlWord);
            //点频测试2
            ctrlWord = (uint)(paraUI.paraCollect.CenterFrequency / ConstData.Fs * Math.Pow(2 , 32));
            CtrlW.Add(ctrlWord);
            //PRF周期3
            ctrlWord = (uint)(paraUI.paraSignal.PRFCycle*300);
            CtrlW.Add(ctrlWord);
            //PRF脉宽4
            ctrlWord = (uint)(paraUI.paraSignal.PRFPulseWidth * 300);
            CtrlW.Add(ctrlWord);
            //起始频率5
            if ((paraUI.paraSignal.SignalType == 1) || (paraUI.paraSignal.SignalType == 2)) //修改命令字 20170704
            {
                //起始频率5
                ctrlWord = (uint)(paraUI.paraSignal.StartFrequency * 1e6 / ConstData.Fs * Math.Pow(2, 32));
                CtrlW.Add(ctrlWord);
            }
            else
            {  //起始频率5
                ctrlWord = (uint)((paraUI.paraSignal.StartFrequency * 1e6 - paraUI.paraSignal.BandWidth * 1e6 / 2) / ConstData.Fs * Math.Pow(2, 32));
                CtrlW.Add(ctrlWord);

            }
            //调频斜率6         20170704
            ctrlWord = (uint)(paraUI.paraSignal.BandWidth / paraUI.paraSignal.PRFPulseWidth * Math.Pow(10, 6) / 300 / ConstData.Fs * 8 * Math.Pow(2, 32));
            CtrlW.Add(ctrlWord);
            //检波值7
            ctrlWord = (uint)(paraUI.paraDebug.Detection);
            CtrlW.Add(ctrlWord);
            //固有延迟8
            ctrlWord = (uint)(paraUI.paraDebug.InherentDelay*0.3);
            CtrlW.Add(ctrlWord);
            //frame周期
            ctrlWord = (uint)(paraUI.paraSignal.FramePeriod * 300);
            CtrlW.Add(ctrlWord);
            //frame脉宽
            ctrlWord = (uint)(paraUI.paraSignal.FramePulseWidth * 300);
            CtrlW.Add(ctrlWord);
            //帧内prf个数
            ctrlWord = paraUI.paraSignal.PrfNum;
            CtrlW.Add(ctrlWord);
            //组内帧个数
            ctrlWord = paraUI.paraSignal.FrameNum ;
            CtrlW.Add(ctrlWord);
            //保留
            CtrlW.Add(0);
            //codenum
            ctrlWord = paraUI.paraSignal.CodeLength;
            CtrlW.Add(ctrlWord);
            //codeword
            ctrlWord = paraUI.paraSignal.CodeWord;
            CtrlW.Add(ctrlWord);
            //fcstep      20170704
            ctrlWord = (uint)(paraUI.paraSignal.FrequencyStep * Math.Pow(10, 6) / ConstData.Fs * Math.Pow(2, 32));
            CtrlW.Add(ctrlWord);
            //反补长截位
            ctrlWord = paraUI.paraDebug.CeofCut;
            CtrlW.Add(ctrlWord);         //添加到包内 20170704
            //dds频率10
            //ctrlWord = (uint)(paraUI.paraDebug.FrequencyDDS *Math.Pow(2,32)/2400);
            //CtrlW.Add(ctrlWord);
            ////待定
            //CtrlW.Add(0);
            //CtrlW.Add(0);
            //文件数据
            TxtOperation.ReadTxtFile(ConstData.DRFMData , ref CtrlW , "回车换行");
            //剩余
            while (CtrlW.Count < 256) CtrlW.Add(0);//长度改为256  20170704
           // while (CtrlW.Count <512 ) CtrlW.Add(0);
            return CtrlW;
        }

        #endregion


    }
   public class myDevice
   {
       /// <summary>
       /// 设备句柄
       /// </summary>
       public PCI_Device pDev;
       /// <summary>
       /// 设备信息
       /// </summary>
       public DeviceInfo deviceinfo = new DeviceInfo();
       /// <summary>
       /// 初始化状态
       /// </summary>
       public bool IniResult;
   }
   public class DeviceInfo
   {
       /// <summary>
       /// VID
       /// </summary>
       public uint VendorID;
       /// <summary>
       /// DID
       /// </summary>
       public uint DeviceID;
       /// <summary>
       /// BID
       /// </summary>
       public uint BoradID;
       /// <summary>
       /// 板卡描述
       /// </summary>
       public string deviceInstruction;
   }
   public class DevIniResultInfo
   {
       public string sTRResult;
       public InformationType infotype;
   }
   public class FreqMeasureInfo
   {
       /// <summary>
       /// 原始测频值
       /// </summary>
       public uint freq;
       /// <summary>
       /// 原始周期
       /// </summary>
       public uint pri;
       /// <summary>
       /// 原始脉宽
       /// </summary>
       public uint pw;
       /// <summary>
       ///  方位角
       /// </summary>
       public uint Azi;
       /// <summary>
       ///  俯仰角
       /// </summary>
       public uint Pitch;
   }

   public enum enumBaseBoradRegs
   {
       /// <summary>
       /// 基带测频值fr
       /// </summary>
       fr = 0x00001C ,

       /// <summary>
       /// 周期sys_prf_c
       /// </summary>
       pri = 0x000020 ,

       /// <summary>
       /// 脉宽sys_prf_w
       /// </summary>
       pw = 0x000024 ,

       /// <summary>
       /// 方位角
       /// </summary>
       Azi = 0x000028 ,

       /// <summary>
       /// 俯仰角
       /// </summary>
       Pitch = 0x00002C ,

       /// <summary>
       /// 滤波器系数起始地址
       /// </summary>
       coefstart = 0x380000 ,
       /// <summary>
       /// 滤波器系数bit_sel
       /// </summary>
       bitsel = 0x3800C0 ,
       /// <summary>
       /// 基带控制字包AD节点起始地址
       /// </summary>
       ctrlpackagestart = 0x0C0000 ,//修改地址 20170704
       ///// <summary>
       ///// 基带控制字包DA节点起始地址
       ///// </summary>
       //ctrlpackagestartDA = 0x2C0000 ,
       /// <summary>
       /// 基带停止复位控制字包AD节点起始地址
       /// </summary>
       ctrlpackagestop = 0x000000 ,
       /// <summary>
       /// 基带停止复位控制字包DA节点起始地址
       /// </summary>
       ctrlpackagestopDA = 0x200000,
              /// <summary>
              /// 信号产生文件长度
              /// </summary>
        SignalDataLength = 0x000020,
        /// <summary>
        /// 信号产生文件地址
        /// </summary>
        SignalData0 = 0x100000,
    }
}
