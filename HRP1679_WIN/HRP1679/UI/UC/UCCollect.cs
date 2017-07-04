using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRP1679.DAL.Para;
using HRP1679.BLL.Other;

namespace HRP1679.UI.UC
{
    public partial class UCCollect : UserControl
    {
        public UCCollect()
        {
            InitializeComponent();

            chbAD1.Checked = chbAD2.Checked = true;

            SetDataGridViewStyle();
            DataBinding();
            this.SetStyle(ControlStyles.DoubleBuffer |
      ControlStyles.UserPaint |
      ControlStyles.AllPaintingInWmPaint ,
      true);
            this.UpdateStyles();
        }
        #region"参数绑定"

        private void btneFilePath_Click(object sender , EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               this.btneUpLoadFilePath.Text=  ParaUI.Instance.paraCollect.UpLoadPath = dialog.SelectedPath;
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

        #region"表格样式"
        /// <summary>
        /// 设置表格样式
        /// </summary>
        private void SetDataGridViewStyle()
        {
            this.dgvSignCollect.AllowUserToAddRows = false;
            this.dgvSignCollect.AllowUserToDeleteRows = false;
            //   //dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            this.dgvSignCollect.ForeColor = System.Drawing.Color.White;//字颜色
            //   this.dgvSignCollect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSignCollect.BackgroundColor = System.Drawing.Color.FromArgb(41 , 63 , 112);//表格背景色
            this.dgvSignCollect.BorderStyle = System.Windows.Forms.BorderStyle.None;//表格的外边框
            this.dgvSignCollect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;//标题边框
         //   this.dgvSignCollect.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;//211, 223, 240
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(29 , 43 , 78);//标题背景色
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软雅黑" , 10.2F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , (byte)134);
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(188 , 188 , 188);//标题字颜色
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvSignCollect.RowHeadersVisible = false;
            this.dgvSignCollect.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41 , 63 , 112);//表格背景色
            this.dgvSignCollect.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //this.dgvSignCollect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dgvSignCollect.EnableHeadersVisualStyles = false;
            this.dgvSignCollect.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvSignCollect.ReadOnly = true;
          //  this.dgvSignCollect.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion
        #region"通道选择样式"
        private void chbCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb.Checked)
                chb.ForeColor = Color.FromArgb(0, 191, 255);
            else
                chb.ForeColor = Color.White;
        }
        #endregion
        #region"参数绑定"
        private void DataBinding()
        {
            this.dgvSignCollect.DataBindings.Add("DataSource",ParaUI.Instance.paraCollect,"DMAParas",false,DataSourceUpdateMode.OnPropertyChanged);
          //  this.dgvSignCollect.DataSource = ParaUI.Instance.paraCollect.DMAParas;
            this.btneUpLoadFilePath.DataBindings.Add("Text" , ParaUI.Instance.paraCollect , "UpLoadPath",false,DataSourceUpdateMode.OnPropertyChanged);
            this.cmbWorkMode.DataBindings.Add("SelectedIndex", ParaUI.Instance.paraCollect, "WorkModel", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chbAD3.DataBindings.Add("Checked", ParaUI.Instance.paraCollect, "IADch3", false, DataSourceUpdateMode.Never);
        }

        #endregion

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpLoad_Click(object sender , EventArgs e)
        {
            try
            {
                ParaUI.Instance.paraCollect.TransferBlock.Clear();
                //for(int i=0;i<this.dgvSignCollect.SelectedRows.Count;i++)
                //ParaUI.Instance.paraCollect.TransferBlock.Add((uint)this.dgvSignCollect.SelectedRows[i].Index);
                // for (int i = 0; i < this.dgvSignCollect.SelectedRows.Count; i++)

                ParaUI.Instance.paraCollect.TransferCell.AddRange(ParaUI.Instance.paraCollect.Cell[this.dgvSignCollect.SelectedRows[0].Index]);
                EventDef.TransferData();
            }
            catch(Exception)
            {
            //donothing
            }
        }

        private void btnErase_Click(object sender , EventArgs e)
        {
            ParaUI.Instance.paraCollect.CleanCell.Clear();

            #region"支持多项删除，不支持从中间删除"
            //List<int> selectlist = new List<int>();
            //for (int i = 0; i < this.dgvSignCollect.SelectedRows.Count; i++)
            //   selectlist.Add(this.dgvSignCollect.SelectedRows[i].Index);
            ////对所选的Block号进行排序 从大到小排序
            //selectlist.Sort((x , y) => -x.CompareTo(y));
            ////从后往前依次删除，不支持从中间删除
            //for (int i =0 ; i <selectlist.Count; i++)
            //    if (this.dgvSignCollect.Rows.Count - i == selectlist[i])
            //    {
            //        ParaUI.Instance.paraCollect.CleanCell.AddRange(ParaUI.Instance.paraCollect.Cell[selectlist[i]]);
            //    }
            //    else
            //        break;
            #endregion

            #region"支持多选删除和中间位置删除"
            for (int i = 0; i < this.dgvSignCollect.SelectedRows.Count; i++)
                ParaUI.Instance.paraCollect.CleanCell.AddRange(ParaUI.Instance.paraCollect.Cell[this.dgvSignCollect.SelectedRows[i].Index]);
            #endregion

             //判断是否全部删除
            if ((sender as Button) == this.btnErase)
                ParaUI.Instance.paraCollect.CleanAll = 0;
            else
                ParaUI.Instance.paraCollect.CleanAll = 1;
            EventDef.EraseData();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ParaUI.Instance.paraCollect.DMAParas.Add(new ParaDMA());
        }
    }
}
