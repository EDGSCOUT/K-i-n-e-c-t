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

namespace Video
{
    /// <summary>
    /// Video.xaml 的交互逻辑
    /// </summary>
    public partial class Video : UserControl
    {
        private bool is_playinig;
        private DispatcherTimer timer = new DispatcherTimer();
        private String FilePath = "E:\\video\\study.mp4";
        public int PageNum;
        int buttontime = 1;

        public Video(int PageNum)
        {
            InitializeComponent();

            this.PageNum = PageNum;
            this.TimeLine.Visibility = Visibility.Hidden;
            mediaElement1.Source = new Uri(FilePath, UriKind.Relative);
            //将音量调节到我们设定的音量值0.5
            InitializePropertyValues();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);   //设置刷新的间隔时间

            TimeLine.Value = 0;
            mediaElement1.Stop();
            mediaElement1.Play();
            is_playinig = true;
            timer.Start();
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            //获取媒体的时间长度，并赋予进度条的Maxinum
            TimeLine.Maximum = mediaElement1.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void TimeLine_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (is_playinig)
            {
                //获取当前进度条的Value,也就是要播放的时间位置，定位媒体的位置
                int SliderValue = (int)TimeLine.Value;
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                mediaElement1.Position = ts;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (is_playinig)
            {
                if (TimeLine.Value == TimeLine.Maximum)
                {
                    buttontime = 2;
                    mediaElement1.Stop();
                    timer.Stop();
                    is_playinig = false;
                    this.Play.Content = "继续";
                }
                else
                {
                    TimeLine.Value = TimeLine.Value + 1000;

                }
            }
        }

        void InitializePropertyValues()
        {
            mediaElement1.Volume = 1.0;
        }

        private void NextPage()
        {
            EmotionStudy.MainWindow win = Window.GetWindow(this) as EmotionStudy.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            EmotionStudy.MainWindow win = Window.GetWindow(this) as EmotionStudy.MainWindow;
            win.PreviousPage(PageNum);
        }

        private void button_Play(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                buttontime = 2;
                mediaElement1.Stop();
                timer.Stop();
                is_playinig = false;
                this.Play.Content = "继续";
            }
            else
                this.NextPage();
        }

    }
}
