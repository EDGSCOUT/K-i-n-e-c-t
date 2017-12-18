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

namespace ShowFace
{
    /// <summary>
    /// ShowFace.xaml 的交互逻辑
    /// </summary>
    public partial class ShowFace : UserControl
    {
        public int PageNum;
        

        public ShowFace(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.button.IsEnabled = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.SetCanShowFace(false);
            this.NextPage();
        }

        public void SensorOpen()
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.SensorOpen();
        }

        public void SetCanShowFace(bool CanShowFace)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.SetCanShowFace(CanShowFace);
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

        public void SetPage(int PageNum)
        {
            this.PageNum = PageNum;
        }
    }
}
