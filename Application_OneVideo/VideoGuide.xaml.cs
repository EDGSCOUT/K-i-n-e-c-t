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
                                  
namespace VideoGuide2
{
    /// <summary>
    /// VideoGuide.xaml 的交互逻辑
    /// </summary>
    public partial class VideoGuide : UserControl
    {
        public int PageNum;
        MediaPlayer player = new MediaPlayer();

        public VideoGuide(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.picture.Source = new BitmapImage(new Uri("F:\\ad\\文本\\onevideoguide.png", UriKind.Absolute));

            //this.Submit.IsEnabled = false;

            //player.Open(new Uri("F:\\ad\\文本\\VideoGuide.wav", UriKind.Absolute));
            //player.Play();
            //player.MediaEnded += new EventHandler(MediaEnded);
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
