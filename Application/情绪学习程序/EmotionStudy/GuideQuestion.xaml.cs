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

namespace GuideQuestion
{
    /// <summary>
    /// GuideQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class GuideQuestion : UserControl
    {
        MediaPlayer player = new MediaPlayer();
        public int PageNum;

        public GuideQuestion(int PageNum)
        {
            InitializeComponent();
            this.Submit.IsEnabled = false;
            this.PageNum = PageNum;

            picture.Source = new BitmapImage(new Uri("E:\\文本\\GuideQuestion.png", UriKind.Absolute));

            player.Open(new Uri("e:\\文本\\GuideQuestion.wav", UriKind.Relative));
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
