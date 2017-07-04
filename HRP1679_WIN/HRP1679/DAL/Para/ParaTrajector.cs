using Hirain.Lib.Common;
using HRP1679.DAL.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    [Serializable]
    public class ParaTrajector
    {
        public ParaTrajector()
        {
            trajectorType = 0;
            powerOutputPortTwo = powerOutputPortOne = 0;
         //   trajectorFilePath =ConstData.TrajectorFilePath;
        }

        private uint trajectorType;
        private string trajectorFilePath;
        private float powerOutputPortOne;
        private float powerOutputPortTwo;
        private uint enumPort;
        /// <summary>
        /// 端口二输出功率 -100~15
        /// </summary>
        public float PowerOutputPortTwo
        {
            get { return powerOutputPortTwo; }
            set {if(value>=-100&&value<=15) powerOutputPortTwo = value;
            else LoggingService.logShowed("端口二输出功率参数范围不合法：-100～50" , InformationType.Error , InformationDisplayMode.FormList);
            }
        }

        /// <summary>
        /// 端口一输出功率 -100~15
        /// </summary>
        public float PowerOutputPortOne
        {
            get { return powerOutputPortOne; }
            set { if (value >= -100 && value <= 15) powerOutputPortOne = value;
            else LoggingService.logShowed("端口一输出功率参数范围不合法：-100～50" , InformationType.Error , InformationDisplayMode.FormList);
            }
        }
        /// <summary>
        /// 导弹轨迹文件路径
        /// </summary>
    
        public string TrajectorFilePath
        {
            get { return trajectorFilePath; }
            set { if(File.Exists(value)) trajectorFilePath = value;
            else LoggingService.logShowed("不存在该导弹轨迹文件路径："+value, InformationType.Error , InformationDisplayMode.FormList);
            }
        }
       
        /// <summary>
        /// 弹道轨迹生成类型
        /// 0：
        /// 1：
        /// </summary>
        public uint TrajectorType
        {
            get { return trajectorType; }
            set { trajectorType = (uint)(value <= 0 ? 0 :1); }
        }
        //public string IEnumPort { get => enumPort.ToString("X2");
        //    set => enumPort = uint.Parse(value, System.Globalization.NumberStyles.AllowHexSpecifier); }
        //>= 会报错   modified by zhm_2017.6.28
          public string IEnumPort { get{return enumPort.ToString("X2");}
            set{ enumPort = uint.Parse(value, System.Globalization.NumberStyles.AllowHexSpecifier);} }

        public uint EnumPort { get{return enumPort;}

            set { enumPort = value; }
        }
        
    }
}
