using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MatryoshkaDollTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string vswhere_path = System.Environment.CurrentDirectory + @"\" + "vswhere.exe";

            if (!System.IO.File.Exists(vswhere_path))
            {
                try
                {
                    byte[] vswhere = global::MatryoshkaDollTool.Properties.Resource1.vswhere;
                    FileStream f = new FileStream(vswhere_path, FileMode.CreateNew);
                    f.Write(vswhere, 0, vswhere.Length);
                    f.Close();
                }
                catch
                {

                    MessageBox.Show("Release vswhere.exe fail");
                    return;

                }
            }

            Cmd c = new Cmd();

            //C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe
            string vspath = c.run(vswhere_path + " -prerelease -latest -property installationPath", 10).Replace("\r","").Replace("\n", "");
            if (vspath.Equals("error")) {
                MessageBox.Show("can not found Visual studio IDE");
            }
            else if(!System.IO.File.Exists(@vspath + @"\Common7\IDE\devenv.exe"))
            {
                MessageBox.Show("can not found devenv.exe (PLS Install Visual studio IDE)");
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
