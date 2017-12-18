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

namespace ShowPicture2
{
    /// <summary>
    /// ShowPicture.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPicture : UserControl
    {
        private String PicturePath, userPath;
        private DispatcherTimer timer = new DispatcherTimer();
        public int PageNum;
        private int buttontime;
        private String wavfile = "C:\\Users\\ad\\Desktop\\new.wav";
        public int Type;
        public int Time;
        public int timeRecorder = 0;
        public bool isStart;
        public bool isEnd;
        private bool is_playinig;
        FileStream fs = null;
        StreamWriter sw = null;
        MediaPlayer player = new MediaPlayer();
        MediaPlayer player2 = new MediaPlayer();

        public ShowPicture(int PageNum, int type, int time, String PicturePath, string userPath, bool isStart, bool isEnd)
        {
            InitializeComponent();
            if (type == 2)
                this.restart.Visibility = Visibility.Hidden;

            if (type == 1 || type == 3)
            {
                this.start.IsEnabled = false;
                this.restart.IsEnabled = false;
                this.start.Content = "结束回答";
            }
                

            this.Type = type;
            this.Time = time;
            this.isStart = isStart;
            this.isEnd = isEnd;
            this.PageNum = PageNum;
            this.PicturePath = PicturePath;
            this.userPath = userPath;
            picture.Source = new BitmapImage(new Uri(PicturePath, UriKind.Absolute));
            buttontime = 1;
            this.recording.Visibility = Visibility.Hidden;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);   //设置刷新的间隔时间
            this.timeout.Visibility = Visibility.Hidden;

            string recordname = PicturePath.Split('\\')[2].Split('.')[0];

            player.Open(new Uri("e:\\文本\\" + recordname + ".wav", UriKind.Absolute));
            player.MediaEnded += new EventHandler(MediaEnded);
            


            player2.Open(new Uri("e:\\文本\\ding.wav", UriKind.Absolute));
            player2.MediaEnded += new EventHandler(MediaEnded2);
            
            
            player.Play();
        }

        void MediaEnded(object sender, EventArgs e)
        {
            player.Stop();
            player2.Play();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (is_playinig)
            {
                if (timeRecorder == 180)
                {
                    this.timeout.Visibility = Visibility.Visible;
                    is_playinig = false;
                    timer.Stop();
                }
                else
                {
                    timeRecorder = timeRecorder + 1;
                }
            }
        }

        void MediaEnded2(object sender, EventArgs e)
        {
            player2.Stop();

            
            this.recording.Visibility = Visibility.Visible;
            this.stoping.Visibility = Visibility.Hidden;
            switch (this.Type)
            {
                case 1:
                    if (this.Time == 1)
                    {
                        

                        // 创建目录
                        if (!Directory.Exists(this.userPath))
                        {
                            Directory.CreateDirectory(this.userPath);
                        }

                        if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "happy-1"))
                            this.setWavfile(this.userPath + "\\01.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "neutrality-1"))
                            this.setWavfile(this.userPath + "\\02.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "sad-1"))
                            this.setWavfile(this.userPath + "\\03.wav");

                    }
                    else if (this.Time == 2)
                        this.setWavfile(this.userPath + "\\" + "Interview2" + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    else
                        this.setWavfile(this.userPath + "\\" + "Interview3" + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    break;


                case 2:
                    this.setWavfile(this.userPath + "\\" + "Read" + this.Time + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    break;


                case 3:
                    if (this.Time == 1)
                        this.setWavfile(this.userPath + "\\" + "Picture1" + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    else if (this.Time == 2)
                        this.setWavfile(this.userPath + "\\" + "Picture2" + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    else
                        this.setWavfile(this.userPath + "\\" + "Picture3" + "\\" + PicturePath.Split('\\')[2].Split('.')[0] + ".wav");
                    break;
            }

            if (this.isStart)
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                Double startTime = Convert.ToInt64(ts.TotalSeconds);
                this.setStartTime(startTime);
            }

            if (this.Type == 2)
                log();

            timer.Start();
            is_playinig = true;

            this.StartRecord();

            this.start.IsEnabled = true;
            this.restart.IsEnabled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                buttontime = 2;
                start.Content = "继续";
                this.recording.Visibility = Visibility.Hidden;
                this.stoping.Visibility = Visibility.Visible;

                if (this.isEnd)
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    Double endTime = Convert.ToInt64(ts.TotalSeconds);
                    this.setEndTime(endTime);
                }

                this.StopRecord();
            }
            else
            {
                this.NextPage();
            }
        }

        private void restart_Click_1(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
                this.StopRecord();
            this.recording.Visibility = Visibility.Hidden;
            this.stoping.Visibility = Visibility.Visible;
            this.start.Content = "结束回答";
            buttontime = 1;

            timer.Stop();
            is_playinig = false;
            timeRecorder = 0;
            this.timeout.Visibility = Visibility.Hidden;
            
            this.start.IsEnabled = false;
            this.restart.IsEnabled = false;
            
            player.Play();
        }


        public void setStartTime(double StartTime)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setStartTime(2,StartTime);
        }

        public void setEndTime(double EndTime)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setEndTime(2,EndTime);
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

        public void setWavfile(String filename)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setWavfile(filename);
        }

        public void StartRecord()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.StartRecord();
        }

        public void StopRecord()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.StopRecord();
        }

        public void log()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            fs = new FileStream(win.getUserPath() + "\\log.txt", FileMode.Append);
            sw = new StreamWriter(fs);
            if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "happy-1"))
                sw.Write("正性视频");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "neutrality-1"))
                sw.Write("中性视频");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "sad-1"))
                sw.Write("负性视频");

            sw.WriteLine();
            sw.Close();
            fs.Close();
        }

    }
}

