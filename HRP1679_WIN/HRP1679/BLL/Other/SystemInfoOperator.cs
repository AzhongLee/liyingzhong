using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Management;
namespace HRP1679.BLL.Other
{
    public class SystemInfoOperator
    {
        private int m_ProcessorCount = 0;   //CPU个数 
        PerformanceCounter pcCpuLoad = new PerformanceCounter();

        #region " 单例 "

        private static SystemInfoOperator instance;
        private static object lockObj = new object();

        public static SystemInfoOperator Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                        instance = new SystemInfoOperator();
                    return instance;
                };
            }
        }

        private SystemInfoOperator()
        {

        }

        #endregion

        //获取CPU使用率
        public string GetCpuUtilization()
        {
            string cpuUti = string.Empty;
            pcCpuLoad.CategoryName = "Processor";//指定获取计算机进程信息  如果传Processor参数代表查询计算机CPU 
            pcCpuLoad.CounterName = "% Processor Time";//占有率
            //如果pp.CategoryName="Processor",那么你这里赋值这个参数 pp.InstanceName = "_Total"代表查询本计算机的总CPU。
            pcCpuLoad.InstanceName = "_Total";//指定进程 
            pcCpuLoad.MachineName = ".";

            cpuUti = pcCpuLoad.NextValue().ToString("F0");

            return cpuUti + "%";
        }

        /// <summary>
        /// 获取物理内存总量
        /// </summary>
        /// <returns>物理内存总量，单位byte</returns>
        public long GetPhysicalMemory()
        {
            long m_PhysicalMemory = 0;   //物理内存

            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    m_PhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }

            return m_PhysicalMemory;
        }

        /// <summary>
        /// 获取可用物理内存
        /// </summary>
        /// <returns>可用物理内存，单位byte</returns>

        public long GetAvailableMemory()
        {
            long availablebytes = 0;

            ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
            foreach (ManagementObject mo in mos.GetInstances())
            {
                if (mo["FreePhysicalMemory"] != null)
                {
                    availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                }
            }
            return availablebytes;

        }
        /// <summary>
        /// 获取CPU温度
        /// </summary>
        /// <returns></returns>
        public string GetCpuTemp()
        {
            Double CPUtprt = 0;
            string temp = string.Empty;

            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(@"root\WMI" , "Select * From MSAcpi_ThermalZoneTemperature");
            try
            {
                foreach (System.Management.ManagementObject mo in mos.Get())
                {

                    CPUtprt = Convert.ToDouble(Convert.ToDouble(mo.GetPropertyValue("CurrentTemperature").ToString()) - 2732) / 10;

                    temp =((uint) CPUtprt).ToString() + "°C";
                }
            }
            catch (Exception)
            {

            }
            return temp;
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI" , "SELECT * FROM MSAcpi_ThermalZoneTemperature"); ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator();
            //int raw = 0; 

            //while (enumerator.MoveNext()) 
            //{ 
            //    ManagementBaseObject tempObject = enumerator.Current; 
            //    raw = Convert.ToInt32(tempObject["CurrentTemperature"].ToString());
            //} 
            //double celsius = (raw / 10) - 273.15;
            //return celsius.ToString();
        }
    }
}