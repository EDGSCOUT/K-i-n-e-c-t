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
    /// QuestionnaireDemography_2.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionnaireDemography_2 : UserControl
    {
        FileStream fs = null;
        StreamWriter sw = null;
        String filePath = "C:\\Users\\ad\\Desktop";

        public QuestionnaireDemography_2()
        {
            InitializeComponent();
        }

        private void save()
        {
            fs = new FileStream(filePath + "\\Demography.txt", FileMode.Append);
            sw = new StreamWriter(fs);

            if (cb111.IsChecked == true)
                sw.Write("11:1");
            if (cb112.IsChecked == true)
                sw.Write("11:2");
            if (cb113.IsChecked == true)
                sw.Write("11:3");
            if (cb114.IsChecked == true)
                sw.Write("11:4");
            if (cb115.IsChecked == true)
                sw.Write("11:5");
            sw.WriteLine();

            if (cb121.IsChecked == true)
                sw.Write("12:1");
            if (cb122.IsChecked == true)
                sw.Write("12:2");
            if (cb123.IsChecked == true)
                sw.Write("12:3");
            sw.WriteLine();

            if (cb131.IsChecked == true)
                sw.Write("13:1");
            if (cb132.IsChecked == true)
                sw.Write("13:2");
            if (cb133.IsChecked == true)
                sw.Write("13:3");
            if (cb134.IsChecked == true)
                sw.Write("13:4");
            if (cb135.IsChecked == true)
                sw.Write("13:5");
            sw.WriteLine();


            if (cb141.IsChecked == true)
                sw.Write("14:1 " + tb_son.Text + " " + tb_dau.Text);
            if (cb142.IsChecked == true)
                sw.Write("14:2");
            sw.WriteLine();


            if (cb151.IsChecked == true)
                sw.Write("15:0");
            if (cb152.IsChecked == true)
                sw.Write("15:1");
            if (cb153.IsChecked == true)
                sw.Write("15:2");
            if (cb154.IsChecked == true)
                sw.Write("15:3");
            if (cb155.IsChecked == true)
                sw.Write("15:4");
            if (cb156.IsChecked == true)
                sw.Write("15:5");
            if (cb157.IsChecked == true)
                sw.Write("15:6 " + tb_live.Text);
            sw.WriteLine();

            if (cb161.IsChecked == true)
                sw.Write("16:1");
            if (cb162.IsChecked == true)
                sw.Write("16:2");
            if (cb163.IsChecked == true)
                sw.Write("16:3");
            if (cb164.IsChecked == true)
                sw.Write("16:4");
            sw.WriteLine();


            if (cb171.IsChecked == true)
                sw.Write("17:1");
            if (cb172.IsChecked == true)
                sw.Write("17:2");
            if (cb173.IsChecked == true)
                sw.Write("17:3");
            if (cb174.IsChecked == true)
                sw.Write("17:4");
            if (cb175.IsChecked == true)
                sw.Write("17:5 " + tb_yb.Text);
            sw.WriteLine();

            if (cb181.IsChecked == true)
                sw.Write("18:1");
            if (cb182.IsChecked == true)
                sw.Write("18:2 " + tb_zj.Text);
            sw.WriteLine();

            sw.Close();
            fs.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cb111.IsChecked == false && cb112.IsChecked == false && cb113.IsChecked == false && cb114.IsChecked == false && cb115.IsChecked == false)
            {
                MessageBox.Show("请选择您的工作状况");
                return;
            }

            if (cb121.IsChecked == false && cb122.IsChecked == false && cb123.IsChecked == false)
            {
                MessageBox.Show("请选择您的工作性质");
                return;
            }

            if (cb131.IsChecked == false && cb132.IsChecked == false && cb133.IsChecked == false && cb134.IsChecked == false && cb135.IsChecked == false)
            {
                MessageBox.Show("请选择您的婚姻状况");
                return;
            }

            if (cb141.IsChecked == false && cb142.IsChecked == false)
            {
                MessageBox.Show("请选择是否有子女");
                return;
            }

            if (cb151.IsChecked == false && cb152.IsChecked == false && cb153.IsChecked == false && cb154.IsChecked == false && cb155.IsChecked == false && cb156.IsChecked == false && cb157.IsChecked == false)
            {
                MessageBox.Show("请选择您与谁生活在一起");
                return;
            }

            if (cb161.IsChecked == false && cb162.IsChecked == false && cb163.IsChecked == false && cb164.IsChecked == false)
            {
                MessageBox.Show("请选择您的独立程度");
                return;
            }

            if (cb171.IsChecked == false && cb172.IsChecked == false && cb173.IsChecked == false && cb174.IsChecked == false && cb175.IsChecked == false)
            {
                MessageBox.Show("请选择您的医疗保险");
                return;
            }

            if (cb181.IsChecked == false && cb182.IsChecked == false)
            {
                MessageBox.Show("请选择您的宗教信仰");
                return;
            }

            save();
        }

        
        private void cb111_Checked(object sender, RoutedEventArgs e)
        {
            cb112.IsChecked = false;
            cb113.IsChecked = false;
            cb114.IsChecked = false;
            cb115.IsChecked = false;
        }

        private void cb112_Checked(object sender, RoutedEventArgs e)
        {
            cb111.IsChecked = false;
            cb113.IsChecked = false;
            cb114.IsChecked = false;
            cb115.IsChecked = false;
        }

        private void cb113_Checked(object sender, RoutedEventArgs e)
        {
            cb111.IsChecked = false;
            cb112.IsChecked = false;
            cb114.IsChecked = false;
            cb115.IsChecked = false;
        }

        private void cb114_Checked(object sender, RoutedEventArgs e)
        {
            cb111.IsChecked = false;
            cb112.IsChecked = false;
            cb113.IsChecked = false;
            cb115.IsChecked = false;
        }

        private void cb115_Checked(object sender, RoutedEventArgs e)
        {
            cb111.IsChecked = false;
            cb112.IsChecked = false;
            cb113.IsChecked = false;
            cb114.IsChecked = false;
            
        }

        private void cb121_Checked(object sender, RoutedEventArgs e)
        {
            
            cb122.IsChecked = false;
            cb123.IsChecked = false;
        }

        private void cb122_Checked(object sender, RoutedEventArgs e)
        {
            cb121.IsChecked = false;
            cb123.IsChecked = false;
        }

        private void cb123_Checked(object sender, RoutedEventArgs e)
        {
            cb121.IsChecked = false;
            cb122.IsChecked = false;
            
        }

        private void cb131_Checked(object sender, RoutedEventArgs e)
        {
            cb132.IsChecked = false;
            cb133.IsChecked = false;
            cb134.IsChecked = false;
            cb135.IsChecked = false;
        }

        private void cb132_Checked(object sender, RoutedEventArgs e)
        {
            cb131.IsChecked = false;
            cb133.IsChecked = false;
            cb134.IsChecked = false;
            cb135.IsChecked = false;
        }

        private void cb133_Checked(object sender, RoutedEventArgs e)
        {
            cb131.IsChecked = false;
            cb132.IsChecked = false;
            cb134.IsChecked = false;
            cb135.IsChecked = false;
        }

        private void cb134_Checked(object sender, RoutedEventArgs e)
        {
            cb131.IsChecked = false;
            cb132.IsChecked = false;
            cb133.IsChecked = false;
            cb135.IsChecked = false;
        }

        private void cb135_Checked(object sender, RoutedEventArgs e)
        {
            cb131.IsChecked = false;
            cb132.IsChecked = false;
            cb133.IsChecked = false;
            cb134.IsChecked = false;

        }

        private void cb141_Checked(object sender, RoutedEventArgs e)
        {
            cb142.IsChecked = false;
        }

        private void cb142_Checked(object sender, RoutedEventArgs e)
        {
            cb141.IsChecked = false;
        }

        private void cb151_Checked(object sender, RoutedEventArgs e)
        {
            cb152.IsChecked = false;
            cb153.IsChecked = false;
            cb154.IsChecked = false;
            cb155.IsChecked = false;
            cb156.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb152_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb153.IsChecked = false;
            cb154.IsChecked = false;
            cb155.IsChecked = false;
            cb156.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb153_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb152.IsChecked = false;
            cb154.IsChecked = false;
            cb155.IsChecked = false;
            cb156.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb154_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb152.IsChecked = false;
            cb153.IsChecked = false;
            cb155.IsChecked = false;
            cb156.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb155_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb152.IsChecked = false;
            cb153.IsChecked = false;
            cb154.IsChecked = false;
            cb156.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb156_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb152.IsChecked = false;
            cb153.IsChecked = false;
            cb154.IsChecked = false;
            cb155.IsChecked = false;
            cb157.IsChecked = false;
        }

        private void cb157_Checked(object sender, RoutedEventArgs e)
        {
            cb151.IsChecked = false;
            cb152.IsChecked = false;
            cb153.IsChecked = false;
            cb154.IsChecked = false;
            cb155.IsChecked = false;
            cb156.IsChecked = false;

        }

        private void cb161_Checked(object sender, RoutedEventArgs e)
        {
            cb162.IsChecked = false;
            cb163.IsChecked = false;
            cb164.IsChecked = false;
        }

        private void cb162_Checked(object sender, RoutedEventArgs e)
        {
            cb161.IsChecked = false;
            cb163.IsChecked = false;
            cb164.IsChecked = false;
        }

        private void cb163_Checked(object sender, RoutedEventArgs e)
        {
            cb161.IsChecked = false;
            cb162.IsChecked = false;
            cb164.IsChecked = false;
        }

        private void cb164_Checked(object sender, RoutedEventArgs e)
        {
            cb161.IsChecked = false;
            cb162.IsChecked = false;
            cb163.IsChecked = false;

        }

        private void cb171_Checked(object sender, RoutedEventArgs e)
        {
            cb172.IsChecked = false;
            cb173.IsChecked = false;
            cb174.IsChecked = false;
            cb175.IsChecked = false;
        }

        private void cb172_Checked(object sender, RoutedEventArgs e)
        {
            cb171.IsChecked = false;
            cb173.IsChecked = false;
            cb174.IsChecked = false;
            cb175.IsChecked = false;
        }

        private void cb173_Checked(object sender, RoutedEventArgs e)
        {
            cb171.IsChecked = false;
            cb172.IsChecked = false;
            cb174.IsChecked = false;
            cb175.IsChecked = false;
        }

        private void cb174_Checked(object sender, RoutedEventArgs e)
        {
            cb171.IsChecked = false;
            cb172.IsChecked = false;
            cb173.IsChecked = false;
            cb175.IsChecked = false;
        }

        private void cb175_Checked(object sender, RoutedEventArgs e)
        {
            cb171.IsChecked = false;
            cb172.IsChecked = false;
            cb173.IsChecked = false;
            cb174.IsChecked = false;

        }

        private void cb181_Checked(object sender, RoutedEventArgs e)
        {
            cb182.IsChecked = false;
        }

        private void cb182_Checked(object sender, RoutedEventArgs e)
        {
            cb181.IsChecked = false;
        }

    }
}
