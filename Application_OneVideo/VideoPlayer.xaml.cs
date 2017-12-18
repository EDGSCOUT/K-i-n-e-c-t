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

namespace VideoPlayer
{
    /// <summary>
    /// VideoPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        private bool is_playinig;
        private DispatcherTimer timer = new DispatcherTimer();
        private String FilePath;
        public int PageNum;
        private int buttontime;
        FileStream fs = null;
        StreamWriter sw = null;
        

        public VideoPlayer(int PageNum,String FilePath)
        {
            InitializeComponent();

            this.FilePath = FilePath;
            this.PageNum = PageNum;
            this.buttontime = 1;

            
            mediaElement1.Source = new Uri(FilePath, UriKind.Relative);
            //将音量调节到我们设定的音量值0.5
            InitializePropertyValues();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);   //设置刷新的间隔时间
            
            this.over.Visibility = Visibility.Hidden;
        }

        private void button_Play(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                Play.IsEnabled = false;
                Play.Content = "结束";
                this.setCurrentVideo(FilePath);
                //this.log();
                
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                Double startTime = Convert.ToInt64(ts.TotalSeconds);
                this.setStartTime(startTime);
                this.buttontime = 2;
                this.black.Visibility = Visibility.Hidden;
                TimeLine.Value = 0;
                mediaElement1.Play();
                
            }
            else
            {
                this.NextPage();
            }
            
        }

        private void button_Restart(object sender, RoutedEventArgs e)
        {
            if (is_playinig)
            {
                mediaElement1.Stop();
                timer.Stop();
                is_playinig = false;
            }
            
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            switch (this.PageNum)
            {
                case 5: win.setOK1(false); break;
                case 10: win.setOK2(false); break;
                case 15: win.setOK3(false); break;

            }
            win.SetRetryVideoPage(this.PageNum - 1);
            win.SetCurrentPage(25);
        }
        
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            mediaElement1.Volume = (double)Volume.Value;

        }


        void InitializePropertyValues()
        {
            mediaElement1.Volume = (double)Volume.Value;
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            //获取媒体的时间长度，并赋予进度条的Maxinum
            TimeLine.Maximum = mediaElement1.NaturalDuration.TimeSpan.TotalMilliseconds;
            Console.WriteLine("maximum = " + TimeLine.Maximum);
            Console.WriteLine("MediaOpened");

            is_playinig = true;
            timer.Start();
        }

        private void mediaElement1_MediaEnded_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("MediaEnded");
        }


        private void mediaElement1_MediaFailed_1(object sender, ExceptionRoutedEventArgs e)
        {
            Console.WriteLine("MediaFailed");
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
                    mediaElement1.Stop();
                    timer.Stop();
                    is_playinig = false;
                    Play.IsEnabled = true;

                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    Double endTime = Convert.ToInt64(ts.TotalSeconds);
                    this.setEndTime(endTime);
                    this.black.Visibility = Visibility.Visible;
                    this.over.Visibility = Visibility.Visible;
                }
                else
                {
                    TimeLine.Value = TimeLine.Value + 1000;
                    
                }
            }
        }


        public void setStartTime(double StartTime)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setStartTime(1,StartTime);
        }

        public void setEndTime(double EndTime)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setEndTime(1,EndTime);
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

        public void setCurrentVideo(string videopath)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setCurrentVideo(videopath);
        }

        public void log()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            fs = new FileStream(win.getUserPath()+ "\\log.txt", FileMode.Append);
            sw = new StreamWriter(fs);
            
            if (String.Equals(this.FilePath.Split('\\')[2].Split('.')[0], "happy-1"))
                sw.Write("正性视频");
            else if (String.Equals(this.FilePath.Split('\\')[2].Split('.')[0], "neutrality-1"))
                sw.Write("中性视频");
            else if (String.Equals(this.FilePath.Split('\\')[2].Split('.')[0], "sad-1"))
                sw.Write("负性视频");

            sw.WriteLine();
            sw.Close();
            fs.Close();
        }

        
    }
}
