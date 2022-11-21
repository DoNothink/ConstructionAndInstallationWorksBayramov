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

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.AdminFolder
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
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
    }
}
