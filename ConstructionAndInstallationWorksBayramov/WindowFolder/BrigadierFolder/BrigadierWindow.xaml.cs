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

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.BrigadierFolder
{
    /// <summary>
    /// Interaction logic for BrigadierWindow.xaml
    /// </summary>
    public partial class BrigadierWindow : Window
    {
        CBClass cBClass = new CBClass();
        DGClass dGClass;
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-D69MI98;
                    Initial Catalog=ConstructionAndInstallationWorksBayramov;
                    Integrated Security=True");
        SqlCommand sqlCommand;
        private string DateOfBirth { get; set; }
        public BrigadierWindow()
        {
            InitializeComponent();

            cBClass.CBLoad(CitizenshipCB, "Citizenship", "IdCitizenship", "CitizenshipName");
            cBClass.CBLoad(SpecialtyCB, "Specialty", "IdSpecialty", "NameSpecialty");
            cBClass.CBLoad(PostCB, "Post", "IdPost", "NamePost");
            cBClass.CBLoad(BrigadeCB, "Brigade", "IdBrigade", "NameBrigade");
            dGClass = new DGClass(ListWorkersDG);
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
                case "Список рабочих":
                    GridListBuilders.Visibility = Visibility.Visible; //вкл. грид+список
                    SearchTB.Visibility = Visibility.Visible; //вкл. поискТБ
                    GridAddBuilders.Visibility = Visibility.Hidden; //выкл. грид с добавлением
                    break;

                case "Добавить рабочего":
                    GridAddBuilders.Visibility = Visibility.Visible; //вкл. грид с добавлением
                    GridListBuilders.Visibility = Visibility.Hidden; //выкл. грид+список
                    SearchTB.Visibility = Visibility.Hidden; //выкл поискТБ
                    break;
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
        /// Метод, отвечающий за выход
        /// из аккаунта
        /// </summary>
        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            bool resultMB = MBClass.QuestionMB("Вы действительно " +
                "желаете выйти из аккаунта?");
            if (resultMB)
            {
                new AuthorizationWindow().Show();
                this.Close();
            }
        }

        /// <summary>
        /// Обработчик поиска в 
        /// DataGrid
        /// </summary>
        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[WorkersView] " +
               $"Where LastNameWorker Like '%{SearchTB.Text}%' " +
               $"OR FirstNameWorker Like '%{SearchTB.Text}%'" +
               $"OR MiddleNameWorker Like '%{SearchTB.Text}%'");
        }

        /// <summary>
        /// Выгрузка представления
        /// из базы данных
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.[WorkersView]");
        }

        private void AddWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert into dbo.[Workers] " +
                    "([LastNameWorker], [FirstNameWorker],[MiddleNameWorker], " +
                    "[IdCitizenship], [DateOfBirthWorker], [IdSpecialty], " +
                    "[IdPost], [IdBrigade], [PhoneNumberWorker], " +
                    "[EmailWorker]) " +
                    $"Values ('{LastNameWorker.Text}', " +
                    $"'{FirstNameWorker.Text}', " +
                    $"'{MiddleNameWorker.Text}', " +
                    $"'{CitizenshipCB.SelectedValue.ToString()}', " +
                    $"'{DateOfBirth}', " +
                    $"'{SpecialtyCB.SelectedValue.ToString()}', " +
                    $"'{PostCB.SelectedValue.ToString()}', " +
                    $"'{BrigadeCB.SelectedValue.ToString()}', " +
                    $"'{PhoneNumberTB.Text}', " +
                    $"'{EmailTB.Text}')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Рабочий {LastNameWorker.Text} " +
                    $"{FirstNameWorker.Text} успешно добавлен");
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

        /// <summary>
        /// Обработка даты 
        /// и помещение её в свойство DateOfBirth
        /// </summary>
        private void DateOfBirthDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = DateOfBirthDP.SelectedDate;
            DateOfBirth = selectedDate.Value.ToShortDateString();
        }
    }
}
