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

namespace BDI
{
    /// <summary>
    /// BDI.xaml 的交互逻辑
    /// </summary>
    public partial class BDI : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "";
        int Score = 0;
        public int PageNum;

        public BDI(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (rb11.IsChecked == false && rb12.IsChecked == false && rb13.IsChecked == false && rb14.IsChecked == false)
            { MessageBox.Show("问题1未回答"); return; }
            if (rb21.IsChecked == false && rb22.IsChecked == false && rb23.IsChecked == false && rb24.IsChecked == false)
            { MessageBox.Show("问题2未回答"); return; }
            if (rb31.IsChecked == false && rb32.IsChecked == false && rb33.IsChecked == false && rb34.IsChecked == false)
            { MessageBox.Show("问题3未回答"); return; }
            if (rb41.IsChecked == false && rb42.IsChecked == false && rb43.IsChecked == false && rb44.IsChecked == false)
            { MessageBox.Show("问题4未回答"); return; }
            if (rb51.IsChecked == false && rb52.IsChecked == false && rb53.IsChecked == false && rb54.IsChecked == false)
            { MessageBox.Show("问题5未回答"); return; }
            if (rb61.IsChecked == false && rb62.IsChecked == false && rb63.IsChecked == false && rb64.IsChecked == false)
            { MessageBox.Show("问题6未回答"); return; }
            if (rb71.IsChecked == false && rb72.IsChecked == false && rb73.IsChecked == false && rb74.IsChecked == false)
            { MessageBox.Show("问题7未回答"); return; }
            if (rb81.IsChecked == false && rb82.IsChecked == false && rb83.IsChecked == false && rb84.IsChecked == false)
            { MessageBox.Show("问题8未回答"); return; }
            if (rb91.IsChecked == false && rb92.IsChecked == false && rb93.IsChecked == false && rb94.IsChecked == false)
            { MessageBox.Show("问题9未回答"); return; }
            if (rb101.IsChecked == false && rb102.IsChecked == false && rb103.IsChecked == false && rb104.IsChecked == false)
            { MessageBox.Show("问题10未回答"); return; }
            if (rb111.IsChecked == false && rb112.IsChecked == false && rb113.IsChecked == false && rb114.IsChecked == false)
            { MessageBox.Show("问题11未回答"); return; }
            if (rb121.IsChecked == false && rb122.IsChecked == false && rb123.IsChecked == false && rb124.IsChecked == false)
            { MessageBox.Show("问题12未回答"); return; }
            if (rb131.IsChecked == false && rb132.IsChecked == false && rb133.IsChecked == false && rb134.IsChecked == false)
            { MessageBox.Show("问题13未回答"); return; }
            if (rb141.IsChecked == false && rb142.IsChecked == false && rb143.IsChecked == false && rb144.IsChecked == false)
            { MessageBox.Show("问题14未回答"); return; }
            if (rb151.IsChecked == false && rb152.IsChecked == false && rb153.IsChecked == false && rb154.IsChecked == false)
            { MessageBox.Show("问题15未回答"); return; }
            if (rb161.IsChecked == false && rb162.IsChecked == false && rb163.IsChecked == false && rb164.IsChecked == false && rb165.IsChecked == false && rb166.IsChecked == false && rb167.IsChecked == false)
            { MessageBox.Show("问题16未回答"); return; }
            if (rb171.IsChecked == false && rb172.IsChecked == false && rb173.IsChecked == false && rb174.IsChecked == false)
            { MessageBox.Show("问题17未回答"); return; }
            if (rb181.IsChecked == false && rb182.IsChecked == false && rb183.IsChecked == false && rb184.IsChecked == false && rb185.IsChecked == false && rb186.IsChecked == false && rb187.IsChecked == false)
            { MessageBox.Show("问题18未回答"); return; }
            if (rb191.IsChecked == false && rb192.IsChecked == false && rb193.IsChecked == false && rb194.IsChecked == false)
            { MessageBox.Show("问题19未回答"); return; }
            if (rb201.IsChecked == false && rb202.IsChecked == false && rb203.IsChecked == false && rb204.IsChecked == false)
            { MessageBox.Show("问题20未回答"); return; }
            if (rb211.IsChecked == false && rb212.IsChecked == false && rb213.IsChecked == false && rb214.IsChecked == false)
            { MessageBox.Show("问题21未回答"); return; }
            save();
            this.NextPage();
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

            fs = new FileStream(filePath + "\\BDI.txt", FileMode.Create);
            sw = new StreamWriter(fs);

            if (rb11.IsChecked == true)
            {
                sw.Write("1：0");
            }
            if (rb12.IsChecked == true)
            {
                sw.Write("1：1");
                Score += 1;
            }
            if (rb13.IsChecked == true)
            {
                sw.Write("1：2");
                Score += 2;
            }
            if (rb14.IsChecked == true)
            {
                sw.Write("1：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb21.IsChecked == true)
            {
                sw.Write("2：0");
            }
            if (rb22.IsChecked == true)
            {
                sw.Write("2：1");
                Score += 1;
            }
            if (rb23.IsChecked == true)
            {
                sw.Write("2：2");
                Score += 2;
            }
            if (rb24.IsChecked == true)
            {
                sw.Write("2：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb31.IsChecked == true)
            {
                sw.Write("3：0");
            }
            if (rb32.IsChecked == true)
            {
                sw.Write("3：1");
                Score += 1;
            }
            if (rb33.IsChecked == true)
            {
                sw.Write("3：2");
                Score += 2;
            }
            if (rb34.IsChecked == true)
            {
                sw.Write("3：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb41.IsChecked == true)
            {
                sw.Write("4：0");
            }
            if (rb42.IsChecked == true)
            {
                sw.Write("4：1");
                Score += 1;
            }
            if (rb43.IsChecked == true)
            {
                sw.Write("4：2");
                Score += 2;
            }
            if (rb44.IsChecked == true)
            {
                sw.Write("4：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb51.IsChecked == true)
            {
                sw.Write("5：0");
            }
            if (rb52.IsChecked == true)
            {
                sw.Write("5：1");
                Score += 1;
            }
            if (rb53.IsChecked == true)
            {
                sw.Write("5：2");
                Score += 2;
            }
            if (rb54.IsChecked == true)
            {
                sw.Write("5：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb61.IsChecked == true)
            {
                sw.Write("6：0");
            }
            if (rb62.IsChecked == true)
            {
                sw.Write("6：1");
                Score += 1;
            }
            if (rb63.IsChecked == true)
            {
                sw.Write("6：2");
                Score += 2;
            }
            if (rb64.IsChecked == true)
            {
                sw.Write("6：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb71.IsChecked == true)
            {
                sw.Write("7：0");
            }
            if (rb72.IsChecked == true)
            {
                sw.Write("7：1");
                Score += 1;
            }
            if (rb73.IsChecked == true)
            {
                sw.Write("7：2");
                Score += 2;
            }
            if (rb74.IsChecked == true)
            {
                sw.Write("7：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb81.IsChecked == true)
            {
                sw.Write("8：0");
            }
            if (rb82.IsChecked == true)
            {
                sw.Write("8：1");
                Score += 1;
            }
            if (rb83.IsChecked == true)
            {
                sw.Write("8：2");
                Score += 2;
            }
            if (rb84.IsChecked == true)
            {
                sw.Write("8：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb91.IsChecked == true)
            {
                sw.Write("9：0");
            }
            if (rb92.IsChecked == true)
            {
                sw.Write("9：1");
                Score += 1;
            }
            if (rb93.IsChecked == true)
            {
                sw.Write("9：2");
                Score += 2;
            }
            if (rb94.IsChecked == true)
            {
                sw.Write("9：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb101.IsChecked == true)
            {
                sw.Write("10：0");
            }
            if (rb102.IsChecked == true)
            {
                sw.Write("10：1");
                Score += 1;
            }
            if (rb103.IsChecked == true)
            {
                sw.Write("10：2");
                Score += 2;
            }
            if (rb104.IsChecked == true)
            {
                sw.Write("10：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb111.IsChecked == true)
            {
                sw.Write("11：0");
            }
            if (rb112.IsChecked == true)
            {
                sw.Write("11：1");
                Score += 1;
            }
            if (rb113.IsChecked == true)
            {
                sw.Write("11：2");
                Score += 2;
            }
            if (rb114.IsChecked == true)
            {
                sw.Write("11：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb121.IsChecked == true)
            {
                sw.Write("12：0");
            }
            if (rb122.IsChecked == true)
            {
                sw.Write("12：1");
                Score += 1;
            }
            if (rb123.IsChecked == true)
            {
                sw.Write("12：2");
                Score += 2;
            }
            if (rb124.IsChecked == true)
            {
                sw.Write("12：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb131.IsChecked == true)
            {
                sw.Write("13：0");
            }
            if (rb132.IsChecked == true)
            {
                sw.Write("13：1");
                Score += 1;
            }
            if (rb133.IsChecked == true)
            {
                sw.Write("13：2");
                Score += 2;
            }
            if (rb134.IsChecked == true)
            {
                sw.Write("13：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb141.IsChecked == true)
            {
                sw.Write("14：0");
            }
            if (rb142.IsChecked == true)
            {
                sw.Write("14：1");
                Score += 1;
            }
            if (rb143.IsChecked == true)
            {
                sw.Write("14：2");
                Score += 2;
            }
            if (rb144.IsChecked == true)
            {
                sw.Write("14：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb151.IsChecked == true)
            {
                sw.Write("15：0");
            }
            if (rb152.IsChecked == true)
            {
                sw.Write("15：1");
                Score += 1;
            }
            if (rb153.IsChecked == true)
            {
                sw.Write("15：2");
                Score += 2;
            }
            if (rb154.IsChecked == true)
            {
                sw.Write("15：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb161.IsChecked == true)
            {
                sw.Write("16：0");
            }
            if (rb162.IsChecked == true)
            {
                sw.Write("16：1a");
                Score += 1;
            }
            if (rb163.IsChecked == true)
            {
                sw.Write("16：1b");
                Score += 1;
            }
            if (rb164.IsChecked == true)
            {
                sw.Write("16：2a");
                Score += 2;
            }
            if (rb165.IsChecked == true)
            {
                sw.Write("16：2b");
                Score += 2;
            }
            if (rb166.IsChecked == true)
            {
                sw.Write("16：3a");
                Score += 3;
            }
            if (rb167.IsChecked == true)
            {
                sw.Write("16：3b");
                Score += 3;
            }
            sw.WriteLine();

            if (rb171.IsChecked == true)
            {
                sw.Write("17：0");
            }
            if (rb172.IsChecked == true)
            {
                sw.Write("17：1");
                Score += 1;
            }
            if (rb173.IsChecked == true)
            {
                sw.Write("17：2");
                Score += 2;
            }
            if (rb174.IsChecked == true)
            {
                sw.Write("17：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb181.IsChecked == true)
            {
                sw.Write("18：0");
            }
            if (rb182.IsChecked == true)
            {
                sw.Write("18：1a");
                Score += 1;
            }
            if (rb183.IsChecked == true)
            {
                sw.Write("18：1b");
                Score += 1;
            }
            if (rb184.IsChecked == true)
            {
                sw.Write("18：2a");
                Score += 2;
            }
            if (rb185.IsChecked == true)
            {
                sw.Write("18：2b");
                Score += 2;
            }
            if (rb186.IsChecked == true)
            {
                sw.Write("18：3a");
                Score += 3;
            }
            if (rb187.IsChecked == true)
            {
                sw.Write("18：3b");
                Score += 3;
            }
            sw.WriteLine();

            if (rb191.IsChecked == true)
            {
                sw.Write("19：0");
            }
            if (rb192.IsChecked == true)
            {
                sw.Write("19：1");
                Score += 1;
            }
            if (rb193.IsChecked == true)
            {
                sw.Write("19：2");
                Score += 2;
            }
            if (rb194.IsChecked == true)
            {
                sw.Write("19：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb201.IsChecked == true)
            {
                sw.Write("20：0");
            }
            if (rb202.IsChecked == true)
            {
                sw.Write("20：1");
                Score += 1;
            }
            if (rb203.IsChecked == true)
            {
                sw.Write("20：2");
                Score += 2;
            }
            if (rb204.IsChecked == true)
            {
                sw.Write("20：3");
                Score += 3;
            }
            sw.WriteLine();

            if (rb211.IsChecked == true)
            {
                sw.Write("21：0");
            }
            if (rb212.IsChecked == true)
            {
                sw.Write("21：1");
                Score += 1;
            }
            if (rb213.IsChecked == true)
            {
                sw.Write("21：2");
                Score += 2;
            }
            if (rb214.IsChecked == true)
            {
                sw.Write("21：3");
                Score += 3;
            }
            sw.WriteLine();

            sw.Write("总分：" + Score);
            sw.WriteLine();

            sw.Close();
            fs.Close();

        }

    }
}
