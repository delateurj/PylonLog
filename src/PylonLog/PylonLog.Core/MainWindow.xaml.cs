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

            this.Left = 0;
            this.Top = 0;
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

                if (chkBoxNonZeroRPM.IsChecked == true)
                {
                    lstBxLogSessions.ItemsSource = spektrumLog.logSessions;
                }
                else
                {
                    lstBxLogSessions.ItemsSource = spektrumLog.logSessions.Where(element => element.numberOfNonZeroDataBlocksOfThisDataType("RPM") > 0);
                }
            }
        }


        private void logSessionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GraphWindow graphWindow = new GraphWindow();

            WindowsFormsHost host = new WindowsFormsHost();

            TelemetrySession selectedLogSession = (TelemetrySession)lstBxLogSessions.SelectedItem;

            if (selectedLogSession != null)
            {
                List<Double[]> firstGraphData = selectedLogSession.getSelectedDataBlocks("RPM");

                List<Double[]> secondGraphData = selectedLogSession.getSelectedDataBlocks("RX-VOLT");

                graph = new PylonLogGraphUserControl.PylonLogGraphUserControl(firstGraphData, secondGraphData);

                host.Child = graph;

                graphWindow.Left = this.Left;
                graphWindow.Top = this.Top + this.ActualHeight;

                graphWindow.MainGrid.Children.Add(host);

                graphWindow.Width = graph.Width;

                graphWindow.Height = graph.Height;

                graphWindow.Title = selectedLogSession.ToString() + " Non zero rpm:" + selectedLogSession.numberOfNonZeroDataBlocksOfThisDataType("RPM");

                graphWindow.Show();
            }
        }

        private void btnSaveDataGrid_Click(object sender, RoutedEventArgs e)
        {
            pylonLogContext.SaveChanges();

            dgPylonLog.Focus();

            dgPylonLog.SelectedItem = this.dgPylonLog.Items.MoveCurrentToLast();

            dgPylonLog.ScrollIntoView(dgPylonLog.SelectedItem);
        }

        private void btnOpenCreateLogEntry_Click(object sender, RoutedEventArgs e)
        {
            TelemetrySession selectedLogSession = (TelemetrySession)lstBxLogSessions.SelectedItem;
   
            PylonLogEntry previous = pylonLogEntry;

            pylonLogEntry = new PylonLogEntry();

            pylonLogEntry.planeName = selectedLogSession.planeName;

            pylonLogEntry.humidity = previous.humidity;

            pylonLogEntry.temperature = previous.temperature;

            pylonLogEntry.prop = previous.prop;

            pylonLogEntry.plugType = previous.plugType;

            pylonLogEntry.engineID = previous.engineID;

            pylonLogEntry.entryType = previous.entryType;

            foreach (DataBlock dataBlock in selectedLogSession.dataBlocks)
            {
                if (dataBlock.dataValue != 0)
                {
                    pylonLogEntry.DataBlocks.Add(dataBlock.shallowClone());
                }
            }

            pylonLogEntry.avgRPM =(int) (pylonLogEntry.averageOfSpecifiedValueType("RPM"));

            pylonLogEntry.telemetryDuration = selectedLogSession.duration;

            pylonLogContext.pylonLogEntries.Add(pylonLogEntry);

            pylonLogContext.SaveChanges();

            spektrumLog.logSessions.Remove(selectedLogSession);

            this.dgPylonLog.Focus();

            dgPylonLog.SelectedItem = this.dgPylonLog.Items.MoveCurrentToLast();

            dgPylonLog.ScrollIntoView(dgPylonLog.SelectedItem);

        }

        private void chkBoxNonZeroRPM_Click(object sender, RoutedEventArgs e)
        {
            if (spektrumLog != null)
            {
                if (spektrumLog.logSessions != null)
                {
                    if (chkBoxNonZeroRPM.IsChecked == true)
                    {
                        lstBxLogSessions.ItemsSource = spektrumLog.logSessions;
                    }
                    else
                    {
                        lstBxLogSessions.ItemsSource = spektrumLog.logSessions.Where(element => element.numberOfNonZeroDataBlocksOfThisDataType("RPM") > 0);
                    }
                }
            }
            
        }

        private void btnUpdateAvgRPM_Click(object sender, RoutedEventArgs e)
        {
            PylonLogEntry selectedEntry =(PylonLogEntry) dgPylonLog.SelectedItem;

            

            if(selectedEntry  != null)
            {
                if(selectedEntry.endTimeStamp != 0)
                {
                    selectedEntry.avgRPM = (int)(selectedEntry.averageOfSpecifiedValueType("RPM", 100*selectedEntry.launchTimeStamp, 100*selectedEntry.endTimeStamp));
                }
                else
                {
                    selectedEntry.avgRPM = (int)(selectedEntry.averageOfSpecifiedValueType("RPM", 100*selectedEntry.launchTimeStamp));
                }
            }
        }
    }
}

