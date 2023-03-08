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
using System.Net.NetworkInformation;

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
            File.Delete(path);
            StreamWriter writer = new StreamWriter(path, true);
            string[] network = networkUsage();
            writer.WriteLine("CPU usage is at " + cpuUsage() + "%");
            writer.WriteLine("RAM usage is at " + ramUsage() + "%");
            writer.WriteLine("Hard Drive usage is at " + hddUsage() + "%");
            writer.WriteLine("kbytes sent " + network[0] + " kbs");
            writer.WriteLine("kbytes recieved " + network[1] + " kbs");
            writer.Close();

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
        public string[] networkUsage()
        {
            long kbytesSent = 0;
            long kbytesReceived = 0;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                kbytesSent += ni.GetIPv4Statistics().BytesSent / 1024;
                kbytesReceived += ni.GetIPv4Statistics().BytesReceived / 1024;
            }
            Thread.Sleep(1000);
            string[] sent_recieve = new string [2];
            sent_recieve[0] = kbytesSent.ToString();
            sent_recieve[1] = kbytesReceived.ToString();
            return sent_recieve;
        }
    }
}
