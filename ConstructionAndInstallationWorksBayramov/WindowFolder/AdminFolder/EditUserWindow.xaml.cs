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

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.AdminFolder
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(GlobalClass.SqlConnection);
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        CBClass cBClass = new CBClass();

        public EditUserWindow()
        {
            InitializeComponent();
            cBClass.CBLoad(RoleCB,"Role","IdRole","RoleName");
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                   new SqlCommand("Update " +
                   "dbo.[User] " +
                   $"Set [Login] = '{LoginTB.Text}', " +
                   $"[Password] = '{PasswordPB.Password}', " +
                   $"IdRole = '{RoleCB.SelectedValue.ToString()}' " +
                   $"Where IdUser = '{VariableClass.UserId}'", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные пользователя " +
                    $"{LoginTB.Text} " +
                    $"Успешно отредактированы");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SELECT * FROM dbo.[User] " +
                    $"WHERE IdUser = '{VariableClass.UserId}'", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTB.Text = dataReader[1].ToString();
                PasswordPB.Password = dataReader[2].ToString();
                RoleCB.SelectedValue = dataReader[3].ToString();
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
