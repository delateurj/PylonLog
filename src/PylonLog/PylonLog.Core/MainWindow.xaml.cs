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
using System.Windows.Forms;
using ZedGraphUserControl;
using System.Windows.Forms.Integration;

namespace PylonLog.Core
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            
            ZedGraphUserControl.ZedGraphUserControl graph = new ZedGraphUserControl.ZedGraphUserControl();
            host.Child = graph;
            MainGrid.Children.Add(host);
            this.Width = graph.Width + 2 * 20;
            this.Height = graph.Height + 3 * 20;
        }

    }
}

