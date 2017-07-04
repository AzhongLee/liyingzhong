using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.UI.UC
{
    public partial class UCAnalyze : UserControl
    {
        public UCAnalyze()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer |
      ControlStyles.UserPaint |
      ControlStyles.AllPaintingInWmPaint ,
      true);
            this.UpdateStyles();
        }

        private void chb_CheckedChanged(object sender , EventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb.Checked)
            {
                chb.BackgroundImage = Properties.Resources.tabSelectNo;
                chb.ForeColor = Color.Black;
            }
            else 
            {
                chb.BackgroundImage = Properties.Resources.tabSelect;
                chb.ForeColor = Color.White;
            }
        }
        #region"窗口样式设置,防止闪屏"
        protected override CreateParams CreateParams
        {
            //
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= ~0x02000000;
                return cp;
            }
        }
        #endregion
    }
}
