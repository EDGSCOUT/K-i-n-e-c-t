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
        public bool isNewId = true;

        public Login(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;

            if (File.Exists("e:\\文本\\NextId.txt"))
            {
                FileStream fsr = new FileStream("e:\\文本\\NextId.txt", FileMode.Open, FileAccess.Read);
                StreamReader mysr = new StreamReader(fsr);

                try
                {
                    this.textbox.Text = mysr.ReadLine();
                }
                catch (Exception E)
                {
                    return;
                }

                mysr.Close();
                fsr.Close();
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                this.UserId = Convert.ToInt32(this.textbox.Text);

                if (File.Exists("e:\\文本\\ID.txt"))
                {
                    FileStream fsr = new FileStream("e:\\文本\\ID.txt", FileMode.Open, FileAccess.Read);
                    StreamReader mysr = new StreamReader(fsr);

                    string line;

                    while ((line = mysr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        if (Convert.ToInt32(line) == this.UserId)
                        {
                            string message = "该编号进行过实验，开始实验可能会覆盖之前的数据，确定继续吗?";
                            string title = "提示";
                            MessageBoxButton button = MessageBoxButton.OKCancel;
                            MessageBoxImage img = MessageBoxImage.Question;
                            MessageBoxResult result = MessageBox.Show(message, title, button, img);
                            if (result == MessageBoxResult.OK)
                            {
                                isNewId = false;
                                break;
                            }
                            else if (result == MessageBoxResult.Cancel)
                            {
                                mysr.Close();
                                fsr.Close();
                                return;
                            }
                        }
                    }

                    mysr.Close();
                    fsr.Close();

                }

            }
            catch (Exception E)
            {
                MessageBox.Show("请正确输入编号");
                return;
            }

            this.setUserId(this.UserId);

            if (isNewId)
            {
                if (!Directory.Exists("e:\\文本"))
                {
                    Directory.CreateDirectory("e:\\文本");
                }

                fs = new FileStream("e:\\文本\\ID.txt", FileMode.Append);
                sw = new StreamWriter(fs);
                sw.Write(this.UserId);
                sw.WriteLine();
                sw.Close();
                fs.Close();
            }


            fs = new FileStream("e:\\文本\\NextId.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            sw.Write(this.UserId+1);
            sw.WriteLine();
            sw.Close();
            fs.Close();

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
