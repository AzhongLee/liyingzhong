using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HRP1679.BLL.FileOperation
{
    public class BinFileOperation
    {
        public static void WriteBinFile<T>(string FilePath , List<T> WriteData)
        {
            using (FileStream fs = new FileStream(FilePath , FileMode.Append , FileAccess.Write))
            {
                using (BinaryWriter sw = new BinaryWriter(fs))
                {
                    switch (typeof(T).ToString())
                    {
                        case "System.Byte":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((byte)Convert.ChangeType(WriteData[i] , typeof(byte)));
                            }
                            break;
                        case "System.SByte":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((sbyte)Convert.ChangeType(WriteData[i] , typeof(sbyte)));
                            }
                            break;
                        case "System.Int16":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((Int16)Convert.ChangeType(WriteData[i] , typeof(Int16)));
                            }
                            break;
                        case "System.UInt16":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((UInt16)Convert.ChangeType(WriteData[i] , typeof(UInt16)));
                            }
                            break;
                        case "System.Int32":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((Int32)Convert.ChangeType(WriteData[i] , typeof(Int32)));
                            }
                            break;
                        case "System.UInt32":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((UInt32)Convert.ChangeType(WriteData[i] , typeof(UInt32)));
                            }
                            break;
                        case "System.Int64":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((Int64)Convert.ChangeType(WriteData[i] , typeof(Int64)));
                            }
                            break;
                        case "System.UInt64":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((UInt64)Convert.ChangeType(WriteData[i] , typeof(UInt64)));
                            }
                            break;
                        case "System.Single":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((float)Convert.ChangeType(WriteData[i] , typeof(float)));
                            }
                            break;
                        case "System.Double":
                            for (int i = 0; i < WriteData.Count; i++)
                            {
                                sw.Write((double)Convert.ChangeType(WriteData[i] , typeof(double)));
                            }
                            break;
                    }
                }
            }
        }

        public static void ReadBinFile(string FilePath,out List<uint> ReadData) 
	   {
            ReadData = new List<uint>();
        
            using (var fs = new FileStream(FilePath , FileMode.Open , FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position < (br.BaseStream.Length - sizeof(uint)))
                    {
                        UInt32 a = br.ReadUInt32();
                        ReadData.Add(a);
                    }
                }
            }
        }
        public static int ReadBinFile(FileStream fs , out byte[] data , int offset , int length)
        {
            data = new byte[length];
            try
            {
                if (offset + length < fs.Length)
                    fs.Read(data , 0 , length);
                else
                    fs.Read(data , offset , (int)(fs.Length - offset));
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out sbyte[] data , int offset , int length)
        {
            data = new sbyte[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadSByte();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadSByte();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out short[] data , int offset , int length)
        {
            data = new short[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadInt16();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadInt16();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out ushort[] data , int offset , int length)
        {
            data = new ushort[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadUInt16();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadUInt16();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out int[] data , int offset , int length)
        {
            data = new int[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadInt32();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadInt32();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out uint[] data , int offset , int length)
        {
            data = new uint[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadUInt32();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadUInt32();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out long[] data , int offset , int length)
        {
            data = new long[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadInt64();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadInt64();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public static int ReadBinFile(BinaryReader br , out ulong[] data , int offset , int length)
        {
            data = new ulong[length];
            try
            {
                if (offset + length < br.BaseStream.Length)
                    for (int i = 0; i < length; i++)
                        data[i] = br.ReadUInt64();
                else
                    for (int i = 0; i < br.BaseStream.Length - offset; i++)
                        data[i] = br.ReadUInt64();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
    }
}
