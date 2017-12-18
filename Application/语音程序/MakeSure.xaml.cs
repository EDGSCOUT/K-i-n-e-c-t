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

namespace MakeSure
{
    /// <summary>
    /// MakeSure.xaml 的交互逻辑
    /// </summary>
    public partial class MakeSure : UserControl
    {
        public int PageNum;
        public int VideoIndex;
        public bool VideoOK;
        FileStream fs = null;
        StreamWriter sw = null;

        public MakeSure(int PageNum, int VideoIndex)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.VideoIndex = VideoIndex;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cb1.IsChecked == false && cb2.IsChecked == false)
            {
                MessageBox.Show("请选择记录是否顺利");
                return;
            }
            if (cb1.IsChecked == true)
            {
                VideoOK = true;
            }
            if (cb2.IsChecked == true)
            {
                VideoOK = false;
            }

            setOK();
            this.log();
            this.NextPage();
        }

        private void setOK()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            switch (VideoIndex)
            {
                case 1: win.setOK1(VideoOK); break;
                case 2: win.setOK2(VideoOK); break;
                case 3: win.setOK3(VideoOK); break;
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            VideoOK = true;
            cb2.IsChecked = false;
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            VideoOK = false;
            cb1.IsChecked = false;
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

        public void log()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            fs = new FileStream(win.getUserPath() + "\\log.txt", FileMode.Append);
            sw = new StreamWriter(fs);
            if(this.VideoOK)
                sw.Write("正常");
            else
                sw.Write("异常");

            sw.WriteLine();
            sw.Close();
            fs.Close();
        }
    }
}
