namespace HRP1679.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //设定按字体来缩放控件  
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;  
            //设定字体大小为12px       
            this.Font = new System.Drawing.Font("微软雅黑" , 13.6F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Pixel , ((byte)(134)));  
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btnSignAnalyze = new System.Windows.Forms.Button();
            this.btnSignCollect = new System.Windows.Forms.Button();
            this.btnSignCreate = new System.Windows.Forms.Button();
            this.splitContent = new System.Windows.Forms.SplitContainer();
            this.imlistMain = new System.Windows.Forms.ImageList(this.components);
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            this.pnlNav.SuspendLayout();
            this.splitContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.Transparent;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.IsSplitterFixed = true;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.btnExit);
            this.splitMain.Panel1.Controls.Add(this.btnClose);
            this.splitMain.Panel1.Controls.Add(this.picTitle);
            this.splitMain.Panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TitleMouseDoubleClick);
            this.splitMain.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TtitleMouseDown);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlNav);
            this.splitMain.Panel2.Controls.Add(this.splitContent);
            this.splitMain.Size = new System.Drawing.Size(1024, 768);
            this.splitMain.SplitterDistance = 88;
            this.splitMain.SplitterWidth = 1;
            this.splitMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::HRP1679.Properties.Resources.exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(901, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            this.btnExit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::HRP1679.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(960, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关机";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseMove);
            // 
            // picTitle
            // 
            this.picTitle.Image = global::HRP1679.Properties.Resources.logo;
            this.picTitle.Location = new System.Drawing.Point(14, 21);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(573, 58);
            this.picTitle.TabIndex = 0;
            this.picTitle.TabStop = false;
            this.picTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TitleMouseDoubleClick);
            this.picTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TtitleMouseDown);
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.btnSignAnalyze);
            this.pnlNav.Controls.Add(this.btnSignCollect);
            this.pnlNav.Controls.Add(this.btnSignCreate);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNav.Location = new System.Drawing.Point(0, 587);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(1024, 92);
            this.pnlNav.TabIndex = 1;
            // 
            // btnSignAnalyze
            // 
            this.btnSignAnalyze.BackgroundImage = global::HRP1679.Properties.Resources.menu;
            this.btnSignAnalyze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSignAnalyze.FlatAppearance.BorderSize = 0;
            this.btnSignAnalyze.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSignAnalyze.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSignAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignAnalyze.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnSignAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnSignAnalyze.Location = new System.Drawing.Point(732, 24);
            this.btnSignAnalyze.Name = "btnSignAnalyze";
            this.btnSignAnalyze.Size = new System.Drawing.Size(196, 68);
            this.btnSignAnalyze.TabIndex = 2;
            this.btnSignAnalyze.Text = "信号分析";
            this.btnSignAnalyze.UseVisualStyleBackColor = true;
            this.btnSignAnalyze.Click += new System.EventHandler(this.btnSignAnalyze_Click);
            this.btnSignAnalyze.MouseLeave += new System.EventHandler(this.navBtnMouseLeave);
            this.btnSignAnalyze.MouseMove += new System.Windows.Forms.MouseEventHandler(this.navBtnMouseMove);
            // 
            // btnSignCollect
            // 
            this.btnSignCollect.BackgroundImage = global::HRP1679.Properties.Resources.menu;
            this.btnSignCollect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSignCollect.FlatAppearance.BorderSize = 0;
            this.btnSignCollect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSignCollect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSignCollect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignCollect.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnSignCollect.ForeColor = System.Drawing.Color.White;
            this.btnSignCollect.Location = new System.Drawing.Point(413, 24);
            this.btnSignCollect.Name = "btnSignCollect";
            this.btnSignCollect.Size = new System.Drawing.Size(196, 68);
            this.btnSignCollect.TabIndex = 1;
            this.btnSignCollect.Text = "信号采集";
            this.btnSignCollect.UseVisualStyleBackColor = true;
            this.btnSignCollect.Click += new System.EventHandler(this.btnSignCollect_Click);
            this.btnSignCollect.MouseLeave += new System.EventHandler(this.navBtnMouseLeave);
            this.btnSignCollect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.navBtnMouseMove);
            // 
            // btnSignCreate
            // 
            this.btnSignCreate.BackgroundImage = global::HRP1679.Properties.Resources.menu;
            this.btnSignCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSignCreate.FlatAppearance.BorderSize = 0;
            this.btnSignCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSignCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSignCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignCreate.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btnSignCreate.ForeColor = System.Drawing.Color.White;
            this.btnSignCreate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSignCreate.Location = new System.Drawing.Point(95, 24);
            this.btnSignCreate.Name = "btnSignCreate";
            this.btnSignCreate.Size = new System.Drawing.Size(196, 68);
            this.btnSignCreate.TabIndex = 0;
            this.btnSignCreate.Text = "信号生成";
            this.btnSignCreate.UseVisualStyleBackColor = true;
            this.btnSignCreate.Click += new System.EventHandler(this.btnSignCreate_Click);
            this.btnSignCreate.MouseLeave += new System.EventHandler(this.navBtnMouseLeave);
            this.btnSignCreate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.navBtnMouseMove);
            // 
            // splitContent
            // 
            this.splitContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContent.Location = new System.Drawing.Point(3, 3);
            this.splitContent.Name = "splitContent";
            this.splitContent.Size = new System.Drawing.Size(1018, 578);
            this.splitContent.SplitterDistance = 788;
            this.splitContent.SplitterWidth = 1;
            this.splitContent.TabIndex = 0;
            // 
            // imlistMain
            // 
            this.imlistMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlistMain.ImageStream")));
            this.imlistMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlistMain.Images.SetKeyName(0, "exit.png");
            this.imlistMain.Images.SetKeyName(1, "exitHover.png");
            this.imlistMain.Images.SetKeyName(2, "close.png");
            this.imlistMain.Images.SetKeyName(3, "closeHover.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::HRP1679.Properties.Resources.main;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            this.pnlNav.ResumeLayout(false);
            this.splitContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imlistMain;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.SplitContainer splitContent;
        private System.Windows.Forms.Button btnSignAnalyze;
        private System.Windows.Forms.Button btnSignCollect;
        private System.Windows.Forms.Button btnSignCreate;
    }
}