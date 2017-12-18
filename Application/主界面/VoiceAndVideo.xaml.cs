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

namespace TopWindow
{
    /// <summary>
    /// Voice.xaml 的交互逻辑
    /// </summary>
    public partial class VoiceAndVideo : UserControl
    {
        public int PageNum;

        public VoiceAndVideo(int PageNum, int UserId)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.label.Content = "被试编号：" + UserId;
        }

        private void NextPage()
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.PreviousPage(PageNum);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.EmotionStudy();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.Face();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.Voice(1);
        }
        
        private void button4_Click_1(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.Voice(2);
        }

        private void button5_Click_1(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.Voice(3);
        }

        private void button6_Click_1(object sender, RoutedEventArgs e)
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.Voice(4);
        }
        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            this.PreviousPage();
        }

        

    }
}
