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
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace VideoCut
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //FileInfo fi = new FileInfo("C:\\Users\\ad\\Documents\\Bandicam\\333.avi");
            //DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);  
            //label.Content = fi.CreationTime.ToString();
            //label.Content = Convert.ToInt32((fi.CreationTime - DateStart).TotalSeconds);
        }

        private void CutOneSlice(String FilePath,int Index,Double StartTimeStamp, Double EndTimeStamp)
        {
            ProcessStartInfo ps = new ProcessStartInfo();
            //ps.CreateNoWindow = true;
            //ps.WindowStyle = ProcessWindowStyle.Hidden;
            ps.FileName = "..\\..\\\\ffmpeg\\bin\\ffmpeg.exe";

            String RecordTimeString,StartTimeString, EndTimeString,OutputFilePath,FolderPath = "";
            String []temp;

            temp = FilePath.Split('\\');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                FolderPath = FolderPath + temp[i] + "\\";
            }
            
            RecordTimeString = temp[temp.Length - 1].Split('.')[0];
            StartTimeString = TimeStampConvertToClock(StartTimeStamp - DateConvertToTimeStamp(RecordTimeString));
            EndTimeString = TimeStampConvertToClock(EndTimeStamp - DateConvertToTimeStamp(RecordTimeString));

            if (Directory.Exists(FolderPath + "\\" + "VideoClip" + "\\") == false)
            {
                Directory.CreateDirectory(FolderPath + "\\" + "VideoClip" + "\\");
            }

            if(Index < 10)
                OutputFilePath = FolderPath + "\\" + "VideoClip" + "\\0" + Index + ".avi";
            else
                OutputFilePath = FolderPath + "\\" + "VideoClip" + "\\" + Index + ".avi";

            ps.Arguments = " -i " + FilePath + " -ss " + StartTimeString + " -s 1920*1080 -r 30 -b 20000000" + " -acodec copy " + " -to " + EndTimeString + " " + OutputFilePath;         
                             
            Process proc = new Process();
            proc.StartInfo = ps;
            //proc.WaitForExit();//不等待完成就不调用此方法
            proc.Start(); 
        }

        private void CutOnePerson(String FilePath)
        {
            String[] time = new String[49];
            String strline;
            String[] aryline;
            String FolderPath = "";
            String[] temp;

            temp = FilePath.Split('\\');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                FolderPath = FolderPath + temp[i] + "\\";
            }

            for (int i = 0; i <= 48; i++)
            {
                time[i] = "";
            }

            for (int i = 1; i <= 24; i++)
            {
                if (i < 10)
                {
                    if (!File.Exists(FolderPath + "time0" + i + ".csv"))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!File.Exists(FolderPath + "time" + i + ".csv"))
                    {
                        continue;
                    }
                }


                StreamReader mysr;
                if (i < 10)
                    mysr = new StreamReader(FolderPath + "time0" + i + ".csv", System.Text.Encoding.Default);
                else
                    mysr = new StreamReader(FolderPath + "time" + i + ".csv", System.Text.Encoding.Default);

                int j = 1;

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    time[(i - 1) * 2 + j] = aryline[0];
                    j++;
                }
                mysr.Close();
            }

            for (int i = 1; i <= 24; i++)
                CutOneSlice(FilePath, i, Double.Parse(time[(i - 1) * 2 + 1]), Double.Parse(time[(i - 1) * 2 + 2]));
        }

        private String TimeStampConvertToClock(Double TimeStamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddSeconds(TimeStamp);
            newDateTime = newDateTime.ToLocalTime().AddHours(-8);
            return newDateTime.ToString().Split(' ')[1];  
        }

        private Double DateConvertToTimeStamp(String DateString)
        {
            DateTime Date = Convert.ToDateTime(DateString.Replace("：", ":").Replace("_", " "));  
            TimeSpan span = (Date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (double)span.TotalSeconds;  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //label.Content = TimeStampConvertToClock(1467858967);
            //label.Content = "2016‎-‎7-7‎ ‏‎10：36：07".Replace("：", ":");
            //label.Content = DateConvertToTimeStamp("2016-7-7 10：36：07").ToString();

            //label.Content = TimeStampConvertToClock(1467859700 - 1467858967);
            //label.Content = "ffmpeg\\bin\\ffmpeg.exe".Split('\\')["ffmpeg\\bin\\ffmpeg.exe".Split('\\').Length - 1].Split('.')[0];

            //CutOneSlice("E:\\VoiceData\\2410101\\2016-7-7_10：36：07.avi", 1, 1467859865, 1467859949);

            //CutOnePerson("E:\\VoiceData\\2410101");

            /*
            String str = "E:\\VoiceData\\2410101\\2016-7-7_10：36：07.avi";
            Regex reg = new Regex(@"\d+-\d+-\d+_\d+：\d+：\d+.avi"); 
            var mat = reg.Match(str);
            if(reg.IsMatch(str))
                Console.WriteLine(mat.Groups[0]);
            */


            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Console.WriteLine(folderBrowserDialog.SelectedPath);

                //this.start.Visibility = Visibility.Hidden;
                //this.guide.Visibility = Visibility.Hidden;
                //this.doing.Visibility = Visibility.Visible;

                string path = folderBrowserDialog.SelectedPath;

                DirectoryInfo folder = new DirectoryInfo(path);

                foreach (FileInfo file in folder.GetFiles())
                {
                    Regex reg = new Regex(@"\d+-\d+-\d+_\d+：\d+：\d+.avi");
                    var mat = reg.Match(file.FullName);
                    if (reg.IsMatch(file.FullName))
                        CutOnePerson(file.FullName);
                        //Console.WriteLine(file.FullName);
                }

            }
        }
    }
}
