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
using ConstructionAndInstallationWorksBayramov.ClassFolder;

namespace ConstructionAndInstallationWorksBayramov.WindowFolder.BrigadierFolder
{
    /// <summary>
    /// Логика взаимодействия для BrigadierWindow.xaml
    /// </summary>
    public partial class BrigadierWindow : Window
    {
        DGClass dGClass;
        public BrigadierWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListWorkersDG);
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            switch(pressed.Content.ToString())
            {
                case "Список сотрудников":
                    GridListWorkers.Visibility = Visibility.Visible;
                    SearchTB.Visibility = Visibility.Visible;
                   break;

                case "Добавить сотрудника":
                    GridListWorkers.Visibility = Visibility.Hidden;
                    SearchTB.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[WorkersView]");
        }
    }
}
