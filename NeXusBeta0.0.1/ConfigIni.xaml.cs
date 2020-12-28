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

namespace NeXusBeta0._0._1
{
    /// <summary>
    /// Lógica de interacción para ConfigIni.xaml
    /// </summary>
    public partial class ConfigIni : Window
    {
        public ConfigIni()
        {
            InitializeComponent();
        }

        private void btnFalse_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CfgIni = false;
            Properties.Settings.Default.Save();
            lblSalida.Content = Properties.Settings.Default.CfgIni.ToString();
        }

        private void btnTrue_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CfgIni = true;
            Properties.Settings.Default.Save();
            lblSalida.Content = Properties.Settings.Default.CfgIni.ToString();
        }
    }
}
