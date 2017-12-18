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

namespace AllEnd
{
    /// <summary>
    /// End.xaml 的交互逻辑
    /// </summary>
    public partial class AllEnd : UserControl
    {
        public int PageNum;
        MediaPlayer player = new MediaPlayer();

        public AllEnd(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.picture.Source = new BitmapImage(new Uri("E:\\文本\\GuideEnd.png", UriKind.Absolute));

            this.Submit.IsEnabled = false;

            player.Open(new Uri("e:\\文本\\GuideEnd.wav", UriKind.Absolute));
            player.Play();
            player.MediaEnded += new EventHandler(MediaEnded);
        }

        void MediaEnded(object sender, EventArgs e)
        {
            this.Submit.IsEnabled = true;
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

    }
}