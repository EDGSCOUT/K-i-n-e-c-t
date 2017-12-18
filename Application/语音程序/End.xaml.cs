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

namespace End
{
    /// <summary>
    /// End.xaml 的交互逻辑
    /// </summary>
    public partial class End : UserControl
    {
        public int PageNum;
        MediaPlayer player = new MediaPlayer();

        public End(int PageNum,int type)
        {
            InitializeComponent();
            this.PageNum = PageNum;

            this.Submit.IsEnabled = false;
            this.Submit2.IsEnabled = false;

            switch (type)
            {
                case 1: picture.Source = new BitmapImage(new Uri("E:\\文本\\InterviewEnd.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\InterviewEnd.wav", UriKind.Absolute)); break;
                case 2: picture.Source = new BitmapImage(new Uri("E:\\文本\\ReadEnd.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\ReadEnd.wav", UriKind.Absolute)); break;
                case 3: picture.Source = new BitmapImage(new Uri("E:\\文本\\PictureEnd.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\PictureEnd.wav", UriKind.Absolute)); break;
             }

            player.Play();
            player.MediaEnded += new EventHandler(MediaEnded);
        }

        void MediaEnded(object sender, EventArgs e)
        {
            this.Submit.IsEnabled = true;
            this.Submit2.IsEnabled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.ShutDownTopwindow = false;
            win.Close();
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

        private void Submit2_Click_1(object sender, RoutedEventArgs e)
        {
            this.NextPage();
        }
    }
}
