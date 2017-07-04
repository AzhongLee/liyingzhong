using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRP1679.DAL.Para;
using DevExpress.XtraEditors;

namespace HRP1679.UI.UC
{
    public partial class UCSystem : UserControl
    {
        public UCSystem()
        {
            InitializeComponent();
            Initialization();
            DataBinding();
        }

        ComboBoxEdit[] cmbAGCIpuSignType = new ComboBoxEdit[3] { new ComboBoxEdit(), new ComboBoxEdit(), new ComboBoxEdit() };

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

        /// <summary>
        /// 控件样式初始化设置
        /// </summary>
        private void Initialization()
        {
            
            cmbAGCIpuSignType.Initialize();
            //设置AGC输入功率控件 的样式
            foreach (var item in cmbAGCIpuSignType)
            {
                item.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                item.Properties.LookAndFeel.SkinName = "Blue";
                item.Font = new Font("微软雅黑", 10.2f);
                item.Properties.Items.Add("脉冲");
                item.Properties.Items.Add("连续波");
                item.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            }
            cmbAGCWorkMode1_SelectedIndexChanged(this.cmbAGCWorkMode1, null);
            cmbAGCWorkMode2_SelectedIndexChanged(this.cmbAGCWorkMode2, null);
            cmbAGCWorkMode3_SelectedIndexChanged(this.cmbAGCWorkMode3, null);
          //  cmbSignDataTypeSelectedIndexChanged(this.cmbSignDataType, null);
            //cmbEnumWorkMode_SelectedIndexChanged(this.cmbEnumWorkMode, null);
        }






        private void cmbAGCWorkMode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbAGCWorkMode1.SelectedIndex == 0)
            {
                this.pnlAGC1.Controls.Remove(this.speInputPow1);
                this.pnlAGC1.Controls.Add(this.cmbAGCIpuSignType[0]);
                this.cmbAGCIpuSignType[0].Dock = DockStyle.Fill;
                this.lblAGC1.Text = "AGC1输入信号类型";
            }
            else
            {
                this.pnlAGC1.Controls.Remove(this.cmbAGCIpuSignType[0]);
                this.pnlAGC1.Controls.Add(this.speInputPow1);
                this.speInputPow1.Dock = DockStyle.Fill;
                this.lblAGC1.Text = "射频1输入功率[dBm]";
            }
        }

        private void cmbAGCWorkMode2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.cmbAGCWorkMode2.SelectedIndex == 0)
            {
                this.pnlAGC2.Controls.Remove(this.speInputPow2);
                this.pnlAGC2.Controls.Add(this.cmbAGCIpuSignType[1]);
                this.cmbAGCIpuSignType[1].Dock = DockStyle.Fill;
                this.lblAGC2.Text = "AGC2输入信号类型";
            }
            else
            {
                this.pnlAGC2.Controls.Remove(this.cmbAGCIpuSignType[1]);
                this.pnlAGC2.Controls.Add(this.speInputPow2);
                this.speInputPow2.Dock = DockStyle.Fill;
                this.lblAGC2.Text = "射频2输入功率[dBm]";
            }
        }

        private void cmbAGCWorkMode3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.cmbAGCWorkMode3.SelectedIndex == 0)
            {
                this.pnlAGC3.Controls.Remove(this.speInputPow3);
                this.pnlAGC3.Controls.Add(this.cmbAGCIpuSignType[2]);
                this.cmbAGCIpuSignType[2].Dock = DockStyle.Fill;
                this.lblAGC3.Text = "AGC3输入信号类型";
            }
            else
            {
                this.pnlAGC3.Controls.Remove(this.cmbAGCIpuSignType[2]);
                this.pnlAGC3.Controls.Add(this.speInputPow3);
                this.speInputPow3.Dock = DockStyle.Fill;
                this.lblAGC3.Text = "射频3输入功率[dBm]";
            }
        }





        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBinding()
        {
            this.speFC.DataBindings.Add("Value", ParaUI.Instance.paraCollect, "CenterFrequency", false, DataSourceUpdateMode.OnPropertyChanged);
            this.spePowerOut.DataBindings.Add("Value", ParaUI.Instance.paraWave, "PowerOutput", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCWorkMode1.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "WorkMode1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCWorkMode2.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "WorkMode2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCWorkMode3.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "WorkMode3", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speInputPow1.DataBindings.Add("Value", ParaUI.Instance.paraWave, "PowerInput1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speInputPow2.DataBindings.Add("Value", ParaUI.Instance.paraWave, "PowerInput2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speInputPow3.DataBindings.Add("Value", ParaUI.Instance.paraWave, "PowerInput3", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCIpuSignType[0].DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "SignType1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCIpuSignType[1].DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "SignType2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbAGCIpuSignType[2].DataBindings.Add("SelectedIndex", ParaUI.Instance.paraWave, "SignType3", false, DataSourceUpdateMode.OnPropertyChanged);

        }




    }
}
