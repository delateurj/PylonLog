using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Data.Entity;
using System.Linq;


namespace PylonLog.Core
{

    public partial class MainWindow : Window
    {
        private PylonLogContext pylonLogContext = new PylonLogContext();

        private PylonLogGraphUserControl.PylonLogGraphUserControl graph;

        private PylonLogEntry pylonLogEntry = new PylonLogEntry();

        private SpektrumLog spektrumLog;

        public MainWindow()
        {
            InitializeComponent();

            pylonLogContext.Database.Initialize(false);

            dckPnlMain.DataContext = pylonLogEntry;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource pylonLogEntryViewSource = ((CollectionViewSource)(this.FindResource("pylonLogEntryViewSource")));

            CollectionViewSource propViewSource = ((CollectionViewSource)(this.FindResource("propViewSource")));

            CollectionViewSource plugViewSource = ((CollectionViewSource)(this.FindResource("plugViewSource")));

            pylonLogContext.props.Load();

            pylonLogContext.plugs.Load();

            pylonLogContext.pylonLogEntries.Load();
           
            plugViewSource.Source = pylonLogContext.plugs.Local;

            propViewSource.Source = pylonLogContext.props.Local;

            pylonLogEntryViewSource.Source = pylonLogContext.pylonLogEntries.Local;
        }


        private void openLogToInspectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Log Files (*.TLM)|*.TLM";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                spektrumLog = new SpektrumLog(openFileDialog.FileName);

                lstBxLogSessions.ItemsSource = spektrumLog.logSessions;
            }
        }


        private void logSessionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GraphWindow graphWindow = new GraphWindow();

            WindowsFormsHost host = new WindowsFormsHost();

            TelemetrySession selectedLogSession = (TelemetrySession)lstBxLogSessions.SelectedItem;

             pylonLogEntry.planeName = selectedLogSession.planeName;

            List<Double[]> firstGraphData = selectedLogSession.getSelectedDataBlocks("RPM");

            List<Double[]> secondGraphData = selectedLogSession.getSelectedDataBlocks("RX-VOLT");

            graph = new PylonLogGraphUserControl.PylonLogGraphUserControl(firstGraphData, secondGraphData);

            host.Child = graph;

            graphWindow.Left = this.Left + this.ActualWidth;

            graphWindow.MainGrid.Children.Add(host);

            graphWindow.Width = graph.Width;

            graphWindow.Height = graph.Height;

            graphWindow.Title = selectedLogSession.ToString();

            graphWindow.Show();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            TelemetrySession selectedLogSession = (TelemetrySession)lstBxLogSessions.SelectedItem;

            foreach (DataBlock dataBlock in selectedLogSession.dataBlocks)
            {
                if (dataBlock.dataValue != 0)
                {
                    pylonLogEntry.DataBlocks.Add(dataBlock);
                }
            }
            pylonLogEntry.telemetryDuration = selectedLogSession.duration;

            pylonLogContext.pylonLogEntries.Add(pylonLogEntry);

            PylonLogEntry justSavedPylonLog = pylonLogEntry;

            pylonLogContext.SaveChanges();

            pylonLogEntry = new PylonLogEntry();

            dckPnlMain.DataContext = pylonLogEntry;

            //Copy the attributes that are likely to be the same for the
            //next entry.

            pylonLogEntry.planeName = justSavedPylonLog.planeName;

            pylonLogEntry.humidity = justSavedPylonLog.humidity;

            pylonLogEntry.temperature = justSavedPylonLog.temperature;

            pylonLogEntry.prop = justSavedPylonLog.prop;

            pylonLogEntry.plugType = justSavedPylonLog.plugType;

            pylonLogEntry.engineID = justSavedPylonLog.engineID;

            pylonLogEntry.entryType = justSavedPylonLog.entryType;

            this.lstBxLogSessions.Focus();
        }
    }
}

