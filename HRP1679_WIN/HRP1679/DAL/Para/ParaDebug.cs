using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    [Serializable]
    public class ParaDebug
    {
        public ParaDebug()
        {
            pRFModel = 0;
            detection = 8076;
        }

        #region“生成采集参数”
        private uint pRFModel;
        private uint detection;
        private uint inherentDelay;
        private uint frequencyDDS;
        private uint clockReference;
        private uint clockFrequency;
        private uint ceofCut;
        private uint detectionswitch;

        /// <summary>
        /// 检波选择
        /// </summary>
        public uint Detectionswitch
        {
            get { return detectionswitch; }
            set { detectionswitch = value; }
        }

        /// <summary>
        /// 外参考频率
        /// </summary>
        public uint ClockFrequency
        {
            get { return clockFrequency; }
            set {
                if(value>=10&&value<=160&&value%10==0)
                clockFrequency = value; }
        }
        /// <summary>
        /// 时钟参考
        /// </summary>
        public uint ClockReference
        {
            get { return clockReference; }
            set { clockReference = value; }
        }
        /// <summary>
        /// DDS频率
        /// </summary>
        public uint FrequencyDDS
        {
            get { return frequencyDDS; }
            set { frequencyDDS = value; }
        }
        /// <summary>
        /// 固有延迟
        /// </summary>
        public uint InherentDelay
        {
            get { return inherentDelay; }
            set { inherentDelay = value; }
        }

        /// <summary>
        /// 检波值
        /// </summary>
        public uint Detection
        {
            get { return detection; }
            set { detection =value; }
        }
        /// <summary>
        /// PRF选择 
        /// </summary>
        public uint PRFModel
        {
            get { return pRFModel; }
            set { pRFModel =(uint) (value<=0?0:1); }
        }
        /// <summary>
        /// 反补偿滤波器截位
        /// </summary>
        public uint CeofCut { get { return ceofCut; } set { ceofCut = value; } }


        /// <summary>
        /// 时序选择
        /// </summary>
        public uint TimingSwith { get { return timingSwith; } set { timingSwith = value; } }
        /// <summary>
        /// 时序间隔
        /// </summary>
        public uint TimingInterval { get { return timingInterval; } set { timingInterval = value; } }
        /// <summary>
        /// PRF选择
        /// </summary>
        public uint PrfSwitch { get
            {
                switch (prfSwitch)
                {
                    case 3:
                        return 2;
                    case 2:
                        return 1;
                    case 0:
                        return 0;
                }
                return 0;
            } set { prfSwitch = 0;
                switch (value)
                {
                    case 2:prfSwitch += 1;
                        goto case 1;
                    case 1:
                        prfSwitch += 2;
                        break;
                    default:
                        break;
                } } }
        /// <summary>
        /// 1ms选择
        /// </summary>
        public uint MsSwitch { get { return msSwitch; } set { msSwitch = value; } }
        /// <summary>
        /// 1ms周期
        /// </summary>
        public uint MsPeriod { get { return msPeriod; } set { msPeriod = value; } }
        /// <summary>
        /// 1ms脉宽
        /// </summary>
        public uint MsBandwidth { get { return msBandwidth; } set { msBandwidth = value; } }
        /// <summary>
        /// 启动信号选择
        /// </summary>
        public uint LunchSwith { get { return lunchSwitch; } set { lunchSwitch = value; } }
        /// <summary>
        /// 启动电平信号高低
        /// </summary>
        public uint ElectricalLevel { get { return electricalLevel; } set { electricalLevel = value; } }
        /// <summary>
        /// 是否给下位机中断
        /// </summary>
        public uint IRQ { get { return irq; } set { irq = value; } }
        /// <summary>
        /// 弹目距1
        /// </summary>
        public float Target1Distance { get { return target1Distance; } set { target1Distance = value; } }
        /// <summary>
        /// 弹目距2
        /// </summary>
        public float Target2Distance { get { return target2Distance; } set { target2Distance = value; } }
        /// <summary>
        /// 开始信号
        /// </summary>
        public uint OffOn { get { return offOn; } set { offOn = value; } }

        #endregion

        #region"控制板参数"
        private uint timingSwith;
        private uint timingInterval;
        public uint prfSwitch;  //modified by lchsh-20170701
        private uint msSwitch;
        private uint msPeriod;
        private uint msBandwidth;
        private uint lunchSwitch;
        private uint electricalLevel;
        private uint irq;
        private float target1Distance;
        private float target2Distance;
        private uint offOn;
        #endregion
    }

}
