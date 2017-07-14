using Hirain.Lib.Common;
using HRP1679.BLL.BoardCtrl;
using HRP1679.BLL.CtrlWord;
using HRP1679.BLL.FileOperation;
using HRP1679.DAL.Common;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRP1679.BLL.NetCtrl
{
    public class NetHandler
    {
        private static NetHandler instance = new NetHandler();
     
        public static NetHandler Instance
        {
            get
            {
                    return instance;
            }
        }
       

        private const uint frameHead = 0x55aa55aa;
        private const uint frameTail = 0xaa55aa55;
        private const uint deviceID = 0;

        #region"DMA"
        #region"打包"

        public byte[] DMAPack(DMASCmdType cdmtype)
        {
            List<uint> cmdlist = new List<uint>();
            List<byte> cmdArray = new List<byte>();
            cmdlist.Add(frameHead); //帧头
            cmdlist.Add(deviceID);//设备号
            cmdlist.Add(0);//命令字
            cmdlist.Add(0);//联网模式
            cmdlist.Add(0);//参数类型
            cmdlist.Add(0);//工作模式
            cmdlist.Add(0);//通道号

            switch (cdmtype)
            {
                case DMASCmdType.GetFileFolder:
                    cmdlist[2] = 0x01;
                    goto default;
                case DMASCmdType.SelfCheck:
                    cmdlist[2] = 0x02;
                    goto default;
                case DMASCmdType.Reset:
                    cmdlist[2] = 0x03;
                    goto default;
                case DMASCmdType.DataTransfer:
                    cmdlist[2] = 0x05;
                    //获取文件路径bytes
                    byte[] pathByte = Encoding.Default.GetBytes(ParaUI.Instance.paraCollect.UpLoadPath);
                    //文件路径补齐为4*byte
                    int remain = (int)(pathByte.Length % 4);
                    byte[] bufferPath = new byte[pathByte.Length + (4 - remain)];
                    bufferPath.Initialize();
                    pathByte.CopyTo(bufferPath , 0);
                    long fileLength = ParaUI.Instance.paraCollect.UpLoadLongLength; //上传文件大小
                    //参数包长度
                    cmdlist.Add((uint)(4 + 8 + bufferPath.Length * 4));
                    //参数包参数添加
                    //块号
                    uint tmp = ParaUI.Instance.paraCollect.UploadCell;
                    cmdlist.Add(tmp);
                    //
                    foreach (var item in cmdlist)
                        cmdArray.AddRange(BitConverter.GetBytes(item));
                    //上传文件大小
                    cmdArray.AddRange(BitConverter.GetBytes(fileLength));
                    cmdArray.AddRange(bufferPath);
                    cmdArray.AddRange(BitConverter.GetBytes(frameTail));//帧尾
                    return cmdArray.ToArray();
                case DMASCmdType.DataErase:
                    cmdlist[2] = 0x08;
                    //参数包长度
                    cmdArray.AddRange(BitConverter.GetBytes((uint)36));
                    //参数包
                    cmdArray.AddRange(BitConverter.GetBytes(ParaUI.Instance.paraCollect.CleanAll));
                    uint[] cellFlagArray = new uint[8];
                    for (int i = 0; i < ParaUI.Instance.paraCollect.CleanCell.Count; i++)
                    {
                        int a = (int)(ParaUI.Instance.paraCollect.CleanCell[i]) / 32;
                        int b = (int)(ParaUI.Instance.paraCollect.CleanCell[i]) % 32;
                        cellFlagArray[a] = cellFlagArray[a] | (uint)(1 << b);
                    }
                    foreach (var item in cellFlagArray)
                        cmdArray.AddRange(BitConverter.GetBytes(item));
                    cmdArray.AddRange(BitConverter.GetBytes(frameTail));//帧尾
                    return cmdArray.ToArray();
                case DMASCmdType.StartRecord:
                    cmdlist[2] = 0x09;
                    goto default;
                case DMASCmdType.StopRecord:
                    cmdlist[2] = 0x0a;
                    goto default;
                case DMASCmdType.SetDataSource:
                    cmdlist[2] = 0x0D;
                    //参数包长度
                    cmdlist.Add(0x4);
                    //数据源参数
                    cmdlist.Add(ParaUI.Instance.paraCollect.DataResource);
                    //帧尾
                    cmdlist.Add(frameTail); 
                    foreach (var item in cmdlist)
                        cmdArray.AddRange(BitConverter.GetBytes(item));
                    break;
                default:
                    cmdlist.Add(0);//参数包长度
                    cmdlist.Add(frameTail); //帧尾
                    foreach (var item in cmdlist)
                        cmdArray.AddRange(BitConverter.GetBytes(item));
                    break;
            }

            return cmdArray.ToArray();
        }
        #endregion
        #region"解包"
        public void DMADepack(byte[] recData, System.Net.IPAddress ip)
        {
            //根据ip判断存储板的通道号
            uint channel = 0;
            if (DMACtrl.Instance.tcpclient[1].ServerIP == ip)
            {
                channel = 1;
            }
            else if (DMACtrl.Instance.tcpclient[2].ServerIP == ip)
            {
                channel = 2;
            }

            uint head = BitConverter.ToUInt32(recData , 0);
            uint tail = BitConverter.ToUInt32(recData , recData.Length - 4);
            if (head == 0x55aa55aa && tail == 0xaa55aa55)
            {
                uint cmd = BitConverter.ToUInt32(recData , 8);
                uint paraLength = BitConverter.ToUInt32(recData , 28);
                switch ((DMARCmdType)cmd)
                {
                    case DMARCmdType.SaveForm:
                        UnpackMemoryFileInfo(recData , paraLength,channel);
                        break;
                    case DMARCmdType.SelfChecked:
                      //  UnpackSelfCheck(recData , paraLength);
                        ParaUI.Instance.paraCollect.ResetResult = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.Reset:
                        ParaUI.Instance.paraCollect.ResetResult = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.Imported:
                        ParaUI.Instance.paraCollect.ImportResult = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.Exported:
                        ParaUI.Instance.paraCollect.ExportResult = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.DataErased:
                        ParaUI.Instance.paraCollect.EraseResult = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.RecordStarted:
                        ParaUI.Instance.paraCollect.RecordStartRes = BitConverter.ToUInt32(recData , 32);
                        break;
                    case DMARCmdType.RecordStopped:
                        ParaUI.Instance.paraCollect.RecordStopRes = BitConverter.ToUInt32(recData , 32);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                LoggingService.logShowed("数据包格式不对！" , InformationType.Error , InformationDisplayMode.FormList);
            }
        }
        /// <summary>
        /// 解析存储板的管理文件
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="paraLength"></param>
        /// <param name="channel"></param>
        public void UnpackMemoryFileInfo(byte[] buffer , uint paraLength,uint channel)
        {
            //字典记录以方便查询
            Dictionary<Block, List<MemoryManageParas>> mmFiles = new Dictionary<Block, List<MemoryManageParas>> ();
           //用来记录每一个block对应的cell
            List<MemoryManageParas> listmmParas = new List<MemoryManageParas>();

            //时间信息
            DataParas beginParas = new DataParas();
            DataParas endParas = new DataParas();
            string month , day , hour , minute , second;
            //每16个字节一个cell数据包，结算数据包个数
            uint fileNum = paraLength / 16;
            //记录通道号，实验序号，存在一个Blcok里面
            uint serialNum = 0;
            //文件总长度
            ulong filelength = 0;
            Block block = new Block(channel,serialNum,string.Empty);


            //解析cell数据包
            for (int i = 0; i < fileNum; i++)
            {
               
                MemoryManageParas mmParas = new MemoryManageParas();

                #region"解析cell基本信息"
                mmParas.TaskNum = buffer[33 + 16 * i];                          //任务号
                mmParas.CellNum = Convert.ToUInt32(buffer[34 + 16 * i] + buffer[35 + 16 * i] * 256);        //数据块号
                //mmParas.WorkType = (RadarWorkMode)buffer[35 + 16 * i];          //雷达工作方式
                mmParas.RecordMode = (RecorderWorkMode)(buffer[36 + 16 * i] & 0x3);     //记录方式
                mmParas.FileLength = Convert.ToUInt64(buffer[37 + 16 * i]); ;    //文件长度
                mmParas.FileLength = mmParas.FileLength * (ulong)268435456;      //转换实际文件长度，乘256M

                uint endData = BitConverter.ToUInt16(buffer , 38 + 16 * i);
                UInt64 beginData = BitConverter.ToUInt64(buffer , 40 + 16 * i);                 //注意：此处转换后是否与下位机一致

                mmParas.BeginTimeInDecimal = beginData;   //起始时间（十进制表示）

                beginParas.Year = (uint)(beginData >> 40 & 0xFF) + 2000;   //年
                beginParas.Month = (uint)(beginData >> 32 & 0xFF);         //月
                beginParas.Day = (uint)(beginData >> 24 & 0xFF);           //日
                beginParas.Hour = (uint)(beginData >> 16 & 0xFF);          //时
                beginParas.Minute = (uint)(beginData >> 8 & 0xFF);         //分
                beginParas.Second = (uint)(beginData & 0xFF);              //秒

                endParas.Minute = endData >> 8 & 0xFF;           //分
                endParas.Second = endData & 0xFF;                //秒

                //根据结束时间与开始时间“分”的比较，确定结束时间“时”的值
                if (beginParas.Minute > endParas.Minute)         //时
                    endParas.Hour = beginParas.Hour + 1;
                else
                    endParas.Hour = beginParas.Hour;

                //起始时间（string型）
                if (beginParas.Month < 10)
                    month = "0" + beginParas.Month.ToString();
                else
                    month = beginParas.Month.ToString();
                if (beginParas.Day < 10)
                    day = "0" + beginParas.Day.ToString();
                else
                    day = beginParas.Day.ToString();
                if (beginParas.Hour < 10)
                    hour = "0" + beginParas.Hour.ToString();
                else
                    hour = beginParas.Hour.ToString();
                if (beginParas.Minute < 10)
                    minute = "0" + beginParas.Minute.ToString();
                else
                    minute = beginParas.Minute.ToString();
                if (beginParas.Second < 10)
                    second = "0" + beginParas.Second.ToString();
                else
                    second = beginParas.Second.ToString();
                mmParas.BeginTime = beginParas.Year.ToString() + "-" + month + "-" + day +
                    "_" + hour + "-" + minute + "-" + second;

                //年月日的字符长度 added by lchsh 20140421
                mmParas.YearMonthDayLength = (beginParas.Year.ToString() + "-" + month + "-" + day + "_").Length;

                //结束时间
                if (endParas.Hour < 10)
                    hour = "0" + endParas.Hour.ToString();
                else
                    hour = endParas.Hour.ToString();
                if (endParas.Minute < 10)
                    minute = "0" + endParas.Minute.ToString();
                else
                    minute = endParas.Minute.ToString();
                if (endParas.Second < 10)
                    second = "0" + endParas.Second.ToString();
                else
                    second = endParas.Second.ToString();
                mmParas.EndTime = hour + "-" + minute + "-" + second;

                //起始地址
                mmParas.BeginPage = mmParas.CellNum * (ulong)0x80000000;    //modified by lchsh 改为cell号*0x80000000 

                #endregion

                //第一包数据
                if (i == 0)
                {
                    block = new Block(channel, ++channel, mmParas.BeginTime);


                }

                //已记录的文件中存在当前解析的任务号
                if (ParaUI.Instance.paraCollect.ManageFiles.SerialNums[channel].Values.Contains<uint>(mmParas.TaskNum))
                {
                    //文件长度计算
                    filelength += mmParas.FileLength;
                    //任务号相同，记录在同一个block信息
                    listmmParas.Add(mmParas);


                }
                else//新的任务
                {
                    block = new Block(channel, ++channel, mmParas.BeginTime);
                    mmFiles.Add(block, listmmParas);

                    ParaUI.Instance.paraCollect.ManageFiles.SerialNums[channel].Add(mmParas.BeginTime, mmParas.TaskNum);
                    filelength = mmParas.FileLength;


                }

              




                //将该条数据添加进记录文件列表
             //   mmFiles.Files.Add(mmParas);
            }
            mmFiles.Add(block, listmmParas);
         //   ParaUI.Instance.paraCollect.ManageFiles = mmFiles;
            //   return mmFiles;
        }
        public void UnpackSelfCheck(byte[] buffer , uint paraLength)
        {
            SelfCheckParas selfCheckParas = new SelfCheckParas();
            selfCheckParas.Result = BitConverter.ToUInt32(buffer , 32);
            if (selfCheckParas.Result == 1)
                selfCheckParas.Capacity = BitConverter.ToUInt32(buffer , 36);
            else
            {
                //暂不使用
                //uint errCellNum = (paraLength - 8) / 4;  //错误数据块个数
                //for (int i = 0; i < errCellNum; i++)
                //{
                //    selfCheckParas.ErrorCellList.Add(BitConverter.ToUInt32(buffer, 34 + i));
                //}
            }

            ParaUI.Instance.paraCollect.SelfcheckParas = selfCheckParas;
        }
        #endregion
        #endregion

        #region"下位机"

        #region"打包"
        public byte[] SlavePack(SlaveSCmdType cmdType)
        {
            List<uint> cmdlist = new List<uint>();
            List<byte> cmdBytes = new List<byte>();
            cmdlist.Clear();
            cmdBytes.Clear();

            cmdlist.Add(frameHead);//帧头
            cmdlist.Add(deviceID);//设备号
            cmdlist.Add(0x0);//命令字
            cmdlist.Add(0x0);//联网模式
            cmdlist.Add(0x0);//参数类型
            cmdlist.Add(0x0);//工作模式
            cmdlist.Add(0x0);//通道号
            cmdlist.Add(0x0);//参数包长度
            switch (cmdType)
            {
                case SlaveSCmdType.SelfCheck:
                    cmdlist[2] = 0x1;
                    goto default;
                case SlaveSCmdType.SoftReset:
                    cmdlist[2] = 0x2;
                    goto default;
                case SlaveSCmdType.HardReset:
                    cmdlist[2] = 0x3;
                    goto default;
                case SlaveSCmdType.Start:
                    cmdlist[2] = 0x4;
                    cmdlist[5] = ParaUI.Instance.paraTrajector.TrajectorType;
                    goto default;
                case SlaveSCmdType.Stop:
                    cmdlist[2] = 0x5;
                    cmdlist[5] = ParaUI.Instance.paraTrajector.TrajectorType;
                    goto default;
                case SlaveSCmdType.BoudParaPackPortOne:
                    {
                        cmdlist[2] = 0x6;
                        cmdlist[6] = 0x1;
                        byte[] port = BitConverter.GetBytes(ParaUI.Instance.paraTrajector.EnumPort);        //仿真机光纤接口起始地址
                        byte[] fc = BitConverter.GetBytes(ParaUI.Instance.paraCollect.CenterFrequency);     //雷达中心频点
                        byte[] power = BitConverter.GetBytes(ParaUI.Instance.paraTrajector.PowerOutputPortOne);  //端口输出功率
                        byte[] modeCount = BitConverter.GetBytes(ExcelOperate.Instance.ParaSim.PortOne.ModeOne.Value.Count);//通道模式个数
                        byte[] modeDeploy = PackModeDeploy(ExcelOperate.Instance.ParaSim.PortOne.ModeOne);          
                        byte[] modePara = PackModePara(ExcelOperate.Instance.ParaSim.PortOne.ModeOne,ExcelOperate.Instance.ParaSim.PortOne.Para);
                        cmdlist[7] = (uint)(port.Length + fc.Length + power.Length + modeCount.Length + modeDeploy.Length + modePara.Length); //增加光纤接口、雷达中心频点参数长度-modified by lchsh-20160626

                        foreach (var item in cmdlist)
                            cmdBytes.AddRange(BitConverter.GetBytes(item));
                        cmdBytes.AddRange(port);
                        cmdBytes.AddRange(fc);
                        cmdBytes.AddRange(power);
                        cmdBytes.AddRange(modeCount);
                        cmdBytes.AddRange(modeDeploy);
                        cmdBytes.AddRange(modePara);
                        cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                        return cmdBytes.ToArray();
                    }
                case SlaveSCmdType.BoudParaPackPortTow:
                    {
                        cmdlist[2] = 0x6;
                        cmdlist[6] = 0x2;
                        byte[] port = BitConverter.GetBytes(ParaUI.Instance.paraTrajector.EnumPort);        //仿真机光纤接口起始地址
                        byte[] fc = BitConverter.GetBytes(ParaUI.Instance.paraCollect.CenterFrequency);     //雷达中心频点
                        byte[] power = BitConverter.GetBytes(ParaUI.Instance.paraTrajector.PowerOutputPortTwo);  //端口输出功率
                        byte[] modeCount = BitConverter.GetBytes(ExcelOperate.Instance.ParaSim.PortTow.ModeTow.Value.Count);//通道模式个数
                        byte[] modeDeploy = PackModeDeploy(ExcelOperate.Instance.ParaSim.PortTow.ModeTow);
                        byte[] modePara = PackModePara(ExcelOperate.Instance.ParaSim.PortTow.ModeTow,ExcelOperate.Instance.ParaSim.PortTow.Para);
                        cmdlist[7] = (uint)(port.Length + fc.Length + power.Length + modeCount.Length + modeDeploy.Length + modePara.Length); //增加光纤接口、雷达中心频点参数长度-modified by lchsh-20160626
                        foreach (var item in cmdlist)
                            cmdBytes.AddRange(BitConverter.GetBytes(item));
                        cmdBytes.AddRange(port);
                        cmdBytes.AddRange(fc);
                        cmdBytes.AddRange(power);
                        cmdBytes.AddRange(modeCount);
                        cmdBytes.AddRange(modeDeploy);
                        cmdBytes.AddRange(modePara);
                        cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                        return cmdBytes.ToArray();
                    }
                case SlaveSCmdType.SetMicroWavePara:
                    cmdlist[2] = 0x7;
                    cmdlist[7] = 47*4;  //参数包长度设置为47*4
                    foreach (var item in cmdlist)
                        cmdBytes.AddRange(BitConverter.GetBytes(item));
                    cmdBytes.AddRange(MicroCtrl.PackMicroWaveCmd());
                    cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                    return cmdBytes.ToArray();
                case SlaveSCmdType.StartRecord:
                    cmdlist[2] = 0x8;
                    goto default;
                case SlaveSCmdType.StopRecord:
                    cmdlist[2] = 0x9;
                    goto default;
                case SlaveSCmdType.UploadData:
                    cmdlist[2] = 0xa;
                    goto default;
                case SlaveSCmdType.InitializeDevice:
                    cmdlist[2] = 0xb;
                    goto default;
                case SlaveSCmdType.DownloadSignalFile:
                    List<string> lststrSignal = new List<string>();
                    FileStream fs = new FileStream(ParaUI.Instance.paraSignal.SingalDataFilePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    FileStream fs2 = new FileStream(ConstData.SignalFile, FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs2);
                    try
                    {
                        while (sr.Peek() >= 0)
                        {
                            string strTemp = sr.ReadLine();
                            strTemp.Trim();
                            if(strTemp!=string.Empty)
                            lststrSignal.Add(strTemp);
                            bw.Write(Convert.ToUInt32(strTemp));
                        }
                        sr.Close();
                        fs.Close();
                        fs2.Close();
                        bw.Close();
                    }
                    catch (Exception)
                    {
                        sr.Close();
                        fs.Close();
                        fs2.Close();
                        bw.Close();
                    }
                    cmdlist[2] = 0xd;
                    uint filelength = (uint)lststrSignal.Count;

                    byte[] lenth = BitConverter.GetBytes(filelength);
                    byte[] path = Encoding.Default.GetBytes(ConstData.SignalFile);
                    cmdlist[7] = (uint)(lenth.Length + path.Length);
                    foreach (var item in cmdlist)
                        cmdBytes.AddRange(BitConverter.GetBytes(item));
                    cmdBytes.AddRange(lenth);
                    cmdBytes.AddRange(path);
                    cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                    return cmdBytes.ToArray();
                case SlaveSCmdType.DownloadCeofFile:
                    cmdlist[2] = 0xc;
                    cmdlist[7] = 128;
                    cmdlist.AddRange(DRFMCtrl.GetCirDataPackage(ConstData.CoefFile));
                    foreach (var item in cmdlist)
                        cmdBytes.AddRange(BitConverter.GetBytes(item));
                    cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                    return cmdBytes.ToArray();
                case SlaveSCmdType.SetDRFMCtrlWord:
                    cmdlist[2] = 0xe;
                    cmdlist[7] = 1024;
                    cmdlist.AddRange(DRFMCtrl.CalcCtrlWord(ParaUI.Instance));
                    foreach (var item in cmdlist)
                        cmdBytes.AddRange(BitConverter.GetBytes(item));
                    cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                    return cmdBytes.ToArray();
                default:
                    cmdlist.Add(frameTail);
                    foreach (var item in cmdlist)
                        cmdBytes.AddRange(BitConverter.GetBytes(item));
                  //  cmdBytes.AddRange(BitConverter.GetBytes(frameTail));
                    return cmdBytes.ToArray();
            }

       //     return cmdBytes.ToArray();
        }
        /// <summary>
        /// 索引参数包 模式配置
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        private byte[] PackModeDeploy(ModeDeploy md)
        {
            List<byte> cmdArray = new List<byte>();
            cmdArray.Clear();
            for (int i = 0; i < md.Key.Count; i++)
            {
                if (md.Key[i] == "")
                    break;
                cmdArray.AddRange(BitConverter.GetBytes(i + 1));
                switch (md.Key[i])
                {
                    case "点目标":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1001));
                        break;
                    case "扩展目标":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1002));
                        break;
                    case "密集假目标":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1003));
                        break;
                    case "SAR":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1004));
                        break;
                    case "ISAR":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1005));
                        break;
                    case "点阵目标":
                        cmdArray.AddRange(BitConverter.GetBytes(0x1006));
                        break;
                }
                cmdArray.AddRange(BitConverter.GetBytes(Convert.ToUInt32( md.Value[i]*1000))); //时间改为ms  uint
            }
            return cmdArray.ToArray();
        }

        private byte[] PackModePara(float[][] para)
        {

            List<byte> dataArray = new List<byte>();
            foreach (var item in para)
            {
                foreach (var ite in item)
                {
                    dataArray.AddRange(BitConverter.GetBytes(ite));
                }
                for (int i = item.Length; i < 256; i++)
                    dataArray.AddRange(BitConverter.GetBytes(0));
            }
           
            return dataArray.ToArray();
        }
        private byte[] PackModePara(ModeDeploy md,float[][] para)
        {
            List<byte> dataArray = new List<byte>();
            for (int i = 0; i < md.Key.Count; i++)
            {
                if (md.Key[i] == "")
                    break;
                switch (md.Key[i])
                {
                    case "点目标":
                        dataArray.AddRange(GetBytesFromArry(para[0]));
                        break;
                    case "扩展目标":
                        dataArray.AddRange(GetBytesFromArry(para[1]));
                        break;
                    case "密集假目标":
                        dataArray.AddRange(GetBytesFromArry(para[2]));
                        break;
                    case "SAR":
                        dataArray.AddRange(GetBytesFormSARArry(para[3]));
                        break;
                    case "ISAR":
                        dataArray.AddRange(GetBytesFormISARArry(para[4]));
                        break;
                    case "点阵目标":
                        dataArray.AddRange(GetBytesFormLatticeArry(para[5]));
                        break;
                }
            }
            return dataArray.ToArray();
        }
        private List<byte> GetBytesFromArry(float[] Array)
        {
            List<byte> arrayBytes = new List<byte>();
            foreach (var ite in Array)
            {
                arrayBytes.AddRange(BitConverter.GetBytes(ite));
            }
            //补齐256个byte-added by lchsh-20170628
            while (arrayBytes.Count < 256)
                arrayBytes.Add((byte)0);
            return arrayBytes;
        }

        private List<byte> GetBytesFormSARArry(float[] Array)
        {
            List<byte> arrayBytes = new List<byte>();
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes(Array[i]));
                }
                for(int i=15;i<31;i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes((int)Array[i]));
                }
                for (int i = 31; i < 34; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes(Array[i]));
                }
            }
            catch (Exception)
            { }
           while(arrayBytes.Count<256)
                arrayBytes.Add((byte)0);
            return arrayBytes;
        }
        private List<byte> GetBytesFormISARArry(float[] Array)
        {
            List<byte> arrayBytes = new List<byte>();
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes(Array[i]));
                }
                for (int i = 15; i < 31; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes((int)Array[i]));
                }
                for (int i = 31; i < 35; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes(Array[i]));
                }
                arrayBytes.AddRange(BitConverter.GetBytes((int)Array[31]));
            }
            catch (Exception)
            { }
            while (arrayBytes.Count < 256)
                arrayBytes.Add((byte)0);
            return arrayBytes;
        }
        private List<byte> GetBytesFormLatticeArry(float[] Array)
        {
            List<byte> arrayBytes = new List<byte>();
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes(Array[i]));
                }
                arrayBytes.AddRange(BitConverter.GetBytes((uint)Array[15]));
                for (int i = 16; i < 25; i++)
                {
                    arrayBytes.AddRange(BitConverter.GetBytes((int)Array[i]));
                }

            }
            catch (Exception)
            { }
            while (arrayBytes.Count < 256)
                arrayBytes.Add((byte)0);
            return arrayBytes;
        }
        #endregion

        #region"解包"
        public void SlaveDepack(byte[] recData)
        {
            
            uint head = BitConverter.ToUInt32(recData , 0);
            uint tail = BitConverter.ToUInt32(recData , recData.Length - 4);
            if (head == 0x55aa55aa && tail == 0xaa55aa55)
            {
                switch ((SlaveRCmdType)BitConverter.ToUInt32(recData , 8))
                {
                    case SlaveRCmdType.SoftReset:
                    case SlaveRCmdType.HardReset:
                    case SlaveRCmdType.Started:
                    case SlaveRCmdType.Stoped:
                    case SlaveRCmdType.MicroWaveParaSet:
                    case SlaveRCmdType.DataUploaded:
                        ParaUI.Instance.paraStatus.State = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32));
                        break;
                    case SlaveRCmdType.ParaPackBouded:
                        ParaUI.Instance.paraStatus.Port = BitConverter.ToUInt32(recData , 24);
                        ParaUI.Instance.paraStatus.State = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32));
                        break;
                    case SlaveRCmdType.SelfChecked:
                        ParaUI.Instance.paraStatus.IDRFMCheck= Convert.ToBoolean(BitConverter.ToUInt32(recData, 32) & 0x4);
                        ParaUI.Instance.paraStatus.IMemSelfcheck = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32) & 0x2);
                        ParaUI.Instance.paraStatus.IGCStatus = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32) & 0x1);

                        break;
                    case SlaveRCmdType.DeviceInitialized:
                        ParaUI.Instance.paraStatus.IDRFMStatus = Convert.ToBoolean(BitConverter.ToUInt32(recData, 32) & 0x8);
                        ParaUI.Instance.paraStatus.IMemInit = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32) & 0x4);
                        ParaUI.Instance.paraStatus.IGCInit = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32) & 0x2);
                        ParaUI.Instance.paraStatus.Initialized = Convert.ToBoolean(BitConverter.ToUInt32(recData , 32) & 0x1);
                        break;
                }
            }
            else
            {
                LoggingService.logShowed("数据包格式不对！" , InformationType.Error , InformationDisplayMode.FormList);
            }
            SlaveCtrl.Instance.autoResetEvent.Set();
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 存储板发送帧命令字
    /// </summary>
    public enum DMASCmdType
    {
        GetFileFolder = 0x01 ,
        SelfCheck ,
        Reset ,
        DataTransfer = 0x05 ,
        DataPlayBack ,
        StopPlayBack ,
        DataErase ,
        StartRecord ,
        StopRecord ,
        SetTime ,
        GetChipTime ,
        SetDataSource ,
        SearchDataSource ,
        SetIP ,
        HardwarePlayBack ,
        LoadFlash = 0x12 ,
        LoadFPGA ,
        DownloadSoftware
    }
    /// <summary>
    ///存储板接收帧命令字
    /// </summary>
    public enum DMARCmdType
    {
        SaveForm = 0x01 ,
        SelfChecked ,
        Reset ,
        Imported ,
        Exported ,
        PlaybackCompleted ,
        PlaybackStopped ,
        DataErased ,
        RecordStarted ,
        RecordStopped ,
        TimeSet ,
        ChipTimeRecorded ,
        DataSourceSet ,
        DataSourceSeach ,
        IPSet ,
        HardwarePlayedback ,
        FlashLoaded = 0x12 ,
        FPGALoaded ,
        SoftwareDownload
    }
    /// <summary>
    /// 上位机发送帧命令字
    /// </summary>
    public enum SlaveSCmdType
    {
        SelfCheck = 0x01 ,
        SoftReset ,
        HardReset ,
        Start ,
        Stop ,
        BoudParaPackPortOne ,
        SetMicroWavePara ,
        StartRecord ,
        StopRecord ,
        UploadData ,
        InitializeDevice ,
        BoudParaPackPortTow,//占用一个
        DownloadCeofFile = 0x0d,
        DownloadSignalFile,
        SetDRFMCtrlWord
    }
    /// <summary>
    /// 下位机接收帧命令字
    /// </summary>
    public enum SlaveRCmdType
    {
        SelfChecked = 0x01 ,
        SoftReset ,
        HardReset ,
        Started ,
        Stoped ,
        ParaPackBouded ,
        MicroWaveParaSet ,
        RecordStarted ,
        RecordStoped ,
        DataUploaded ,
        DeviceInitialized,
        CeofFileDownloaded=0x0c,
        SignalFileDownloaded,
        DRFMCtrlWordSet
    }

}
