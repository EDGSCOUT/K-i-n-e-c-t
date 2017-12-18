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

namespace QuestionnairePSQI
{
    /// <summary>
    /// QuestionnairePSQI.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionnairePSQI : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";

        public QuestionnairePSQI()
        {
            InitializeComponent();
        }

        private void save()
        {
            fs = new FileStream(filePath + "\\PSQI.txt", FileMode.Create);
            sw = new StreamWriter(fs);


            sw.Write("1:" + tb_1.Text);
            sw.WriteLine();

            sw.Write("2:" + tb_2.Text);
            sw.WriteLine();

            sw.Write("3:" + tb_3.Text);
            sw.WriteLine();

            sw.Write("4:" + tb_4.Text);
            sw.WriteLine();

            if (cb51.IsChecked == true)
                sw.Write("5:a");
            if (cb52.IsChecked == true)
                sw.Write("5:b");
            if (cb53.IsChecked == true)
                sw.Write("5:c");
            if (cb54.IsChecked == true)
                sw.Write("5:d");
            if (cb55.IsChecked == true)
                sw.Write("5:e");
            if (cb56.IsChecked == true)
                sw.Write("5:f");
            if (cb57.IsChecked == true)
                sw.Write("5:g");
            if (cb58.IsChecked == true)
                sw.Write("5:h");
            if (cb59.IsChecked == true)
                sw.Write("5:i");
            if (cb510.IsChecked == true)
                sw.Write("5:j");
            if (cb511.IsChecked == true)
                sw.Write("5:无");

            if (cb511.IsChecked == false)
            {
                if (cb521.IsChecked == true)
                    sw.Write(" 无");
                if (cb522.IsChecked == true)
                    sw.Write(" 小于每周一次");
                if (cb523.IsChecked == true)
                    sw.Write(" 每周1次到2次");
                if (cb524.IsChecked == true)
                    sw.Write(" 每周3次或以上");
            }

            sw.WriteLine();


            if (cb61.IsChecked == true)
                sw.Write("6:很好");
            if (cb62.IsChecked == true)
                sw.Write("6:较好");
            if (cb63.IsChecked == true)
                sw.Write("6:较差");
            if (cb64.IsChecked == true)
                sw.Write("6:很差");
            
            sw.WriteLine();

            if (cb71.IsChecked == true)
                sw.Write("7:无");
            if (cb72.IsChecked == true)
                sw.Write("7:小于每周一次");
            if (cb73.IsChecked == true)
                sw.Write("7:每周1次到2次");
            if (cb74.IsChecked == true)
                sw.Write("7:每周3次或以上");

            sw.WriteLine();

            if (cb81.IsChecked == true)
                sw.Write("8:没有");
            if (cb82.IsChecked == true)
                sw.Write("8:偶尔有");
            if (cb83.IsChecked == true)
                sw.Write("8:有时有");
            if (cb84.IsChecked == true)
                sw.Write("8:经常有");

            sw.WriteLine();

            if (cb91.IsChecked == true)
                sw.Write("9:没有");
            if (cb92.IsChecked == true)
                sw.Write("9:偶尔有");
            if (cb93.IsChecked == true)
                sw.Write("9:有时有");
            if (cb94.IsChecked == true)
                sw.Write("9:经常有");

            sw.WriteLine();

            sw.Close();
            fs.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (tb_1.Text == "")
            {
                MessageBox.Show("第1题未回答");
                return;
            }

            if (tb_2.Text == "")
            {
                MessageBox.Show("第2题未回答");
                return;
            }

            if (tb_3.Text == "")
            {
                MessageBox.Show("第3题未回答");
                return;
            }

            if (tb_4.Text == "")
            {
                MessageBox.Show("第4题未回答");
                return;
            }

            if (cb51.IsChecked == false && cb52.IsChecked == false && cb53.IsChecked == false && cb54.IsChecked == false && cb55.IsChecked == false && cb56.IsChecked == false && cb57.IsChecked == false && cb58.IsChecked == false && cb59.IsChecked == false && cb510.IsChecked == false && cb511.IsChecked == false)
            {
                MessageBox.Show("第5题未回答");
                return;
            }

            if (cb511.IsChecked == false && cb521.IsChecked == false && cb522.IsChecked == false && cb523.IsChecked == false && cb524.IsChecked == false)
            {
                MessageBox.Show("第5题未选择次数");
                return;
            }

            if (cb61.IsChecked == false && cb62.IsChecked == false && cb63.IsChecked == false && cb64.IsChecked == false)
            {
                MessageBox.Show("第6题未回答");
                return;
            }

            if (cb71.IsChecked == false && cb72.IsChecked == false && cb73.IsChecked == false && cb74.IsChecked == false)
            {
                MessageBox.Show("第7题未回答");
                return;
            }

            if (cb81.IsChecked == false && cb82.IsChecked == false && cb83.IsChecked == false && cb84.IsChecked == false)
            {
                MessageBox.Show("第8题未回答");
                return;
            }

            if (cb91.IsChecked == false && cb92.IsChecked == false && cb93.IsChecked == false && cb94.IsChecked == false)
            {
                MessageBox.Show("第9题未回答");
                return;
            }

            save();
        }

        private void cb51_Checked(object sender, RoutedEventArgs e)
        {
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb52_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb53_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb54_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb55_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb56_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb57_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb58_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb59_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb510.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb510_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb511.IsChecked = false;
        }

        private void cb511_Checked(object sender, RoutedEventArgs e)
        {
            cb51.IsChecked = false;
            cb52.IsChecked = false;
            cb53.IsChecked = false;
            cb54.IsChecked = false;
            cb55.IsChecked = false;
            cb56.IsChecked = false;
            cb57.IsChecked = false;
            cb58.IsChecked = false;
            cb59.IsChecked = false;
            cb510.IsChecked = false;
        }

        private void cb521_Checked(object sender, RoutedEventArgs e)
        {
            cb522.IsChecked = false;
            cb523.IsChecked = false;
            cb524.IsChecked = false;
        }

        private void cb522_Checked(object sender, RoutedEventArgs e)
        {
            cb521.IsChecked = false;
            cb523.IsChecked = false;
            cb524.IsChecked = false;
        }

        private void cb523_Checked(object sender, RoutedEventArgs e)
        {
            cb521.IsChecked = false;
            cb522.IsChecked = false;
            cb524.IsChecked = false;
        }

        private void cb524_Checked(object sender, RoutedEventArgs e)
        {
            cb521.IsChecked = false;
            cb522.IsChecked = false;
            cb523.IsChecked = false;
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

    }
}
