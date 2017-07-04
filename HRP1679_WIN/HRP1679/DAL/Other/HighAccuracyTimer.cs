
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace HRP1679.DAL.Other
{
  public  class HighAccuracyTimer
    {

        #region 构造函数
        public HighAccuracyTimer(double timeInterval)
        {
            // 获取时钟频率
            if (!QueryPerformanceFrequency(out m_lFreq))
            {
                throw new Win32Exception("QueryPerformanceFrequecy() function is not supported!");
            }
            //20121122这句必须在获取时钟频率后面，不然定时就不是要求的时间间隔了
            m_fIntervalTime = timeInterval;
            fIntervalTime = timeInterval;
            // 定义定时器触发事件
            m_trigEvent = new AutoResetEvent(false);
            TimerLoopEvent = new AutoResetEvent(false);

            m_runStatus = RunStatus.stop;
        }    
        #endregion

        #region 变量声明

        /// <summary>
        /// 运行状态枚举定义
        /// </summary>
        private enum RunStatus
        {
            stop = 0 ,   // 停止
            start = 1 ,  // 启动
            pause = 2   // 暂停
        };
        private RunStatus m_runStatus;

        private long m_lFreq; // 时钟频率，单位：Hz
        private double m_fIntervalTime; // 定时间隔，由用户设置，单位：ms
        public double m_fConsumeTime; // 记录一次timer实际消耗的时间，用户可得到，单位：ms
        private AutoResetEvent m_trigEvent; // 定时同步触发事件
        public AutoResetEvent TimerLoopEvent; // 定时同步触发事件
        private Thread m_timerThread; // 定时器线程
        private Thread m_handlerThread; // 定时响应线程
        private long lIntervalTicks = 0; // 定时间隔Ticks
        public delegate void ON_TIMER(); // 定时器响应事件的代理
        public ON_TIMER OnTimer; // 声明代理的引用
        public bool IsRunning;//运行状态指示
        private double timepasttest = (double)0;
        #endregion

        #region 属性
        //属性
        private double fIntervalTime
        {
            set
            {
                m_fIntervalTime = value;
                lIntervalTicks = (long)((double)m_fIntervalTime * (double)m_lFreq / (double)1e3);
            }
        }
        #endregion

        #region 外部调用函数声明

        /// <summary>
        /// Pointer to a variable that receives the current performance-counter frequency, 
        /// in counts per second. 
        /// If the installed hardware does not support a high-resolution performance counter, 
        /// this parameter can be zero. 
        /// </summary>
        /// <param name="lpFrequency"></param>
        /// <returns>If the installed hardware supports a high-resolution performance counter, 
        /// the return value is nonzero.
        /// </returns>
        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCounter);

        /// <summary>
        /// Pointer to a variable that receives the current performance-counter value, in counts. 
        /// </summary>
        /// <param name="lpPerformanceCount"></param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// </returns>
        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        [DllImport("kernel32.dll")]
        private static extern void Sleep(Int64 dwMilliseconds);
        #endregion

        #region 启动定时器
        /// <summary>
        /// 启动定时器
        /// </summary>
        public void StartTimer()
        {
            m_runStatus = RunStatus.start;

            // 定时器线程
            m_timerThread = new Thread(new ThreadStart(TimerThread));
            //add by vv 20121113
            m_timerThread.IsBackground = true;
            //m_timerThread.Priority = ThreadPriority.Highest;

            // 定时器事件处理线程
            m_handlerThread = new Thread(new ThreadStart(HandlerThread));
            //add by vv 20121113
            m_handlerThread.IsBackground = true;

            m_handlerThread.Start();
            m_timerThread.Start();
            IsRunning = true;
        }
        #endregion

        #region 终止定时器，定时器线程和事件处理线程会退出
        /// <summary>
        /// 终止定时器，定时器线程和事件处理线程会退出
        /// </summary>
        public void StopTimer()
        {
            m_runStatus = RunStatus.stop;
            IsRunning = false;
        }
        #endregion

        #region 暂停定时器，定时器线程和事件处理线程不退出
        /// <summary>
        /// 暂停定时器，定时器线程和事件处理线程不退出
        /// </summary>
        public void PauseTimer()
        {
            m_runStatus = RunStatus.pause;
        }
        #endregion

        #region 暂停后恢复启动定时器，不需重新创建线程
        /// <summary>
        /// 暂停后恢复启动定时器，不需重新创建线程
        /// </summary>
        public void ResumeTimer()
        {
            m_runStatus = RunStatus.start;
        }
        #endregion

        #region 定时器线程
        /// <summary>
        /// 定时器线程
        /// </summary>
        private void TimerThread()
        {
            long lCurrentTicks = 0; // 当前Ticks
            long lLastTicks = 0; // 上一次Ticks
            //long lIntervalTicks = 0; // 定时间隔Ticks
            long lNextTrigTicks = 0; // 下一次触发时的Ticks
            //Console.WriteLine("TimerThread:" + "{0}", Thread.CurrentThread.ManagedThreadId);
            //lIntervalTicks = (long)((double)m_fIntervalTime * (double)m_lFreq / (double)1e3);
            QueryPerformanceCounter(out lCurrentTicks);
            lLastTicks = lCurrentTicks;

            while (true)
            {
                switch (m_runStatus)
                {
                    case RunStatus.start:
                        lNextTrigTicks = lCurrentTicks + lIntervalTicks; // 计算下一次触发时的Ticks
                        while (lCurrentTicks < lNextTrigTicks) // 轮询当前时间，直到触发时刻到来
                        {
                            QueryPerformanceCounter(out lCurrentTicks);
                        }

                        m_fConsumeTime = (lCurrentTicks - lLastTicks) * (double)1e3 / (double)m_lFreq; // 计算实际的CPU时钟间隔  
                      //  TraceOutput.DebugInfoPrint("m_fConsumeTime_In = " + (m_fConsumeTime).ToString() + "ms" , false);
                        //timepasttest += m_fConsumeTime;
                        //TraceOutput.DebugInfoPrint("timepasttest = " + (timepasttest).ToString() + "ms", Comdef.isPrintDebugInfo);
                        m_trigEvent.Set(); // 唤醒同步事件
                        lLastTicks = lCurrentTicks; // 记录上一次Ticks
                        TimerLoopEvent.WaitOne();//线程阻塞，等待同步事件
                        break;

                    case RunStatus.pause: // 暂停，等待不退出
                        break;

                    case RunStatus.stop: // 停止，退出线程
                        {
                            m_timerThread.Abort();   
                            m_handlerThread.Abort();                                                                           
                            return;
                        }
                }
            }
        }
        #endregion

        #region 定时事件处理线程
        /// <summary>
        /// 定时事件处理线程
        /// </summary>
        private void HandlerThread()
        {
            //Console.WriteLine("HandlerThread:" + "{0}", Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                //Sleep(10);
                switch (m_runStatus)
                {
                    case RunStatus.start:
                        // 等待同步事件
                        m_trigEvent.WaitOne();
                        // 执行定时处理操作
                        OnTimer();
                        break;

                    case RunStatus.pause: // 暂停，等待不退出
                        break;

                    case RunStatus.stop: // 停止，退出线程
                        {
                         //   System.Diagnostics.Debug.WriteLine("timer is stop.");
                            m_handlerThread.Abort(); 
                            m_timerThread.Abort();                                                    
                           
                            return;
                        }
                }
            }
        }
        #endregion

        #region 释放资源
        public void DisopseTimer()
        {
          
            StopTimer();
            if (IsRunning)
            {
                m_timerThread.Abort();
                m_handlerThread.Abort();               
            }
            int count = 0;
       
            while (m_timerThread.ThreadState != ThreadState.Stopped)
            {
               count++;
               System.Diagnostics.Debug.WriteLine( m_timerThread.ThreadState.ToString());
                if (count >= 5)
                {
                    System.Diagnostics.Debug.WriteLine("break");
                    break;
                }
            }
          
        }
        #endregion

        #region 获得当前时钟滴嗒数
        public long GetCounter()
        {
            long counter;

            if (!QueryPerformanceCounter(out counter))
                throw new Win32Exception("QueryPerformanceCounter() failed!");

            return counter;
        }
        #endregion

        #region 计算逝去的时间
        public double Func_Interval(long CurrentTime , long LastTime)
        {
            double m_Interval = (double)(CurrentTime - LastTime) / (double)m_lFreq;
            return m_Interval;//s
        }
        #endregion
    }
}
