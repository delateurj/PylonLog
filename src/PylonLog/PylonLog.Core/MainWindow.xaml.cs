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
using PylonLogGraphUserControl;
using System.Windows.Forms.Integration;

namespace PylonLog.Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string integrationTestLog = "C:\\Users\\djoe\\Dropbox\\Programming\\PylonLog\\TestData\\Log.TLM";

        private SpektrumLog spektrumLog = new SpektrumLog(integrationTestLog);

        private PylonLogGraphUserControl.PylonLogGraphUserControl graph;

        public MainWindow()
        {
            InitializeComponent();

            DisplayTestLogData();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void DisplayTestLogData()
        {
            spektrumLog = new SpektrumLog(integrationTestLog);

            WindowsFormsHost host = new WindowsFormsHost();

            List<Double[]> firstGraphData = spektrumLog.logSessions[1].getSelectedDataBlocks("RPM");

            List<Double[]> secondGraphData = spektrumLog.logSessions[1].getSelectedDataBlocks("RX-VOLT");

            graph = new PylonLogGraphUserControl.PylonLogGraphUserControl(firstGraphData, secondGraphData);

            host.Child = graph;
            MainGrid.Children.Add(host);
            this.Width = graph.Width + 2 * 20;
            this.Height = graph.Height + 3 * 20;

        }
    }
}

