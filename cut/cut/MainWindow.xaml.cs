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

namespace cut
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        FileStream fs = null;
        StreamWriter sw = null;

        string filepath = "C:\\Users\\ad\\Desktop\\数据6月11日\\demographic_information";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //string str = "Robynne,1496762022,1:2;2:1977;3:Brooklyn, ny;4:5;5:Urban studies;6:3;7:Askenazi Jewish;8:Jewish;9:40;10:4;10-6:;11-1:6;11-2:6;11-3:5;11-4:5;11-5:4;11-6:6;11-7:7;11-8:7;11-9:7;11-10:6;11-11:4;11-12:6;11-13:6;11-14:7;11-15:7;11-16:3;11-17:7;11-18:1;11-19:7;11-20:3;q1:2;q2:2;";
            //convertString(str);

            FileStream fr = new FileStream(filepath + ".csv", System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader sr = new StreamReader(fr, Encoding.UTF8);
            string strLine = "";
            int time = 0;
            while ((strLine = sr.ReadLine()) != null)
            {
                string[] Array = strLine.Split(',');
                string answer = "";
                for (int i = 2; i < Array.Length; i++)
                {
                    answer = answer + Array[i];
                }
                if(string.Equals(answer[0], '\"') == true)
                    answer = answer.Substring(1, answer.Length-2);

                answer = strLine.Split(',')[0] + "," + strLine.Split(',')[1] + "," + answer;
                convertString(answer,time);
                time = 1;
            }
            sr.Close();
            fr.Close();
        }

        private void convertString(string s,int time)
        {
            string part1 = s.Split(',')[0] + "," + s.Split(',')[1] + ",";
            string part2 = "";
            string[] Array = s.Split(',');
            for (int i = 2; i < Array.Length; i++)
            {
                part2 = part2 + Array[i];
            }

            string part2s = cutString2(part2);
            string sok = part1 + part2s;
            //Console.WriteLine(sok);

            string index = "name,timestamp,";
            index = index + cutString1(part2);
            //Console.WriteLine(index);

            if(time == 0)
            {
                fs = new FileStream(filepath + "2.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(index);
                sw.WriteLine(sok);
            }    
            else    
            {
                fs = new FileStream(filepath + "2.csv", FileMode.Append);
                sw = new StreamWriter(fs);
                sw.WriteLine(sok);
            }    
               
            sw.Close();
            fs.Close();

        }

        private string cutString1(string answer)
        {
            string[] Array = answer.Split(';');
            for (int i = 0; i < Array.Length; i++)
            {
                try
                {
                    Array[i] = Array[i].Split(':')[0];

                }
                catch (Exception e)
                {
                    Array[i] = "";
                }
            }

            string s = "";
            for (int i = 0; i <= Array.Length - 2; i++)
                s = s + Array[i].ToString() + ",";

            return s;
        }

        private string cutString2(string answer)
        {
            string[] Array = answer.Split(';');
            for (int i = 0; i < Array.Length; i++)
            {
                try
                {
                    Array[i] = Array[i].Split(':')[1];

                }
                catch (Exception e)
                {
                    Array[i] = "";
                }
            }

            string s = "";
            for(int i = 0;i<=Array.Length-2;i++)
                s = s + Array[i].ToString() + ",";

            return s;
        }

    }
}
