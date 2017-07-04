using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRP1679.BLL.FileOperation
{
    public class TxtOperation
    {
        public static void ReadTxtFile(string FilePath , ref List<string> ReadData)
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException(FilePath);

            string strInfo = string.Empty;
            char[] splitChar = new char[] { ' ' };
            string[] strArraySplited = new string[2];
            using (FileStream fs = new FileStream(FilePath , FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        strInfo = reader.ReadLine();
                        strArraySplited = strInfo.Split(splitChar , StringSplitOptions.RemoveEmptyEntries);
                        ReadData.AddRange(strArraySplited);
                    }

                    reader.Close();
                }
                fs.Close();
            }
        }
        public static void ReadTxtFile(string FilePath , ref List<uint> ReadData , string Separator)
        {
            if (!File.Exists(FilePath))
                // throw new FileNotFoundException(FilePath);
                File.Create(FilePath);
            string strInfo = string.Empty;
         //   ReadData=new  List<uint>();
            using (FileStream fs = new FileStream(FilePath , FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    switch (Separator) 
                    {
                        case "空格":
                            char[] splitChar = new char[] { ' ' };
                            string[] strArraySplited;
                            while (!reader.EndOfStream)
                            {
                                strInfo = reader.ReadLine();
                                strArraySplited = strInfo.Split(splitChar , StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < strArraySplited.Length;i++ )
                                    ReadData.Add(Convert.ToUInt32( strArraySplited));
                            }
                            break;
                        case "回车换行":
                            while (!reader.EndOfStream)
                            {
                                strInfo = reader.ReadLine();
                                ReadData.Add(Convert.ToUInt32(strInfo.Trim()));
                            }
                            break;
                        default:
                            break;
                    }
                    
                  
                }
            }
        }
        public static void WrtieTxtFile<T>(string FilePath , T[] WriteData , string Separator)
        {

            FileStream fs = new FileStream(FilePath , FileMode.Append , FileAccess.Write);
            //写入
            switch (Separator)
            {
                case "空格":
                    using (StreamWriter sw = new StreamWriter(fs))
                    {

                        for (int i = 0; i < WriteData.Length; i++)
                        {
                            sw.Write(WriteData[i] + " ");
                        }
                    }
                    break;
                case "制表符":
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        for (int i = 0; i < WriteData.Length; i += 1)
                        {
                            sw.Write(WriteData[i] + "\t");
                        }
                    }
                    break;
                case "回车换行":
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        for (int i = 0; i < WriteData.Length; i++)
                        {
                            sw.Write(WriteData[i] + "\n");
                        }
                    }
                    break;
            }
            fs.Close();
        }
    }
}
