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

namespace Log
{
    /// <summary>
    /// Log.xaml 的交互逻辑
    /// </summary>
    public partial class Log : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";
        public int PageNum;
        
        public Log(int PageNum,int userid)
        {
            InitializeComponent();
            this.PageNum = PageNum;

            if (File.Exists("e:\\voicedata\\" + userid + "\\PHQ9.txt"))
            {
                try
                {
                    StreamReader mysr;
                    String[] line = new String[11];

                    mysr = new StreamReader("e:\\voicedata\\" + userid + "\\PHQ9.txt", System.Text.Encoding.UTF8);

                    for (int i = 1; i <= 10; i++)
                    {
                        line[i] = mysr.ReadLine();
                    }

                    tb10.Text = line[10].ToString().Split('：')[1];
                }
                catch (Exception e)
                {

                }
            }

            if (File.Exists("e:\\voicedata\\" + userid + "\\BDI.txt"))
            {   
                try
                {
                    StreamReader mysr;
                    String[] line = new String[23];

                    mysr = new StreamReader("e:\\voicedata\\" + userid + "\\BDI.txt", System.Text.Encoding.UTF8);

                    for (int i = 1; i <= 22; i++)
                    {
                        line[i] = mysr.ReadLine();
                    }

                    tb8.Text = line[22].ToString().Split('：')[1];
                }
                catch (Exception e)
                {

                }
            }

            if (File.Exists("e:\\voicedata\\" + userid + "\\VHI.txt"))
            {
                try
                {
                    StreamReader mysr;
                    String[] line = new String[12];

                    mysr = new StreamReader("e:\\voicedata\\" + userid + "\\VHI.txt", System.Text.Encoding.UTF8);

                    for (int i = 1; i <= 11; i++)
                    {
                        line[i] = mysr.ReadLine();
                    }

                    tb9.Text = line[11].ToString().Split('：')[1];
                }
                catch (Exception e)
                {

                }
            }

            tb1.Text = userid.ToString();

            int usertype = (userid % 1000) % 6;
            string video1 = "", video2 = "", video3 = "";
            switch (usertype)
            {
                case 0: video1 = "负"; video2 = "中"; video3 = "正"; break;
                case 1: video1 = "正"; video2 = "中"; video3 = "负"; break;
                case 2: video1 = "正"; video2 = "负"; video3 = "中"; break;
                case 3: video1 = "中"; video2 = "负"; video3 = "正"; break;
                case 4: video1 = "中"; video2 = "正"; video3 = "负"; break;
                case 5: video1 = "负"; video2 = "正"; video3 = "中"; break;
            }
            tb17.Text = "" + video1 + video2 + video3;
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text == "")
            { MessageBox.Show("语音组编号未填写"); return; }
            if (tb2.Text == "")
            { MessageBox.Show("总编号未填写"); return; }
            if (tb3.Text == "")
            { MessageBox.Show("姓名未填写"); return; }
            if (tb4.Text == "")
            { MessageBox.Show("被试类型未填写"); return; }
            if (tb5.Text == "")
            { MessageBox.Show("年龄未填写"); return; }
            if (tb6.Text == "")
            { MessageBox.Show("学历未填写"); return; }
            if (tb7.Text == "")
            { MessageBox.Show("职业未填写"); return; }
            if (tb8.Text == "")
            { MessageBox.Show("BDI分数未填写"); return; }
            if (tb9.Text == "")
            { MessageBox.Show("VHI-10分数未填写"); return; }
            if (tb10.Text == "")
            { MessageBox.Show("PHQ-9分数未填写"); return; }
            if (tb11.Text == "" || tb12.Text == "" || tb13.Text == "")
            { MessageBox.Show("实验日期未填写"); return; }
            if (tb14.Text == "")
            { MessageBox.Show("开始时间未填写"); return; }
            if (tb15.Text == "")
            { MessageBox.Show("持续时间未填写"); return; }
            if (tb16.Text == "")
            { MessageBox.Show("环境噪声未填写"); return; }
            if (tb17.Text == "")
            { MessageBox.Show("刺激顺序未填写"); return; }
            if (tb18.Text == "")
            { MessageBox.Show("数据完整性未填写"); return; }
            if (tb19.Text == "")
            { MessageBox.Show("服药未填写"); return; }
            if (tb20.Text == "")
            { MessageBox.Show("配合程度未填写"); return; }
            if (tb21.Text == "")
            { MessageBox.Show("主试突发状况未填写"); return; }
            if (tb22.Text == "")
            { MessageBox.Show("主试姓名未填写"); return; }
            if (tb23.Text == "")
            { MessageBox.Show("实验地点未填写"); return; }
            if (tb24.Text == "")
            { MessageBox.Show("备注未填写"); return; }
            if (tb25.Text == "")
            { MessageBox.Show("重录的语音段未填写"); return; }
            if (tb26.Text == "")
            { MessageBox.Show("丢失的视频段未填写"); return; }

