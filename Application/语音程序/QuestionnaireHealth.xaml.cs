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

namespace QuestionnaireHealth2
{
    /// <summary>
    /// QuestionnaireHealth.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionnaireHealth : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";
        int Score = 0;
        public QuestionnaireHealth()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cb11.IsChecked == false && cb12.IsChecked == false && cb13.IsChecked == false && cb14.IsChecked == false)
            { MessageBox.Show("问题1未回答"); return; } 
            if (cb21.IsChecked == false && cb22.IsChecked == false && cb23.IsChecked == false && cb24.IsChecked == false)
            { MessageBox.Show("问题2未回答"); return; } 
            if (cb31.IsChecked == false && cb32.IsChecked == false && cb33.IsChecked == false && cb34.IsChecked == false)
            { MessageBox.Show("问题3未回答"); return; } 
            if (cb41.IsChecked == false && cb42.IsChecked == false && cb43.IsChecked == false && cb44.IsChecked == false)
            { MessageBox.Show("问题4未回答"); return; } 
            if (cb51.IsChecked == false && cb52.IsChecked == false && cb53.IsChecked == false && cb54.IsChecked == false)
            { MessageBox.Show("问题5未回答"); return; }
            if (cb61.IsChecked == false && cb62.IsChecked == false && cb63.IsChecked == false && cb64.IsChecked == false)
            { MessageBox.Show("问题6未回答"); return; }
            if (cb71.IsChecked == false && cb72.IsChecked == false && cb73.IsChecked == false && cb74.IsChecked == false)
            { MessageBox.Show("问题7未回答"); return; }
            if (cb81.IsChecked == false && cb82.IsChecked == false && cb83.IsChecked == false && cb84.IsChecked == false)
            { MessageBox.Show("问题8未回答"); return; }
            if (cb91.IsChecked == false && cb92.IsChecked == false && cb93.IsChecked == false && cb94.IsChecked == false)
            { MessageBox.Show("问题9未回答"); return; }

            save();
        }

        private void cb11_Checked(object sender, RoutedEventArgs e)
        {
            cb12.IsChecked = false;
            cb13.IsChecked = false;
            cb14.IsChecked = false;
        }

        private void cb12_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb13.IsChecked = false;
            cb14.IsChecked = false;
        }

        private void cb13_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb12.IsChecked = false;
            cb14.IsChecked = false;
        }

        private void cb14_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb12.IsChecked = false;
            cb13.IsChecked = false;
        }


        private void cb21_Checked(object sender, RoutedEventArgs e)
        {
            cb22.IsChecked = false;
            cb23.IsChecked = false;
            cb24.IsChecked = false;
        }

        private void cb22_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb23.IsChecked = false;
            cb24.IsChecked = false;
        }

        private void cb23_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb22.IsChecked = false;
            cb24.IsChecked = false;
        }

        private void cb24_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb22.IsChecked = false;
            cb23.IsChecked = false;
        }

        private void cb31_Checked(object sender, RoutedEventArgs e)
        {
            cb32.IsChecked = false;
            cb33.IsChecked = false;
            cb34.IsChecked = false;
        }

        private void cb32_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb33.IsChecked = false;
            cb34.IsChecked = false;
        }

        private void cb33_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb32.IsChecked = false;
            cb34.IsChecked = false;
        }

        private void cb34_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb32.IsChecked = false;
            cb33.IsChecked = false;
        }

        private void cb41_Checked(object sender, RoutedEventArgs e)
        {
            cb42.IsChecked = false;
            cb43.IsChecked = false;
            cb44.IsChecked = false;
        }

        private void cb42_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb43.IsChecked = false;
            cb44.IsChecked = false;
        }

        private void cb43_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb42.IsChecked = false;
            cb44.IsChecked = false;
        }

        private void cb44_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb42.IsChecked = false;
            cb43.IsChecked = false;
        }

        private void cb51_Checked(object sender, RoutedEventArgs e)
        {
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
        }

        private void cb52_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
        }

        private void cb53_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb54.IsChecked = false;
        }

        private void cb54_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
        }

        private void cb61_Checked(object sender, RoutedEventArgs e)
        {
            cb62.IsChecked = false;
            cb63.IsChecked = false;
            cb64.IsChecked = false;
        }

        private void cb62_Checked(object sender, RoutedEventArgs e)
        {
            cb61.IsChecked = false;
            cb63.IsChecked = false;
            cb64.IsChecked = false;
        }

        private void cb63_Checked(object sender, RoutedEventArgs e)
        {
            cb61.IsChecked = false;
            cb62.IsChecked = false;
            cb64.IsChecked = false;
        }

        private void cb64_Checked(object sender, RoutedEventArgs e)
        {
            cb61.IsChecked = false;
            cb62.IsChecked = false;
            cb63.IsChecked = false;
        }

        private void cb71_Checked(object sender, RoutedEventArgs e)
        {
            cb72.IsChecked = false;
            cb73.IsChecked = false;
            cb74.IsChecked = false;
        }

        private void cb72_Checked(object sender, RoutedEventArgs e)
        {
            cb71.IsChecked = false;
            cb73.IsChecked = false;
            cb74.IsChecked = false;
        }

        private void cb73_Checked(object sender, RoutedEventArgs e)
        {
            cb71.IsChecked = false;
            cb72.IsChecked = false;
            cb74.IsChecked = false;
        }

        private void cb74_Checked(object sender, RoutedEventArgs e)
        {
            cb71.IsChecked = false;
            cb72.IsChecked = false;
            cb73.IsChecked = false;
        }

        private void cb81_Checked(object sender, RoutedEventArgs e)
        {
            cb82.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
        }

        private void cb82_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
        }

        private void cb83_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb84.IsChecked = false;
        }

        private void cb84_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb83.IsChecked = false;
        }

        private void cb91_Checked(object sender, RoutedEventArgs e)
        {
            cb92.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
        }

        private void cb92_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
        }

        private void cb93_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb94.IsChecked = false;
        }

        private void cb94_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb93.IsChecked = false;
        }

        private void save()
        {
            fs = new FileStream(filePath + "\\QuestionnaireHealth.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            if (cb11.IsChecked == true)
            {
                sw.Write("1：0");
            }
            if (cb12.IsChecked == true)
            {
                sw.Write("1：1");
                Score += 1;
            }
            if (cb13.IsChecked == true)
            {
                sw.Write("1：2");
                Score += 2;
            }
            if (cb14.IsChecked == true)
            {
                sw.Write("1：3");
                Score += 3;
            }
            sw.WriteLine();


            if (cb21.IsChecked == true)
            {
                sw.Write("2：0");
            }
            if (cb22.IsChecked == true)
            {
                sw.Write("2：1");
                Score += 1;
            }
            if (cb23.IsChecked == true)
            {
                sw.Write("2：2");
                Score += 2;
            }
            if (cb24.IsChecked == true)
            {
                sw.Write("2：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb31.IsChecked == true)
            {
                sw.Write("3：0");
            }
            if (cb32.IsChecked == true)
            {
                sw.Write("3：1");
                Score += 1;
            }
            if (cb33.IsChecked == true)
            {
                sw.Write("3：2");
                Score += 2;
            }
            if (cb34.IsChecked == true)
            {
                sw.Write("3：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb41.IsChecked == true)
            {
                sw.Write("4：0");
            }
            if (cb42.IsChecked == true)
            {
                sw.Write("4：1");
                Score += 1;
            }
            if (cb43.IsChecked == true)
            {
                sw.Write("4：2");
                Score += 2;
            }
            if (cb44.IsChecked == true)
            {
                sw.Write("4：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb51.IsChecked == true)
            {
                sw.Write("5：0");
            }
            if (cb52.IsChecked == true)
            {
                sw.Write("5：1");
                Score += 1;
            }
            if (cb53.IsChecked == true)
            {
                sw.Write("5：2");
                Score += 2;
            }
            if (cb54.IsChecked == true)
            {
                sw.Write("5：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb61.IsChecked == true)
            {
                sw.Write("6：0");
            }
            if (cb62.IsChecked == true)
            {
                sw.Write("6：1");
                Score += 1;
            }
            if (cb63.IsChecked == true)
            {
                sw.Write("6：2");
                Score += 2;
            }
            if (cb64.IsChecked == true)
            {
                sw.Write("6：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb71.IsChecked == true)
            {
                sw.Write("7：0");
            }
            if (cb72.IsChecked == true)
            {
                sw.Write("7：1");
                Score += 1;
            }
            if (cb73.IsChecked == true)
            {
                sw.Write("7：2");
                Score += 2;
            }
            if (cb74.IsChecked == true)
            {
                sw.Write("7：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb81.IsChecked == true)
            {
                sw.Write("8：0");
            }
            if (cb82.IsChecked == true)
            {
                sw.Write("8：1");
                Score += 1;
            }
            if (cb83.IsChecked == true)
            {
                sw.Write("8：2");
                Score += 2;
            }
            if (cb84.IsChecked == true)
            {
                sw.Write("8：3");
                Score += 3;
            }
            sw.WriteLine();

            if (cb91.IsChecked == true)
            {
                sw.Write("9：0");
            }
            if (cb92.IsChecked == true)
            {
                sw.Write("9：1");
                Score += 1;
            }
            if (cb93.IsChecked == true)
            {
                sw.Write("9：2");
                Score += 2;
            }
            if (cb94.IsChecked == true)
            {
                sw.Write("9：3");
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
