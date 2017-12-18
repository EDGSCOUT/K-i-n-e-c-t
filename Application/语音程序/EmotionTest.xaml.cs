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

namespace EmotionTest
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
        public int Type;
        public int Time;
        private int EmotionType;
        private bool first;
        public EmotionTest(int PageNum, int type, int time, string FilePath, int EmotionType, bool first)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.filePath = FilePath;
            this.Type = type;
            this.Time = time;
            this.EmotionType = EmotionType;
            this.first = first;

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

            if (this.first)
            {
                if (this.Type == 1)
                {
                    if (this.EmotionType == 1)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈正性.txt", FileMode.Create);
                    }
                    else if (this.EmotionType == 2)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈中性.txt", FileMode.Create);
                    }
                    else if (this.EmotionType == 3)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈负性.txt", FileMode.Create);
                    }
                }
                else if (this.Type == 2)
                {
                    if (this.EmotionType == 1)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读正性.txt", FileMode.Create);
                    }
                    else if (this.EmotionType == 2)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读中性.txt", FileMode.Create);
                    }
                    else if (this.EmotionType == 3)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读负性.txt", FileMode.Create);
                    }
                }
                else if (this.Type == 3)
                {
                    if (this.Time == 1 || this.Time == 2 || this.Time == 3)
                    {
                        if (this.EmotionType == 1)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片正性.txt", FileMode.Create);
                        }
                        else if (this.EmotionType == 2)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片中性.txt", FileMode.Create);
                        }
                        else if (this.EmotionType == 3)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片负性.txt", FileMode.Create);
                        }
                    }
                    else if (this.Time == 4 || this.Time == 5 || this.Time == 6)
                    {
                        if (this.EmotionType == 1)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片正性.txt", FileMode.Create);
                        }
                        else if (this.EmotionType == 2)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片中性.txt", FileMode.Create);
                        }
                        else if (this.EmotionType == 3)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片负性.txt", FileMode.Create);
                        }
                    }
                }
            }
            else
            {
                if (this.Type == 1)
                {
                    if (this.EmotionType == 1)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈正性.txt", FileMode.Append);
                    }
                    else if (this.EmotionType == 2)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈中性.txt", FileMode.Append);
                    }
                    else if (this.EmotionType == 3)
                    {
                        fs = new FileStream(filePath + "\\Emotion-访谈负性.txt", FileMode.Append);
                    }
                }
                else if (this.Type == 2)
                {
                    if (this.EmotionType == 1)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读正性.txt", FileMode.Append);
                    }
                    else if (this.EmotionType == 2)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读中性.txt", FileMode.Append);
                    }
                    else if (this.EmotionType == 3)
                    {
                        fs = new FileStream(filePath + "\\Emotion-文本朗读负性.txt", FileMode.Append);
                    }
                }
                else if (this.Type == 3)
                {
                    if (this.Time == 1 || this.Time == 2 || this.Time == 3)
                    {
                        if (this.EmotionType == 1)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片正性.txt", FileMode.Append);
                        }
                        else if (this.EmotionType == 2)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片中性.txt", FileMode.Append);
                        }
                        else if (this.EmotionType == 3)
                        {
                            fs = new FileStream(filePath + "\\Emotion-面部图片负性.txt", FileMode.Append);
                        }
                    }
                    else if (this.Time == 4 || this.Time == 5 || this.Time == 6)
                    {
                        if (this.EmotionType == 1)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片正性.txt", FileMode.Append);
                        }
                        else if (this.EmotionType == 2)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片中性.txt", FileMode.Append);
                        }
                        else if (this.EmotionType == 3)
                        {
                            fs = new FileStream(filePath + "\\Emotion-图片负性.txt", FileMode.Append);
                        }
                    }
                }
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
