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
using NeXusLibrary;
using System.Data.SqlClient;

namespace NeXusBeta0._0._1
{
    /// <summary>
    /// Lógica de interacción para ConfigVista.xaml
    /// </summary>
    public partial class ConfigVista : Window
    {
        public ConfigVista()
        {
            InitializeComponent();
        }
        public void Create()
        {
            SqlConnection con = ConnectionDB.getConnection();

            Commands c = new Commands(txtCommand.Text, txtAction.Text, txtResponse.Text);

            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Command(Command,Action,Response) Values('{0}','{1}','{2}');", c.Command, c.Action, c.Response),con);

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Comando Insertado");
            }
            else
            {
                MessageBox.Show("Comando No Insertado");
            }
            con.Close();
        }
        public void fillDataGrid()
        {
            SqlConnection con = ConnectionDB.getConnection();
            List<Commands> list = new List<Commands>();

            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Command;"), con);

            SqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                list.Add(new Commands(rs.GetInt32(0), rs.GetString(1), rs.GetString(2), rs.GetString(3)));
            }
            dgCommand.ItemsSource = list;
            con.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Create();
            fillDataGrid();
        }
    }
}
