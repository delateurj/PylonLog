using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Log Files (*.TLM)|*.TLM";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                spektrumLog = new SpektrumLog(openFileDialog.FileName);

                logSessionsListBox.ItemsSource = spektrumLog.logSessions;
            }
        }

        private void logSessionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            GraphWindow graphWindow = new GraphWindow();

            WindowsFormsHost host = new WindowsFormsHost();

            TelemetrySession selectedLogSession = (TelemetrySession)logSessionsListBox.SelectedItem;

            List<Double[]> firstGraphData = selectedLogSession.getSelectedDataBlocks("RPM");

            List<Double[]> secondGraphData = selectedLogSession.getSelectedDataBlocks("RX-VOLT");

            graph = new PylonLogGraphUserControl.PylonLogGraphUserControl(firstGraphData, secondGraphData);
            
            host.Child = graph;

            graphWindow.MainGrid.Children.Add(host);
            graphWindow.Width = graph.Width + 2 * 20;
            graphWindow.Height = graph.Height + 3 * 20;

            graphWindow.Title = selectedLogSession.ToString();

            graphWindow.Show();
        }
    }
}

