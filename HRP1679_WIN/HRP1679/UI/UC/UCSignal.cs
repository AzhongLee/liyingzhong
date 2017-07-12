using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HRP1679.DAL.Para;

namespace HRP1679.UI.UC
{
    public partial class UCSignal : UserControl
    {
        public UCSignal()
        {
            InitializeComponent();
            Initialization();
            DataBinding();
            this.SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint ,
           true);
            this.UpdateStyles();
           this.tlpSignal.GetType().GetProperty("DoubleBuffered" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.tlpSignal , true , null);
           this.pnlGenerate.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.pnlGenerate, true, null);
        }
        UCGeneration ucgenerate = new UCGeneration();
        UCSystem ucsystem = new UCSystem();
    
        private void Initialization()
        {
            this.pnlGenerate.Controls.Add(ucgenerate);
            this.pnlGenerate.Controls.Add(ucsystem);
            ucsystem.Dock = DockStyle.Fill;
            ucgenerate.Dock = DockStyle.Fill;
            ucsystem.BringToFront();
            cmbEnumWorkMode_SelectedIndexChanged(this.cmbEnumWorkMode,null);
        }
       
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

        #region"界面样式"

        #endregion

        #region"参数绑定"
     

        private void btneTrajFilePath_Click(object sender , EventArgs e)
        {
            this.ofdSignal.Filter = "*.xls|*.xls";
            this.ofdSignal.FilterIndex = 1;
            if (this.ofdSignal.ShowDialog() != DialogResult.Cancel)
            {
                ParaUI.Instance.paraTrajector.TrajectorFilePath = this.ofdSignal.FileName;
                this.btneTrajFilePath.Text = this.ofdSignal.SafeFileName;
            }
        }
        private void DataBinding()
        {
            this.spePowPort1.DataBindings.Add("Value" , ParaUI.Instance.paraTrajector , "PowerOutputPortOne" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.spePowPort2.DataBindings.Add("Value" , ParaUI.Instance.paraTrajector , "PowerOutputPortTwo" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.cmbEnumWorkMode.DataBindings.Add("SelectedIndex" , ParaUI.Instance.paraTrajector , "TrajectorType" , false , DataSourceUpdateMode.OnPropertyChanged);
            this.txtEnumPort.DataBindings.Add("Text", ParaUI.Instance.paraTrajector, "IEnumPort", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        #endregion

        private void cmbEnumWorkMode_SelectedIndexChanged(object sender , EventArgs e)
        {
            if ((sender as ComboBoxEdit).SelectedIndex == 1)
            {
                this.spePowPort1.Enabled = this.spePowPort2.Enabled = this.btneTrajFilePath.Enabled =this.txtEnumPort.Enabled= true;            
            }
            else this.spePowPort1.Enabled = this.spePowPort2.Enabled = this.btneTrajFilePath.Enabled = this.txtEnumPort.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.tabSelectNo;

            this.button2.BackgroundImage = null;
            this.ucsystem.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.tabSelectNo;
            this.button1.BackgroundImage = null;
            this.ucgenerate.BringToFront();
        }
    }
}
