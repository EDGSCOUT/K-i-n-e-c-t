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

namespace EmotionTest2
{
    /// <summary>
    /// EmotionTest.xaml 的交互逻辑
    /// </summary>
    public partial class EmotionTest : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "";
        public int PageNum;
        string EmotionType;
        bool First;

        public EmotionTest(int PageNum, string FilePath, string EmotionType,bool first)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.filePath = FilePath;
            this.EmotionType = EmotionType;
            this.First = first;

            this.emotion1.Source = new BitmapImage(new Uri("E:\\文本\\emotion1.png", UriKind.Absolute));
            this.emotion2.Source = new BitmapImage(new Uri("E:\\文本\\emotion2.png", UriKind.Absolute));
            this.emotion3.Source = new BitmapImage(new Uri("E:\\文本\\emotion3.png", UriKind.Absolute));

            this.p11.Source = new BitmapImage(new Uri("E:\\文本\\e11.png", UriKind.Absolute));
            this.p12.Source = new BitmapImage(new Uri("E:\\文本\\e12.png", UriKind.Absolute));
            this.p13.Source = new BitmapImage(new Uri("E:\\文本\\e13.png", UriKind.Absolute));
            this.p14.Source = new BitmapImage(new Uri("E:\\文本\\e14.png", UriKind.Absolute));
            this.p15.Source = new BitmapImage(new Uri("E:\\文本\\e15.png", UriKind.Absolute));
            this.p16.Source = new BitmapImage(new Uri("E:\\文本\\e16.png", UriKind.Absolute));
            this.p17.Source = new BitmapImage(new Uri("E:\\文本\\e17.png", UriKind.Absolute));

            this.p21.Source = new BitmapImage(new Uri("E:\\文本\\e21.png", UriKind.Absolute));
            this.p22.Source = new BitmapImage(new Uri("E:\\文本\\e22.png", UriKind.Absolute));
            this.p23.Source = new BitmapImage(new Uri("E:\\文本\\e23.png", UriKind.Absolute));
            this.p24.Source = new BitmapImage(new Uri("E:\\文本\\e24.png", UriKind.Absolute));
            this.p25.Source = new BitmapImage(new Uri("E:\\文本\\e25.png", UriKind.Absolute));
            this.p26.Source = new BitmapImage(new Uri("E:\\文本\\e26.png", UriKind.Absolute));
            this.p27.Source = new BitmapImage(new Uri("E:\\文本\\e27.png", UriKind.Absolute));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (rb11.IsChecked == false && rb12.IsChecked == false && rb13.IsChecked == false && rb14.IsChecked == false && rb15.IsChecked == false && rb16.IsChecked == false && rb17.IsChecked == false)
            { MessageBox.Show("请选择您现在的心情"); return; }

            if (rb21.IsChecked == false && rb22.IsChecked == false && rb23.IsChecked == false && rb24.IsChecked == false && rb25.IsChecked == false && rb26.IsChecked == false && rb27.IsChecked == false)
            { MessageBox.Show("请选择您心情的强烈程度"); return; }

            // 创建目录
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (this.First)
            {
                if (String.Equals(this.EmotionType, "happy"))
                    fs = new FileStream(filePath + "\\Emotion-视频正性.txt", FileMode.Create);
                else if (String.Equals(this.EmotionType, "neutrality"))
                    fs = new FileStream(filePath + "\\Emotion-视频中性.txt", FileMode.Create);
                else if (String.Equals(this.EmotionType, "sad"))
                    fs = new FileStream(filePath + "\\Emotion-视频负性.txt", FileMode.Create);
            }
            else 
            {
                if (String.Equals(this.EmotionType, "happy"))
                    fs = new FileStream(filePath + "\\Emotion-视频正性.txt", FileMode.Append);
                else if (String.Equals(this.EmotionType, "neutrality"))
                    fs = new FileStream(filePath + "\\Emotion-视频中性.txt", FileMode.Append);
                else if (String.Equals(this.EmotionType, "sad"))
                    fs = new FileStream(filePath + "\\Emotion-视频负性.txt", FileMode.Append);
            }
            

            
            sw = new StreamWriter(fs);
            
            if (rb11.IsChecked == true)
              sw.Write("愉悦度：1");
            if (rb12.IsChecked == true)
                sw.Write("愉悦度：2");
            if (rb13.IsChecked == true)
                sw.Write("愉悦度：3");
            if (rb14.IsChecked == true)
                sw.Write("愉悦度：4");
            if (rb15.IsChecked == true)
                sw.Write("愉悦度：5");
            if (rb16.IsChecked == true)
                sw.Write("愉悦度：6");
            if (rb17.IsChecked == true)
                sw.Write("愉悦度：7");
            sw.WriteLine();


            if (rb21.IsChecked == true)
                sw.Write("唤醒度：1");
            if (rb22.IsChecked == true)
                sw.Write("唤醒度：2");
            if (rb23.IsChecked == true)
                sw.Write("唤醒度：3");
            if (rb24.IsChecked == true)
                sw.Write("唤醒度：4");
            if (rb25.IsChecked == true)
                sw.Write("唤醒度：5");
            if (rb26.IsChecked == true)
                sw.Write("唤醒度：6");
            if (rb27.IsChecked == true)
                sw.Write("唤醒度：7");
            sw.WriteLine();

            sw.Close();
            fs.Close();

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
