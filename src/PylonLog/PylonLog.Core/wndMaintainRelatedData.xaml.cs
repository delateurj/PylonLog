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
using System.Windows.Shapes;

namespace PylonLog.Core
{
    /// <summary>
    /// Interaction logic for wndMaintainRelatedData.xaml
    /// </summary>
    public partial class wndMaintainRelatedData : Window
    {
        public wndMaintainRelatedData()
        {
            InitializeComponent();
        }

        private void btnSavePlugs_Click(object sender, RoutedEventArgs e)
        {
            GlobalDataContext.pylonLogContext.SaveChanges();
        }
    }
}