            if (rb11.IsChecked == false && rb12.IsChecked == false && rb13.IsChecked == false && rb14.IsChecked == false)
            { MessageBox.Show("被试特殊情况未选择"); return; }
            if (rb21.IsChecked == false && rb22.IsChecked == false && rb23.IsChecked == false && rb24.IsChecked == false && rb25.IsChecked == false)
            { MessageBox.Show("被试突发情况未选择"); return; }
            if (rb41.IsChecked == false && rb42.IsChecked == false)
            { MessageBox.Show("性别未选择"); return; }
            if (rb51.IsChecked == false && rb52.IsChecked == false)
            { MessageBox.Show("右利手未选择"); return; }
            if (rb61.IsChecked == false && rb62.IsChecked == false)
            { MessageBox.Show("是否首次发作未选择"); return; }
            if (rb71.IsChecked == false && rb72.IsChecked == false)
            { MessageBox.Show("是否目前住院未选择"); return; }

            save();

            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            win.SetCurrentPage(2);
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

        private void save()
        {
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            filePath = win.getUserPath();

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            fs = new FileStream(filePath + "\\实验记录表.txt", FileMode.Create);
            sw = new StreamWriter(fs);

            sw.Write("语音组编号：" + tb1.Text);
            sw.WriteLine();

            sw.Write("总编号：" + tb2.Text);
            sw.WriteLine();

            sw.Write("姓名：" + tb3.Text);
            sw.WriteLine();

            sw.Write("被试类型：" + tb4.Text);
            sw.WriteLine();

            if (rb41.IsChecked == true)
            {
                sw.Write("性别：男");
                sw.WriteLine();
            }
            if (rb42.IsChecked == true)
            {
                sw.Write("性别：女");
                sw.WriteLine();
            }

            
            sw.Write("年龄：" + tb5.Text);
            sw.WriteLine();

            sw.Write("学历：" + tb6.Text);
            sw.WriteLine();

            sw.Write("职业：" + tb7.Text);
            sw.WriteLine();

            sw.Write("BDI分数：" + tb8.Text);
            sw.WriteLine();

            sw.Write("VHI分数：" + tb9.Text);
            sw.WriteLine();

            sw.Write("PHQ-9分数：" + tb10.Text);
            sw.WriteLine();

            sw.Write("实验日期：" + tb11.Text);
            sw.Write("年" + tb12.Text);
            sw.Write("月" + tb13.Text + "日");
            sw.WriteLine();

            sw.Write("开始时间：" + tb14.Text);
            sw.WriteLine();

            sw.Write("持续时间：" + tb15.Text);
            sw.WriteLine();

            sw.Write("环境噪声：" + tb16.Text + "dB");
            sw.WriteLine();

            sw.Write("刺激顺序：" + tb17.Text);
            sw.WriteLine();

            sw.Write("数据完整性：" + tb18.Text);
            sw.WriteLine();

            sw.Write("近两周服药情况：" + tb19.Text);
            sw.WriteLine();

            sw.Write("配合程度：" + tb20.Text);
            sw.WriteLine();

            if (rb51.IsChecked == true)
            {
                sw.Write("右利手：是");
                sw.WriteLine();
            }
            if (rb52.IsChecked == true)
            {
                sw.Write("右利手：否");
                sw.WriteLine();
            }

            sw.Write("重录的语音段：" + tb25.Text);
            sw.WriteLine();

            sw.Write("丢失的视频段：" + tb26.Text);
            sw.WriteLine();

            if (rb11.IsChecked == true)
            {
                sw.Write("被试特殊情况：A");
            }
            if (rb12.IsChecked == true)
            {
                sw.Write("被试特殊情况：B");
            }
            if (rb13.IsChecked == true)
            {
                sw.Write("被试特殊情况：C");
            }
            if (rb14.IsChecked == true)
            {
                sw.Write("被试特殊情况：D");
            }

            sw.WriteLine();


            if (rb21.IsChecked == true)
            {
                sw.Write("被试突发情况：A");
            }
            if (rb22.IsChecked == true)
            {
                sw.Write("被试突发情况：B");
            }
            if (rb23.IsChecked == true)
            {
                sw.Write("被试突发情况：C");
            }
            if (rb24.IsChecked == true)
            {
                sw.Write("被试突发情况：D");
            }
            if (rb25.IsChecked == true)
            {
                sw.Write("被试突发情况：E");
            }

            sw.WriteLine();

            sw.Write("主试突发状况：" + tb21.Text);
            sw.WriteLine();

            sw.Write("主试姓名：" + tb22.Text);
            sw.WriteLine();

            sw.Write("实验地点：" + tb23.Text);
            sw.WriteLine();

            sw.Write("备注：" + tb24.Text);
            sw.WriteLine();

            if (rb61.IsChecked == true)
            {
                sw.Write("是否首次发作：是");
                sw.WriteLine();
            }
            if (rb62.IsChecked == true)
            {
                sw.Write("是否首次发作：否");
                sw.WriteLine();
            }

            if (rb71.IsChecked == true)
            {
                sw.Write("是否目前住院：是");
                sw.WriteLine();
            }
            if (rb72.IsChecked == true)
            {
                sw.Write("是否目前住院：否");
                sw.WriteLine();
            }

            sw.Close();
            fs.Close();

        }      

    }
}
