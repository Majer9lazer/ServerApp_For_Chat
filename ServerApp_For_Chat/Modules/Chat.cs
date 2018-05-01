using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace ServerApp_For_Chat.Modules
{
    public sealed class Chat : NancyModule
    {
        public Chat()
        {
            Get("/GetLocalhostWithPort", a => "http://localhost:52312/");
            Post("/AddMessage/{GroupName}/{Message}", a =>
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent;
                    DirectoryInfo info = Directory.CreateDirectory(dir?.FullName + $"/{a.GroupName}");
                    string fullpath = $"{info.FullName}/{a.GroupName}.txt";
                    if (!File.Exists(fullpath))
                    {
                        FileStream chatFileStream = File.Create(fullpath);
                        chatFileStream.Dispose();
                    }
                    using (StreamWriter sw = new StreamWriter(fullpath, true))
                    {
                        sw.WriteLine(String.Format("{0}", a.Message));
                    }
                    return "Ok";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }
    }
}
