using System;
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

namespace Relex2
{
    /// <summary>
    /// Relex.xaml 的交互逻辑
    /// </summary>
    public partial class Relex : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private bool is_playinig;
        public int PageNum;
        public bool isover;

        public Relex(int PageNum,double relextime,bool IsOver)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.progressbar.Maximum = relextime;
            this.isover = IsOver;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);   //设置刷新的间隔时间
            timer.Start();
            is_playinig = true;
            Submit.IsEnabled = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (is_playinig)
            {
                if (progressbar.Value == progressbar.Maximum)
                {

                    timer.Stop();
                    is_playinig = false;
                    Submit.IsEnabled = true;
                    progressbar.Foreground = new SolidColorBrush(Color.FromRgb(226, 46, 46));
                    this.label.Content = "  休息结束";
                }
                else
                {
                    progressbar.Value = progressbar.Value + 1;

                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.isover)
            {
                Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
                win.ShutDownTopwindow = false;
                win.OpenVoicewindow = true;
                win.Close();
            }
            else
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

    }
}
