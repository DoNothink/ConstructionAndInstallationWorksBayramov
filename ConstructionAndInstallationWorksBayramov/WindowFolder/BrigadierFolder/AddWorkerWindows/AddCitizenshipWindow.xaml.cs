using ConstructionAndInstallationWorksBayramov.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.BrigadierFolder.AddWorkerWindows
{
    /// <summary>
    /// Interaction logic for AddCitizenshipWindow.xaml
    /// </summary>
    public partial class AddCitizenshipWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(GlobalClass.SqlConnection);
        SqlCommand sqlCommand;

        public AddCitizenshipWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert into dbo.[Citizenship] " +
                    "(CitizenshipName) " +
                    $"Values ('{CitizenshipTB.Text}')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Гражданство {CitizenshipTB.Text} " +
                    $"успешно добавлено");
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
