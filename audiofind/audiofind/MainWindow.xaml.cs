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

namespace audiofind
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<String> TextFileList = new List<String>();
        List<String> AudioFileList = new List<String>();
        List<String> IntersectionFileList_T = new List<String>();
        List<String> IntersectionFileList_A = new List<String>();
        
        List<String> AudioFileOriginList = new List<String>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void getFileList()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;

                DirectoryInfo folder1 = new DirectoryInfo(path);

                foreach (FileInfo file in folder1.GetFiles())
                {
                    TextFileList.Add(file.Name);
                }



                path = path + "\\yuyin";

                DirectoryInfo folder2 = new DirectoryInfo(path);

                foreach (FileInfo file in folder2.GetFiles())
                {
                    AudioFileOriginList.Add(file.Name);
                    string filename = file.Name;
                    filename = filename.Replace(".", "");
                    filename = filename.Replace(",", "");
                    filename = filename.Replace(" ", "");
                    filename = filename.Substring(0,filename.Length - 3);

                    AudioFileList.Add(filename);
                }

                Intersection(TextFileList, AudioFileList);

            }
        }

        public void Intersection(List<String> textlist, List<String> audiolist)
        {
            foreach (string text in textlist)
            {
                foreach (string audio in audiolist)
                {
                    if (string.Equals(text, audio, StringComparison.CurrentCultureIgnoreCase))
                    {
                        IntersectionFileList_T.Add(text);
                        IntersectionFileList_A.Add(audio);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tb1.Clear();
            tb2.Clear();

            getFileList();

            foreach (string filename in IntersectionFileList_T)
                TextFileList.Remove(filename);

            foreach (string filename in IntersectionFileList_A)
                AudioFileList.Remove(filename);
            /*
            Console.WriteLine("文本删除列表：");
            foreach (string textfilename in TextFileList)
                Console.WriteLine(textfilename);
            Console.WriteLine(" ");
            Console.WriteLine("语音删除列表：");
            foreach (string audiofilename in AudioFileList)
                Console.WriteLine(audiofilename);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            */

            foreach (string textfilename in TextFileList)
                tb1.AppendText(textfilename + "\n");

            foreach (string audiofilename in AudioFileList)
                tb2.AppendText(audiofilename + "\n");

            TextFileList.Clear();
            AudioFileList.Clear();
            IntersectionFileList_T.Clear();
            IntersectionFileList_A.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;

                DirectoryInfo theFolder = new DirectoryInfo(path);
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();

                foreach (DirectoryInfo NextFolder in dirInfo)
                {
                    if (!Directory.Exists(NextFolder.FullName + "\\yuyin"))
                        Directory.CreateDirectory(NextFolder.FullName + "\\yuyin");
                    
                }

            }
        }
    }
}
