using Hirain.Lib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    [Serializable]
    public class ParaSignal
    {
        public ParaSignal()
        {
            signalDataType = signalType = 0;
            pRFCycle = 20;
            pRFPulseWidth = 10;
        }

        private float centerFrequency;
        private uint signalType;
        private uint signalDataType;
        private float pRFCycle;
        private float pRFPulseWidth;
        private uint startFrequency;
        private uint bandWidth;
        private string singalDataFilePath;

        //新添加了
        private uint framePeriod;
        private uint framePulseWidth;
        private uint frameNum;
        private uint prfNum;
        private uint codeLength;
        private uint codeWord;
        private float frequencyStep;


        /// <summary>
        /// 雷达信号数据源文件
        /// </summary>
        public string SingalDataFilePath
        {
            get { return singalDataFilePath; }
            set { if (File.Exists(value))  singalDataFilePath = value;
            else LoggingService.logShowed("不存在该数据源文件路径：" + value , InformationType.Error , InformationDisplayMode.FormList);
            }
        }


        /// <summary>
        /// 雷达信号类型
        /// </summary>
        public uint SignalType
        {
            get { return signalType; }
            set { signalType = value; }
        }
        /// <summary>
        /// 雷达数据类型
        /// </summary>
        public uint SignalDataType
        {
            get { return signalDataType; }
            set { signalDataType = value; }
        }
        /// <summary>
        /// prf周期
        /// 20-1000us
        /// </summary>
        public float PRFCycle
        {
            get { return pRFCycle; }
            set
            {
                if (value >= 20 && value <= 1000)
                {
                    pRFCycle = value;
                }
            }
        }
        /// <summary>
        /// PRF脉冲宽度
        /// 1-30us
        /// </summary>
        public float PRFPulseWidth
        {
            get { return pRFPulseWidth; }
            set
            {
                if (value >= 1 && value <= 30)
                {
                    pRFPulseWidth = value;
                }
            }
        }
        /// <summary>
        /// chirp起始频率
        /// </summary>
        public uint StartFrequency
        {
            get { return startFrequency; }
            set { startFrequency = value; }
        }
        /// <summary>
        /// chirp频率偏移
        /// </summary>
        public uint BandWidth
        {
            get { return bandWidth; }
            set { bandWidth = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public uint FramePeriod { get { return framePeriod; } set { framePeriod = value; } }

        public uint FramePulseWidth { get { return framePulseWidth; } set { framePulseWidth = value; } }

        public uint FrameNum { get { return frameNum; } set { frameNum = value; } }

        public uint PrfNum { get { return prfNum; } set { prfNum = value; } }
        
        public uint CodeLength { get { return codeLength; } set { codeLength = value; } }

        public uint CodeWord { get { return codeWord; }
            set
            {
                switch (CodeLength)
                {
                    case 0:
                        codeWord = 0;
                        break;
                    case 1:
                        codeWord = bianma(value.ToString()) & 0x1;
                        break;
                    case 2:
                        codeWord = bianma(value.ToString()) & 0x3;
                        break;
                    case 3:
                        codeWord = bianma(value.ToString()) & 0x7;
                        break;
                    case 4:
                        codeWord = bianma(value.ToString()) & 0x15;
                        break;
                    case 5:
                        codeWord = bianma(value.ToString()) & 0x31;
                        break;
                    default:
                        break;

                }

                codeWord = value;
            }
        }

        public float FrequencyStep { get { return frequencyStep; } set { frequencyStep = value; } }

        /// <summary>
        /// 将字符串转成二进制数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static uint bianma(string s)
        {
            byte[] data = Encoding.Unicode.GetBytes(s);
            return Convert.ToUInt32(s, 2);
        }
    }
}
