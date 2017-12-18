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

namespace REMIT
{
    /// <summary>
    /// REMIT.xaml 的交互逻辑
    /// </summary>
    public partial class REMIT : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";
        int Score = 0;

        public REMIT()
        {
            InitializeComponent();
        }

        private void save()
        {
            fs = new FileStream(filePath + "\\QuestionnaireREMIT.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            if (cb11.IsChecked == true)
            {
                Score += 4;
                sw.Write("1：4");
            }
            if (cb12.IsChecked == true)
            {
                sw.Write("1：3");
                Score += 3;
            }
            if (cb13.IsChecked == true)
            {
                sw.Write("1：2");
                Score += 2;
            }
            if (cb14.IsChecked == true)
            {
                sw.Write("1：1");
                Score += 1;
            }
            if (cb15.IsChecked == true)
            {
                sw.Write("1：0");
                
            }
            sw.WriteLine();


            if (cb21.IsChecked == true)
            {
                sw.Write("2：4");
                Score += 4;
            }
            if (cb22.IsChecked == true)
            {
                sw.Write("2：3");
                Score += 3;
            }
            if (cb23.IsChecked == true)
            {
                sw.Write("2：2");
                Score += 2;
            }
            if (cb24.IsChecked == true)
            {
                sw.Write("2：1");
                Score += 1;
            }
            if (cb25.IsChecked == true)
            {
                sw.Write("2：0");

            }
            sw.WriteLine();

            if (cb31.IsChecked == true)
            {
                sw.Write("3：4");
                Score += 4;
            }
            if (cb32.IsChecked == true)
            {
                sw.Write("3：3");
                Score += 3;
            }
            if (cb33.IsChecked == true)
            {
                sw.Write("3：2");
                Score += 2;
            }
            if (cb34.IsChecked == true)
            {
                sw.Write("3：1");
                Score += 1;
            }
            if (cb35.IsChecked == true)
            {
                sw.Write("3：0");

            }
            sw.WriteLine();

            if (cb41.IsChecked == true)
            {
                sw.Write("4：4");
                Score += 4;
            }
            if (cb42.IsChecked == true)
            {
                sw.Write("4：3");
                Score += 3;
            }
            if (cb43.IsChecked == true)
            {
                sw.Write("4：2");
                Score += 2;
            }
            if (cb44.IsChecked == true)
            {
                sw.Write("4：1");
                Score += 1;
            }
            if (cb45.IsChecked == true)
            {
                sw.Write("4：0");

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
            if (cb55.IsChecked == true)
            {
                sw.Write("5：4");
                Score += 4;
            }
            sw.WriteLine();

            sw.Write("总分：" + Score);
            sw.WriteLine();

            sw.Close();
            fs.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cb11.IsChecked == false && cb12.IsChecked == false && cb13.IsChecked == false && cb14.IsChecked == false && cb15.IsChecked == false)
            { MessageBox.Show("问题1未回答"); return; }
            if (cb21.IsChecked == false && cb22.IsChecked == false && cb23.IsChecked == false && cb24.IsChecked == false && cb25.IsChecked == false)
            { MessageBox.Show("问题2未回答"); return; }
            if (cb31.IsChecked == false && cb32.IsChecked == false && cb33.IsChecked == false && cb34.IsChecked == false && cb35.IsChecked == false)
            { MessageBox.Show("问题3未回答"); return; }
            if (cb41.IsChecked == false && cb42.IsChecked == false && cb43.IsChecked == false && cb44.IsChecked == false && cb45.IsChecked == false)
            { MessageBox.Show("问题4未回答"); return; }
            if (cb51.IsChecked == false && cb52.IsChecked == false && cb53.IsChecked == false && cb54.IsChecked == false && cb55.IsChecked == false)
            { MessageBox.Show("问题5未回答"); return; }

            save();
        }

        private void cb11_Checked(object sender, RoutedEventArgs e)
        {
            cb12.IsChecked = false;
            cb13.IsChecked = false;
            cb14.IsChecked = false;
            cb15.IsChecked = false;
        }

        private void cb12_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb13.IsChecked = false;
            cb14.IsChecked = false;
            cb15.IsChecked = false;
        }

        private void cb13_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb12.IsChecked = false;
            cb14.IsChecked = false;
            cb15.IsChecked = false;
        }

        private void cb14_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb12.IsChecked = false;
            cb13.IsChecked = false;
            cb15.IsChecked = false;
        }

        private void cb15_Checked(object sender, RoutedEventArgs e)
        {
            cb11.IsChecked = false;
            cb12.IsChecked = false;
            cb13.IsChecked = false;
            cb14.IsChecked = false;
        }


        private void cb21_Checked(object sender, RoutedEventArgs e)
        {
            cb22.IsChecked = false;
            cb23.IsChecked = false;
            cb24.IsChecked = false;
            cb25.IsChecked = false;
        }

        private void cb22_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb23.IsChecked = false;
            cb24.IsChecked = false;
            cb25.IsChecked = false;
        }

        private void cb23_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb22.IsChecked = false;
            cb24.IsChecked = false;
            cb25.IsChecked = false;
        }

        private void cb24_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb22.IsChecked = false;
            cb23.IsChecked = false;
            cb25.IsChecked = false;
        }

        private void cb25_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb22.IsChecked = false;
            cb23.IsChecked = false;
            cb24.IsChecked = false;
        }

        private void cb31_Checked(object sender, RoutedEventArgs e)
        {
            cb32.IsChecked = false;
            cb33.IsChecked = false;
            cb34.IsChecked = false;
            cb35.IsChecked = false;
        }

        private void cb32_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb33.IsChecked = false;
            cb34.IsChecked = false;
            cb35.IsChecked = false;
        }

        private void cb33_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb32.IsChecked = false;
            cb34.IsChecked = false;
            cb35.IsChecked = false;
        }

        private void cb34_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb32.IsChecked = false;
            cb33.IsChecked = false;
            cb35.IsChecked = false;
        }

        private void cb35_Checked(object sender, RoutedEventArgs e)
        {
            cb31.IsChecked = false;
            cb32.IsChecked = false;
            cb33.IsChecked = false;
            cb34.IsChecked = false;
        }

        private void cb41_Checked(object sender, RoutedEventArgs e)
        {
            cb42.IsChecked = false;
            cb43.IsChecked = false;
            cb44.IsChecked = false;
            cb45.IsChecked = false;
        }

        private void cb42_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb43.IsChecked = false;
            cb44.IsChecked = false;
            cb45.IsChecked = false;
        }

        private void cb43_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb42.IsChecked = false;
            cb44.IsChecked = false;
            cb45.IsChecked = false;
        }

        private void cb44_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb42.IsChecked = false;
            cb43.IsChecked = false;
            cb45.IsChecked = false;
        }

        private void cb45_Checked(object sender, RoutedEventArgs e)
        {
            cb41.IsChecked = false;
            cb42.IsChecked = false;
            cb43.IsChecked = false;
            cb44.IsChecked = false;
        }

        private void cb51_Checked(object sender, RoutedEventArgs e)
        {
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
        }

        private void cb52_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
        }

        private void cb53_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
        }

        private void cb54_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb55.IsChecked = false;
        }

        private void cb55_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
        }
    }
}
