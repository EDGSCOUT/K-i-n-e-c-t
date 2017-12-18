using System;
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

namespace Report
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : UserControl
    {
        public int PageNum;

        public Report(int PageNum, bool OK1, bool OK2, bool OK3)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            if(OK1)
                this.label1.Content = "是";
            else
                this.label1.Content = "否";

            if (OK2)
                this.label2.Content = "是";
            else
                this.label2.Content = "否";

            if (OK3)
                this.label3.Content = "是";
            else
                this.label3.Content = "否";

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NextPage();
        }

        private void NextPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.PreviousPage(PageNum);
        }

    }
}
