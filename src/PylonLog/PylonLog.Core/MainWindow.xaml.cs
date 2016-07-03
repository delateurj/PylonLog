using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Data.Entity;
using System.Linq;


namespace PylonLog.Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PylonLogContext _context = new PylonLogContext();

        private PylonLogGraphUserControl.PylonLogGraphUserControl graph;

        private SpektrumLog spektrumLog;

        public MainWindow()
        {
            InitializeComponent();

        //  _context.pylonLogEntries.Load();

        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource pylonLogEntryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pylonLogEntryViewSource")));


            _context.pylonLogEntries.Load();

            pylonLogEntryViewSource.Source = _context.pylonLogEntries.Local;
        }

        private void pylonLogEntryDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PylonLogEntry pylonLogEntry = new PylonLogEntry();


            pylonLogEntry.planeName = "Hello World";

             _context.pylonLogEntries.Add(pylonLogEntry);

                _context.SaveChanges();
           

        }

        private void openLogToInspectButton_Click(object sender, RoutedEventArgs e)
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

