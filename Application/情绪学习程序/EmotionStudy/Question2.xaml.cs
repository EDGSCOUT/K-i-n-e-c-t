using System;
using System.Collections.Generic;
using System.IO;
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


namespace Question2
{
    /// <summary>
    /// Question1.xaml 的交互逻辑
    /// </summary>
    public partial class Question2 : UserControl
    {
        public int PageNum;
        private int buttontime;
        private String wavfile = "C:\\Users\\ad\\Desktop\\new.wav";
        MediaPlayer player = new MediaPlayer();
        MediaPlayer player2 = new MediaPlayer();

        public Question2(int PageNum)
        {
            InitializeComponent();

            this.start.IsEnabled = false;
            this.Back.IsEnabled = false;
            this.start.Content = "结束回答";

            this.PageNum = PageNum;
            picture.Source = new BitmapImage(new Uri("E:\\文本\\Question2.png", UriKind.Absolute));
            buttontime = 1;
            this.recording.Visibility = Visibility.Hidden;

            this.timeout.Visibility = Visibility.Hidden;
            player.Open(new Uri("e:\\文本\\Question2.wav", UriKind.Absolute));
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

        void MediaEnded2(object sender, EventArgs e)
        {
            player2.Stop();

            this.recording.Visibility = Visibility.Visible;
            this.stoping.Visibility = Visibility.Hidden;

            this.start.IsEnabled = true;
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                buttontime = 2;
                start.Content = "继续";
                this.recording.Visibility = Visibility.Hidden;
                this.stoping.Visibility = Visibility.Visible;
                this.Back.IsEnabled = true;
            }
            else
            {
                EmotionStudy.MainWindow win = Window.GetWindow(this) as EmotionStudy.MainWindow;
                win.ShutDownTopwindow = false;
                win.OpenVideowindow = true;
                win.Close();
            }
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            EmotionStudy.MainWindow win = Window.GetWindow(this) as EmotionStudy.MainWindow;
            win.ShutDownTopwindow = false;
            win.Close();
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

        
    }
}
