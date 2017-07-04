using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    [Serializable]
    public class ParaCollect : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void RaisePropertyChanged(string propertyName)
        {

            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this , new PropertyChangedEventArgs(propertyName));
            }
        }

        public ParaCollect()
        {
            centerFrequency = 9.8F;
            frequencyModel = 0;
            workModel = 0;
            aDch1 = aDch2 = aDch3 = 1;
            upLoadLength = upLoadOffset = 0;
            upLoadPath = String.Empty;
            cleanAll = 0;
            cleanPageStart = cleanPageEnd = cleanBlockStart = cleanBlockEnd = 0;
            dMAParas = new List<ParaDMA>() { new ParaDMA() , new ParaDMA() };
            transferBlock = new List<uint>();
            cleanCell = new List<uint>();
            workStart = 1;
            cell = new Dictionary<int , List<uint>>();
            
        }

        #region"字段"
        private List<ParaDMA> dMAParas;
        private Dictionary<int,List<uint>> cell;
        private List<uint> transferBlock;
        private List<uint> transferCell;
        
        /// <summary>
        /// 采集相关参数
        /// </summary>
        private float centerFrequency;
        private uint frequencyModel;
        private uint workModel;
        private readonly uint aDch1;
        private readonly uint aDch2;
        private uint aDch3;
        private uint workStart;
        private uint collectStart;
        /// <summary>
        /// 上传相关参数
        /// </summary>
        private uint upLoadOffset;
        private uint upLoadLength;
        private long upLoadLongLength;
        private uint uploadCell;
        private string upLoadPath;
        /// <summary>
        ///擦除相关参数
        /// </summary>
        private uint cleanPageStart;
        private uint cleanPageEnd;
        private uint cleanBlockStart;
        private uint cleanBlockEnd;
        private uint cleanAll;
        /// <summary>
        /// 存储板返回数据字
        /// </summary>
        private List<uint> cleanCell;
        private MemoryManageFiles manageFiles;
        private SelfCheckParas selfcheckParas;
        private uint resetResult;
        private uint importResult;
        private uint exportResult;
        private uint eraseResult;
        private uint recordStartRes;
        private uint recordStopRes;

        //配置相关参数
        private uint dataResource;


        #endregion

        #region"属性"
        /// <summary>
        /// 上传的总单元
        /// </summary>
        public List<uint> TransferCell
        {
            get { return transferCell; }
            set { transferCell = value; }
        }
       
        /// <summary>
        /// 每块上传块号号集合
        /// </summary>
        public List<uint> TransferBlock
        {
            get { return transferBlock; }
            set { transferBlock = value; }
        }
        /// <summary>
        /// 块号对应单元号，每块对应一个list
        /// </summary>
        public Dictionary<int,List<uint>> Cell
        {
            get { return cell; }
            set { cell = value; }
        }
        /// <summary>
        /// 界面参数数据源
        /// </summary>
        public List<ParaDMA> DMAParas
        {
            get { return dMAParas; }
            set { dMAParas = value;
            this.RaisePropertyChanged("DMAParas");
            }
        }
        
        /// <summary>
        /// 工作开始
        /// </summary>
        public uint WorkStart
        {
            get { return workStart; }
            set { workStart = value; }
        }

        /// <summary>
        /// 结束记录应答
        /// </summary>
        public uint RecordStopRes
        {
            get { return recordStopRes; }
            set { recordStopRes = value; }
        }

        /// <summary>
        /// 开始记录应答
        /// </summary>
        public uint RecordStartRes
        {
            get { return recordStartRes; }
            set { recordStartRes = value; }
        }
        /// <summary>
        /// 擦除结果
        /// </summary>
        public uint EraseResult
        {
            get { return eraseResult; }
            set { eraseResult = value; }
        }
        /// <summary>
        /// 导出结果
        /// </summary>
        public uint ExportResult
        {
            get { return exportResult; }
            set { exportResult = value; }
        }

        /// <summary>
        /// 导入结果
        /// </summary>
        public uint ImportResult
        {
            get { return importResult; }
            set { importResult = value; }
        }
        /// <summary>
        /// 复位结果
        /// </summary>
        public uint ResetResult
        {
            get { return resetResult; }
            set { resetResult = value; }
        }
        /// <summary>
        /// 自检结果文件
        /// </summary>
        public SelfCheckParas SelfcheckParas
        {
            get { return selfcheckParas; }
            set { selfcheckParas = value; }
        }
        /// <summary>
        /// 存储管理文件
        /// </summary>
        public MemoryManageFiles ManageFiles
        {
            get { return manageFiles; }
            set { manageFiles = value; }
        }
        /// <summary>
        /// 采集开始
        /// </summary>
        public uint CollectStart
        {
            get { return collectStart; }
            set { collectStart =(uint)( value<=0?0:1); }
        }
        /// <summary>
        /// 擦除单元集合
        /// </summary>
        public List<uint> CleanCell
        {
            get { return cleanCell; }
            set { cleanCell = value; }
        }
        /// <summary>
        /// 是否全部擦除
        /// </summary>
        public uint CleanAll
        {
            get { return cleanAll; }
            set { cleanAll = value<=0?value:1; }
        }
        /// <summary>
        /// 擦除结束块
        /// </summary>
        public uint CleanBlockEnd
        {
            get { return cleanBlockEnd; }
            set { cleanBlockEnd = value; }
        }
        /// <summary>
        /// 擦除起始块
        /// </summary>
        public uint CleanBlockStart
        {
            get { return cleanBlockStart; }
            set { cleanBlockStart = value; }
        }
        /// <summary>
        /// 擦除起始页
        /// </summary>
        public uint CleanPageStart
        {
            get { return cleanPageStart; }
            set { cleanPageStart = value; }
        }
        /// <summary>
        /// 擦除结束页
        /// </summary>
        public uint CleanPageEnd
        {
            get { return cleanPageEnd; }
            set { cleanPageEnd = value; }
        }
        
        /// <summary>
        /// 上传参数路径
        /// </summary>
        public string UpLoadPath
        {
            get { return upLoadPath; }
            set { upLoadPath = value; }
        }
       
        /// <summary>
        /// 上传参数长度[字节]
        /// </summary>
         public long UpLoadLongLength
        {
            get { return upLoadLongLength; }
            set { upLoadLongLength = value; }
        }
        /// <summary>
        /// 上传参数起始地址[页]
        /// </summary>
        public uint UpLoadOffset
        {
            get { return upLoadOffset; }
            set { upLoadOffset = value; }
        }
        /// <summary>
        /// 上传参数块号
        /// </summary>
        public uint UploadCell
        {
            get { return uploadCell; }
            set { uploadCell = value; }
        }
        /// <summary>
        /// 工作模式
        /// </summary>
        public uint WorkModel
        {
            get { return workModel; }
            set {    if (value <= 0)
                {
                    workModel = 0;
                    ADch3 = 1;
                }
                else 
                {
                    workModel = 1;
                    ADch3 = 0;
                }
                }
        }
        /// <summary>
        /// 测频模式
        /// </summary>
        public uint FrequencyModel
        {
            get { return frequencyModel; }
            set {
                if (value <= 0)
                {
                    frequencyModel = 0;
                }
                else 
                {
                    frequencyModel = 1;
                }
                }
        }
        /// <summary>
        /// 雷达中心频率
        /// </summary>
        public float CenterFrequency
        {
            get { return centerFrequency; }
            set { centerFrequency = value; }
        }
        /// <summary>
        /// AD采集通道1
        /// </summary>
        public uint ADch1
        {
            get { return aDch1; }
        }
        /// <summary>
        /// AD采集通道2
        /// </summary>
        public uint ADch2
        {
            get { return aDch2; }
        }
        /// <summary>
        /// AD采集通道3
        /// </summary>
        public uint ADch3
        {
            get { return aDch3; }
            private  set { aDch3 = value; }
        }
        /// <summary>
        /// AD采集界面访问接口
        /// </summary>
        public bool IADch3
        {
            get { return Convert.ToBoolean(aDch3); }
        }
        /// <summary>
        /// 参数配置
        /// </summary>
        public uint DataResource { get { return dataResource; } set { dataResource = value; } }
        #endregion

    }
    [Serializable]
    public class ParaDMA
    {
        private string exNum;
        private string channel;
        private string offset;
        private string length;
        private string startTime;
        private string stopTime;
        private string datatype;
        public ParaDMA()
        {
            exNum = offset = length = startTime = stopTime = "String.Empty";
        }

        /// <summary>
        /// 实验号
        /// </summary>
        public string ExNum
        {
            get { return exNum; }
            set { exNum = value; }
        }
        /// <summary>
        /// 起始地址
        /// </summary>
        public string Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// 文件长度
        /// </summary>
        public string Length
        {
            get { return length; }
            set { length = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string StopTime
        {
            get { return stopTime; }
            set { stopTime = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        /// <summary>
        /// 通道号
        /// </summary>
        public string Channel { get { return channel; } set { channel = value; } }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string Datatype { get { return datatype; } set { datatype = value; } }
    }
}
