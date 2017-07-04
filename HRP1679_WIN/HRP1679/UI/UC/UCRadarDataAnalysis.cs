using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRP1524.BLL;
using HRP1524.DAL;
using System.Runtime.InteropServices;
using Hrp1524matlab;

namespace HRP1524
{
    public partial class UCRadarDataAnalysis : UserControl
    {
        private RtxDataPacker rtxUcPacker = new RtxDataPacker();

        Hrp1524Matlab m_matlab = new Hrp1524Matlab();
        private WindowManager winMng = new WindowManager();

        public UCRadarDataAnalysis()
        {
            InitializeComponent();
        }

        public void changeTextBoxShowParam(SystemParameters SystemParameters)
        {
            if (SystemParameters.inPutSignal == 0)
            {
                this.label2param.Text = "脉冲信号";
            }
            else
            {
                this.label2param.Text = "连续波信号";
            }

            this.label3param.Text =(SystemParameters.radarCenterFrequency.ToString());


             //"8.毫米波中心频率(MHz)
            this.label4param.Text = (SystemParameters.radarfc.ToString());

            this.label5param.Text = SystemParameters.delayed.ToString();

            if (SystemParameters.Flag == 0)
            {
                this.label6.Text = "程控衰减(dB):";
                this.label6param.Text =(SystemParameters.sendCapacityPt.ToString());
            }
            else
            {
                this.label6.Text = "幅度标定(dB):";
                this.label6param.Text = (SystemParameters.airWireG.ToString());
            }


            /*
#if 0
            this.textBox1.Clear();
            this.textBox1.AppendText("1.工作模式: ");
            if (SystemParameters.workModel == 0)
            {
                this.textBox1.AppendText("自动控制模式");
            }
            else
            {
                this.textBox1.AppendText("软件控制模式");
            }           

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("2.输入信号类型: ");
            if (SystemParameters.inPutSignal == 0)
            {
                this.textBox1.AppendText("脉冲信号");
            }
            else
            {
                this.textBox1.AppendText("连续波信号");
            }

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("3.内外参考选择: ");
            if (SystemParameters.inOutChoose == 0)
            {
                this.textBox1.AppendText("外参考");
            }
            else
            {
                this.textBox1.AppendText("内参考");
            }

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("4.基带AD控制: ");
            if ((SystemParameters.FpgaParamPacket&0x3) == 0)
            {
                this.textBox1.AppendText("AD采集数据");
            }
            else
            {
                this.textBox1.AppendText("雷达信号模拟");
            }

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("5.基带DA控制: ");
            if (((SystemParameters.FpgaParamPacket & 0xc)>>2) == 0)
            {
                this.textBox1.AppendText("上变频数据");
            }
            else
            {
                this.textBox1.AppendText("补偿滤波信号");
            }

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("6.基带工作模式控制: ");
            if (((SystemParameters.FpgaParamPacket & 0x30) >> 4) == 1)
            {
                this.textBox1.AppendText("计算多普勒");
            }
            else
            {
                this.textBox1.AppendText("不计算多普勒");
            }

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("7.厘米波中心频率(MHz) =");
            this.textBox1.AppendText(SystemParameters.radarCenterFrequency.ToString());

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("8.毫米波中心频率(MHz) =");
            this.textBox1.AppendText(SystemParameters.radarfc.ToString());


            //this.textBox1.AppendText("\r\n");
            //this.textBox1.AppendText("PRF周期(us) =");
            //this.textBox1.AppendText(((float)SystemParameters.prfCircle / 400).ToString());

            //this.textBox1.AppendText("\r\n");
            //this.textBox1.AppendText("PRF脉宽(us) =");
            //this.textBox1.AppendText(((float)SystemParameters.PWidth / 400).ToString());

            this.textBox1.AppendText("\r\n");
            this.textBox1.AppendText("9.固有延迟(ns) =");
            this.textBox1.AppendText(SystemParameters.delayed.ToString());

            if (SystemParameters.Flag == 0)
            {
                this.textBox1.AppendText("\r\n");
                this.textBox1.AppendText("10.程控衰减(dB) =");
                this.textBox1.AppendText(SystemParameters.sendCapacityPt.ToString());
            }
            else
            {
                this.textBox1.AppendText("\r\n");
                this.textBox1.AppendText("10.幅度标定(dB) =");
                this.textBox1.AppendText(SystemParameters.airWireG.ToString());
            }
            if (SystemParameters.workModel == 1)
            {
                this.textBox1.AppendText("\r\n");
                this.textBox1.AppendText("11.输入功率补偿 =");
                this.textBox1.AppendText(SystemParameters.inPutCapacity.ToString());
            }
#endif
             */
        }

        private static void Delay(double secend)
        {
             DateTime tempTime = DateTime.Now;
             while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
             {
                 Application.DoEvents();
             }
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            this.button2.Visible = true;
            this.button1.Visible = false;
            rtxUcPacker.Pack(CmdType.DataLoad, NetMode.Local, new List<SystemParameters>(), new List<TargetInfo>());
            Delay(16);
            m_matlab.FPGA_DATA_PROC_FOR_SW();

            string str = System.IO.File.ReadAllText(Application.StartupPath + "\\temp.txt").Substring(0,19);
            string str1 = str.Insert(19,".png");

            this.picMatlab.BackgroundImage = Image.FromFile(Application.StartupPath + "\\" + str1);
            //this.picMatlab.ImageLocation = Application.StartupPath + "\\fig0.jpg";
            this.picMatlab.BackgroundImageLayout = ImageLayout.Zoom;

            //winMng.ShowToControl("Figure 1", this.panel1);
            
            this.button1.Visible = true;
            this.button2.Visible = false;

        }
    }
}
