using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRP1679.BLL.Other;
using HRP1679.DAL.Para;

namespace HRP1679.UI.UC
{
    public partial class UCInfo : UserControl
    {
        public UCInfo()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint ,
           true);
            this.UpdateStyles();
            this.tblpInfo.GetType().GetProperty("DoubleBuffered" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.tblpInfo , true , null);
        }
        
        private void UCInfo_Load(object sender , EventArgs e)
        {
            DataBinding();
            this.btnStop.Enabled = false;

        }

        #region"数据绑定及更新"
        private Timer tmMonitor = new Timer();
        private void DataBinding()
        {
            this.Invoke((Action)(() =>
            {
                this.lblDRFM.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "DRFMStatus" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblMicro.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "MicroStatus" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblRfmem.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "MemStatus" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblMem1.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "DMAOne" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblMem2.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "DMATwo" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblMem3.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "DMAThree" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblSlav.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "SlaveStatus" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.lblGCB.DataBindings.Add("Text" , ParaUI.Instance.paraStatus , "GCStatus" , false , DataSourceUpdateMode.OnPropertyChanged);
                this.btnStart.DataBindings.Add("Enabled" , ParaUI.Instance.paraStatus , "BtnStartEnable" , false , DataSourceUpdateMode.OnPropertyChanged);
            }));
            tmMonitor.Interval = 1000;
            tmMonitor.Tick += new EventHandler(tmMonitor_Tick);
          
        }

        void tmMonitor_Tick(object sender , EventArgs e)
        {
            if (this.InvokeRequired)
            {
                lblCPUUtilization.Invoke((Action)(() => { lblCPUUtilization.Text = SystemInfoOperator.Instance.GetCpuUtilization(); }));
                lblMemUtilization.Invoke((Action)(() => { lblMemUtilization.Text = (100 - Math.Round((decimal)SystemInfoOperator.Instance.GetAvailableMemory() / (decimal)SystemInfoOperator.Instance.GetPhysicalMemory() * 100)).ToString() + "%"; }));
                //    lblCPUTemp.Invoke((Action)(() => { lblCPUTemp.Text = SystemInfoOperator.Instance.GetCpuTemp(); }));
            }
            else 
            {
                lblCPUUtilization.Text = SystemInfoOperator.Instance.GetCpuUtilization();
                lblMemUtilization.Text = (100 - Math.Round((decimal)SystemInfoOperator.Instance.GetAvailableMemory() / (decimal)SystemInfoOperator.Instance.GetPhysicalMemory() * 100)).ToString() + "%";
               // lblCPUTemp.Text = SystemInfoOperator.Instance.GetCpuTemp(); 
            }

        }


        #endregion

        #region"事件绑定"
        private void CmdBinding()
        { 
        
        }
        
        #endregion

        #region"窗口样式设置,防止闪屏"
        protected override CreateParams CreateParams
        {
            //main
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= ~0x02000000;
                return cp;
            }
        }
        #endregion

        #region"按钮样式"
        private void btnMouseMove(object sender , MouseEventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.btn2Hover;
        }

        private void btnMouseLeave(object sender , EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.btn2;
        }
        #endregion

        #region"显示字体样式"
        private void lblDRFM_TextChanged(object sender , EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Text == "正常")
            {
                lbl.ForeColor = Color.FromArgb(0x74f42b);
            }
            else
            {
                lbl.ForeColor = Color.FromArgb(0xff2e2f);
            }
        }
        #endregion

        private void btnSelfCheck_Click(object sender , EventArgs e)
        {
            EventDef.SefCheck();
        }

        private void btnReset_Click(object sender , EventArgs e)
        {
            EventDef.Reset();
        }

        private void btnStart_Click(object sender , EventArgs e)
        {
         //   if (this.InvokeRequired)
                Invoke((Action)(() => { EventDef.Start(); })); 
        }

        private void btnStop_Click(object sender , EventArgs e)
        {
           // if (this.InvokeRequired)
                Invoke((Action)(() => { EventDef.Stop(); })); 
        }

        private void btnStart_EnabledChanged(object sender , EventArgs e)
        {
            this.btnStop.Enabled =! this.btnStart.Enabled;
        }

        private void UCInfo_ParentChanged(object sender , EventArgs e)
        {
            tmMonitor.Start();
        }
    }



}
