using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hirain.Lib.UI;
using Hirain.Lib.Common;

namespace HRP1679.UI.UC
{
    public partial class UCState : UserControl
    {

        private static UCState instance = null;
        private static object obj = new object();
        public static UCState Instance
        {
            get
            {
                lock (obj)
                {
                    instance = instance ?? new UCState();
                    return instance;
                }
            }
        }

        InfoShower inforShower;
        public UCState()
        {
            InitializeComponent();
            inforShower = new InfoShower();
            this.pnlStatus.Controls.Add(inforShower);
            this.inforShower.Dock = DockStyle.Fill;
            LoggingService.logShowed = MyLog;
            this.inforShower.Height = 50;
            ucSet();
        }

        private void ucSet()
        {
            this.SetStyle(
                  ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.DoubleBuffer |
                  ControlStyles.UserPaint |
                  ControlStyles.ResizeRedraw,
                  true);
            this.pnlStatus.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.pnlStatus, true, null);
        }


        public void MyLog(string strInfo, InformationType type, InformationDisplayMode displayMode)
        {
            Action method = delegate
            {
                if (type == InformationType.Error)
                    inforShower.SetInfo(InfoType.Error, strInfo);
                if (type == InformationType.Normal)
                    inforShower.SetInfo(InfoType.Information, strInfo);
                if (type == InformationType.Success)
                    inforShower.SetInfo(InfoType.Success, strInfo);
                if (type == InformationType.Warning)
                    inforShower.SetInfo(InfoType.Warning, strInfo);
            };
            if (this.InvokeRequired)
                this.Invoke(method);
            else
                method();
        }

    }
}
