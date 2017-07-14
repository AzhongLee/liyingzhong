
using Hirain.Lib.Common;
using Hirain.Lib.IO;
using HRP1679.BLL.FileOperation;
using HRP1679.DAL.Common;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.BLL.CtrlWord
{
    public class MicroCtrl
    {
        private List<double> Link_Freq = new List<double>();
        private List<double> Link_Adjust = new List<double>();
        public List<double> Device_Freq = new List<double>();
        public List<double> Device_Attenuation = new List<double>();
        public double[,] Device_RealAttenuation;

        #region 单例模式
        private static MicroCtrl instance;
        public static MicroCtrl Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new MicroCtrl();
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 控制板命令字打包-modified by lchsh-20170701
        /// </summary>
        /// <returns></returns>
        private static List<byte> PackUniversalCmd()
        {
            List<byte> cmd = new List<byte>();
            cmd.Clear();
            //添加帧头
            cmd.AddRange(BitConverter.GetBytes(0xFFAAFF55));
            cmd.AddRange(BitConverter.GetBytes(0xAAFF55FF));
            cmd.AddRange(BitConverter.GetBytes(0x00010010));
            cmd.AddRange(BitConverter.GetBytes(0x00000000));
            //1雷达中心频率
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraCollect.CenterFrequency*1000000));//MHz->Hz
            if (ParaUI.Instance.paraDebug.TimingSwith == 0)     //20170704
            {//1ms在前
                //2-prf延迟时间间隔
                cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.TimingInterval * 1e3 / 20)));
                //3-1ms延迟时间间隔
                cmd.AddRange(BitConverter.GetBytes(0));
            }
            else if (ParaUI.Instance.paraDebug.TimingSwith == 1)
            {//prf在前
                //2-prf延迟时间间隔
                cmd.AddRange(BitConverter.GetBytes(0));
                //3-1ms延迟时间间隔
                cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.TimingInterval * 1e3 / 20)));
            }          
            //4prf选择
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.prfSwitch));   //20170704
            //5prf周期
            cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.PrfPeroid * 1e3 / 20)));
            //6prf脉宽
            cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.PrfBandwidth * 1e3 / 20)));
            //71ms选择
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.MsSwitch));
            //81ms周期
            cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.MsPeriod * 1e3 / 20)));
            //91ms脉宽
            cmd.AddRange(BitConverter.GetBytes(Convert.ToUInt32(ParaUI.Instance.paraDebug.MsBandwidth * 1e3 / 20)));
            //10启动信号选择
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.LunchSwith));
            //11启动电平高低
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.ElectricalLevel));
            //12
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.IRQ));
            //13
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.Target1Distance));
            //14
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.Target2Distance));
            //15
            cmd.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraDebug.OffOn));
            //16-25 10个0
            for(int i =0;i<10;i++)
                cmd.AddRange(BitConverter.GetBytes(0));
            //帧尾
            cmd.AddRange(BitConverter.GetBytes(0xAA55FFFF));
            cmd.AddRange(BitConverter.GetBytes(0xFFFFAA55));
            return cmd;
        }
        /// <summary>
        /// 微波控制字打包
        /// </summary>
        /// <returns></returns>
        public static byte[] PackMicroWaveCmd()
        {
            List<byte> cmd = new List<byte>();
            cmd.Clear();
            //添加包头
            cmd.AddRange(BitConverter.GetBytes(0xFFAAFF55));
            cmd.AddRange(BitConverter.GetBytes(0xAAFF55FF));
            cmd.AddRange(BitConverter.GetBytes(0x00000074));
            cmd.AddRange(BitConverter.GetBytes(0x00000074));

            //添加控制板命令字
            cmd.AddRange(PackUniversalCmd());

            //添加帧头
            cmd.AddRange(BitConverter.GetBytes(0xFFAAFF55));
            cmd.AddRange(BitConverter.GetBytes(0xAAFF55FF));
            cmd.AddRange(BitConverter.GetBytes(0x0002001E));
            cmd.AddRange(BitConverter.GetBytes(0x00000000));

            ExcelOperate.Instance.ReadInMicroSheet();
            //和路下变频
            double PowCmpAjust = ExcelOperate.Instance.FindValueByKey(ExcelOperate.Instance.PowCmpAjust1 , ParaUI.Instance.paraWave.PowerInput1 , ParaUI.Instance.paraCollect.CenterFrequency/1000); //下变频衰减
            cmd.AddRange(BitConverter.GetBytes(CalcAGCWord(PowCmpAjust , ParaUI.Instance.paraWave.SignType1 , ParaUI.Instance.paraWave.WorkMode1)));
            //差
            PowCmpAjust = ExcelOperate.Instance.FindValueByKey(ExcelOperate.Instance.PowCmpAjust2 , ParaUI.Instance.paraWave.PowerInput2 , ParaUI.Instance.paraCollect.CenterFrequency/1000); //下变频衰减
            cmd.AddRange(BitConverter.GetBytes(CalcAGCWord(PowCmpAjust , ParaUI.Instance.paraWave.SignType2 , ParaUI.Instance.paraWave.WorkMode2)));
            //差
            PowCmpAjust = ExcelOperate.Instance.FindValueByKey(ExcelOperate.Instance.PowCmpAjust3 , ParaUI.Instance.paraWave.PowerInput3 , ParaUI.Instance.paraCollect.CenterFrequency/1000); //下变频衰减
            cmd.AddRange(BitConverter.GetBytes(CalcAGCWord(PowCmpAjust , ParaUI.Instance.paraWave.SignType3 , ParaUI.Instance.paraWave.WorkMode2)));
            //上变频
            double PowAdjust = ExcelOperate.Instance.FindValueByKey(ExcelOperate.Instance.PowAdjust , ParaUI.Instance.paraCollect.CenterFrequency/1000);
            double Atten = ExcelOperate.Instance.FindValueByKey(ExcelOperate.Instance.PowAtten , ConstData.MaxPow - ParaUI.Instance.paraWave.PowerOutput , ParaUI.Instance.paraCollect.CenterFrequency/1000);
            cmd.AddRange(BitConverter.GetBytes(CalcSwitch(ParaUI.Instance.paraCollect.CenterFrequency /1000, PowAdjust+Atten)));
            //时钟参考
            cmd.AddRange(BitConverter.GetBytes(CalcCLKCtrlWord(ParaUI.Instance.paraDebug.ClockReference,ParaUI.Instance.paraDebug.ClockFrequency)));
            //频综
            cmd.AddRange(BitConverter.GetBytes(CalcFreModule(ParaUI.Instance.paraCollect.CenterFrequency/1000)));
            //帧尾
            cmd.AddRange(BitConverter.GetBytes(0xAA55FFFF));
            cmd.AddRange(BitConverter.GetBytes(0xFFFFAA55));
            return cmd.ToArray();
        }


        #region"时钟控制模块"
        /// <summary>
        /// 计算时钟模块控制字
        /// </summary>
        /// <param name="nClk">0-外，1-内</param>
        /// <returns></returns>
        public static uint CalcCLKCtrlWord(uint nClk,uint clfc)
        {
             clfc /= 10;   // 20170704
          
            return nClk|clfc<<8;
        }
        #endregion

        #region"上变频模块"
        /// <summary>
        /// 上变频模块
        /// </summary>
        /// <param name="atten"> 标平adjust0+衰减atten0</param>
        /// <param name="fc"></param>
        /// <returns></returns>
        public static uint CalcSwitch(float fc,double atten)
        {
           
            uint control = 0,switch1 = 0,switch2 = 0;
            uint Atten1 = 0 , Atten2 = 0 , Atten3 = 0;
            //开关滤波器组
            if (fc >= 6 && fc < 6.8)
                switch1 = 0;
            else if (fc >= 6.8 && fc < 7.6)
                switch1 = 1;
            else if (fc >= 7.6 && fc < 8.4)
                switch1 = 2;
            else if (fc >= 8.4 && fc < 9.2)
                switch1 = 3;
            else if (fc >= 9.2 && fc < 10)
                switch1 = 4;
            else if (fc >= 10 && fc < 10.8)
                switch1 = 5;
            else if (fc >= 10.8 && fc < 11.6)
                switch1 = 6;
            else if (fc >= 11.6 && fc < 12.4)
                switch1 = 7;
            else if (fc >= 12.4 && fc < 13.2)
                switch2 = 0;
            else if (fc >= 13.2 && fc < 14)
                switch2 = 1;
            else if (fc >= 14 && fc < 14.8)
                switch2 = 2;
            else if (fc >= 14.8 && fc < 15.6)
                switch2 = 3;
            else if (fc >= 15.6 && fc < 16.4)
                switch2 = 4;
            else if (fc >= 16.4 && fc < 17.2)
                switch2 = 5;
            else if (fc >= 17.2 && fc < 18)
                switch2 = 6;
            //切换开关
            if (fc >12.4) control = 1;
            else control = 0;
            //数控衰减器
            if (0 <= atten && atten <= 31)
            { 
            Atten1=(uint)(~((int)atten)&0x1f);
            Atten2 = 0x1f;
            Atten3 = 0x1f;
            }
            else if(31<atten&&atten<=62)
            {
                Atten1 = 0x0;
                Atten2 = (uint)(~((int)atten-31) & 0x1f);
                Atten3 = 0x1f;
            }
            else if (62 < atten && atten <= 93)
            {
                Atten1 = 0x0;
                Atten2 = 0x0;
                Atten3 = (uint)(~((int)atten - 62) & 0x1f);
            }
            return switch1|switch2<<3|Atten1<<6|Atten2<<11|Atten3<<16 | control << 21;
        }
     
        #endregion

        #region"频综模块"
        /// <summary>
        /// 频综控制字计算
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public static uint CalcFreModule(double fc)
        {
           
         //   uint cmd = 0xa58;
            uint F = 600 , onOff = 2;
            if (fc >= 8 && fc <= 14)
            {
                F = (uint)(fc + 2) * 100 - 800;
            }
            else if (fc > 14 && fc <= 18)
            {
                F = (uint)(fc - 2)*100-800;
            }
           
            if (fc >= 8 && fc <10)
            {
                onOff = 0;
            }
            else if (fc >= 10 && fc < 12)
            {
                onOff = 1;
            }
            else if (fc >= 12 && fc < 16)
            {
                onOff = 2;
            }
          //  cmd = F | onOff << 10;
            return F | onOff << 10;
        }

        #endregion

        #region"和差差下变频AGC控制字计算"
        /// <summary>
        /// 下变频AGC控制字计算
        /// </summary>
        /// <param name="powCmpAjust"></param>
        /// <param name="signType"></param>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public static uint CalcAGCWord(double powCmpAjust , uint signType , uint workMode)
        {
            uint gain = (uint)((~((int)powCmpAjust)) & 0x3F);
            return gain << 2 | signType << 1 | workMode;
        }
        #endregion


        /// <summary>
        /// 获取标平值
        /// </summary>
        /// <param name="freq">频率值</param>
        /// <param name="FilePath">Excel文件路径（绝对路径）</param>
        /// <param name="TableName">待查的表名称</param>
        /// <returns>标平值</returns>
        public double GetLinkAdjustValue(double freq , string FilePath , string TableName)
        {
            this.ReadLinkAdjust(FilePath , TableName);
            return Link_Adjust[SearchMostClose(Link_Freq , freq)];
        }
        /// <summary>
        /// 获取衰减值
        /// </summary>
        /// <param name="freq">频率值</param>
        /// <param name="attenuation">衰减值 =（最大功率-界面设置的功率）</param>
        ///         /// <param name="FilePath">Excel文件路径（绝对路径）</param>
        /// <param name="TableName">待查的表名称</param>
        /// <returns>标定后的衰减值</returns>
        public double GetDeviceAdjustValue(double freq , double attenuation , string FilePath , string TableName)
        {
            this.ReadDeviceAdjust(FilePath , TableName);
            return Device_RealAttenuation[SearchMostClose(Device_Attenuation , attenuation) , SearchMostClose(Device_Freq , freq)];
        }
        /// <summary>
        /// 读取标平表
        /// </summary>
        /// <param name="FilePath">Excel文件路径（绝对路径）</param>
        /// <param name="TableName">待查的表名称</param>
        private void ReadLinkAdjust(string FilePath , string TableName)
        {
            try
            {
                DataSet ds = ExcelOperator.Read(FilePath , TableName);
                Link_Adjust.Clear();
                Link_Freq.Clear();
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Link_Freq.Add(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[i]));
                    Link_Adjust.Add(Convert.ToDouble(ds.Tables[0].Rows[1].ItemArray[i]));
                }
            }
            catch (Exception exp)
            {
                //System.Windows.Forms.MessageBox.Show(exp.Message, "提示", System.Windows.Forms.MessageBoxButtons.OK,
                //    System.Windows.Forms.MessageBoxIcon.Error);
                throw new Exception(exp.Message);
            }
        }

        /// <summary>
        /// 读取衰减表
        /// </summary>
        /// <param name="FilePath">Excel文件路径（绝对路径）</param>
        /// <param name="TableName">待查的表名称</param>
        public void ReadDeviceAdjust(string FilePath , string TableName)
        {
            try
            {
                //ExcelOperator excl = new ExcelOperator();
                //excl.Open(FilePath);
                DataSet ds = ExcelOperator.Read(FilePath , TableName);
                Device_Freq.Clear();
                for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                {
                    Device_Freq.Add(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[i]));
                }
                Device_Attenuation.Clear();
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    Device_Attenuation.Add(Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[0]));
                }
                Device_RealAttenuation = new double[ds.Tables[0].Rows.Count - 1 , ds.Tables[0].Columns.Count - 1];
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                    {
                        Device_RealAttenuation[i - 1 , j - 1] = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[j]);
                    }
                }
                //excl.Close();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        /// <summary>
        /// 查衰减值所在的行编号（Index从0开始）
        /// </summary>
        /// <param name="array">衰减值序列（第一列）</param>
        /// <param name="value">衰减值</param>
        /// <param name="index">衰减值所在行号</param>
        /// <param name="valueClose">截至步进点处的衰减值</param>
        private void SearchMostClose(List<double> array , double value , ref int index , ref double valueClose)
        {
            decimal distance = decimal.MaxValue;
            for (int i = 0; i < array.Count; i++)
            {
                if (Math.Abs((decimal)value - (decimal)array[i]) < distance)
                {
                    distance = (decimal)Math.Abs(value - array[i]);
                    index = i;
                    valueClose = array[i];
                }
            }
        }

        /// <summary>
        /// 查找最相近的频率所在的列号
        /// </summary>
        /// <param name="array">频率序列（第一行）</param>
        /// <param name="value">频点值</param>
        /// <returns>频率所在的列编号（Index开始于0）</returns>
        public int SearchMostClose(List<double> array , double value)
        {
            decimal distance = decimal.MaxValue;
            int index = 0;
            for (int i = 0; i < array.Count; i++)
            {
                //查找与value差值最小的频率所在列
                if (Math.Abs((decimal)value - (decimal)array[i]) < distance)
                {
                    distance = (decimal)Math.Abs(value - array[i]);
                    index = i;
                }
            }
            return index;
        }

        #region 读天线参数
        /// <summary>
        /// 在注入式模式下获得天线参数，要求天线方向图文件的为等步进，可以不需要为对称
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="m_antParas"></param>
        public void ReadInAntParas(AntParas m_antparas)
        {

            m_antparas.WeightSum.Clear();
            m_antparas.WeigthAzi.Clear();
            m_antparas.WeigthPit.Clear();

            //读取天线方向图文件
            #region 和通道
            try
            {
                string DirFileSumChn = AppDomain.CurrentDomain.BaseDirectory + "\\ant_dir_pic\\antSumChn.txt";
                FileStream sumFs = new FileStream(DirFileSumChn , FileMode.Open);
                StreamReader sumRd = new StreamReader(sumFs);

                double beamwidth = 0 , angStep = 0;
                m_antparas.WeightSum = ReadAntFile(sumRd , ref beamwidth , ref angStep);
                m_antparas.BeamWidth = beamwidth;
                m_antparas.AngStep = angStep;
            }
            catch (Exception)
            {
                MessageBox.Show("和通道天线方向图文件读取失败" , "警告" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            #endregion


        }

        /// <summary>
        /// 读取天线方向图文件
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private List<double> ReadAntFile(StreamReader sr , ref double beamwidth , ref double step)
        {
            List<double> weigth = new List<double>();
            char[] charSeparators = new char[] { ',' , ' ' };
            List<double> angle = new List<double>();

            while (sr.Peek() >= 0)
            {
                string strTemp = sr.ReadLine();
                strTemp.Trim();
                string[] strData = strTemp.Split(charSeparators , StringSplitOptions.RemoveEmptyEntries);
                if (strData.Length == 2)
                {
                    angle.Add(Convert.ToDouble(strData[0]));
                    weigth.Add(Convert.ToDouble(strData[1]));
                }
            }
            if (angle.Count > 1)
            {
                beamwidth = (angle[angle.Count - 1] - angle[0]) * Math.PI / 180;   //转成弧度
                step = (angle[1] - angle[0]) * Math.PI / 180;               //转成弧度，认为步进一致
            }
            return weigth;
        }

        #endregion

        public class AntParas
        {
            public double BeamWidth { get; set; }    //波束宽度，单位rad
            public double AngStep { get; set; }         //角度步进，单位rad
            //public int Num { get; set; }                     //点数
            public List<double> WeightSum { get; set; }           //和天线方向图系数，单位dB
            public List<double> WeigthAzi { get; set; }             //方位差天线方向图系数，单位dB
            public List<double> WeigthPit { get; set; }              //俯仰差天线方向图系数，单位dB

            public AntParas()
            {
                WeightSum = new List<double>();
                WeigthAzi = new List<double>();
                WeigthPit = new List<double>();
            }

            public void Clear()
            {
                BeamWidth = 0;
                AngStep = 0;
                //Num = 0;
                WeightSum.Clear();
                WeigthAzi.Clear();
                WeigthPit.Clear();
            }
        }
    }
}
