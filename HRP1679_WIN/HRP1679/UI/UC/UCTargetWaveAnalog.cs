using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRP1524.DAL;
using System.Threading;
using HRP1524.BLL;
using System.Diagnostics;
using HRP1524.Model;

namespace HRP1524.UC
{
    public partial class UCTargetWaveAnalog : UserControl
    {
        #region 属性
        /// <summary>
        /// 该指标指示被拖动的对象是否进入了控件的边界
        /// </summary>
        bool bMouseDown;

        Form2 form2 = new Form2();
        public Form3 form3 = new Form3();
        public List<TargetInfo> targetInfoList { get; set; }
        public List<SystemParameters> systemParametersList { get; set; }
        private Thread thdSetGridViewStyle;
        private Process prcRtx = new Process();
        private WindowManager winMng = new WindowManager();
        private RtxDataPacker rtxPacker = new RtxDataPacker();
        private bool _allowDrop;
        public bool AllowDrop
        {
            get
            {
                return _allowDrop;
            }
            set
            {
                _allowDrop = value;
                this.panel2.AllowDrop = _allowDrop;
                this.panel3.AllowDrop = _allowDrop;
            }
        }
        public NetMode netModeType { get; set; }
        #endregion

        #region 构造函数
        public UCTargetWaveAnalog()
        {
            InitializeComponent();
            //this.button1.FlatAppearance.BorderColor = Color.Transparent;
            this.label7.Text = "10700";
            this.label8.Text = "76500";
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(112)))));
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(112)))));
            //this.label5.ForeColor =
            netModeType = NetMode.Local;
            SimController.Instance.ToWinDataBytes += new SimController.ToWinDataBytesEventHandler(Instance_ToWinDataBytes);
            InitSystemParameters();
            systemParametersList = form3.systemParametersList;
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(43)))), ((int)(((byte)(78)))));
            //SetDataGridViewStyle();
            this.panel3.Visible = true;
            this.panel6.Visible = false;

            thdSetGridViewStyle = new Thread(SetDataGridViewStyle);
            thdSetGridViewStyle.Start();

            this.StartRtxApp();
            // 默认为 false,即不接受用户拖动到其上的控件

            this.panel2.AllowDrop = true;
            this.panel3.AllowDrop = true;
            // 拖动对象进入控件边界时触发

            //this.panel2.DragEnter += new DragEventHandler(Panel_DragEnter);
            //this.panel3.DragEnter += new DragEventHandler(Panel_DragEnter);
            //// 完成拖动时触发
            //this.panel2.DragDrop += new DragEventHandler(Panel_DragDrop);
            //this.panel3.DragDrop += new DragEventHandler(Panel_DragDrop);

            // 拖动对象进入控件边界时触发
            panel2.DragEnter += new DragEventHandler(panel2_DragEnter);
            panel3.DragEnter += new DragEventHandler(panel2_DragEnter);
            //// 完成拖动时触发
            panel2.DragDrop += new DragEventHandler(panel2_DragDrop);
            panel3.DragDrop += new DragEventHandler(panel2_DragDrop);
        }
        #endregion

        #region 方法
        /// <summary>
        /// RTX返回的数据解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="winData"></param>
        void Instance_ToWinDataBytes(object sender, ToWinPack winData)
        {
            Action method = delegate
            {
                this.button6.Visible = false;
                this.button7.Visible = false;
                this.button8.Visible = false;
                this.button9.Visible = false;

                this.button10.Visible = false;
                this.button11.Visible = false;
                this.button12.Visible = false;
                this.button13.Visible = false;

                this.button14.Visible = false;
                this.button15.Visible = false;
                this.button16.Visible = false;
                this.button17.Visible = false;

                this.button18.Visible = false;
                this.button19.Visible = false;
                this.button20.Visible = false;
                this.button21.Visible = false;


                if (winData.packContent != null)
                {
                    #region agc值
                    byte[] agc = new byte[4];//agc值
                    for (int i = 80; i < 84; i++)
                    {
                        agc[i - 80] = winData.packContent[i];
                    }
                    int agcvalue = BitConverter.ToInt32(agc, 0);
                    form3.AgcValue = agcvalue.ToString();
                    #endregion

                    #region 目标参数数据
                    form2.targetInfoList.Clear();
                    byte[] count = new byte[4];//目标个数
                    for (int i = 84; i < 88; i++)
                    {
                        count[i - 84] = winData.packContent[i];
                    }
                    int targetCount = BitConverter.ToInt32(count, 0);
                    List<byte> targetList = new List<byte>();
                    for (int i = 0; i < targetCount; i++)
                    {
                        TargetInfo info = new TargetInfo();

                        targetList.Clear();
                        for (int j = 88 + 16 * i; j < 104 + 16 * i; j++)
                        {
                            targetList.Add(winData.packContent[j]);
                        }
                        byte[] distance = new byte[4];
                        byte[] speed = new byte[4];
                        byte[] type = new byte[4];
                        byte[] num = new byte[4];
                        for (int k = 0; k < 4; k++)
                        {
                            distance[k] = targetList[k];
                        }
                        for (int k = 4; k < 8; k++)
                        {
                            speed[k - 4] = targetList[k];
                        }
                        for (int k = 8; k < 12; k++)
                        {
                            type[k - 8] = targetList[k];
                        }
                        for (int k = 12; k < 16; k++)
                        {
                            num[k - 12] = targetList[k];
                        }
                        info.distance = (float)BitConverter.ToSingle(distance, 0);
                        info.speed = (float)BitConverter.ToSingle(speed, 0);
                        info.number = BitConverter.ToInt32(num, 0);
                        switch (BitConverter.ToInt32(type, 0))
                        {
                            case 1:
                                info.Type = "行人";
                                if (info.distance < 0 || info.distance > 200)
                                {
                                    this.button6.Visible = false;
                                    this.button10.Visible = false;
                                    this.button14.Visible = false;
                                    this.button18.Visible = false;
                                    info.dangerousGrade = "超出扫描范围";
                                }
                                else if (info.distance >= 0 && info.distance <= 50)
                                {
                                    info.dangerousGrade = "很危险";
                                    this.button18.Visible = true;

                                }
                                else if (info.distance > 50 && info.distance <= 100)
                                {
                                    info.dangerousGrade = "危险";
                                    this.button14.Visible = true;
                                }
                                else if (info.distance > 100 && info.distance <= 150)
                                {
                                    info.dangerousGrade = "一般";
                                    this.button10.Visible = true;
                                }
                                else if (info.distance > 150 && info.distance <= 200)
                                {
                                    info.dangerousGrade = "安全";
                                    this.button6.Visible = true;
                                }
                                break;
                            case 2:
                                info.Type = "自行车";
                                if (info.distance < 0 || info.distance > 200)
                                {
                                    this.button7.Visible = false;
                                    this.button11.Visible = false;
                                    this.button15.Visible = false;
                                    this.button19.Visible = false;
                                    info.dangerousGrade = "超出扫描范围";
                                }
                                else if (info.distance >= 0 && info.distance <= 50)
                                {
                                    info.dangerousGrade = "很危险";
                                    this.button19.Visible = true;

                                }
                                else if (info.distance > 50 && info.distance <= 100)
                                {
                                    info.dangerousGrade = "危险";
                                    this.button15.Visible = true;
                                }
                                else if (info.distance > 100 && info.distance <= 150)
                                {
                                    info.dangerousGrade = "一般";
                                    this.button11.Visible = true;
                                }
                                else
                                {
                                    info.dangerousGrade = "安全";
                                    this.button7.Visible = true;
                                }
                                break;
                            case 3:
                                info.Type = "小汽车";
                                if (info.distance < 0 || info.distance > 200)
                                {
                                    this.button8.Visible = false;
                                    this.button12.Visible = false;
                                    this.button16.Visible = false;
                                    this.button20.Visible = false;
                                    info.dangerousGrade = "超出扫描范围";
                                }
                                else if (info.distance >= 0 && info.distance <= 50)
                                {
                                    info.dangerousGrade = "很危险";
                                    this.button20.Visible = true;

                                }
                                else if (info.distance > 50 && info.distance <= 100)
                                {
                                    info.dangerousGrade = "危险";
                                    this.button16.Visible = true;
                                }
                                else if (info.distance > 100 && info.distance <= 150)
                                {
                                    info.dangerousGrade = "一般";
                                    this.button12.Visible = true;
                                }
                                else
                                {
                                    info.dangerousGrade = "安全";
                                    this.button8.Visible = true;
                                }
                                break;
                            case 4:
                                info.Type = "大货车";
                                if (info.distance < 0 || info.distance > 200)
                                {
                                    this.button9.Visible = false;
                                    this.button13.Visible = false;
                                    this.button17.Visible = false;
                                    this.button21.Visible = false;
                                    info.dangerousGrade = "超出扫描范围";
                                }
                                else if (info.distance >= 0 && info.distance <= 50)
                                {
                                    info.dangerousGrade = "很危险";
                                    this.button21.Visible = true;

                                }
                                else if (info.distance > 50 && info.distance <= 100)
                                {
                                    info.dangerousGrade = "危险";
                                    this.button17.Visible = true;
                                }
                                else if (info.distance > 100 && info.distance <= 150)
                                {
                                    info.dangerousGrade = "一般";
                                    this.button13.Visible = true;
                                }
                                else
                                {
                                    info.dangerousGrade = "安全";
                                    this.button9.Visible = true;
                                }
                                break;
                            default:
                                break;
                        }
                        form2.targetInfoList.Add(info);
                    }
                    AddData("");

                    #endregion
                }
            };
            if (this.InvokeRequired)
                this.Invoke(method);
            else
                method();

        }
        /// <summary>
        /// 拖放操作完成
        /// </summary>
        void panel2_DragDrop(object sender, DragEventArgs e)
        {
            // 从事件参数 DragEventArgs 中获取被拖动的元素
            Button btn = (Button)e.Data.GetData(typeof(Button));
            btn.BackColor = System.Drawing.Color.Transparent;
            string targetType = ((System.Windows.Forms.Control)(btn)).Tag.ToString();

            if (bMouseDown)
            {
                if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                {
                    form2.TargetType(targetType);
                    form2.ShowDialog();
                    AddData(targetType);
                }
                else
                {

                }
                if (form2.flag)
                {

                    switch (targetType)
                    {
                        case "行人":
                            btn.Image = HRP1524.Properties.Resources.iconA;
                            if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                            {
                                btn.Name = form2.targetInfoList[form2.targetInfoList.Count - 1].number.ToString();
                            }
                            //btn.ForeColor = Color.Red;
                            break;
                        case "自行车":
                            btn.Image = HRP1524.Properties.Resources.iconB;
                            if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                            {
                                btn.Name = form2.targetInfoList[form2.targetInfoList.Count - 1].number.ToString();
                            }
                            break;
                        case "小汽车":
                            btn.Image = HRP1524.Properties.Resources.iconC;
                            if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                            {
                                btn.Name = form2.targetInfoList[form2.targetInfoList.Count - 1].number.ToString();
                            }
                            break;
                        case "大货车":
                            btn.Image = HRP1524.Properties.Resources.iconD;
                            if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                            {
                                btn.Name = form2.targetInfoList[form2.targetInfoList.Count - 1].number.ToString();
                            }
                            break;
                        default:
                            break;
                    }


                    btn.TextImageRelation = TextImageRelation.ImageAboveText;

                    Panel grp = (Panel)btn.Parent;

                    //grp.Controls.Remove(btn);
                    ((Panel)sender).Controls.Add(btn);
                    RefreshControls(new Control[] { grp, (Panel)sender });
                    bMouseDown = false;
                    this.panel2.Controls.Clear();
                    CreateControls();

                    if (((System.Windows.Forms.Control)(sender)).Name == "panel3")
                    {
                        //AddData();
                    }
                    else
                    {
                        RemoveData(btn.Name);
                    }
                }
            }
        }
        /// <summary>
        /// 拖动对象进入本控件的边界
        /// </summary>
        void panel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            if (this.panel2.AllowDrop == true)
            {
                bMouseDown = true;
            }
        }
        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCTargetWaveAnalog_Load(object sender, EventArgs e)
        {
            CreateControls();
        }
        /// <summary>
        /// 生成一定数量的控件,本例中使用 Button
        /// 注册 Button 的鼠标点击事件
        /// </summary>
        private void CreateControls()
        {
            int x = 15;
            int y = 15;
            Button btn = null;
            for (int i = 1; i <= 4; i++)
            {
                btn = new Button();
                btn.Left = x;
                btn.Top = y;
                //btn.Text = "Button " + i;
                btn.AllowDrop = true;
                switch (i)
                {
                    case 1:
                        btn.Image = HRP1524.Properties.Resources.iconA1;
                        btn.Tag = "行人";
                        break;
                    case 2:
                        btn.Image = HRP1524.Properties.Resources.iconB1;
                        btn.Tag = "自行车";
                        break;
                    case 3:
                        btn.Image = HRP1524.Properties.Resources.iconC1;
                        btn.Tag = "小汽车";
                        break;
                    case 4:
                        btn.Image = HRP1524.Properties.Resources.iconD1;
                        btn.Tag = "大货车";
                        break;
                    default:
                        break;
                }
                //btn.Image = HRP1524.Properties.Resources.iconA1;
                btn.Width = 57;
                btn.BackColor = System.Drawing.Color.Transparent;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Height = 56;
                x += btn.Width + 35;
                if (btn.Width > panel2.Width - x)
                {
                    x = 15;
                    y += btn.Height + 15;
                }
                btn.AllowDrop = true; // 默认为 false,即不可拖动
                btn.MouseDown += new MouseEventHandler(btn_MouseDown);
                this.panel2.Controls.Add(btn);
            }
        }

        /// <summary>
        /// 按下鼠标后即开始执行拖放操作
        /// 这里指定了拖放操作的最终效果为一个枚举值: Move
        /// </summary>
        void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.DoDragDrop(btn, DragDropEffects.Move);
        }

        /// <summary>
        /// 对控件中的项进行排列
        /// </summary>
        private void RefreshControls(Control[] p)
        {
            foreach (Control control in p)
            {
                int x = 15;
                int y = 15;
                Button btn = null;
                foreach (Control var in control.Controls)
                {
                    btn = var as Button;
                    btn.Left = x;
                    btn.Top = y;
                    x += btn.Width;
                    if (btn.Width > control.Width - x)
                    {
                        x = 15;
                        y += btn.Height;
                    }
                }
            }
        }
        /// <summary>
        /// 设置表格样式
        /// </summary>
        private void SetDataGridViewStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            //dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            this.DataGridView1.ForeColor = System.Drawing.Color.White;//字颜色
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(112)))));//表格背景色
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;//表格的外边框
            this.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;//标题边框
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;//211, 223, 240
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(43)))), ((int)(((byte)(78)))));//标题背景色
            //dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));

            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(188)))), ((int)(((byte)(188)))));//标题字颜色
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;


            //this.DataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.Red;

            this.DataGridView1.ColumnHeadersHeight = 44;

            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.EnableHeadersVisualStyles = false;
            this.DataGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.RowHeadersVisible = false; //建议改为true；为了以后显示序号。
            this.DataGridView1.RowTemplate.Height = 37;

            this.DataGridView1.RowTemplate.ReadOnly = false;

        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="str"></param>
        private void AddData(string str)
        {
            Action method = delegate
            {
                DataGridView1.Rows.Clear();
                for (int i = 0; i < form2.targetInfoList.Count; i++)
                {
                    DataGridView1.Rows.Add(form2.targetInfoList[i].Type, form2.targetInfoList[i].distance.ToString(), form2.targetInfoList[i].speed.ToString(), form2.targetInfoList[i].dangerousGrade);
                }
                for (int i = 0; i < this.DataGridView1.Rows.Count; i++)
                {
                    this.DataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(112)))));
                }
            };
            if (this.InvokeRequired)
                this.Invoke(method);
            else
                method();

        }
        /// <summary>
        /// 移除数据
        /// </summary>
        private void RemoveData(string name_num)
        {
            for (int i = 0; i < form2.targetInfoList.Count; i++)
            {
                if (form2.targetInfoList[i].number == Convert.ToInt32(name_num))
                {
                    form2.targetInfoList.RemoveAt(i);
                }
            }
            AddData("");
        }
        /// <summary>
        /// 初始化系统参数
        /// </summary>
        private void InitSystemParameters()
        {
            form3.systemParametersList.Clear();
            form3.systemParameters.workModel = 0;
            form3.systemParameters.inPutSignal = 0;
            form3.systemParameters.inPutCapacity = 0;
            form3.systemParameters.radarFrequencyfirst = 0;
            form3.systemParameters.radarRange = 0;
            form3.systemParameters.radarSlope = 0;
            form3.systemParameters.inOutChoose = 1;
            form3.systemParameters.Flag = 1;
            form3.systemParameters.radarCenterFrequency = 10700;
            form3.systemParameters.prfCircle = 10000 * 400;
            form3.systemParameters.PWidth = 409600;
            form3.systemParameters.radarfc = 76500;
            form3.systemParameters.delayed = 154;
            form3.systemParameters.sendCapacityPt = 0;
            form3.systemParameters.airWireG = 0;
            form3.systemParameters.FpgaParamPacket = 80;
            form3.systemParameters.DopplerFreq1 = 0;
            form3.systemParameters.DopplerFreq2 = 0;
            form3.systemParameters.DopplerFreq3 = 0;
            form3.systemParameters.DopplerFreq4 = 0;
            form3.systemParameters.agcNum = -1;
            form3.systemParametersList.Add(form3.systemParameters);
        }
        /// <summary>
        /// 关闭RTX-modified by lchsh-2011203-调整关闭顺序
        /// </summary>
        public void CloseRtx()
        {
            SimController.Instance.StopFeedbackHandler();
            
            rtxPacker.Pack(CmdType.ExitRtx, NetMode.Local, new List<SystemParameters>(), new List<TargetInfo>());          
            System.Threading.Thread.Sleep(2000);
            if (prcRtx != null && !prcRtx.HasExited)
                prcRtx.Close();
            winMng.CloseForm();
        }
        //启动RTX程序
        private void StartRtxApp()
        {
            prcRtx.StartInfo.FileName = Application.StartupPath + "\\Rtx\\HRP1524_RTX.rtss";
            prcRtx.StartInfo.CreateNoWindow = false;
            try
            {
                if (prcRtx.Start())
                {
                    //winMng.ShowToControl("RtxServer", this.panel3);

                    //LoggingService.logShowed("启动RTX成功", InformationType.Success, InformationDisplayMode.FormList);
                    System.Threading.Thread.Sleep(4000);
                    /*初始化与RTX的接口资源*/
                    RtxInvoker.InitRtxResource();
                    /*启动接收RTX反馈监控器*/
                    SimController.Instance.StartFeedbackHandler();
                }
            }
            catch (Exception exc)
            {
                //LoggingService.logShowed("启动RTX异常：" + exc.Message, InformationType.Error, InformationDisplayMode.FormList);
            }
        }
        /// <summary>
        /// 系统参数按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
            if (form3.DialogResult == DialogResult.No)
            {
                InitSystemParameters();
                this.label7.Text = "10700";
                this.label8.Text = "76500";
            }
            else
            {
                this.label7.Text = form3.systemParameters.radarCenterFrequency.ToString();
                this.label8.Text = form3.systemParameters.radarfc.ToString();
            }
        }
        /// <summary>
        /// 开始按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.button2.Visible = false;
            this.button5.Visible = true;
            this.panel2.AllowDrop = false;
            this.panel3.AllowDrop = false;
            rtxPacker.Pack(CmdType.Start, netModeType, form3.systemParametersList, form2.targetInfoList);
            this.panel3.Visible = false;
            this.panel6.Visible = true;
        }
        /// <summary>
        /// 结束按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.button6.Visible = false;
            this.button7.Visible = false;
            this.button8.Visible = false;
            this.button9.Visible = false;

            this.button10.Visible = false;
            this.button11.Visible = false;
            this.button12.Visible = false;
            this.button13.Visible = false;

            this.button14.Visible = false;
            this.button15.Visible = false;
            this.button16.Visible = false;
            this.button17.Visible = false;

            this.button18.Visible = false;
            this.button19.Visible = false;
            this.button20.Visible = false;
            this.button21.Visible = false;


            this.button2.Visible = true;
            this.button5.Visible = false;
            this.panel2.AllowDrop = true;
            this.panel3.AllowDrop = true;
            rtxPacker.Pack(CmdType.Stop, netModeType, new List<SystemParameters>(), new List<TargetInfo>());
            this.panel3.Visible = true;
            this.panel6.Visible = false;
        }
        #endregion

        
      
    }
}
