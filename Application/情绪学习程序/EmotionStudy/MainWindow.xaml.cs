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
using System.IO;
using System.Windows.Threading;

namespace EmotionStudy
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ShutDownTopwindow = true;
        public bool OpenVideowindow = false;
        public int UserId = 0;
        public int CurrentPage = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new Video.Video(CurrentPage);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ShutDownTopwindow)
                this.Owner.Close();
            else
            {
                if (OpenVideowindow == true)
                {
                    Kinect2FaceHD_NET.MainWindow w2 = new Kinect2FaceHD_NET.MainWindow();
                    Application.Current.MainWindow = w2;
                    w2.WindowStartupLocation = WindowStartupLocation.Manual;

                    w2.setUserId(this.UserId);
                    w2.Left = this.Left;
                    w2.Top = this.Top;
                    w2.Width = this.Width;
                    w2.Height = this.Height;
                    w2.WindowState = this.WindowState;
                    w2.Owner = this.Owner;
                    w2.Show();
                }
                else 
                {
                    this.Owner.Left = this.Left;
                    this.Owner.Top = this.Top;
                    this.Owner.Width = this.Width;
                    this.Owner.Height = this.Height;
                    this.Owner.WindowState = this.WindowState;
                    this.Owner.Show();
                }
                
            }
        }

        public void setUserId(int UserId)
        {
            this.UserId = UserId;
        }

        public void NextPage(int num)
        {
            SetCurrentPage(num + 1);

        }

        public void PreviousPage(int num)
        {
            if (num > 0)
            {
                SetCurrentPage(num - 1);
            }
        }

        public void SetCurrentPage(int num)
        {
            switch (num)
            {
                case 1: CurrentPage = num; ContentControl.Content = new Video.Video(CurrentPage);break;
                case 2: CurrentPage = num; ContentControl.Content = new MainGuide.MainGuide(CurrentPage); break;
                
            }
        }

    }
}
