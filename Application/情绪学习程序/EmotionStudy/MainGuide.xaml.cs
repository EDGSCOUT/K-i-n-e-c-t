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

namespace MainGuide
{
    /// <summary>
    /// MainGuide.xaml 的交互逻辑
    /// </summary>
    public partial class MainGuide : UserControl
    {
        MediaPlayer player = new MediaPlayer();
        int buttontime = 1;
        public int PageNum;

        public MainGuide(int PageNum)
        {
            InitializeComponent();

            this.PageNum = PageNum;
            this.Back.IsEnabled = false;
            picture.Source = new BitmapImage(new Uri("E:\\文本\\mainguide.png", UriKind.Absolute));

            player.Open(new Uri("e:\\文本\\总引导语.wav", UriKind.Relative));
            player.Play();
            player.MediaEnded += new EventHandler(MediaEnded);
        }

        void MediaEnded(object sender, EventArgs e)
        {
            player.Stop();
            buttontime = 2;
            this.Submit.Content = "继续";
            this.Back.IsEnabled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (buttontime == 1)
            {
                player.Stop();
                buttontime = 2;
                this.Submit.Content = "继续";
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
