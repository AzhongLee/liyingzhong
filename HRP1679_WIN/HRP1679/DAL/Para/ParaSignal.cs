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
            set
            {
                if (File.Exists(value)) singalDataFilePath = value;
                else LoggingService.logShowed("不存在该数据源文件路径：" + value, InformationType.Error, InformationDisplayMode.FormList);
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
                if (value >= 0 && value <= 1000)
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
        /// frame周期
        /// </summary>
        public uint FramePeriod
        {
            get
            {
                return framePeriod;
            }
            set
            {
                if (value > pRFCycle * prfNum + 1)
                    framePeriod = value;
            }
        }
        /// <summary>
        /// frame脉宽
        /// </summary>
        public uint FramePulseWidth { get { return framePulseWidth; } set { framePulseWidth = value; } }
        /// <summary>
        /// 组内帧个数
        /// </summary>
        public uint FrameNum { get { return frameNum; } set { frameNum = value; } }
        /// <summary>
        /// 帧内prf个数
        /// </summary>
        public uint PrfNum { get { return prfNum; } set { prfNum = value; } }
        /// <summary>
        /// 编码长度
        /// </summary>
        public uint CodeLength { get { return codeLength; } set { codeLength = value; } }

        public uint CodeWord
        {
            get { return codeWord; }
            set
            {
                codeWord = value;
            }
        }
        /// <summary>
        /// 编码字符取有效位
        /// </summary>
        public uint CodeWordEffect
        {
            get
            {
                switch (CodeLength)
                {
                    case 0:
                        return 0;
                    case 1:
                        return bianma(codeWord.ToString()) & 0x1;
                    case 2:
                        return bianma(codeWord.ToString()) & 0x3;
                    case 3:
                        return bianma(codeWord.ToString()) & 0x7;
                    case 4:
                        return bianma(codeWord.ToString()) & 0x15;
                    case 5:
                        return bianma(codeWord.ToString()) & 0x31;
                    default:
                        return 0;
                }
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
            //  byte[] data = Encoding.Unicode.GetBytes(s);
            return Convert.ToUInt32(s, 2);
        }
    }
}
