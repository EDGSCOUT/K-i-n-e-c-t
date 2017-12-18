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

namespace ShowPictureRead
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
        public int ExperimentType;
        public int EmotionType;
        public int Time;
        public int timeRecorder = 0;
        public bool isStart;
        public bool isEnd;
        private bool is_playinig;
        FileStream fs = null;
        StreamWriter sw = null;
        MediaPlayer player = new MediaPlayer();
        MediaPlayer player2;

        public ShowPicture(int PageNum, int ExperimentType, int EmotionType, int Time, String PicturePath, string userPath, bool isStart, bool isEnd)
        {
            InitializeComponent();

            if (ExperimentType == 1)
            {
                this.start.IsEnabled = false;
                this.restart.IsEnabled = false;
                this.start.Content = "开始回答";
            }
            else if (ExperimentType == 3)
            {
                this.start.IsEnabled = false;
                this.restart.IsEnabled = false;
                this.start.Content = "开始描述";
            }

            this.ExperimentType = ExperimentType;
            this.EmotionType = EmotionType;
            this.Time = Time;
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
            if (ExperimentType == 1 || ExperimentType == 3)
            {
                string recordname = PicturePath.Split('\\')[2].Split('.')[0];

                player.Open(new Uri("e:\\文本\\" + recordname + ".wav", UriKind.Absolute));
                player.Play();
                player.MediaEnded += new EventHandler(MediaEnded);
            }

        }

        void MediaEnded(object sender, EventArgs e)
        {
            this.start.IsEnabled = true;
            this.restart.IsEnabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (is_playinig)
            {
                if (timeRecorder == 3)
                {
                    start.IsEnabled = true;
                    timeRecorder = timeRecorder + 1;
                }
                else if (timeRecorder == 180)
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
            this.recording.Visibility = Visibility.Visible;
            this.stoping.Visibility = Visibility.Hidden;
            switch (this.ExperimentType)
            {
                case 1:
                    {
                        // 创建目录
                        if (!Directory.Exists(this.userPath))
                        {
                            Directory.CreateDirectory(this.userPath);
                        }

                        if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q11"))
                            this.setWavfile(this.userPath + "\\04.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q12"))
                            this.setWavfile(this.userPath + "\\05.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q13"))
                            this.setWavfile(this.userPath + "\\06.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q21"))
                            this.setWavfile(this.userPath + "\\07.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q22"))
                            this.setWavfile(this.userPath + "\\08.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q23"))
                            this.setWavfile(this.userPath + "\\09.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q31"))
                            this.setWavfile(this.userPath + "\\10.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q32"))
                            this.setWavfile(this.userPath + "\\11.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q33"))
                            this.setWavfile(this.userPath + "\\12.wav");

                        break;
                    }



                case 2:
                    {
                        // 创建目录
                        if (!Directory.Exists(this.userPath))
                        {
                            Directory.CreateDirectory(this.userPath);
                        }

                        if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "正性1"))
                            this.setWavfile(this.userPath + "\\13.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "中性1"))
                            this.setWavfile(this.userPath + "\\14.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "负性1"))
                            this.setWavfile(this.userPath + "\\15.wav");

                        break;
                    }



                case 3:
                    {
                        // 创建目录
                        if (!Directory.Exists(this.userPath))
                        {
                            Directory.CreateDirectory(this.userPath);
                        }

                        if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p11"))
                            this.setWavfile(this.userPath + "\\16.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p21"))
                            this.setWavfile(this.userPath + "\\17.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p31"))
                            this.setWavfile(this.userPath + "\\18.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p12"))
                            this.setWavfile(this.userPath + "\\19.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p22"))
                            this.setWavfile(this.userPath + "\\20.wav");
                        else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p32"))
                            this.setWavfile(this.userPath + "\\21.wav");

                        break;
                    }

            }

            if (this.isStart)
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                Double startTime = Convert.ToInt64(ts.TotalSeconds);
                switch (this.ExperimentType)
                {
                    case 1: this.setStartTime(this.Time, startTime); break;
                    case 2: this.setStartTime(this.Time, startTime); break;
                    case 3: this.setStartTime(this.Time, startTime); break;
                }

            }


            log();

            timer.Start();
            is_playinig = true;

            this.StartRecord();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                buttontime = 2;
                start.IsEnabled = false;
                if (this.ExperimentType == 1)
                    start.Content = "回答结束";
                else if (this.ExperimentType == 2)
                    start.Content = "朗读结束";
                else
                    start.Content = "描述结束";


                player2 = new MediaPlayer();
                player2.Open(new Uri("e:\\文本\\ding.wav", UriKind.Absolute));
                player2.Play();
                player2.MediaEnded += new EventHandler(MediaEnded2);

            }
            else if (buttontime == 2)
            {
                buttontime = 3;
                start.Content = "继续";
                this.recording.Visibility = Visibility.Hidden;
                this.stoping.Visibility = Visibility.Visible;

                if (this.isEnd)
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    Double endTime = Convert.ToInt64(ts.TotalSeconds);
                    switch (this.ExperimentType)
                    {
                        case 1: this.setEndTime(this.Time, endTime); break;
                        case 2: this.setEndTime(this.Time, endTime); break;
                        case 3: this.setEndTime(this.Time, endTime); break;
                    }
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
            if (buttontime == 2)
                this.StopRecord();
            this.recording.Visibility = Visibility.Hidden;
            this.stoping.Visibility = Visibility.Visible;
            if (this.ExperimentType == 1)
                start.Content = "开始回答";
            else if (this.ExperimentType == 2)
                start.Content = "开始朗读";
            else
                start.Content = "开始描述";
            buttontime = 1;

            timer.Stop();
            is_playinig = false;
            timeRecorder = 0;
            this.timeout.Visibility = Visibility.Hidden;
            this.start.IsEnabled = true;
        }


        public void setStartTime(int index, double StartTime)
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.setStartTime(index, StartTime);
        }

        public void setEndTime(int index, double EndTime)
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.setEndTime(index, EndTime);
        }

        private void NextPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.PreviousPage(PageNum);
        }

        public void setWavfile(String filename)
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.setWavfile(filename);
        }

        public void StartRecord()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.StartRecord();
        }

        public void StopRecord()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.StopRecord();
        }

        public void log()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;

            fs = new FileStream(win.getUserPath() + "\\log.txt", FileMode.Append);
            sw = new StreamWriter(fs);

            if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q11"))
                sw.Write("正性访谈问题1");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q12"))
                sw.Write("正性访谈问题2");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q13"))
                sw.Write("正性访谈问题3");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q21"))
                sw.Write("中性访谈问题1");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q22"))
                sw.Write("中性访谈问题2");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q23"))
                sw.Write("中性访谈问题3");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q31"))
                sw.Write("负性访谈问题1");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q32"))
                sw.Write("负性访谈问题2");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "q33"))
                sw.Write("负性访谈问题3");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "正性1"))
                sw.Write("正性朗读");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "中性1"))
                sw.Write("中性朗读");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "负性1"))
                sw.Write("负性朗读");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p11"))
                sw.Write("正性表情图片");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p21"))
                sw.Write("中性表情图片");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p31"))
                sw.Write("负性表情图片");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p12"))
                sw.Write("正性图片");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p22"))
                sw.Write("中性图片");
            else if (String.Equals(PicturePath.Split('\\')[2].Split('.')[0], "p32"))
                sw.Write("负性图片");


            sw.WriteLine();
            sw.Close();
            fs.Close();

        }

    }
}
