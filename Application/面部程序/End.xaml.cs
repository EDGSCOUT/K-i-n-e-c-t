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

namespace End2
{
    /// <summary>
    /// End.xaml 的交互逻辑
    /// </summary>
    public partial class End : UserControl
    {
        public int PageNum;
        MediaPlayer player = new MediaPlayer();

        public End(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.picture.Source = new BitmapImage(new Uri("E:\\文本\\VideoEnd.png", UriKind.Absolute));

            this.Back.IsEnabled = false;
            this.Next.IsEnabled = false;

            player.Open(new Uri("e:\\文本\\VideoEnd.wav", UriKind.Absolute));
            player.Play();
            player.MediaEnded += new EventHandler(MediaEnded);
        }

        void MediaEnded(object sender, EventArgs e)
        {
            this.Back.IsEnabled = true;
            this.Next.IsEnabled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.ShutDownTopwindow = false;
            win.Close();
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

        private void Next_Click_1(object sender, RoutedEventArgs e)
        {
            this.NextPage();
        }
    }
}
