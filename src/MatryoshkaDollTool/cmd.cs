using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MatryoshkaDollTool
{
    /// <summary>
    /// Cmd 的摘要说明。
    /// </summary>
    public class Cmd
    {
        public string run(string command, int seconds)
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
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();//等待程序执行完退出进程
                    process.Close();
                    return output;
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
                return "error";
            }
            return "error";
        }

        public string build(string devtool,string command, int seconds)
        {
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = devtool;
                startInfo.Arguments = " " + command + " /rebuild";
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
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();//等待程序执行完退出进程
                    process.Close();
                    return output;
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
                return "error";
            }
            return "error";
        }

        public bool resFile(string SourcePath, string DestinationPath)
        {

            string path = SourcePath;
            string path2 = DestinationPath;
            FileInfo fi1 = new FileInfo(path);
            FileInfo fi2 = new FileInfo(path2);

            try
            {
                if (File.Exists(path2))
                {
                    fi2.Delete();
                }
                fi1.CopyTo(path2);
                return true;
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                return false;
            }
        }
    }
}
