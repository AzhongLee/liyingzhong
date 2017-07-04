using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRP1679.DAL.Para
{
    public class ParaSimulator
    {
        public ParaPortOne PortOne { get; set; }
        public ParaPortTow PortTow { get; set; }
        public ParaSimulator()
        {
            PortOne = new ParaPortOne();
            PortTow = new ParaPortTow();
        }
    }
    /// <summary>
    /// 端口1参数包
    /// </summary>
  public  class ParaPortOne
    {
        public ModeDeploy ModeOne { get; set; }
        public float[][] Para { get; set; }
        public ParaPortOne()
        {
            ModeOne = new ModeDeploy(6);
            Para = new float[6][];
        }
    }
    /// <summary>
    /// 端口2参数包
    /// </summary>
 public   class ParaPortTow
    {
        public ModeDeploy ModeTow { get; set; }
        public float[][] Para { get; set; }
        public ParaPortTow()
        {
            ModeTow = new ModeDeploy(3);
            Para = new float[3][];
        }
    }
    /// <summary>
    /// 目标模式配置
    /// </summary>
     public  class ModeDeploy
    {
        public List<string> Key { get; set; }
        public List<float> Value { get; set; }

        public ModeDeploy(int num)
        {
            Key = new List<string>();
            Value = new List<float>();
            Key.Capacity = Value.Capacity = num;
        }
    }
}
   

