using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
      [Serializable]
    public class ParaMicroWave
    {
        private float powerInput1;
        private float powerInput2;
        private float powerInput3;
        private uint workMode1;
        private uint workMode2;
        private uint workMode3;
        private uint signType1;
        private uint signType2;
        private uint signType3;
        private float powerOutput;
        /// <summary>
        /// 输出功率
        /// </summary>
        public float PowerOutput
        {
            get { return powerOutput; }
            set {
                if(value>=-60&&value<=15)
                powerOutput = value; 
            }
        }

        /// <summary>
        /// AGC3输入信号类形
        /// </summary>
        public uint SignType3
        {
            get { return signType3; }
            set { signType3 = value; }
        }

        /// <summary>
        /// AGC2输入信号类形
        /// </summary>
        public uint SignType2
        {
            get { return signType2; }
            set { signType2 = value; }
        }
        /// <summary>
        /// AGC1输入信号类形
        /// </summary>
        public uint SignType1
        {
            get { return signType1; }
            set { signType1 = value; }
        }

        /// <summary>
        /// AGC3工作模式
        /// </summary>
        public uint WorkMode3
        {
            get { return workMode3; }
            set { workMode3 = value; }
        }

        /// <summary>
        /// AGC2工作模式
        /// </summary>
        public uint WorkMode2
        {
            get { return workMode2; }
            set { workMode2 = value; }
        }
        /// <summary>
        /// AGC1工作模式
        /// </summary>
        public uint WorkMode1
        {
            get { return workMode1; }
            set { workMode1 = value; }
        }
        /// <summary>
        /// 射频3输入功率
        /// </summary>
        public float PowerInput3
        {
            get { return powerInput3; }
            set { if (value >= -60 && value <= 10)powerInput3 = value; }
        }
        /// <summary>
        /// 射频2输入功率
        /// </summary>
        public float PowerInput2
        {
            get { return powerInput2; }
            set { if (value >= -60 && value <= 10) powerInput2 = value; }
        }
        /// <summary>
        /// 射频1输入功率
        /// </summary>

        public float PowerInput1
        {
            get { return powerInput1; }
            set { 
                if(value>=-60&&value<=10)
                powerInput1 = value; }
        }


    }
}
