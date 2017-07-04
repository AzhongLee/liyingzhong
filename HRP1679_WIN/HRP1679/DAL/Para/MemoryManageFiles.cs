using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    /// <summary>
    /// 存储管理--记录参数文件
    /// </summary>
    [Serializable]
    public class MemoryManageFiles
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public MemoryManageFiles()
        {
            MemFiles[0] = new Dictionary<Block, List<MemoryManageParas>>();
            MemFiles[1] = new Dictionary<Block, List<MemoryManageParas>>();
            MemFiles[2] = new Dictionary<Block, List<MemoryManageParas>>();
            ExNums = new Dictionary<string, uint>();
            DataTypes[0] = new List<string>();
            DataTypes[1] = new List<string>();
            DataTypes[2] = new List<string>();
            CurrentExNum = ExNums.Values.Max();
            CurrentDataType = new string[3] { string.Empty,string.Empty,string.Empty };
            SerialNums = new Dictionary<string, uint>[3] { new Dictionary<string, uint>(), new Dictionary<string, uint>(), new Dictionary<string, uint>() };
        }
        /// <summary>
        /// 三个通道的记录文件，每个通道中按照block索引来查找Files，不同的通道中通过时间顺序来检索
        /// </summary>
        public Dictionary<Block, List<MemoryManageParas>>[] MemFiles = new Dictionary<Block, List<MemoryManageParas>>[3];
        /// <summary>
        /// 记录是那个通道的时间对应的任务号
        /// </summary>
        public Dictionary<string, uint>[] SerialNums { get; set; }
        /// <summary>
        /// 记录实验号集合 
        /// </summary>
        public Dictionary<string, uint> ExNums { get; set; }
       
      
        /// <summary>
        /// 当前实验的序号 确认在产生新的实验数据之后生效，加入ExNum中
        /// </summary>
        public uint CurrentExNum { get; set; }
        
        /// <summary>
        /// 数据类型集合
        /// </summary>
        public List<string>[] DataTypes = new List<string>[3];

        public string[] CurrentDataType = new string[3];
    }
    /// <summary>
    /// 块号，每一块对应界面中一条记录，块中记录的
    /// </summary>
   public struct Block
    {
        /// <summary>
        /// 通道号
        /// </summary>
        uint channel;//通道号
        /// <summary>
        /// 实验序号
        /// </summary>
        uint serialNumber;//实验序号，由时间来确定

        string startTime;
        /// <summary>
        /// 结构体构造函数
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="num"></param>
        public Block(uint ch,uint num,string time)
        {
            channel = ch;
            serialNumber = num;
            startTime = time;
        }



    }

    /// <summary>
    /// 存储管理--记录数据参数类
    /// </summary>
    [Serializable]
    public class MemoryManageParas
    {
        //任务号
        public byte TaskNum { get; set; }
        //cell号
        public uint CellNum { get; set; }
        //public RadarWorkMode WorkType { get; set; }
        //雷达工作模式
        public RecorderWorkMode RecordMode { get; set; }
        //开始时间
        public string BeginTime { get; set; }
        //结束时间
        public string EndTime { get; set; }
        //起始地址 页
        public UInt64 BeginPage { get; set; }
        /// <summary>
        /// 文件长度，0:0M，1:256M，2：512M，...8：2048M
        /// </summary>
        public UInt64 FileLength { get; set; }

        public UInt64 BeginTimeInDecimal { get; set; }

        public int YearMonthDayLength { get; set; }

        public MemoryManageParas()
        {
            this.TaskNum = 0;
            this.CellNum = 0;
            //this.WorkType = RadarWorkMode.Selfcheck;
            this.RecordMode = RecorderWorkMode.Auto;
            this.BeginTimeInDecimal = 0;
            this.BeginTime = string.Empty;
            this.EndTime = string.Empty;
            this.BeginPage = 0;
            this.FileLength = 0;
            this.YearMonthDayLength = 0;
        }
    }

    //日期参数类
    public class DataParas
    {
        public uint Year { get; set; }
        public uint Month { get; set; }
        public uint Day { get; set; }
        public uint Hour { get; set; }
        public uint Minute { get; set; }
        public uint Second { get; set; }

        public DataParas()
        {
            this.Year = 2013;
            this.Month = 10;
            this.Day = 9;
            this.Hour = 10;
            this.Minute = 19;
            this.Second = 7;
        }
    }

    //自检参数类   add by lchsh 20131230
     [Serializable]
    public class SelfCheckParas
    {
        public uint Result { get; set; }
        public uint Capacity { get; set; }  //硬盘容量，百分比
        public List<uint> ErrorCellList { get; set; }  //错误数据块号

        public SelfCheckParas()
        {
            this.Result = 0;
            this.Capacity = 0;
            this.ErrorCellList = new List<uint>();
        }
    }

    //数据源查询结果类 
    public class DataSourceCheckParas
    {
        public uint Result { get; set; }
        public uint Type { get; set; }  //数据源类型

        public DataSourceCheckParas()
        {
            this.Result = 0;
            this.Type = 0;
        }
    }


    //记录模式
    public enum RecorderWorkMode
    {
        SoftCtrl = 0x0 , //软件控制
        Auto = 0x1 ,   //自动
        Manual = 0x2  //手动
    }

}