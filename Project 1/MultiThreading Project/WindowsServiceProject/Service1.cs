using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace WindowsServiceProject
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            working();
        }

        protected override void OnStop()
        {
        }
        public void working()
        {
            string path = " C:\\Performance_Analyzer.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("CPU usage is at " + cpuUsage() + "%");
                writer.WriteLine("RAM usage is at " + ramUsage() + "%");
                writer.WriteLine("Hard Drive usage is at " + hddUsage() + "%");
                writer.Close();
            }
        }
        public string cpuUsage()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            while (true)
            {
                float ans = cpuCounter.NextValue();
                Thread.Sleep(1000);
                if (ans > 0 && ans < 100)
                {
                    string result = ans.ToString();
                    return result;
                }
            }
        }
        public string ramUsage()
        {


            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");

            while (true)
            {
                //making sure readings are logical
                Thread.Sleep(1000);
                float ramUsage = ramCounter.NextValue();
                if (ramUsage > 0 && ramUsage < 100)
                    return ramUsage.ToString();
            }
        }
        public string hddUsage()
        {
            PerformanceCounter diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

            while (true)
            {
                float diskUsage = diskCounter.NextValue();
                Thread.Sleep(1000);
                if (diskUsage > 0 && diskUsage < 100)
                {
                    return diskUsage.ToString();
                }
            }
        }

    }
}
