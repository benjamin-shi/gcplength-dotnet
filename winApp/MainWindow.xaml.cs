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

using benjaminshi.gs1;

namespace winApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            GCPLength.Refresh();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            string prefix = textboxPrefix.Text;

            bool isExists = GCPLength.Exists(prefix);
            
            string str = "";

            int len = GCPLength.Find(prefix, out str);

            string messgae = "'" + prefix + "' - " + (isExists?"true":"false") + " ('" + str + "':" + len.ToString() + ")";

            labelResult.Content = messgae;
        }
    }
}
