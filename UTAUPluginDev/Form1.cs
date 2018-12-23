using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UTAUPluginDev
{
    public partial class UTAUPluginDev : Form
    {
        // 导入多语言
        private static string rootDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        LangPack pack = new LangPack(rootDir + "lang.xml");
       
        // 基础设置
        private string[] args;
        private string path;
        private string tempPath;
        private string ust = "请作为插件或引擎使用方可显示";
        private string allUstFile = "请作为插件使用方可显示";
        private string batFilein = "请作为引擎使用方可显示";
        private string project;
        private string batHelper = "请作为引擎使用方可显示";
        private int psbarValue = 0;
        private string toolStripStatusLabel2Value = "分析中...";

        public UTAUPluginDev()
        {
            InitializeComponent();
            args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {

                path = args[1];
                ust = File.ReadAllText(path);

                tempPath = path.Substring(0, path.Length - 11);

                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader read = new StreamReader(fs, Encoding.Default);
                    int i = 0;
                    string strReadline = "";
                    while (i == 0)
                    {
                        strReadline = read.ReadLine();
                        //MessageBox.Show(strReadline);
                        if (strReadline.Contains("Project=") == true)
                        {
                            project = strReadline.Substring(strReadline.IndexOf("=") + 1);
                            i = 1;
                            //MessageBox.Show(project);
                        }
                        else
                        {
                            i = 0;
                        }
                    }
                    allUstFile = File.ReadAllText(project);
                    psbarValue = 100;
                    toolStripStatusLabel2Value = "分析完成!";
                }
                catch (Exception e)
                {
                    MessageBox.Show("错误信息：" + e.Message);
                    psbarValue = 0;
                }
            }
            else
            {
                if (args.Length > 3)
                {
                    try
                    {
                        string batFile = Environment.CurrentDirectory + "\\temp.bat";
                        using (StreamReader sr = new StreamReader(batFile, Encoding.UTF8))
                        {
                            //MessageBox.Show(batFile);
                            batFilein = sr.ReadToEnd();
                            byte[] mybyte = Encoding.UTF8.GetBytes(batFilein);
                            batFilein = Encoding.UTF8.GetString(mybyte);
                        }
                        string batHelperPath = Environment.CurrentDirectory + "\\temp_helper.bat";
                        using (StreamReader sr = new StreamReader(batHelperPath, Encoding.UTF8))
                        {
                            //MessageBox.Show(batHelper);
                            batHelper = sr.ReadToEnd();
                            byte[] mybyte = Encoding.UTF8.GetBytes(batHelper);
                            batHelper = Encoding.UTF8.GetString(mybyte);
                        }
                        psbarValue = 100;
                        toolStripStatusLabel2Value = "分析完成!";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("错误信息：" + e.Message);
                    }
                }
                else
                {
                    MessageBox.Show("请作为UTAU的引擎/插件使用", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    toolStripStatusLabel2Value = "分析错误";
                }
            }
        }

        private void samplePlug_Load(object sender, EventArgs e)
        {
            textBox1.Lines = args;
            textBox2.Text = ust;
            textBox3.Text = allUstFile;
            textBox4.Text = batFilein;
            textBox5.Text = batHelper;
            toolStripProgressBar1.Value = psbarValue;
            toolStripStatusLabel2.Text = toolStripStatusLabel2Value;
        }

        private void 保存选择部分的USTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "请选择要保存的位置";
            sav.Filter = "TXT|*.txt";
            sav.ShowDialog();
            string stt = sav.FileName;
            if (stt == "")
            {
                MessageBox.Show("请输入文件名", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FileStream fil = new FileStream(stt, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] byt = new byte[1024];
                string str = ust;
                byt = Encoding.Default.GetBytes(str);
                fil.Write(byt, 0, byt.Length);
                fil.Flush();
                fil.Close();
            }
            MessageBox.Show("保存完毕");
        }

        private void 保存全部USTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "请选择要保存的位置";
            sav.Filter = "TXT|*.txt";
            sav.ShowDialog();
            string stt = sav.FileName;
            if (stt == "")
            {
                MessageBox.Show("请输入文件名", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FileStream fil = new FileStream(stt, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] byt = new byte[1024];
                string str = allUstFile;
                byt = Encoding.Default.GetBytes(str);
                fil.Write(byt, 0, byt.Length);
                fil.Flush();
                fil.Close();
            }
            MessageBox.Show("保存完毕");
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 保存ArgsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "请选择要保存的位置";
            sav.Filter = "TXT|*.txt";
            sav.ShowDialog();
            string stt = sav.FileName;
            if (stt == "")
            {
                MessageBox.Show("请输入文件名", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FileStream fil = new FileStream(stt, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] byt = new byte[1024];
                string str = textBox1.Text;
                byt = Encoding.Default.GetBytes(str);
                fil.Write(byt, 0, byt.Length);
                fil.Flush();
                fil.Close();
            }
            MessageBox.Show("保存完毕");
        }

        private void 保存合成用的BATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "请选择要保存的位置";
            sav.Filter = "TXT|*.txt";
            sav.ShowDialog();
            string stt = sav.FileName;
            if (stt == "")
            {
                MessageBox.Show("请输入文件名", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FileStream fil = new FileStream(stt, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] byt = new byte[1024];
                string str = batFilein;
                byt = Encoding.Default.GetBytes(str);
                fil.Write(byt, 0, byt.Length);
                fil.Flush();
                fil.Close();
            }
            MessageBox.Show("保存完毕");
        }

        private void 保存BAThelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "请选择要保存的位置";
            sav.Filter = "TXT|*.txt";
            sav.ShowDialog();
            string stt = sav.FileName;
            if (stt == "")
            {
                MessageBox.Show("请输入文件名", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FileStream fil = new FileStream(stt, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] byt = new byte[1024];
                string str = batHelper;
                byt = Encoding.Default.GetBytes(str);
                fil.Write(byt, 0, byt.Length);
                fil.Flush();
                fil.Close();
            }
            MessageBox.Show("保存完毕");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是一个小工具（废话），旨在于帮助萌新了解UTAU运作方式", "关于");
        }

        private void 打开UTAU临时文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(tempPath);
            }
            catch(Exception err)
            {
                MessageBox.Show("你不是通过UTAU运行的，嘤嘤嘤，窝找不到文件夹啦！！\n这里是给程序员咕咕咕看的东西：\n" + err.Message, "嘤嘤嘤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 如何使用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("我也不知道怎么用，嘤嘤嘤");
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("恭喜你发现了彩蛋，快去给程序员一个Star吧");
            System.Diagnostics.Process.Start("https://github.com/740291272");
        }
    }
}
