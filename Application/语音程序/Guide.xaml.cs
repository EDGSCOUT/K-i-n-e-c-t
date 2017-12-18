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

namespace Guide
{
    /// <summary>
    /// GuideInterview.xaml 的交互逻辑
    /// </summary>
    public partial class Guide : UserControl
    {

        public int PageNum;
        MediaPlayer player = new MediaPlayer();

        public Guide(int PageNum,int type)
        {
            InitializeComponent();
            this.PageNum = PageNum;

            this.Submit.IsEnabled = false;
            
            switch (type)
            {
                case 1: picture.Source = new BitmapImage(new Uri("E:\\文本\\GuideInterview.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\GuideInterview.wav", UriKind.Absolute)); break;
                case 2: picture.Source = new BitmapImage(new Uri("E:\\文本\\GuideRead.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\GuideRead.wav", UriKind.Absolute)); break;
                case 3: picture.Source = new BitmapImage(new Uri("E:\\文本\\GuidePicture1.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\GuidePicture1.wav", UriKind.Absolute)); break;
                case 4: picture.Source = new BitmapImage(new Uri("E:\\文本\\GuidePicture2.png", UriKind.Absolute)); player.Open(new Uri("e:\\文本\\GuidePicture2.wav", UriKind.Absolute)); break;
            }

            player.Play();
            player.MediaEnded += new EventHandler(MediaEnded);
        }

        void MediaEnded(object sender, EventArgs e)
        {
            this.Submit.IsEnabled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NextPage();
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

    }
}
