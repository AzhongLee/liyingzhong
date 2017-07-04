using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.DAL.Common
{
    public class ConstData
    {
        /// <summary>
        /// 实时计算时间间隔
        /// </summary>
        public const double TimeInterval = 5;
        /// <summary>
        /// 滤波器系数文件目录
        /// </summary>
        public static readonly string CoefFile = Path.Combine(Application.StartupPath , "Coef.txt");
        /// <summary>
        /// 是否开启调试
        /// </summary>
        public static bool IsDebugUsed = false;
        /// <summary>
        /// FGPA程序文件目录
        /// </summary>
        public static readonly string FpgaFile = Path.Combine(Application.StartupPath , "Fpga.dat");
        /// <summary>
        /// 基带数据源
        /// </summary>
        public static readonly string DRFMData = Path.Combine(Application.StartupPath , "DRFMData.txt");
        /// <summary>
        /// 最大输出频率
        /// </summary>
        public const double MaxPow = -20;//
        /// <summary>
        /// 采样率
        /// </summary>
        public const double Fs = 2400000000;
        /// <summary>
        /// 界面参数文件路径
        /// </summary>
        public static  string UIDat = Path.Combine(Application.StartupPath , "ParameterUI\\UIParadata.dat");
        #region"网络配置"
        /// <summary>
        /// 下位机
        /// </summary>
        public static  string NetSlaveConfigPath = Path.Combine(Application.StartupPath , "SlaveNetConfig\\NetConfig.xml");
        /// <summary>
        /// 存储板1
        /// </summary>
        public static  string NetDMAConfigPath1 = Path.Combine(Application.StartupPath , "DMANetConfig\\NetConfig1.xml");
        /// <summary>
        ///存储板2
        /// </summary>
        public static  string NetDMAConfigPath2 = Path.Combine(Application.StartupPath , "DMANetConfig\\NetConfig2.xml");
        /// <summary>
        /// 存储板3
        /// </summary>
        public static  string NetDMAConfigPath3 = Path.Combine(Application.StartupPath , "DMANetConfig\\NetConfig3.xml");
        /// <summary>
        /// 弹道轨迹文件路径
        /// </summary>
      //  public static  string TrajectorFilePath= Path.Combine(Application.StartupPath , "Trajectory\\Trajectory.xls");
        #endregion
    }
}
