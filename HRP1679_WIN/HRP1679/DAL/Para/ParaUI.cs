using Hirain.Lib.Common;
using HRP1679.DAL.Common;
using HRP1679.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HRP1679.DAL.Para
{
    [Serializable]
   public class ParaUI
   {
       
        private static ParaUI instance = null;
         [NonSerialized]
        private static object obj = new object();
        public static ParaUI Instance
        {
            get
            {
                lock (obj)
                {
                    instance = instance ??  LoadParaFormXML();
                    return instance;
                }
            }
        }
      

        public ParaUI()
        {
            paraCollect = new ParaCollect();
            paraDebug = new ParaDebug();
            paraSignal = new ParaSignal();
            paraTrajector = new ParaTrajector();
            paraStatus = new ParaStatus();
             paraWave = new ParaMicroWave();
        }
      [NonSerialized]
      public static ISynchronizeInvoke UIhandler = FormMain.Instance;
       
      public ParaCollect paraCollect;
      
      public ParaDebug paraDebug;

      public ParaSignal paraSignal;
       
      public ParaTrajector paraTrajector;


        public ParaStatus paraStatus;
      
      public ParaMicroWave paraWave;
        /// <summary>
        /// 从文件获取界面参数
        /// </summary>
       
      public static ParaUI LoadParaFormXML()
      {
          object m_TempObj = null;
          if (File.Exists(ConstData.UIDat))
          {
              m_TempObj = BinaryHelper.FormatterByteObject(BinaryHelper.FileObjectBytes(ConstData.UIDat));
              if (m_TempObj != null)
              {
                  LoggingService.logShowed("界面参数装订成功！" , InformationType.Success , InformationDisplayMode.FormList);
                  return Convert.ChangeType(m_TempObj , typeof(ParaUI)) as ParaUI;
              }
              else//文件读取失败
              {
                  LoggingService.logShowed("界面参数文件读取失败！" , InformationType.Error , InformationDisplayMode.FormList);
                  return new ParaUI();
              }
          }
          else//文件不存在
          {
              LoggingService.logShowed("界面参数文件不存在！" , InformationType.Error , InformationDisplayMode.FormList);
          }
          return new ParaUI();
      }
        /// <summary>
        /// 保存界面参数到文件
        /// </summary>
         
      public void SaveParaToXML()
      {
          this.paraStatus = new ParaStatus();
          BinaryHelper.BinaryFileSave(Common.ConstData.UIDat,ParaUI.instance);
      }

      /// <summary>
      /// 把对象序列化为字节数组
      /// </summary>
      public static byte[] SerializeObject(object obj)
      {
          if (obj == null)
              return null;
          MemoryStream ms = new MemoryStream();
          BinaryFormatter formatter = new BinaryFormatter();
          formatter.Serialize(ms , obj);
          ms.Position = 0;
          byte[] bytes = new byte[ms.Length];
          ms.Read(bytes , 0 , bytes.Length);
          ms.Close();
          return bytes;
      }

      /// <summary>
      /// 把字节数组反序列化成对象
      /// </summary>
      public static object DeserializeObject(byte[] bytes)
      {
          object obj = null;
          if (bytes == null)
              return obj;
          MemoryStream ms = new MemoryStream(bytes);
          ms.Position = 0;
          BinaryFormatter formatter = new BinaryFormatter();
          obj = formatter.Deserialize(ms);
          ms.Close();
          return obj;
      }

    }
}
