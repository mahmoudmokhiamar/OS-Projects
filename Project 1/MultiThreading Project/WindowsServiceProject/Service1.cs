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
using System.Net.Mail;

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
            while(true) {
                Thread.Sleep(12*60*60*1000); //making process run every 12 hours.
                working();
                sendMail();
            }
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
            writer.WriteLine("Network kbytes sent " + network[0] + " kbs");
            writer.WriteLine("Network kbytes recieved " + network[1] + " kbs");
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
        public void sendMail()
        {

            string email = "mahmoudmokhiamar@gmail.com";
            string to = email;
            string from = "csprojectsejust@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailbody = "";
            message.Subject = "Computer System Usage Report";
            Attachment attachment = new Attachment(@"C:\Performance_Analyzer.txt");
            message.Attachments.Add(attachment);
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("csprojectsejust@gmail.com", "vznafseskwrrcalw");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);
            attachment.Dispose();
            message.Dispose();
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
