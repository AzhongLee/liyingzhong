using HRP1679.UI.UC;
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
    public partial class FormState : Form
    {

        public FormState()
        {
            InitializeComponent();
           
        }

        public void FormLoad()
        {
            this.Controls.Add(UCState.Instance);
            UCState.Instance.Dock = DockStyle.Fill;
            UCState.Instance.textBox1.MouseDown += new MouseEventHandler(TxtMouseDown);
        }
        private static FormState instance = null;
        private static object obj = new object();
        public static FormState Instance
        {
            get 
            {
                lock (obj)
                {
                    instance = instance ?? new FormState();
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
        private void TxtMouseDown(object sender , MouseEventArgs e)
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

    }
}
