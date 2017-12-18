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
using System.Threading;

namespace DataIntegration
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.state.Visibility = Visibility.Hidden;
            this.close.Visibility = Visibility.Hidden;
            this.doing.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Console.WriteLine(folderBrowserDialog.SelectedPath);
                
                this.start.Visibility = Visibility.Hidden;
                this.guide.Visibility = Visibility.Hidden;
                this.doing.Visibility = Visibility.Visible;

                string path = folderBrowserDialog.SelectedPath;
                Thread t = new Thread(delegate() 
                {
                    AllIntegration(path);
                }); 
                t.Start();
                
            }

        }

        private void AllIntegration(string path)
        {
            DirectoryInfo  theFolder = new DirectoryInfo(path);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                Console.WriteLine(NextFolder.Name);
                try
                {
                    OneIntegration(path,int.Parse(NextFolder.Name));
                }
                catch (Exception e)
                {

                }
            }
            this.Dispatcher.BeginInvoke(new Action(() => this.state.Visibility = Visibility.Visible));
            this.Dispatcher.BeginInvoke(new Action(() => this.close.Visibility = Visibility.Visible));
            this.Dispatcher.BeginInvoke(new Action(() => this.doing.Visibility = Visibility.Hidden));
            
        }

        private void OneIntegration(string path, int userid)
        {
            TimeIntegration(path,userid);
            EmotionIntegration(path,userid);
        }

        private void TimeIntegration(string path, int userid)
        {
            bool nofile = true;
            for (int i = 1; i <= 24; i++)
            {
                if (i < 10)
                {
                    if (File.Exists(path + "\\" + userid + "\\time0" + i + ".csv"))
                    {
                        nofile = false;
                    }
                }
                else
                {
                    if (File.Exists(path + "\\" + userid + "\\time" + i + ".csv"))
                    {
                        nofile = false;
                    }
                }   
            }

            if (nofile)
                return;

            String[] time = new String[49];
            string strline;
            string[] aryline;

            for (int i = 0; i <= 48; i++)
            {
                time[i] = "";
            }

            for (int i = 1; i <= 24; i++)
            {
                if (i < 10)
                {
                    if (!File.Exists(path + "\\" + userid + "\\time0" + i + ".csv"))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!File.Exists(path + "\\" + userid + "\\time" + i + ".csv"))
                    {
                        continue;
                    }
                }   

                
                StreamReader mysr;
                if(i<10)
                    mysr = new StreamReader(path + "\\" + userid + "\\time0" + i + ".csv", System.Text.Encoding.Default);
                else
                    mysr = new StreamReader(path + "\\" + userid + "\\time" + i + ".csv", System.Text.Encoding.Default);

                int j = 1;

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    time[(i - 1) * 2 + j] = aryline[0];
                    j++;
                }
                mysr.Close();
            }


            FileStream fs = new FileStream(path + "\\" + userid + "\\time.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 1; i <= 24; i++)
            {
                if (i < 10)
                {
                    sw.Write("time0" + i + " Start," + time[(i - 1) * 2 + 1]);
                    sw.WriteLine();
                    sw.Write("time0" + i + " End," + time[(i - 1) * 2 + 2]);
                    sw.WriteLine();
                }
                else 
                {
                    sw.Write("time" + i + " Start," + time[(i - 1) * 2 + 1]);
                    sw.WriteLine();
                    sw.Write("time" + i + " End," + time[(i - 1) * 2 + 2]);
                    sw.WriteLine();
                }
                
            }
            sw.Close();
            fs.Close();
        }

        private void EmotionIntegration(string path, int userid)
        {
            bool nofile = true;

            if (File.Exists(path + "\\" + userid + "\\Emotion-视频正性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-视频中性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-视频负性.txt"))
            {
                nofile = false;
            }
            if (File.Exists(path + "\\" + userid + "\\Emotion-访谈正性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-访谈中性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-访谈负性.txt"))
            {
                nofile = false;
            }
            if (File.Exists(path + "\\" + userid + "\\Emotion-文本朗读正性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-文本朗读中性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-文本朗读负性.txt"))
            {
                nofile = false;
            }
            if (File.Exists(path + "\\" + userid + "\\Emotion-面部图片正性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-面部图片中性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-面部图片负性.txt"))
            {
                nofile = false;
            }
            if (File.Exists(path + "\\" + userid + "\\Emotion-图片正性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-图片中性.txt") || File.Exists(path + "\\" + userid + "\\Emotion-图片负性.txt"))
            {
                nofile = false;
            }


            if (nofile)
                return;


            String[] YuYueQian = new String[22];
            String[] YuYueHou = new String[22];
            String[] HuanXingQian = new String[22];
            String[] HuanXingHou = new String[22];
            String[] cha = new String[43];

            for (int i = 0; i <= 21; i++)
            {
                YuYueQian[i] = "";
                YuYueHou[i] = "";
                HuanXingQian[i] = "";
                HuanXingHou[i] = "";
            }

            for (int i = 0; i <= 42; i++)
            {
                cha[i] = "";
            }

            StreamReader mysr;
            String[] line = new String[5];

            String EmotionType1 = null, EmotionType2 = null, EmotionType3 = null;

            int usertype = (userid % 1000) % 6;
            switch (usertype)
            {
                case 0: EmotionType1 = "负"; EmotionType2 = "中"; EmotionType3 = "正"; break;
                case 1: EmotionType1 = "正"; EmotionType2 = "中"; EmotionType3 = "负"; break;
                case 2: EmotionType1 = "正"; EmotionType2 = "负"; EmotionType3 = "中"; break;
                case 3: EmotionType1 = "中"; EmotionType2 = "负"; EmotionType3 = "正"; break;
                case 4: EmotionType1 = "中"; EmotionType2 = "正"; EmotionType3 = "负"; break;
                case 5: EmotionType1 = "负"; EmotionType2 = "正"; EmotionType3 = "中"; break;
            }

            if (File.Exists(path + "\\" + userid + "\\Emotion-视频" + EmotionType1 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-视频" + EmotionType1 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 4; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                YuYueQian[1] = line[1].ToString().Split('：')[1];
                YuYueQian[2] = line[1].ToString().Split('：')[1];
                YuYueQian[3] = line[1].ToString().Split('：')[1];

                HuanXingQian[1] = line[2].ToString().Split('：')[1];
                HuanXingQian[2] = line[2].ToString().Split('：')[1];
                HuanXingQian[3] = line[2].ToString().Split('：')[1];


                try
                {
                    if (EmotionType1 == "正")
                    {
                        YuYueHou[1] = line[3].ToString().Split('：')[1];
                        HuanXingHou[1] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "中")
                    {
                        YuYueHou[2] = line[3].ToString().Split('：')[1];
                        HuanXingHou[2] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "负")
                    {
                        YuYueHou[3] = line[3].ToString().Split('：')[1];
                        HuanXingHou[3] = line[4].ToString().Split('：')[1];
                    }
                }
                catch (Exception e)
                {

                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-视频" + EmotionType2 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-视频" + EmotionType2 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType2 == "正")
                {
                    YuYueHou[1] = line[1].ToString().Split('：')[1];
                    HuanXingHou[1] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "中")
                {
                    YuYueHou[2] = line[1].ToString().Split('：')[1];
                    HuanXingHou[2] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "负")
                {
                    YuYueHou[3] = line[1].ToString().Split('：')[1];
                    HuanXingHou[3] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-视频" + EmotionType3 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-视频" + EmotionType3 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType3 == "正")
                {
                    YuYueHou[1] = line[1].ToString().Split('：')[1];
                    HuanXingHou[1] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "中")
                {
                    YuYueHou[2] = line[1].ToString().Split('：')[1];
                    HuanXingHou[2] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "负")
                {
                    YuYueHou[3] = line[1].ToString().Split('：')[1];
                    HuanXingHou[3] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-访谈" + EmotionType1 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-访谈" + EmotionType1 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 4; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                YuYueQian[4] = line[1].ToString().Split('：')[1];
                YuYueQian[5] = line[1].ToString().Split('：')[1];
                YuYueQian[6] = line[1].ToString().Split('：')[1];
                YuYueQian[7] = line[1].ToString().Split('：')[1];
                YuYueQian[8] = line[1].ToString().Split('：')[1];
                YuYueQian[9] = line[1].ToString().Split('：')[1];
                YuYueQian[10] = line[1].ToString().Split('：')[1];
                YuYueQian[11] = line[1].ToString().Split('：')[1];
                YuYueQian[12] = line[1].ToString().Split('：')[1];


                HuanXingQian[4] = line[2].ToString().Split('：')[1];
                HuanXingQian[5] = line[2].ToString().Split('：')[1];
                HuanXingQian[6] = line[2].ToString().Split('：')[1];
                HuanXingQian[7] = line[2].ToString().Split('：')[1];
                HuanXingQian[8] = line[2].ToString().Split('：')[1];
                HuanXingQian[9] = line[2].ToString().Split('：')[1];
                HuanXingQian[10] = line[2].ToString().Split('：')[1];
                HuanXingQian[11] = line[2].ToString().Split('：')[1];
                HuanXingQian[12] = line[2].ToString().Split('：')[1];


                try
                {
                    if (EmotionType1 == "正")
                    {
                        YuYueHou[4] = line[3].ToString().Split('：')[1];
                        YuYueHou[5] = line[3].ToString().Split('：')[1];
                        YuYueHou[6] = line[3].ToString().Split('：')[1];

                        HuanXingHou[4] = line[4].ToString().Split('：')[1];
                        HuanXingHou[5] = line[4].ToString().Split('：')[1];
                        HuanXingHou[6] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "中")
                    {
                        YuYueHou[7] = line[3].ToString().Split('：')[1];
                        YuYueHou[8] = line[3].ToString().Split('：')[1];
                        YuYueHou[9] = line[3].ToString().Split('：')[1];

                        HuanXingHou[7] = line[4].ToString().Split('：')[1];
                        HuanXingHou[8] = line[4].ToString().Split('：')[1];
                        HuanXingHou[9] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "负")
                    {
                        YuYueHou[10] = line[3].ToString().Split('：')[1];
                        YuYueHou[11] = line[3].ToString().Split('：')[1];
                        YuYueHou[12] = line[3].ToString().Split('：')[1];

                        HuanXingHou[10] = line[4].ToString().Split('：')[1];
                        HuanXingHou[11] = line[4].ToString().Split('：')[1];
                        HuanXingHou[12] = line[4].ToString().Split('：')[1];
                    }
                }
                catch (Exception e)
                {

                }


                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-访谈" + EmotionType2 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-访谈" + EmotionType2 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType2 == "正")
                {
                    YuYueHou[4] = line[1].ToString().Split('：')[1];
                    YuYueHou[5] = line[1].ToString().Split('：')[1];
                    YuYueHou[6] = line[1].ToString().Split('：')[1];

                    HuanXingHou[4] = line[2].ToString().Split('：')[1];
                    HuanXingHou[5] = line[2].ToString().Split('：')[1];
                    HuanXingHou[6] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "中")
                {
                    YuYueHou[7] = line[1].ToString().Split('：')[1];
                    YuYueHou[8] = line[1].ToString().Split('：')[1];
                    YuYueHou[9] = line[1].ToString().Split('：')[1];

                    HuanXingHou[7] = line[2].ToString().Split('：')[1];
                    HuanXingHou[8] = line[2].ToString().Split('：')[1];
                    HuanXingHou[9] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "负")
                {
                    YuYueHou[10] = line[1].ToString().Split('：')[1];
                    YuYueHou[11] = line[1].ToString().Split('：')[1];
                    YuYueHou[12] = line[1].ToString().Split('：')[1];

                    HuanXingHou[10] = line[2].ToString().Split('：')[1];
                    HuanXingHou[11] = line[2].ToString().Split('：')[1];
                    HuanXingHou[12] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-访谈" + EmotionType3 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-访谈" + EmotionType3 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType3 == "正")
                {
                    YuYueHou[4] = line[1].ToString().Split('：')[1];
                    YuYueHou[5] = line[1].ToString().Split('：')[1];
                    YuYueHou[6] = line[1].ToString().Split('：')[1];

                    HuanXingHou[4] = line[2].ToString().Split('：')[1];
                    HuanXingHou[5] = line[2].ToString().Split('：')[1];
                    HuanXingHou[6] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "中")
                {
                    YuYueHou[7] = line[1].ToString().Split('：')[1];
                    YuYueHou[8] = line[1].ToString().Split('：')[1];
                    YuYueHou[9] = line[1].ToString().Split('：')[1];

                    HuanXingHou[7] = line[2].ToString().Split('：')[1];
                    HuanXingHou[8] = line[2].ToString().Split('：')[1];
                    HuanXingHou[9] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "负")
                {
                    YuYueHou[10] = line[1].ToString().Split('：')[1];
                    YuYueHou[11] = line[1].ToString().Split('：')[1];
                    YuYueHou[12] = line[1].ToString().Split('：')[1];

                    HuanXingHou[10] = line[2].ToString().Split('：')[1];
                    HuanXingHou[11] = line[2].ToString().Split('：')[1];
                    HuanXingHou[12] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType1 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType1 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 4; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                YuYueQian[13] = line[1].ToString().Split('：')[1];
                YuYueQian[14] = line[1].ToString().Split('：')[1];
                YuYueQian[15] = line[1].ToString().Split('：')[1];

                HuanXingQian[13] = line[2].ToString().Split('：')[1];
                HuanXingQian[14] = line[2].ToString().Split('：')[1];
                HuanXingQian[15] = line[2].ToString().Split('：')[1];


                try
                {
                    if (EmotionType1 == "正")
                    {
                        YuYueHou[13] = line[3].ToString().Split('：')[1];
                        HuanXingHou[13] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "中")
                    {
                        YuYueHou[14] = line[3].ToString().Split('：')[1];
                        HuanXingHou[14] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "负")
                    {
                        YuYueHou[15] = line[3].ToString().Split('：')[1];
                        HuanXingHou[15] = line[4].ToString().Split('：')[1];
                    }
                }
                catch (Exception e)
                {

                }


                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType2 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType2 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType2 == "正")
                {
                    YuYueHou[13] = line[1].ToString().Split('：')[1];
                    HuanXingHou[13] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "中")
                {
                    YuYueHou[14] = line[1].ToString().Split('：')[1];
                    HuanXingHou[14] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "负")
                {
                    YuYueHou[15] = line[1].ToString().Split('：')[1];
                    HuanXingHou[15] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType3 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-文本朗读" + EmotionType3 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType3 == "正")
                {
                    YuYueHou[13] = line[1].ToString().Split('：')[1];
                    HuanXingHou[13] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "中")
                {
                    YuYueHou[14] = line[1].ToString().Split('：')[1];
                    HuanXingHou[14] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "负")
                {
                    YuYueHou[15] = line[1].ToString().Split('：')[1];
                    HuanXingHou[15] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            if (File.Exists(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType1 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType1 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 4; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                YuYueQian[16] = line[1].ToString().Split('：')[1];
                YuYueQian[17] = line[1].ToString().Split('：')[1];
                YuYueQian[18] = line[1].ToString().Split('：')[1];

                HuanXingQian[16] = line[2].ToString().Split('：')[1];
                HuanXingQian[17] = line[2].ToString().Split('：')[1];
                HuanXingQian[18] = line[2].ToString().Split('：')[1];



                try
                {
                    if (EmotionType1 == "正")
                    {
                        YuYueHou[16] = line[3].ToString().Split('：')[1];
                        HuanXingHou[16] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "中")
                    {
                        YuYueHou[17] = line[3].ToString().Split('：')[1];
                        HuanXingHou[17] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "负")
                    {
                        YuYueHou[18] = line[3].ToString().Split('：')[1];
                        HuanXingHou[18] = line[4].ToString().Split('：')[1];
                    }
                }
                catch (Exception e)
                {

                }


                mysr.Close();
            }




            if (File.Exists(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType2 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType2 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType2 == "正")
                {
                    YuYueHou[16] = line[1].ToString().Split('：')[1];
                    HuanXingHou[16] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "中")
                {
                    YuYueHou[17] = line[1].ToString().Split('：')[1];
                    HuanXingHou[17] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "负")
                {
                    YuYueHou[18] = line[1].ToString().Split('：')[1];
                    HuanXingHou[18] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }




            if (File.Exists(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType3 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-面部图片" + EmotionType3 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType3 == "正")
                {
                    YuYueHou[16] = line[1].ToString().Split('：')[1];
                    HuanXingHou[16] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "中")
                {
                    YuYueHou[17] = line[1].ToString().Split('：')[1];
                    HuanXingHou[17] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "负")
                {
                    YuYueHou[18] = line[1].ToString().Split('：')[1];
                    HuanXingHou[18] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }




            if (File.Exists(path + "\\" + userid + "\\Emotion-图片" + EmotionType1 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-图片" + EmotionType1 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 4; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                YuYueQian[19] = line[1].ToString().Split('：')[1];
                YuYueQian[20] = line[1].ToString().Split('：')[1];
                YuYueQian[21] = line[1].ToString().Split('：')[1];

                HuanXingQian[19] = line[2].ToString().Split('：')[1];
                HuanXingQian[20] = line[2].ToString().Split('：')[1];
                HuanXingQian[21] = line[2].ToString().Split('：')[1];



                try
                {
                    if (EmotionType1 == "正")
                    {
                        YuYueHou[19] = line[3].ToString().Split('：')[1];
                        HuanXingHou[19] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "中")
                    {
                        YuYueHou[20] = line[3].ToString().Split('：')[1];
                        HuanXingHou[20] = line[4].ToString().Split('：')[1];
                    }
                    else if (EmotionType1 == "负")
                    {
                        YuYueHou[21] = line[3].ToString().Split('：')[1];
                        HuanXingHou[21] = line[4].ToString().Split('：')[1];
                    }
                }
                catch (Exception e)
                {

                }


                mysr.Close();
            }




            if (File.Exists(path + "\\" + userid + "\\Emotion-图片" + EmotionType2 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-图片" + EmotionType2 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType2 == "正")
                {
                    YuYueHou[19] = line[1].ToString().Split('：')[1];
                    HuanXingHou[19] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "中")
                {
                    YuYueHou[20] = line[1].ToString().Split('：')[1];
                    HuanXingHou[20] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType2 == "负")
                {
                    YuYueHou[21] = line[1].ToString().Split('：')[1];
                    HuanXingHou[21] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }




            if (File.Exists(path + "\\" + userid + "\\Emotion-图片" + EmotionType3 + "性.txt"))
            {
                mysr = new StreamReader(path + "\\" + userid + "\\Emotion-图片" + EmotionType3 + "性.txt", System.Text.Encoding.UTF8);

                for (int i = 1; i <= 2; i++)
                {
                    line[i] = mysr.ReadLine();
                }

                if (EmotionType3 == "正")
                {
                    YuYueHou[19] = line[1].ToString().Split('：')[1];
                    HuanXingHou[19] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "中")
                {
                    YuYueHou[20] = line[1].ToString().Split('：')[1];
                    HuanXingHou[20] = line[2].ToString().Split('：')[1];
                }
                else if (EmotionType3 == "负")
                {
                    YuYueHou[21] = line[1].ToString().Split('：')[1];
                    HuanXingHou[21] = line[2].ToString().Split('：')[1];
                }

                mysr.Close();
            }



            FileStream fs = new FileStream(path + "\\" + userid + "\\emotion.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(",difference,front,back");
            sw.WriteLine();

            for (int i = 1; i <= 21; i++)
            {
                try
                {
                    cha[(i - 1) * 2 + 1] = "" + (int.Parse(YuYueHou[i]) - int.Parse(YuYueQian[i]));
                }
                catch (Exception e)
                {
                    
                }

                try
                {
                    cha[(i - 1) * 2 + 2] = "" + (int.Parse(HuanXingHou[i]) - int.Parse(HuanXingQian[i]));
                }
                catch (Exception e)
                {

                }
            }


            for (int i = 1; i <= 21; i++)
            {
                if (i < 10)
                {
                    sw.Write("0" + i + " Valence," + cha[(i - 1) * 2 + 1] + "," + YuYueQian[i] + "," + YuYueHou[i]);
                    sw.WriteLine();
                    sw.Write("0" + i + " Arousal," + cha[(i - 1) * 2 + 2] + "," + HuanXingQian[i] + "," + HuanXingHou[i]);
                    sw.WriteLine();
                }
                else
                {
                    sw.Write("" + i + " Valence," + cha[(i - 1) * 2 + 1] + "," + YuYueQian[i] + "," + YuYueHou[i]);
                    sw.WriteLine();
                    sw.Write("" + i + " Arousal," + cha[(i - 1) * 2 + 2] + "," + HuanXingQian[i] + "," + HuanXingHou[i]);
                    sw.WriteLine();
                }
            }
            sw.Close();
            fs.Close();

            
        }

        private void close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
