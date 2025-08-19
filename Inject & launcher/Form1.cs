using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Inject___launcher {
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public partial class Form1 : Form {
        public Form1() {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            if (File.Exists("path.dat")) {
                this.textBox1.Text = File.ReadAllText("path.dat");
            }
        }

        [DllImport("Phasmophobia.dll")] //声明API函数 (Declare API function)
        public static extern void Inject();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            this.listBox1.Items.Clear();
            this.button1.Enabled = false;
            try {
                new Thread(
                           () => {
                               this.listBox1.Items.Add(Log.FormatLog("准备启动游戏... (Preparing to launch game...)"));
                               this.listBox1.Items.Add(Log.FormatLog("正在启动游戏... (Launching game...)"));

                               string url = "steam://rungameid/739630";
                               Process p = new Process();
                               p.StartInfo.FileName = "cmd.exe";
                               p.StartInfo.UseShellExecute = false; //不使用shell启动 (Don't use shell to start)
                               p.StartInfo.RedirectStandardInput = true;  //喊cmd接受标准输入 (Make CMD accept standard input)
                               p.StartInfo.RedirectStandardOutput = false; //不想听cmd讲话所以不要他输出 (Don't want to hear CMD talk so don't output)
                               p.StartInfo.RedirectStandardError = true;  //重定向标准错误输出 (Redirect standard error output)
                               p.StartInfo.CreateNoWindow = true;  //不显示窗口 (Don't show window)
                               p.Start();

                               //向cmd窗口发送输入信息 后面的&exit告诉cmd运行好之后就退出 (Send input to CMD window, &exit tells CMD to exit after running)
                               p.StandardInput.WriteLine("start " + url + "&exit");
                               p.StandardInput.AutoFlush = true;
                               p.WaitForExit(); //等待程序执行完退出进程 (Wait for the program to finish and exit the process)
                               p.Close();

                               this.button1.Enabled = true;
                           }).Start();
            } catch (ThreadStateException ex) {
                // TODO: Handle the System.Threading.ThreadStateException
                MessageBox.Show("创建启动线程失败\n" + ex.ToString() + " (Failed to create launch thread\n" + ex.ToString() + ")", "错误 (Error)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                this.listBox1.Items.Add(Log.FormatLog("创建启动线程失败! (Failed to create launch thread!)"));
                this.button1.Enabled = true;
            } catch (Win32Exception ex) {
                // TODO: Handle the System.ComponentModel.Win32Exception
                MessageBox.Show("启动游戏失败\n" + ex.ToString() + " (Failed to launch game\n" + ex.ToString() + ")", "错误 (Error)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                this.listBox1.Items.Add(Log.FormatLog("启动游戏失败! (Failed to launch game!)"));
                this.button1.Enabled = true;
            }
        }

        private static string GenerateRandomAlphanumeric(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
                byte[] data = new byte[length];
                rng.GetBytes(data);
                
                for (int i = 0; i < length; i++) {
                    result.Append(chars[data[i] % chars.Length]);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) {
            // Set random window title
            this.Text = GenerateRandomAlphanumeric(12);
            
            this.textBox1.PlaceholderText = "路径... (Path...)";
            this.listBox1.Items.Add(Log.FormatLog("启动器启动! (Launcher started!)"));
        }

        private void button2_Click(object sender, EventArgs e) {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择一个目录作为路径： (Please select a directory as path:)";
            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.ApplicationData;
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            this.textBox1.Text = dialog.SelectedPath;
            try {
                if (!File.Exists("path.dat")) {
                    File.Create("path.dat").Close();
                }
                File.WriteAllText("path.dat", this.textBox1.Text);
            } catch (DirectoryNotFoundException ex) {
                // TODO: Handle the System.IO.DirectoryNotFoundException
                MessageBox.Show("写出目录失败\n" + ex.ToString() + " (Failed to write directory\n" + ex.ToString() + ")", "错误 (Error)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            string url = "https://github.com/issuimo/PhasmophobiaCheat/tree/main";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false; //不使用shell启动 (Don't use shell to start)
            p.StartInfo.RedirectStandardInput = true;  //喊cmd接受标准输入 (Make CMD accept standard input)
            p.StartInfo.RedirectStandardOutput = false; //不想听cmd讲话所以不要他输出 (Don't want to hear CMD talk so don't output)
            p.StartInfo.RedirectStandardError = true;  //重定向标准错误输出 (Redirect standard error output)
            p.StartInfo.CreateNoWindow = true;  //不显示窗口 (Don't show window)
            p.Start();

            //向cmd窗口发送输入信息 后面的&exit告诉cmd运行好之后就退出 (Send input to CMD window, &exit tells CMD to exit after running)
            p.StandardInput.WriteLine("start " + url + "&exit");
            p.StandardInput.AutoFlush = true;
            p.WaitForExit(); //等待程序执行完退出进程 (Wait for the program to finish and exit the process)
            p.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e) {
            new Thread(
                       () => {
                           this.button3.Enabled = false;
                           Form1.Inject();
                           this.button3.Enabled = true;
                       }).Start();
        }

        private void label2_Click(object sender, EventArgs e) {

        }
    }
}