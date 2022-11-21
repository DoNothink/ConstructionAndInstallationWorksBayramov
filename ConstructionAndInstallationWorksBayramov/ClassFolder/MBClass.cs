using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConstructionAndInstallationWorksBayramov.ClassFolder
{
    class MBClass
    {
        public static void InfoMB(string text)
        {
            MessageBox.Show(text, "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ErrorMB(Exception text)
        {
            MessageBox.Show(text.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void ErrorMB(string text)
        {
            MessageBox.Show(text, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static bool QuestionMB(string text)
        {
            return MessageBoxResult.Yes == MessageBox.Show(text,
                "Вопрос", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
        }

        public static void ExitMB()
        {
            bool resultMB = QuestionMB("Вы действительно " +
                "желаете закрыть приложение?");
            if (resultMB == true)
            {
                App.Current.Shutdown();
            }
        }
    }
}
