using HRP1679.DAL.Common;
using HRP1679.UI.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HRP1679.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            GetNavButtons();
            Initialization();
            btnSignCreate_Click(this.btnSignCreate,null);
            m_SyncContext = SynchronizationContext.Current;
          //  Control.CheckForIllegalCrossThreadCalls = false;//允许子线程控制主线程的控件    
          //  this.SetStyle(ControlStyles.DoubleBuffer |
          //   ControlStyles.UserPaint |
          //   ControlStyles.AllPaintingInWmPaint ,
          //true);
          //  this.UpdateStyles();
          //  this.splitMain.GetType().GetProperty("DoubleBuffered" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.splitMain , true , null);
          //  this.splitContent.GetType().GetProperty("DoubleBuffered" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.splitContent , true , null);

          //  this.splitMain.Panel2.GetType().GetProperty("DoubleBuffered" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.splitMain , true , null);
           // this.splitMain
        }
        #region"单例"
        private static FormMain instance;
        private static object lockObj = new object();
        public static FormMain Instance
        {
            get
            {
                lock (lockObj)
                {
                        instance =instance?? new FormMain();
                    return instance;
                };
            }
        }
        #endregion

        #region"变量"
        public SynchronizationContext m_SyncContext = null;
        private List<Button> navBtns=new List<Button>();   //底部导航按钮
        bool click = false; //导航按钮点击标志
        UCInfo ucInfo;
        UCSignal ucSignal;
        UCCollect ucCollect;
        UCAnalyze ucAnalyze;
        #endregion

        #region"初始化"
        private void Initialization()
        {
            ucInfo = new UCInfo();
            ucSignal = new UCSignal();
            ucCollect = new UCCollect();
            ucAnalyze = new UCAnalyze();

            splitContent.Panel2.Controls.Add(ucInfo);
            ucInfo.Dock = DockStyle.Fill;
            ucInfo.BringToFront();
            

            splitContent.Panel1.Controls.Add(ucSignal);
            ucSignal.Dock = DockStyle.Fill;
            ucSignal.BringToFront();

            splitContent.Panel1.Controls.Add(ucCollect);
            ucCollect.Dock = DockStyle.Fill;

            splitContent.Panel1.Controls.Add(ucAnalyze);
            ucAnalyze.Dock = DockStyle.Fill;

            DataBinding();
            AddHotKeyWvent();
        }
        #endregion

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

        #region"双击放大缩小"
        private void TitleMouseDoubleClick(object sender , MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
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

        #region"退出关闭按钮样式及事件"
        private void btnExit_MouseMove(object sender , MouseEventArgs e)
        {
          //  this.btnExit.Image = imlistMain.Images[1];
            this.btnExit.BackColor = Color.ForestGreen;
        }

        private void btnExit_MouseLeave(object sender , EventArgs e)
        {
          //  this.btnExit.Image = imlistMain.Images[0];
            this.btnExit.BackColor = Color.Transparent;
            
        }

        private void btnClose_MouseMove(object sender , MouseEventArgs e)
        {
         //   this.btnClose.Image = imlistMain.Images[2];
            this.btnClose.BackColor = Color.ForestGreen;
            
        }

        private void btnClose_MouseLeave(object sender , EventArgs e)
        {
          //  this.btnClose.Image = imlistMain.Images[3];
            this.btnClose.BackColor = Color.Transparent;
        }

        private void btnExit_Click(object sender , EventArgs e)
        {
            HRP1679.DAL.Para.ParaUI.Instance.SaveParaToXML();
            System.Environment.Exit(0);
        }

        private void btnClose_Click(object sender , EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region"底部导航按钮样式及事件"
        private void GetNavButtons()
        {
            navBtns.Clear();
            foreach (var ctrl in this.pnlNav.Controls)
            {
                if (ctrl is Button)
                    navBtns.Add(ctrl as Button);
            }
        }
        private void navBtnMouseMove(object sender , MouseEventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.menuCur;
        }
        private void navBtnMouseLeave(object sender , EventArgs e)
        {
          Button btn= sender as Button;
           if(btn.AccessibleName == null)
            btn.BackgroundImage = Properties.Resources.menu;
          // click = false;
          
        }
        private void SetBackGroundImage(Button btn)
        {
           
           // click = true;
            btn.AccessibleName = "Ac";
            btn.BackgroundImage = Properties.Resources.menuCur;
            foreach(var ctrl in navBtns)
                if (ctrl != btn)
                {
                    ctrl.BackgroundImage = Properties.Resources.menu;
                    ctrl.AccessibleName = null;
                }
        }
        private void btnSignCreate_Click(object sender , EventArgs e)
        {
            Button btn = sender as Button;
            SetBackGroundImage(btn);
            ucSignal.BringToFront();
        }

        private void btnSignCollect_Click(object sender , EventArgs e)
        {
            ucCollect.BringToFront();
            Button btn = sender as Button;
            SetBackGroundImage(btn);
          
        }

        private void btnSignAnalyze_Click(object sender , EventArgs e)
        {
            ucAnalyze.BringToFront();
            Button btn = sender as Button;
            SetBackGroundImage(btn);
        }
        #endregion

        private void DataBinding()
        {
               var bind = new Binding("Enabled" , HRP1679.DAL.Para.ParaUI.Instance.paraStatus , "BtnStartEnable");
              // bind.Format += delegate(object o, ConvertEventArgs args) { args.Value = !((bool) args.Value); };
               this.ucSignal.DataBindings.Add(bind);

          
             
               this.ucAnalyze.DataBindings.Add("Enabled" , HRP1679.DAL.Para.ParaUI.Instance.paraStatus , "BtnStartEnable", false , DataSourceUpdateMode.Never);
               this.ucCollect.DataBindings.Add("Enabled" , HRP1679.DAL.Para.ParaUI.Instance.paraStatus , "BtnStartEnable" , false , DataSourceUpdateMode.Never);
           //    this.ucCollect.Enabled = false;
            
        }


        #region"Ctrl+G调出调试界面"
        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWind , int id , uint control , Keys vk);
        private void AddHotKeyWvent()
        {
            RegisterHotKey(this.Handle , 731 , 2 , Keys.G);
            RegisterHotKey(this.Handle , 746 , 2 , Keys.D);
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x312:
                    if (m.WParam.ToString().Equals("731"))
                    {
                        ConstData.IsDebugUsed = !ConstData.IsDebugUsed;
                        if (ConstData.IsDebugUsed)
                        {
                            FormDebug.Instance.Show();
                            FormDebug.Instance.TopMost = true;
                        }
                        else
                            FormDebug.Instance.Hide();
                    }
                    if (m.WParam.ToString().Equals("746"))
                    {
                        ConstData.IsDebugUsed = !ConstData.IsDebugUsed;
                        if (ConstData.IsDebugUsed)
                        {
                            FormState.Instance.Show();
                            FormState.Instance.TopMost = true;
                        }
                        else
                            FormState.Instance.Hide();

                    }
                    break;

            }
            base.WndProc(ref m);
        }
        #endregion

        private void FormMain_Load(object sender , EventArgs e)
        {
            HRP1679.BLL.Other.EventDef.Initialation();
        }

    }
}
