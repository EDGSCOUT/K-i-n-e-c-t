using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace Result
{
    /// <summary>
    /// Result.xaml 的交互逻辑
    /// </summary>
    public partial class Result : UserControl
    {
        public int PageNum;
        private DispatcherTimer timer = new DispatcherTimer();
        private int userid;
        private bool isPrinted = false;

        public Result(int PageNum,int UserId)
        {
            InitializeComponent();

            this.userid = UserId;

            ProcessStartInfo ps = new ProcessStartInfo();
            ps.CreateNoWindow = true;
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            ps.FileName = "..\\..\\..\\\\BeApp\\run.bat";

            Process proc = new Process();
            proc.StartInfo = ps;
            //proc.WaitForExit();//不等待完成就不调用此方法
            proc.Start(); 


            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(2);   //设置刷新的间隔时间
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (File.Exists("..\\..\\..\\\\BeApp\\UserReport\\" + this.userid + "-result.txt"))
            {
                this.label.Visibility = Visibility.Hidden;
                this.textbox.Visibility = Visibility.Visible;
                this.Submit.IsEnabled = true;
                this.Print.IsEnabled = true;

                System.IO.StreamReader stream = new System.IO.StreamReader("..\\..\\..\\\\BeApp\\UserReport\\" + this.userid + "-result.txt", System.Text.Encoding.UTF8);
                this.textbox.Text = stream.ReadToEnd();
                stream.Close(); 
                timer.Stop();
            } 
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NextPage();
        }

        private void NextPage()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.PreviousPage(PageNum);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (isPrinted == false)
            {
                isPrinted = true;
                this.Print.IsEnabled = false;

                Process pr = new Process();
                pr.StartInfo.FileName = "..\\..\\..\\\\BeApp\\UserReport\\" + this.userid + "-result.txt";
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pr.StartInfo.Verb = "Print";
                pr.Start();
            }
        }

    }
}
