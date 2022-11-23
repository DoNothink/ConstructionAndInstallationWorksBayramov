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
using System.Data.SqlClient;
using ConstructionAndInstallationWorksBayramov.ClassFolder;

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.BrigadierFolder.AddWorkerWindows
{
    /// <summary>
    /// Interaction logic for AddBrigadeWindow.xaml
    /// </summary>
    public partial class AddBrigadeWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-D69MI98;
                    Initial Catalog=ConstructionAndInstallationWorksBayramov;
                    Integrated Security=True");
        SqlCommand sqlCommand;
        public AddBrigadeWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert into dbo.[Brigade] " +
                    "(NameBrigade) " +
                    $"Values ('{BrigadeTB.Text}')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Бригада {BrigadeTB.Text} " +
                    $"успешно добавлена");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
