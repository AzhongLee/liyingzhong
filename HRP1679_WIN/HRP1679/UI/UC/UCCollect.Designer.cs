namespace HRP1679.UI.UC
{
    partial class UCCollect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btneUpLoadFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.btnEraseAll = new System.Windows.Forms.Button();
            this.btnErase = new System.Windows.Forms.Button();
            this.dgvSignCollect = new System.Windows.Forms.DataGridView();
            this.cExNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStopTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbWorkMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chbAD1 = new System.Windows.Forms.CheckBox();
            this.chbAD2 = new System.Windows.Forms.CheckBox();
            this.chbAD3 = new System.Windows.Forms.CheckBox();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btneUpLoadFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignCollect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.dgvSignCollect, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.cmbWorkMode, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.chbAD1, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.chbAD2, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.chbAD3, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxEdit1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxEdit2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxEdit3, 5, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(555, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 6);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(255)))), ((int)(((byte)(29)))));
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(461, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = " 参数配置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 7);
            this.panel1.Controls.Add(this.btneUpLoadFilePath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnUpLoad);
            this.panel1.Controls.Add(this.btnEraseAll);
            this.panel1.Controls.Add(this.btnErase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(16, 386);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(515, 74);
            this.panel1.TabIndex = 4;
            // 
            // btneUpLoadFilePath
            // 
            this.btneUpLoadFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btneUpLoadFilePath.Location = new System.Drawing.Point(107, 5);
            this.btneUpLoadFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.btneUpLoadFilePath.Name = "btneUpLoadFilePath";
            this.btneUpLoadFilePath.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.2F);
            this.btneUpLoadFilePath.Properties.Appearance.Options.UseFont = true;
            this.btneUpLoadFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btneUpLoadFilePath.Properties.LookAndFeel.SkinName = "Blue";
            this.btneUpLoadFilePath.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btneUpLoadFilePath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btneUpLoadFilePath.Size = new System.Drawing.Size(408, 26);
            this.btneUpLoadFilePath.TabIndex = 12;
            this.btneUpLoadFilePath.Click += new System.EventHandler(this.btneFilePath_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.2F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "上传文件路径";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.BackgroundImage = global::HRP1679.Properties.Resources.btn2;
            this.btnUpLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpLoad.FlatAppearance.BorderSize = 0;
            this.btnUpLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUpLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpLoad.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnUpLoad.ForeColor = System.Drawing.Color.White;
            this.btnUpLoad.Location = new System.Drawing.Point(5, 40);
            this.btnUpLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(71, 26);
            this.btnUpLoad.TabIndex = 3;
            this.btnUpLoad.Text = "上传";
            this.btnUpLoad.UseVisualStyleBackColor = true;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            this.btnUpLoad.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnUpLoad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnMouseMove);
            // 
            // btnEraseAll
            // 
            this.btnEraseAll.BackgroundImage = global::HRP1679.Properties.Resources.btn2;
            this.btnEraseAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEraseAll.FlatAppearance.BorderSize = 0;
            this.btnEraseAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEraseAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEraseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEraseAll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnEraseAll.ForeColor = System.Drawing.Color.White;
            this.btnEraseAll.Location = new System.Drawing.Point(224, 40);
            this.btnEraseAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnEraseAll.Name = "btnEraseAll";
            this.btnEraseAll.Size = new System.Drawing.Size(71, 26);
            this.btnEraseAll.TabIndex = 3;
            this.btnEraseAll.Text = "全盘擦除";
            this.btnEraseAll.UseVisualStyleBackColor = true;
            this.btnEraseAll.Click += new System.EventHandler(this.btnErase_Click);
            this.btnEraseAll.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnEraseAll.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnMouseMove);
            // 
            // btnErase
            // 
            this.btnErase.BackgroundImage = global::HRP1679.Properties.Resources.btn2;
            this.btnErase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnErase.FlatAppearance.BorderSize = 0;
            this.btnErase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnErase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnErase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnErase.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnErase.ForeColor = System.Drawing.Color.White;
            this.btnErase.Location = new System.Drawing.Point(115, 40);
            this.btnErase.Margin = new System.Windows.Forms.Padding(2);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(71, 26);
            this.btnErase.TabIndex = 3;
            this.btnErase.Text = "擦除";
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            this.btnErase.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnErase.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnMouseMove);
            // 
            // dgvSignCollect
            // 
            this.dgvSignCollect.AllowUserToAddRows = false;
            this.dgvSignCollect.AllowUserToDeleteRows = false;
            this.dgvSignCollect.AllowUserToResizeRows = false;
            this.dgvSignCollect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSignCollect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSignCollect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSignCollect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cExNum,
            this.cChannel,
            this.cOffset,
            this.cLength,
            this.cStartTime,
            this.cStopTime,
            this.cDataType});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvSignCollect, 7);
            this.dgvSignCollect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSignCollect.Location = new System.Drawing.Point(16, 179);
            this.dgvSignCollect.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSignCollect.MultiSelect = false;
            this.dgvSignCollect.Name = "dgvSignCollect";
            this.dgvSignCollect.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSignCollect.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSignCollect.RowHeadersVisible = false;
            this.dgvSignCollect.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSignCollect.RowTemplate.Height = 27;
            this.dgvSignCollect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSignCollect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSignCollect.Size = new System.Drawing.Size(515, 203);
            this.dgvSignCollect.TabIndex = 5;
            // 
            // cExNum
            // 
            this.cExNum.DataPropertyName = "ExNum";
            this.cExNum.FillWeight = 45F;
            this.cExNum.HeaderText = "试验号";
            this.cExNum.Name = "cExNum";
            this.cExNum.ReadOnly = true;
            // 
            // cChannel
            // 
            this.cChannel.DataPropertyName = "Channel";
            this.cChannel.FillWeight = 45F;
            this.cChannel.HeaderText = "通道号";
            this.cChannel.Name = "cChannel";
            this.cChannel.ReadOnly = true;
            // 
            // cOffset
            // 
            this.cOffset.DataPropertyName = "Offset";
            this.cOffset.FillWeight = 62.21447F;
            this.cOffset.HeaderText = "起始地址";
            this.cOffset.Name = "cOffset";
            this.cOffset.ReadOnly = true;
            // 
            // cLength
            // 
            this.cLength.DataPropertyName = "Length";
            this.cLength.FillWeight = 62.21447F;
            this.cLength.HeaderText = "文件长度";
            this.cLength.Name = "cLength";
            this.cLength.ReadOnly = true;
            // 
            // cStartTime
            // 
            this.cStartTime.DataPropertyName = "StartTime";
            this.cStartTime.FillWeight = 62.21447F;
            this.cStartTime.HeaderText = "起始时间";
            this.cStartTime.Name = "cStartTime";
            this.cStartTime.ReadOnly = true;
            // 
            // cStopTime
            // 
            this.cStopTime.DataPropertyName = "StopTime";
            this.cStopTime.FillWeight = 62F;
            this.cStopTime.HeaderText = "结束时间";
            this.cStopTime.Name = "cStopTime";
            this.cStopTime.ReadOnly = true;
            // 
            // cDataType
            // 
            this.cDataType.DataPropertyName = "DataType";
            this.cDataType.FillWeight = 62F;
            this.cDataType.HeaderText = "数据类型";
            this.cDataType.Name = "cDataType";
            this.cDataType.ReadOnly = true;
            // 
            // cmbWorkMode
            // 
            this.cmbWorkMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWorkMode.EditValue = "和差差三路";
            this.cmbWorkMode.Location = new System.Drawing.Point(172, 50);
            this.cmbWorkMode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbWorkMode.Name = "cmbWorkMode";
            this.cmbWorkMode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.cmbWorkMode.Properties.Appearance.Options.UseFont = true;
            this.cmbWorkMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbWorkMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkMode.Properties.Items.AddRange(new object[] {
            "和差差三路",
            "测试模拟器输入输出"});
            this.cmbWorkMode.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbWorkMode.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbWorkMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbWorkMode.Size = new System.Drawing.Size(96, 29);
            this.cmbWorkMode.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 6);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(255)))), ((int)(((byte)(29)))));
            this.label3.Location = new System.Drawing.Point(2, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(461, 48);
            this.label3.TabIndex = 12;
            this.label3.Text = " 存储管理";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(16, 50);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(152, 23);
            this.label17.TabIndex = 13;
            this.label17.Text = "工作模式";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(16, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "通道1数据类型";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(16, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "通道3数据类型";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(278, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 23);
            this.label7.TabIndex = 13;
            this.label7.Text = "通道采集使能";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(278, 77);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 23);
            this.label8.TabIndex = 13;
            this.label8.Text = "通道2数据类型";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chbAD1
            // 
            this.chbAD1.AutoCheck = false;
            this.chbAD1.AutoSize = true;
            this.chbAD1.BackColor = System.Drawing.Color.Transparent;
            this.chbAD1.Dock = System.Windows.Forms.DockStyle.Left;
            this.chbAD1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.chbAD1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbAD1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chbAD1.ForeColor = System.Drawing.Color.White;
            this.chbAD1.Location = new System.Drawing.Point(433, 50);
            this.chbAD1.Margin = new System.Windows.Forms.Padding(2);
            this.chbAD1.Name = "chbAD1";
            this.chbAD1.Size = new System.Drawing.Size(30, 23);
            this.chbAD1.TabIndex = 14;
            this.chbAD1.Text = "AD1";
            this.chbAD1.UseVisualStyleBackColor = true;
            this.chbAD1.CheckedChanged += new System.EventHandler(this.chbCheckedChanged);
            // 
            // chbAD2
            // 
            this.chbAD2.AutoCheck = false;
            this.chbAD2.AutoSize = true;
            this.chbAD2.BackColor = System.Drawing.Color.Transparent;
            this.chbAD2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbAD2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.chbAD2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbAD2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chbAD2.ForeColor = System.Drawing.Color.White;
            this.chbAD2.Location = new System.Drawing.Point(467, 50);
            this.chbAD2.Margin = new System.Windows.Forms.Padding(2);
            this.chbAD2.Name = "chbAD2";
            this.chbAD2.Size = new System.Drawing.Size(30, 23);
            this.chbAD2.TabIndex = 15;
            this.chbAD2.Text = "AD2";
            this.chbAD2.UseVisualStyleBackColor = false;
            this.chbAD2.CheckedChanged += new System.EventHandler(this.chbCheckedChanged);
            // 
            // chbAD3
            // 
            this.chbAD3.AutoCheck = false;
            this.chbAD3.AutoSize = true;
            this.chbAD3.BackColor = System.Drawing.Color.Transparent;
            this.chbAD3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbAD3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.chbAD3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbAD3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chbAD3.ForeColor = System.Drawing.Color.White;
            this.chbAD3.Location = new System.Drawing.Point(501, 50);
            this.chbAD3.Margin = new System.Windows.Forms.Padding(2);
            this.chbAD3.Name = "chbAD3";
            this.chbAD3.Size = new System.Drawing.Size(30, 23);
            this.chbAD3.TabIndex = 16;
            this.chbAD3.Text = "AD3";
            this.chbAD3.UseVisualStyleBackColor = false;
            this.chbAD3.CheckedChanged += new System.EventHandler(this.chbCheckedChanged);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxEdit1.EditValue = "雷达发射信号";
            this.comboBoxEdit1.Location = new System.Drawing.Point(172, 77);
            this.comboBoxEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "雷达发射信号",
            "和路回波",
            "方位差",
            "俯仰差"});
            this.comboBoxEdit1.Properties.LookAndFeel.SkinName = "Blue";
            this.comboBoxEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(96, 29);
            this.comboBoxEdit1.TabIndex = 11;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxEdit2.EditValue = "雷达发射信号";
            this.comboBoxEdit2.Location = new System.Drawing.Point(172, 104);
            this.comboBoxEdit2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.comboBoxEdit2.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "雷达发射信号",
            "和路回波",
            "方位差",
            "俯仰差"});
            this.comboBoxEdit2.Properties.LookAndFeel.SkinName = "Blue";
            this.comboBoxEdit2.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(96, 29);
            this.comboBoxEdit2.TabIndex = 11;
            // 
            // comboBoxEdit3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.comboBoxEdit3, 3);
            this.comboBoxEdit3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxEdit3.EditValue = "雷达发射信号";
            this.comboBoxEdit3.Location = new System.Drawing.Point(433, 77);
            this.comboBoxEdit3.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.comboBoxEdit3.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Properties.Items.AddRange(new object[] {
            "雷达发射信号",
            "和路回波",
            "方位差",
            "俯仰差"});
            this.comboBoxEdit3.Properties.LookAndFeel.SkinName = "Blue";
            this.comboBoxEdit3.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.comboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit3.Size = new System.Drawing.Size(98, 29);
            this.comboBoxEdit3.TabIndex = 11;
            // 
            // UCCollect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UCCollect";
            this.Size = new System.Drawing.Size(555, 462);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btneUpLoadFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignCollect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpLoad;
        private System.Windows.Forms.Button btnErase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ButtonEdit btneUpLoadFilePath;
        private System.Windows.Forms.DataGridView dgvSignCollect;
        private System.Windows.Forms.Button btnEraseAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn cExNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStopTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDataType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbWorkMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chbAD1;
        private System.Windows.Forms.CheckBox chbAD2;
        private System.Windows.Forms.CheckBox chbAD3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
    }
}
