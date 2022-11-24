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
    /// Interaction logic for AddSpecialtyWindow.xaml
    /// </summary>
    public partial class AddSpecialtyWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(GlobalClass.SqlConnection);
        SqlCommand sqlCommand;

        public AddSpecialtyWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert into dbo.[Specialty] " +
                    "(NameSpecialty) " +
                    $"Values ('{SpecialtyTB.Text}')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Специальность {SpecialtyTB.Text} " +
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
