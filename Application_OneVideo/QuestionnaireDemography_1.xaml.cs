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

namespace QuestionnaireDemography
{
    /// <summary>
    /// QuestionnaireDemography_1.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionnaireDemography_1 : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";

        public QuestionnaireDemography_1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text == "")
            {
                MessageBox.Show("请输入您的姓名");
                return;
            }

            if (cb21.IsChecked == false && cb22.IsChecked == false && cb23.IsChecked == false && cb24.IsChecked == false)
            {
                MessageBox.Show("请选择您的性别");
                return;
            }

            if (tb_year.Text == "" || tb_month.Text == "" || tb_day.Text == "")
            {
                MessageBox.Show("请输入您的出生日期");
                return;
            }

            if (tb_height.Text == "")
            {
                MessageBox.Show("请输入您的身高");
                return;
            }

            if (tb_zhong.Text == "")
            {
                MessageBox.Show("请输入您的体重");
                return;
            }

            if (cb81.IsChecked == false && cb82.IsChecked == false && cb83.IsChecked == false && cb84.IsChecked == false && cb85.IsChecked == false && cb86.IsChecked == false)
            {
                MessageBox.Show("请选择您的民族");
                return;
            }

            if (cb86.IsChecked == true && tb_race.Text == "")
            {
                MessageBox.Show("请输入您的民族");
                return;
            }

            if (cb91.IsChecked == false && cb92.IsChecked == false && cb93.IsChecked == false && cb94.IsChecked == false && cb95.IsChecked == false && cb96.IsChecked == false)
            {
                MessageBox.Show("请选择您的文化水平");
                return;
            }

            if (cb101.IsChecked == false && cb102.IsChecked == false && cb103.IsChecked == false && cb104.IsChecked == false && cb105.IsChecked == false && cb106.IsChecked == false && cb107.IsChecked == false && cb108.IsChecked == false && cb109.IsChecked == false && cb1010.IsChecked == false)
            {
                MessageBox.Show("请选择您的职业");
                return;
            }

            if (cb1010.IsChecked == true && tb_job.Text == "")
            {
                MessageBox.Show("请输入您的职业");
                return;
            }

            save();
        }

        private void save()
        {
            fs = new FileStream(filePath + "\\Demography.txt", FileMode.Create);
            sw = new StreamWriter(fs);


            sw.Write("1:" + tb_name.Text);
            sw.WriteLine();

            if (cb21.IsChecked == true)
                sw.Write("2:1");
            if (cb22.IsChecked == true && cb23.IsChecked == false && cb24.IsChecked == false)
                sw.Write("2:2");
            if (cb23.IsChecked == true)
                sw.Write("2:3");
            if (cb24.IsChecked == true)
                sw.Write("2:4");

            sw.WriteLine();


            sw.Write("3:" + tb_year.Text + " " + tb_month.Text + " " + tb_day.Text);
            sw.WriteLine();

            sw.Write("4:" + tb_height.Text);
            sw.WriteLine();

            sw.Write("5:" + tb_zhong.Text);
            sw.WriteLine();


            sw.Write("6:" + tb_home.Text);
            sw.WriteLine();

            sw.Write("7:" + tb_phone.Text);
            sw.WriteLine();

            if (cb81.IsChecked == true)
                sw.Write("8:1");
            if (cb82.IsChecked == true)
                sw.Write("8:2");
            if (cb83.IsChecked == true)
                sw.Write("8:3");
            if (cb84.IsChecked == true)
                sw.Write("8:4");
            if (cb85.IsChecked == true)
                sw.Write("8:5");
            if (cb86.IsChecked == true)
                sw.Write("8:6 " + tb_race.Text);

            sw.WriteLine();

            if (cb91.IsChecked == true)
                sw.Write("9:1");
            if (cb92.IsChecked == true)
                sw.Write("9:2");
            if (cb93.IsChecked == true)
                sw.Write("9:3");
            if (cb94.IsChecked == true)
                sw.Write("9:4");
            if (cb95.IsChecked == true)
                sw.Write("9:5");
            if (cb96.IsChecked == true)
                sw.Write("9:6");
            sw.WriteLine();

            if (cb101.IsChecked == true)
                sw.Write("10:1");
            if (cb102.IsChecked == true)
                sw.Write("10:2");
            if (cb103.IsChecked == true)
                sw.Write("10:3");
            if (cb104.IsChecked == true)
                sw.Write("10:4");
            if (cb105.IsChecked == true)
                sw.Write("10:5");
            if (cb106.IsChecked == true)
                sw.Write("10:6");
            if (cb107.IsChecked == true)
                sw.Write("10:7");
            if (cb108.IsChecked == true)
                sw.Write("10:8");
            if (cb109.IsChecked == true)
                sw.Write("10:9");
            if (cb1010.IsChecked == true)
            {
                sw.Write("10:10 " + tb_job.Text);

            }
            sw.WriteLine();

            sw.Close();
            fs.Close();
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
        }

        private void cb23_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb24.IsChecked = false;
        }

        private void cb24_Checked(object sender, RoutedEventArgs e)
        {
            cb21.IsChecked = false;
            cb23.IsChecked = false;
        }

        private void cb81_Checked(object sender, RoutedEventArgs e)
        {
            cb82.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
            cb85.IsChecked = false;
            cb86.IsChecked = false;
        }

        private void cb82_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
            cb85.IsChecked = false;
            cb86.IsChecked = false;
        }

        private void cb83_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb84.IsChecked = false;
            cb85.IsChecked = false;
            cb86.IsChecked = false;
        }

        private void cb84_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb83.IsChecked = false;
            cb85.IsChecked = false;
            cb86.IsChecked = false;
        }

        private void cb85_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
            cb86.IsChecked = false;
        }

        private void cb86_Checked(object sender, RoutedEventArgs e)
        {
            cb81.IsChecked = false;
            cb82.IsChecked = false;
            cb83.IsChecked = false;
            cb84.IsChecked = false;
            cb85.IsChecked = false;
        }

        private void cb91_Checked(object sender, RoutedEventArgs e)
        {
            cb92.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
            cb95.IsChecked = false;
            cb96.IsChecked = false;
        }

        private void cb92_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
            cb95.IsChecked = false;
            cb96.IsChecked = false;
        }

        private void cb93_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb94.IsChecked = false;
            cb95.IsChecked = false;
            cb96.IsChecked = false;
        }

        private void cb94_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb93.IsChecked = false;
            cb95.IsChecked = false;
            cb96.IsChecked = false;
        }

        private void cb95_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
            cb96.IsChecked = false;
        }

        private void cb96_Checked(object sender, RoutedEventArgs e)
        {
            cb91.IsChecked = false;
            cb92.IsChecked = false;
            cb93.IsChecked = false;
            cb94.IsChecked = false;
            cb95.IsChecked = false;
        }

        private void cb101_Checked(object sender, RoutedEventArgs e)
        {
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb102_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb103_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb104_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb105_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb106_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb107_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb108_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb109.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb109_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb1010.IsChecked = false;
        }

        private void cb1010_Checked(object sender, RoutedEventArgs e)
        {
            cb101.IsChecked = false;
            cb102.IsChecked = false;
            cb103.IsChecked = false;
            cb104.IsChecked = false;
            cb105.IsChecked = false;
            cb106.IsChecked = false;
            cb107.IsChecked = false;
            cb108.IsChecked = false;
            cb109.IsChecked = false;

        }

    }
}
