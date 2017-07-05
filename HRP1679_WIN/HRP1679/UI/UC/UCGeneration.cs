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
    public partial class UCGeneration : UserControl
    {
        public UCGeneration()
        {
            InitializeComponent();
            ReplaySet();
            DataBinding();
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

        /// <summary>
        /// 是否手动配置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSignDataTypeSelectedIndexChanged(object sender, EventArgs e)
        {          
            if ((sender as ComboBoxEdit).SelectedIndex == 0)
                ControlsVisible(this.cmbSignType.SelectedIndex, true);
            else
                ControlsVisible(this.cmbSignType.SelectedIndex, false);
        }
        /// <summary>
        /// 信号数据类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSignType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSignDataType.SelectedIndex == 0)
                ControlsVisible((sender as ComboBoxEdit).SelectedIndex, true);
            else
                ControlsVisible((sender as ComboBoxEdit).SelectedIndex, false);
        }
        /// <summary>
        /// 空间可见性控制
        /// </summary>
        /// <param name="num">选择的模式</param>
        /// <param name="auto">是否为手动配置参数</param>
        private void ControlsVisible(int num, bool auto)
        {
            if (auto)//手动配置参数
                switch (num)
                {
                    case 0:
                        ReplaySet();
                        break;
                    case 1:
                        SeriaWaveSet();
                        break;
                    case 2:
                        PauseSet();
                        break;
                    case 3:
                        ChirpSet();
                        break;
                    case 4:
                        PhaseCode();
                        break;
                    case 5:
                        PulseAgile();
                        break;
                    case 6:
                        GroupAgile();
                        break;
                    default:
                        break;
                }
            else
            {
                SetAllInvisible();
                this.lblPath.Visible = this.btnePath.Visible = true;
             //  SetRowStyleToAbsolute(this.lblPath);
            }
        }
        /// <summary>
        /// 组间捷变参数
        /// </summary>
        private void GroupAgile()
        {
         //   SetAutoHeight();
            for (int i = 2; i < this.tlpGeneral.RowCount; i++)
            {
                for (int j = 0; j < this.tlpGeneral.ColumnCount; j++)
                {
                    Control ctrl = this.tlpGeneral.GetControlFromPosition(j, i);
                    if (ctrl != null)
                    {
                        SetRowStyleToAbsolute(ctrl);
                        ctrl.Visible = true;
                       // this.tlpGeneral.RowStyles[i].Height = 34;
                    }
                }
            }
            this.lblCodeLength.Visible = this.speCodeLength.Visible = false;
            this.lblCodeWord.Visible = this.speCodeWord.Visible = false;
            SetRowStyleToAuto(this.lblCodeWord);
            this.lblPath.Visible = this.btnePath.Visible = false;
        }

        /// <summary>
        /// 脉间捷变参数
        /// </summary>
        private void PulseAgile()
        {
           // SetAutoHeight();
            for (int i = 2; i < this.tlpGeneral.RowCount; i++)
            {
                for (int j = 0; j < this.tlpGeneral.ColumnCount; j++)
                {
                    Control ctrl = this.tlpGeneral.GetControlFromPosition(j, i);
                    if (ctrl != null)
                    {
                        SetRowStyleToAbsolute(ctrl);
                        ctrl.Visible = true;
                       
                    }
                }
            }
            SetRowStyleToAuto(this.lblCodeWord);

            this.lblFrameNum.Visible = this.speFrameNum.Visible = false;
            this.lblCodeLength.Visible = this.speCodeLength.Visible = false;
            this.lblCodeWord.Visible = this.speCodeWord.Visible = false;
             this.lblPath.Visible = this.btnePath.Visible = false;

        }


        /// <summary>
        /// 相位编码参数
        /// </summary>
        private void PhaseCode()
        {
            SetAllInvisible();
            SetRowStyleToAbsolute(this.lblPRFPer);
            SetRowStyleToAbsolute(this.lblPRFPaulse);
            SetRowStyleToAbsolute(this.lblCodeLength);
            SetRowStyleToAbsolute(this.lblCodeWord);
            SetRowStyleToAbsolute(this.speStartFc);

            this.lblStartFc.Visible = this.speStartFc.Visible = true;
            this.lblPRFPer.Visible = this.spePRFPeriod.Visible = true;
            this.lblPRFPaulse.Visible = this.spePRFPaulseWidth.Visible = true;
            this.lblCodeLength.Visible = this.speCodeLength.Visible = true;
            this.lblCodeWord.Visible = this.speCodeWord.Visible = true;
            this.lblPath.Visible = this.btnePath.Visible = false;


            
        }



        /// <summary>
        /// Chirp模式参数
        /// </summary>
        private void ChirpSet()
        {
            SetAllInvisible();
            SetRowStyleToAbsolute(this.lblStartFc);
            SetRowStyleToAbsolute(this.lbl);
            SetRowStyleToAbsolute(this.lblPRFPaulse);
            SetRowStyleToAbsolute(this.lblPRFPer);

            this.lblStartFc.Visible = this.speStartFc.Visible = true;
            this.lbl.Visible = this.speFcModulRate.Visible = true;
            this.lblPRFPer.Visible = this.spePRFPeriod.Visible = true;
            this.lblPRFPaulse.Visible = this.spePRFPaulseWidth.Visible = true;
            this.lblPath.Visible = this.btnePath.Visible = false;

         

        }

        /// <summary>
        /// 脉冲点频模式参数
        /// </summary>
        private void PauseSet()
        {
            SetAllInvisible();
            SetRowStyleToAbsolute(this.lblStartFc);
            SetRowStyleToAbsolute(this.lblPRFPer);
            SetRowStyleToAbsolute(this.lblPRFPaulse);

            this.lblStartFc.Visible = this.speStartFc.Visible = true;
            this.lblPRFPer.Visible = this.spePRFPeriod.Visible = true;
            this.lblPRFPaulse.Visible = this.spePRFPaulseWidth.Visible = true;
            this.lblPath.Visible = this.btnePath.Visible = false;
          
        }



        /// <summary>
        /// 连续波模式参数显示
        /// </summary>
        private void SeriaWaveSet()
        {
            SetAllInvisible();
            this.lblStartFc.Visible = this.speStartFc.Visible = true;
            this.lblPath.Visible = this.btnePath.Visible = false;
            SetRowStyleToAbsolute(this.lblStartFc);
           // SetRowStyleToAbsolute(this.lblPath);
            //this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(this.lblStartFc)].SizeType = SizeType.Absolute;
            //this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(this.lblPath)].SizeType = SizeType.Absolute;
            //this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(this.lblStartFc)].Height = 34;
            //this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(this.lblPath)].Height = 34;
        }

        /// <summary>
        /// 设置制定行 高度为Auto
        /// </summary>
        /// <param name="ctrl"></param>
        private void SetRowStyleToAuto(Control ctrl)
        {
            this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(ctrl)].SizeType = SizeType.AutoSize;
         //   this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(ctl)].Height = 38;
        }

        /// <summary>
        /// 设置行高为34
        /// </summary>
        /// <param name="ctl"></param>
        private void SetRowStyleToAbsolute(Control ctl)
        {
            this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(ctl)].SizeType = SizeType.Absolute;
            this.tlpGeneral.RowStyles[this.tlpGeneral.GetRow(ctl)].Height = 38;
        }
        
        /// <summary>
        /// 所有手动配置参数不可见
        /// </summary>
        private void SetAllInvisible()
        {
            SetAutoHeight();
            for (int i = 2; i < this.tlpGeneral.RowCount; i++)
            {
                for (int j = 0; j < this.tlpGeneral.ColumnCount; j++)
                {
                    Control ctrl = this.tlpGeneral.GetControlFromPosition(j, i);
                    if (ctrl != null)
                        ctrl.Visible = false;
                }
            }
        }
        /// <summary>
        /// 回放模式参数显示
        /// </summary>
        private void ReplaySet()
        {
            SetAllInvisible();
            this.lblPath.Visible = this.btnePath.Visible = true;
        }
        /// <summary>
        /// 获取参数文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnePath_EditValueChanged(object sender, EventArgs e)
        {
            this.ofdSignal.Filter = "*.txt|*.txt";
            this.ofdSignal.FilterIndex = 1;
            if (this.ofdSignal.ShowDialog() != DialogResult.Cancel)
            {
                ParaUI.Instance.paraSignal.SingalDataFilePath = this.ofdSignal.FileName;
                this.btnePath.Text = this.ofdSignal.SafeFileName;
            }
        }
        /// <summary>
        /// 设置自动行高，从第二行起
        /// </summary>
        private void SetAutoHeight()
        {
            for (int i = 2; i < this.tlpGeneral.RowCount; i++)
            {
                this.tlpGeneral.RowStyles[i].SizeType = SizeType.AutoSize;
            }
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        private void DataBinding()
        {
          
            this.cmbSignType.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraSignal, "SignalType", false, DataSourceUpdateMode.OnPropertyChanged);        
            this.cmbSignDataType.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraSignal, "SignalDataType", false, DataSourceUpdateMode.OnPropertyChanged);
            this.spePRFPeriod.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "PRFCycle", false, DataSourceUpdateMode.OnPropertyChanged);
            this.spePRFPaulseWidth.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "PRFPulseWidth", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speStartFc.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "StartFrequency", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speFcModulRate.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "BandWidth", false, DataSourceUpdateMode.OnPropertyChanged);

            this.speCodeLength.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "CodeLength", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speCodeWord.DataBindings.Add("Value", ParaUI.Instance.paraSignal, "CodeWord", false, DataSourceUpdateMode.OnPropertyChanged);
            this.speFramePeriod.DataBindings.Add("Value",ParaUI.Instance.paraSignal, "FramePeriod",false,DataSourceUpdateMode.OnPropertyChanged);
            this.speFramePaulseWidth.DataBindings.Add("Value",ParaUI.Instance.paraSignal, "FramePulseWidth",false,DataSourceUpdateMode.OnPropertyChanged);
            this.speFrameNum.DataBindings.Add("Value",ParaUI.Instance.paraSignal, "FrameNum",false,DataSourceUpdateMode.OnPropertyChanged);
            this.speFrenquencyStep.DataBindings.Add("Value",ParaUI.Instance.paraSignal, "FrequencyStep",false,DataSourceUpdateMode.OnPropertyChanged);
            this.spePRFNum.DataBindings.Add("Value",ParaUI.Instance.paraSignal, "PrfNum", false,DataSourceUpdateMode.OnPropertyChanged);
        }

    }
}
