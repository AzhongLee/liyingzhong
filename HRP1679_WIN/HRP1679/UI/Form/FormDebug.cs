using DevExpress.XtraEditors;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.UI
{
    public partial class FormDebug : Form
    {
        public FormDebug()
        {
            InitializeComponent();
            DataBinding();
        }
        private static FormDebug instance = null;
        private static object obj = new object();
        public static FormDebug Instance
        {
            get {

                lock (obj)
                {
                    instance = instance ?? new FormDebug();
                    return instance;
                }
            }
        }
        #region"实现窗口拖动响应"
        [DllImport("user32")]
        public static extern bool ReleaseCapture();
        [DllImport("user32")]
        public static extern bool SendMessage(IntPtr Hwnd , int wMsg , int wParam , int Iparam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void TtitleMouseDown(object sender , MouseEventArgs e)
        {
            try
            {
                ReleaseCapture();
                SendMessage(this.Handle , WM_SYSCOMMAND , SC_MOVE + HTCAPTION , 0);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region"窗口样式设置,防止闪屏"
        protected override CreateParams CreateParams
        {
            //main
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region"数据绑定"
        private void DataBinding()
        {
            this.cmbPRF.DataBindings.Add("SelectedIndex" , ParaUI.Instance.paraDebug , "PRFModel",false,DataSourceUpdateMode.OnPropertyChanged);
            this.cmbClock.DataBindings.Add("SelectedIndex" , ParaUI.Instance.paraDebug , "ClockReference" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.speClockReference.DataBindings.Add("Value" , ParaUI.Instance.paraDebug , "ClockFrequency" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.speDetect.DataBindings.Add("Value" , ParaUI.Instance.paraDebug , "Detection" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.speInherentDelay.DataBindings.Add("Value" , ParaUI.Instance.paraDebug , "InherentDelay" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.cmbDataSourceSet.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraCollect, "DataResource", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speCeofCut.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "CeofCut", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbDetection.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "Detectionswitch", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbFcModel.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraCollect, "FrequencyModel", false, DataSourceUpdateMode.OnPropertyChanged);

            this.cmbTimingSwitch.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "TimingSwith", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speTimeingInterval.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "TimingInterval", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbPrfSwitch.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "PrfSwitch", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbmsSwitch.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "MsSwitch", false, DataSourceUpdateMode.OnPropertyChanged);
            this.spemsPeriod.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "MsPeriod", false, DataSourceUpdateMode.OnPropertyChanged);
            this.spemsBandWidth.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "MsBandwidth", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmblunchSwitch.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "LunchSwith", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbElectricalLevel.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "ElectricalLevel", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbIRQ.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "IRQ", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speTargetDis1.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "Target1Distance", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speTargetDis2.DataBindings.Add("Value", ParaUI.Instance.paraDebug, "Target2Distance", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbOffOn.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraDebug, "OffOn", false, DataSourceUpdateMode.OnPropertyChanged);


        }
        #endregion

        private void button1_Click(object sender , EventArgs e)
        {
            this.Hide();
        }

        private void cmbDataSourceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
        

        }
    }
}
