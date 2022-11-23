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
using ConstructionAndInstallationWorksBayramov.ClassFolder;

namespace ConstructionAndInstallationWorksBayramov.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-D69MI98;
                    Initial Catalog=ConstructionAndInstallationWorksBayramov;
                    Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }
        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Вы не ввели логин");
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMB("Вы не ввели пароль");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("SELECT * FROM " +
                        "dbo.[User] " +
                        $"Where [LoginUser] = '{LoginTB.Text}'", sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Read();
                    if (sqlDataReader[2].ToString() != PasswordPB.Password)
                    {
                        MBClass.ErrorMB("Вы ввели неверный логин или пароль");
                        PasswordPB.Focus();
                    }
                    else
                    {
                            switch (sqlDataReader[3].ToString())
                            {
                                case "1":
                                    new AdminFolder.AdminWindow().Show();
                                    this.Close();
                                    break;
                                case "2":
                                    new BrigadierFolder.BrigadierWindow().Show();
                                    this.Close();
                                    break;
                            }                        
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
