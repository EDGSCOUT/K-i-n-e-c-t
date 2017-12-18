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

namespace Login
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        private int UserId = 0;
        string filePath = "e:\\FaceData\\";
        public int PageNum;

        public Login(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UserId = Convert.ToInt32(this.textbox.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("请正确输入编号");
                return;
            }

            this.setUserId(this.UserId);
            this.NextPage();
        }

        public void setUserId(int UserId)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.setUserId(UserId);
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Kinect2FaceHD_NET.MainWindow win = Window.GetWindow(this) as Kinect2FaceHD_NET.MainWindow;
            win.ShutDownTopwindow = false;
            win.Close();
        }

    }
}
