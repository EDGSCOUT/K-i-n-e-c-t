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

namespace Saving
{
    /// <summary>
    /// Saving.xaml 的交互逻辑
    /// </summary>
    public partial class Saving : UserControl
    {
        public int PageNum;

        public Saving(int PageNum)
        {
            InitializeComponent();
            this.PageNum = PageNum;
            this.Next.IsEnabled = false;
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
