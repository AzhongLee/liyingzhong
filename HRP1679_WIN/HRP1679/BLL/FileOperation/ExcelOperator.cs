using Hirain.Lib.IO;
using HRP1679.DAL.Para;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HRP1679.BLL.FileOperation
{
    public class ExcelOperate
    {
        #region"注释"
        //private OleDbConnection conn;
        //private OleDbDataAdapter adap;
        //private string strConn = null;
        //private string strCmd = null;

        ///// <summary>
        ///// 打开Excel文件
        ///// </summary>
        ///// <param name="filename">完整的文件路径</param>
        //public void Open(string filename)
        //{
        //    //HDR = Yes 第一行是列名，不是数据
        //    //HDR = No  第一行是数据，不是列名
        //    //IMEX = IMport EXport Mode
        //    //IMEX = 0 具备写入功能
        //    //IMEX = 1 具备读取功能
        //    //IMEX = 2 具备写入和读取功能
        //    strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + filename + @";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
        //    conn = new OleDbConnection(strConn);

        //    try
        //    {
        //        conn.Open();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// 打开Excel文件
        ///// </summary>
        ///// <param name="filename">完整的文件路径</param>
        ///// <param name="HDR">用于决定第一行是否标题</param>
        //public void Open(string filename , string HDR)
        //{
        //    strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + filename + @";Extended Properties='Excel 8.0;HDR=" + HDR + ";IMEX=1'";
        //    conn = new OleDbConnection(strConn);

        //    try
        //    {
        //        conn.Open();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ////关闭Excel操作
        //public void Close()
        //{
        //    try
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// 读取整张表格
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <returns>数据集合</returns>
        //public DataSet Read(string tablename)
        //{
        //    try
        //    {

        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            strCmd = "select * from[" + tablename + "$]";
        //            adap = new OleDbDataAdapter(strCmd , conn);
        //            DataSet ds = new DataSet();
        //            adap.Fill(ds , "[" + tablename + "$]");
        //            return ds;
        //        }
        //        else
        //            return new DataSet();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// 读取表格的特定列数据
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <param name="columnNO">列号</param>
        ///// <returns>结果的字符串列表形式</returns>
        //public List<string> ReadColumn(string tablename , int columnNO)
        //{
        //    try
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            strCmd = "select * from[" + tablename + "$]";
        //            adap = new OleDbDataAdapter(strCmd , conn);
        //            DataSet ds = new DataSet();
        //            adap.Fill(ds , "[" + tablename + "$]");
        //            List<string> res = new List<string>();
        //            if (ds.Tables[0].Rows[0].ItemArray.Length >= columnNO)
        //            {
        //                for (int i = 0 ; i < ds.Tables[0].Rows.Count ; i++)
        //                {
        //                    res.Add(ds.Tables[0].Rows[i].ItemArray[columnNO].ToString());
        //                }
        //                return res;
        //            }
        //            else
        //            {
        //                return new List<string>();
        //            }
        //        }
        //        else
        //            return new List<string>();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        #endregion

        #region " 单例 "
        private static ExcelOperate instance = null;
        public static ExcelOperate Instance
        {
            get
            {
                    instance =instance?? new ExcelOperate();
                return instance;
            }
        }
        #endregion

        #region " 私有变量 "

        public OneDimensionSheet PowAdjust = new OneDimensionSheet();    //和功率标校表数据,key是频率
        public TwoDimensionSheet PowAtten = new TwoDimensionSheet();     //功率表，key是衰减值和频率值
        public TwoDimensionSheet PowCmpAjust1 = new TwoDimensionSheet();//和下变频功率表，key是衰减值和频率值
        public TwoDimensionSheet PowCmpAjust2 = new TwoDimensionSheet();//差1下变频功率表，key是衰减值和频率值
        public TwoDimensionSheet PowCmpAjust3 = new TwoDimensionSheet();//差2下变频功率表，key是衰减值和频率值
        public ParaSimulator ParaSim = new ParaSimulator();
        #endregion

        #region " Excel表格读入 "
        /// <summary>
        /// 表格读入
        /// </summary>
        public void ReadInMicroSheet()
        {
            //上变频读取标平表
            ReadInSheet(Application.StartupPath + @"\MicroWave\MicroWave.xls" , "PowAdjust" , PowAdjust);
            //读取上变频功率表
            ReadInSheet(Application.StartupPath + @"\MicroWave\MicroWave.xls" , "PowAtten" , PowAtten);
            //读取下变频功率表 和 差 差
            ReadInSheet(Application.StartupPath + @"\MicroWave\MicroWave.xls" , "PowCmpAjust1" , PowCmpAjust1);
            ReadInSheet(Application.StartupPath + @"\MicroWave\MicroWave.xls" , "PowCmpAjust2" , PowCmpAjust2);
            ReadInSheet(Application.StartupPath + @"\MicroWave\MicroWave.xls" , "PowCmpAjust3" , PowCmpAjust3);
        }
        
        /// <summary>
        /// 读取弹道模拟参数
        /// </summary>
        public void ReadInTrajSheet()
        {
            try
            {
                 DataSet ds=new DataSet();
                ExcelOperator.IsContainedHead = true;
                //端口 配置模式
               ds= ExcelOperator.Read(ParaUI.Instance.paraTrajector.TrajectorFilePath , "模式配置");
               ParaSim.PortOne.ModeOne.Key.Clear();
               ParaSim.PortOne.ModeOne.Value.Clear();
               for (int i = 1; i < ds.Tables[0].Rows.Count; i++)  //
               {
                   try
                   {
                       ParaSim.PortOne.ModeOne.Key.Add(Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]));
                       ParaSim.PortOne.ModeOne.Value.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[1]));
                   }
                   catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
                   {
                       continue;
                   }
               }
               ParaSim.PortTow.ModeTow.Key.Clear();
               ParaSim.PortTow.ModeTow.Value.Clear();
               for (int i = 1; i < ds.Tables[0].Rows.Count; i++)  //
               {
                   try
                   {
                       ParaSim.PortTow.ModeTow.Key.Add(Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3]));
                       ParaSim.PortTow.ModeTow.Value.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[4]));
                   }
                   catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
                   {
                       continue;
                   }
               }
               #region"端口1"
               //端口一 模式参数
               ds = ExcelOperator.Read(ParaUI.Instance.paraTrajector.TrajectorFilePath , "端口一模式参数");
                //点目标模式
               List<float> datalist = new List<float>();
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[4]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[0] = datalist.ToArray();
               //扩展目标模式
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[11]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[1] = datalist.ToArray();
               //密集假目标
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[18]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[2] = datalist.ToArray();
                //SAR模式
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       if ((i > 15) && (i < 32)) //参数中包含int类型-modified by lchsh-20170701
                       {
                           datalist.Add(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[25]));
                       }
                       else
                           datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[25]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[3] = datalist.ToArray();
                //ISAR
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {                     
                       if ((i > 15) && (i < 32)) //参数中包含int类型-modified by lchsh-20170701
                       {
                           datalist.Add(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[32]));
                       }
                       else
                           datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[32]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[4] = datalist.ToArray();
               //点阵目标模式
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       if (i == 16) //参数中包含int类型-modified by lchsh-20170701
                       {
                           datalist.Add(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[39]));
                       }
                       else
                           datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[39]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortOne.Para[5] = datalist.ToArray();
               #endregion

               #region"端口2"
             
               ds = ExcelOperator.Read(ParaUI.Instance.paraTrajector.TrajectorFilePath , "端口二模式参数");
               //点目标模式
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[4]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortTow.Para[0] = datalist.ToArray();
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[11]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortTow.Para[1] = datalist.ToArray();
               datalist.Clear();
               try
               {
                   for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                   {
                       datalist.Add(Convert.ToSingle(ds.Tables[0].Rows[i].ItemArray[18]));
                   }
               }
               catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
               {
               }
               ParaSim.PortTow.Para[2] = datalist.ToArray();
                #endregion

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message , "提示" , System.Windows.Forms.MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
         
        }


        /// <summary>
        /// 读取一维表格数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">sheet名</param>
        /// <param name="sheetData">sheet数据</param>
        public void ReadInSheet(string filePath , string sheetName , OneDimensionSheet sheetData)
        {
            try
            {
                ExcelOperator.IsContainedHead = true;
                DataSet ds = ExcelOperator.Read(filePath , sheetName);

                sheetData.Key.Clear();
                sheetData.Value.Clear();

                //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //{
                //    sheetData.Key.Add(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[i]));
                //    sheetData.Value.Add(Convert.ToDouble(ds.Tables[0].Rows[1].ItemArray[i]));
                //}
                for (int i = 0 ; i < ds.Tables[0].Columns.Count ; i++)
                {
                    sheetData.Key.Add(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[i]));
                    sheetData.Value.Add(Convert.ToSingle(ds.Tables[0].Rows[1].ItemArray[i]));
                }
            }
            catch (InvalidCastException)    //处理Excel表格获取的大小与实际大小不一致的情况，当索引数据为invalid时，退出
            {
                return;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message , "提示" , System.Windows.Forms.MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 读取二维表格数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">sheet名</param>
        /// <param name="sheetData">sheet数据</param>
        public void ReadInSheet(string filePath , string sheetName , TwoDimensionSheet sheetData)
        {
            try
            {
                ExcelOperator.IsContainedHead = true;
                DataSet ds = ExcelOperator.Read(filePath , sheetName);

                //频率
                sheetData.Key1.Clear();
                for (int i = 1 ; i < ds.Tables[0].Rows.Count ; i++)
                {
                    try
                    {
                        sheetData.Key1.Add(Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[0]));
                    }
                    catch (InvalidCastException)
                    {
                        break;
                    }
                }

                //理论衰减
                sheetData.Key2.Clear();
                for (int i = 1 ; i < ds.Tables[0].Columns.Count ; i++)
                {
                    try
                    {
                        sheetData.Key2.Add(Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[i]));
                    }
                    catch (InvalidCastException)
                    {
                        break;
                    }
                }

                //实际衰减
                sheetData.Value = new double[ds.Tables[0].Rows.Count - 1 , ds.Tables[0].Columns.Count - 1];
                for (int i = 1 ; i < ds.Tables[0].Rows.Count ; i++)
                {
                    for (int j = 1 ; j < ds.Tables[0].Columns.Count ; j++)
                    {
                        try
                        {
                            sheetData.Value[i - 1 , j - 1] = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[j]);
                        }
                        catch (InvalidCastException)
                        {
                            break;
                        }
                    }
                }
            }
            catch (InvalidCastException)
            {
                return;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message , "提示" , System.Windows.Forms.MessageBoxButtons.OK ,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region " 查询表格值 "

        //一维表格查询
        public double FindValueByKey(OneDimensionSheet sheetData , double findKey)
        {
            return sheetData.Value[FindValue(sheetData.Key , findKey)];
        }
        //二维表格查询
        public double FindValueByKey(TwoDimensionSheet sheetData , double findKey1 , double findKey2)
             {
            int index1 = FindValue(sheetData.Key1 , findKey1);
            int index2 = FindValue(sheetData.Key2 , findKey2);

            return sheetData.Value[index1 , index2];
        }

        private int FindValue(List<double> value , double findValue)
        {
            if (value.Count < 0)
            {
                MessageBox.Show("校准表格索引失败");
                return 0;
            }
            decimal distance = decimal.MaxValue;
            int index = 0;
           
                for (int i = 0 ; i < value.Count  ; i++)
                {   //向下取整求所在列
                    //if ((findValue >= value[i]) && (findValue < value[i + 1]))
                    //    index = i;
                    //查找与value差值最小的频率所在列
                    if (Math.Abs((decimal)findValue - (decimal)value[i]) < distance)
                    {
                        distance = (decimal)Math.Abs(findValue - value[i]);
                        index = i;
                    }
                }
           
            return index;
        }
        /// <summary>
        /// 查找最相近的频率所在的列号
        /// </summary>
        /// <param name="array">频率序列（第一行）</param>
        /// <param name="value">频点值</param>
        /// <returns>频率所在的列编号（Index开始于0）</returns>
        public int SearchMostClose(List<double> array , double value)
        {
            decimal distance = decimal.MaxValue;
            int index = 0;
            for (int i = 0 ; i < array.Count ; i++)
            {
                //查找与value差值最小的频率所在列
                if (Math.Abs((decimal)value - (decimal)array[i]) < distance)
                {
                    distance = (decimal)Math.Abs(value - array[i]);
                    index = i;
                }
            }
            return index;
        }
        /// <summary>
        /// 查衰减值所在的行编号（Index从0开始）
        /// </summary>
        /// <param name="array">衰减值序列（第一列）</param>
        /// <param name="value">衰减值</param>
        /// <param name="index">衰减值所在行号</param>
        /// <param name="valueClose">截至步进点处的衰减值</param>
        private void SearchMostClose(List<double> array , double value , ref int index , ref double valueClose)
        {
            decimal distance = decimal.MaxValue;
            for (int i = 0 ; i < array.Count ; i++)
            {
                if (Math.Abs((decimal)value - (decimal)array[i]) < distance)
                {
                    distance = (decimal)Math.Abs(value - array[i]);
                    index = i;
                    valueClose = array[i];
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// 数据格式为一维的标校表，Key代表频率，Value代表值
    /// </summary>
    public class OneDimensionSheet
    {
        public List<double> Key { get; set; }
        public List<double> Value { get; set; }

        public OneDimensionSheet()
        {
            Key = new List<double>();
            Value = new List<double>();
            
        }
    }

    /// <summary>
    /// 数据格式为二维的标校表，Key1代表频率，Key2是理论衰减，Value是Key1、Key2对应的实际衰减
    /// </summary>
    public class TwoDimensionSheet
    {
        public List<double> Key1 = new List<double>();
        public List<double> Key2 = new List<double>();
        public double[,] Value;
    }

}

