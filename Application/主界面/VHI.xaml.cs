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

namespace VHI
{
    /// <summary>
    /// VHI.xaml 的交互逻辑
    /// </summary>
    public partial class VHI : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "";
        public int PageNum;
        int Score = 0;

        public VHI(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (r11.IsChecked == false && r12.IsChecked == false && r13.IsChecked == false && r14.IsChecked == false && r15.IsChecked == false)
            { MessageBox.Show("问题1未回答"); return; }
            if (r21.IsChecked == false && r22.IsChecked == false && r23.IsChecked == false && r24.IsChecked == false && r25.IsChecked == false)
            { MessageBox.Show("问题2未回答"); return; }
            if (r31.IsChecked == false && r32.IsChecked == false && r33.IsChecked == false && r34.IsChecked == false && r35.IsChecked == false)
            { MessageBox.Show("问题3未回答"); return; }
            if (r41.IsChecked == false && r42.IsChecked == false && r43.IsChecked == false && r44.IsChecked == false && r45.IsChecked == false)
            { MessageBox.Show("问题4未回答"); return; }
            if (r51.IsChecked == false && r52.IsChecked == false && r53.IsChecked == false && r54.IsChecked == false && r55.IsChecked == false)
            { MessageBox.Show("问题5未回答"); return; }
            if (r61.IsChecked == false && r62.IsChecked == false && r63.IsChecked == false && r64.IsChecked == false && r65.IsChecked == false)
            { MessageBox.Show("问题6未回答"); return; }
            if (r71.IsChecked == false && r72.IsChecked == false && r73.IsChecked == false && r74.IsChecked == false && r75.IsChecked == false)
            { MessageBox.Show("问题7未回答"); return; }
            if (r81.IsChecked == false && r82.IsChecked == false && r83.IsChecked == false && r84.IsChecked == false && r85.IsChecked == false)
            { MessageBox.Show("问题8未回答"); return; }
            if (r91.IsChecked == false && r92.IsChecked == false && r93.IsChecked == false && r94.IsChecked == false && r95.IsChecked == false)
            { MessageBox.Show("问题9未回答"); return; }
            if (r101.IsChecked == false && r102.IsChecked == false && r103.IsChecked == false && r104.IsChecked == false && r105.IsChecked == false)
            { MessageBox.Show("问题10未回答"); return; }

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

            fs = new FileStream(filePath + "\\VHI.txt", FileMode.Create);
            sw = new StreamWriter(fs);

            if (r11.IsChecked == true)
            {
                sw.Write("1：0");
            }
            if (r12.IsChecked == true)
            {
                sw.Write("1：1");
                Score += 1;
            }
            if (r13.IsChecked == true)
            {
                sw.Write("1：2");
                Score += 2;
            }
            if (r14.IsChecked == true)
            {
                sw.Write("1：3");
                Score += 3;
            }
            if (r15.IsChecked == true)
            {
                sw.Write("1：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r21.IsChecked == true)
            {
                sw.Write("2：0");
            }
            if (r22.IsChecked == true)
            {
                sw.Write("2：1");
                Score += 1;
            }
            if (r23.IsChecked == true)
            {
                sw.Write("2：2");
                Score += 2;
            }
            if (r24.IsChecked == true)
            {
                sw.Write("2：3");
                Score += 3;
            }
            if (r25.IsChecked == true)
            {
                sw.Write("2：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r31.IsChecked == true)
            {
                sw.Write("3：0");
            }
            if (r32.IsChecked == true)
            {
                sw.Write("3：1");
                Score += 1;
            }
            if (r33.IsChecked == true)
            {
                sw.Write("3：2");
                Score += 2;
            }
            if (r34.IsChecked == true)
            {
                sw.Write("3：3");
                Score += 3;
            }
            if (r35.IsChecked == true)
            {
                sw.Write("3：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r41.IsChecked == true)
            {
                sw.Write("4：0");
            }
            if (r42.IsChecked == true)
            {
                sw.Write("4：1");
                Score += 1;
            }
            if (r43.IsChecked == true)
            {
                sw.Write("4：2");
                Score += 2;
            }
            if (r44.IsChecked == true)
            {
                sw.Write("4：3");
                Score += 3;
            }
            if (r45.IsChecked == true)
            {
                sw.Write("4：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r51.IsChecked == true)
            {
                sw.Write("5：0");
            }
            if (r52.IsChecked == true)
            {
                sw.Write("5：1");
                Score += 1;
            }
            if (r53.IsChecked == true)
            {
                sw.Write("5：2");
                Score += 2;
            }
            if (r54.IsChecked == true)
            {
                sw.Write("5：3");
                Score += 3;
            }
            if (r55.IsChecked == true)
            {
                sw.Write("5：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r61.IsChecked == true)
            {
                sw.Write("6：0");
            }
            if (r62.IsChecked == true)
            {
                sw.Write("6：1");
                Score += 1;
            }
            if (r63.IsChecked == true)
            {
                sw.Write("6：2");
                Score += 2;
            }
            if (r64.IsChecked == true)
            {
                sw.Write("6：3");
                Score += 3;
            }
            if (r65.IsChecked == true)
            {
                sw.Write("6：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r71.IsChecked == true)
            {
                sw.Write("7：0");
            }
            if (r72.IsChecked == true)
            {
                sw.Write("7：1");
                Score += 1;
            }
            if (r73.IsChecked == true)
            {
                sw.Write("7：2");
                Score += 2;
            }
            if (r74.IsChecked == true)
            {
                sw.Write("7：3");
                Score += 3;
            }
            if (r75.IsChecked == true)
            {
                sw.Write("7：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r81.IsChecked == true)
            {
                sw.Write("8：0");
            }
            if (r82.IsChecked == true)
            {
                sw.Write("8：1");
                Score += 1;
            }
            if (r83.IsChecked == true)
            {
                sw.Write("8：2");
                Score += 2;
            }
            if (r84.IsChecked == true)
            {
                sw.Write("8：3");
                Score += 3;
            }
            if (r85.IsChecked == true)
            {
                sw.Write("8：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r91.IsChecked == true)
            {
                sw.Write("9：0");
            }
            if (r92.IsChecked == true)
            {
                sw.Write("9：1");
                Score += 1;
            }
            if (r93.IsChecked == true)
            {
                sw.Write("9：2");
                Score += 2;
            }
            if (r94.IsChecked == true)
            {
                sw.Write("9：3");
                Score += 3;
            }
            if (r95.IsChecked == true)
            {
                sw.Write("9：4");
                Score += 4;
            }
            sw.WriteLine();

            if (r101.IsChecked == true)
            {
                sw.Write("10：0");
            }
            if (r102.IsChecked == true)
            {
                sw.Write("10：1");
                Score += 1;
            }
            if (r103.IsChecked == true)
            {
                sw.Write("10：2");
                Score += 2;
            }
            if (r104.IsChecked == true)
            {
                sw.Write("10：3");
                Score += 3;
            }
            if (r105.IsChecked == true)
            {
                sw.Write("10：4");
                Score += 4;
            }
            sw.WriteLine();

            sw.Write("总分：" + Score);
            sw.WriteLine();

            sw.Close();
            fs.Close();

        }

    }
}
