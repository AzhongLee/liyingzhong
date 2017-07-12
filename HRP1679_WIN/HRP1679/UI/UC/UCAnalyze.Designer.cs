namespace HRP1679.UI.UC
{
    partial class UCAnalyze
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chbSpectral = new System.Windows.Forms.CheckBox();
            this.chbPulse = new System.Windows.Forms.CheckBox();
            this.chbSAR = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chbSpectral, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chbPulse, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chbSAR, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox4, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 354F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(555, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(255)))), ((int)(((byte)(29)))));
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = " 信号分析";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chbSpectral
            // 
            this.chbSpectral.BackgroundImage = global::HRP1679.Properties.Resources.tabSelect;
            this.chbSpectral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chbSpectral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbSpectral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbSpectral.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.chbSpectral.ForeColor = System.Drawing.Color.White;
            this.chbSpectral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chbSpectral.Location = new System.Drawing.Point(16, 50);
            this.chbSpectral.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbSpectral.Name = "chbSpectral";
            this.chbSpectral.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbSpectral.Size = new System.Drawing.Size(127, 40);
            this.chbSpectral.TabIndex = 1;
            this.chbSpectral.Text = "波形频谱分析";
            this.chbSpectral.UseVisualStyleBackColor = true;
            this.chbSpectral.CheckedChanged += new System.EventHandler(this.chb_CheckedChanged);
            // 
            // chbPulse
            // 
            this.chbPulse.BackgroundImage = global::HRP1679.Properties.Resources.tabSelect;
            this.chbPulse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chbPulse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbPulse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbPulse.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.chbPulse.ForeColor = System.Drawing.Color.White;
            this.chbPulse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chbPulse.Location = new System.Drawing.Point(147, 50);
            this.chbPulse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbPulse.Name = "chbPulse";
            this.chbPulse.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbPulse.Size = new System.Drawing.Size(127, 40);
            this.chbPulse.TabIndex = 1;
            this.chbPulse.Text = "脉冲压缩处理";
            this.chbPulse.UseVisualStyleBackColor = true;
            this.chbPulse.CheckedChanged += new System.EventHandler(this.chb_CheckedChanged);
            // 
            // chbSAR
            // 
            this.chbSAR.BackgroundImage = global::HRP1679.Properties.Resources.tabSelect;
            this.chbSAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chbSAR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbSAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbSAR.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.chbSAR.ForeColor = System.Drawing.Color.White;
            this.chbSAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chbSAR.Location = new System.Drawing.Point(278, 50);
            this.chbSAR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbSAR.Name = "chbSAR";
            this.chbSAR.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbSAR.Size = new System.Drawing.Size(127, 40);
            this.chbSAR.TabIndex = 1;
            this.chbSAR.Text = "SAR成像处理";
            this.chbSAR.UseVisualStyleBackColor = true;
            this.chbSAR.CheckedChanged += new System.EventHandler(this.chb_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.BackgroundImage = global::HRP1679.Properties.Resources.tabSelect;
            this.checkBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.checkBox4.ForeColor = System.Drawing.Color.White;
            this.checkBox4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBox4.Location = new System.Drawing.Point(409, 50);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.checkBox4.Size = new System.Drawing.Size(127, 40);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Text = "波形频谱分析";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.chb_CheckedChanged);
            // 
            // UCAnalyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UCAnalyze";
            this.Size = new System.Drawing.Size(555, 462);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbSpectral;
        private System.Windows.Forms.CheckBox chbPulse;
        private System.Windows.Forms.CheckBox chbSAR;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}
