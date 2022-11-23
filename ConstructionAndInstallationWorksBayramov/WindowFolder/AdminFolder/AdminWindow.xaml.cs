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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        CBClass cBClass = new CBClass();
        DGClass dGClass;
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-D69MI98;
                    Initial Catalog=ConstructionAndInstallationWorksBayramov;
                    Integrated Security=True");
        SqlCommand sqlCommand;

        public AdminWindow()
        {
            InitializeComponent();
            cBClass.CBLoad(RoleCB, "Role", "IdRole", "RoleName");
            dGClass = new DGClass(ListUserDG);
        }

        /// <summary>
        /// Метод обработки RadioButton,
        /// которые отвечают за видимость
        /// определенных элементов на экране.
        /// Грубо говоря, наши вкладки :)
        /// </summary>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content)
            {
                case "Список пользователей":
                    GridListUsers.Visibility = Visibility.Visible; //вкл. грид+список
                    SearchTB.Visibility = Visibility.Visible; //вкл. поискТБ
                    GridAddUser.Visibility = Visibility.Hidden; //выкл. грид с добавлением
                    break;

                case "Добавить пользователя":
                    GridAddUser.Visibility = Visibility.Visible; //вкл. грид с добавлением
                    GridListUsers.Visibility = Visibility.Hidden; //выкл. грид+список
                    SearchTB.Visibility = Visibility.Hidden; //выкл поискТБ
                    break;
            }
        }

        /// <summary>
        /// Выгрузка представления
        /// из базы данных
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.[UserView]");
        }

        /// <summary>
        /// Метод, отвечающий за выход
        /// из аккаунта
        /// </summary>
        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            bool resultMB = MBClass.QuestionMB("Вы действительно " +
                "желаете выйти из аккаунта?");
            if(resultMB)
            {      
                new AuthorizationWindow().Show();
                this.Close();
            }
        }

        /// <summary>
        /// Метод, отвечающий за закрытие
        /// приложения
        /// </summary>
        private void CloseAppBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        /// <summary>
        ///  Обработчик кнопки, 
        ///  отвечающий за добавление нового пользователя
        /// </summary>
        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPB.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "1234567890";
            string znak = "!@#$%^&*()_+=-№";

            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Вы не ввели логин");
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMB("Вы не ввели пароль");
                PasswordPB.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать прописную букву");
                PasswordPB.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать строчную букву");
                PasswordPB.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PasswordPB.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "один из следующих знаков: " + znak);
                PasswordPB.Focus();
            }
            else if (RoleCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите роль");
                RoleCB.Focus();
            }
            else
            {                
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert into dbo.[User] " +
                        "([LoginUser], [PasswordUser], IdRole) " +
                        $"Values ('{LoginTB.Text}', " +
                        $"'{PasswordPB.Password}', " +
                        $"'{RoleCB.SelectedValue.ToString()}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Пользователь {LoginTB.Text} " +
                        $"успешно добавлен");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                    dGClass.LoadDG("SELECT * FROM dbo.[UserView]");
                }            
            }
        }

        /// <summary>
        /// Обработчик поиска в 
        /// DataGrid
        /// </summary>
        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[UserView] " +
                $"Where LoginUser Like '%{SearchTB.Text}%' " +
                $"OR RoleName Like '%{SearchTB.Text}%'");
        }

        /// <summary>
        /// Обработчик DataGrid,
        /// отвечающий за запуск окна редактирования
        /// выбранного пользователя
        /// </summary>
        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserId = dGClass.SelectId();
                    new EditUserWindow().Show();
                    dGClass.LoadDG("Select * From dbo.[UserView]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }
    }
}
