using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;

namespace ConstructionAndInstallationWorksBayramov.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection =
            new SqlConnection("Data Source=DESKTOP-D69MI98;" +
            "Initial Catalog=ConstructionAndInstallationWorksBayramov;" +
            "Integrated Security=True");
        SqlDataAdapter dataAdapter;
        DataSet dataSet;

        /// <summary>
        /// Метод загрузки роли в 
        /// ComboBox в окне Администратора
        /// </summary>
        /// <param name="comboBox"> комбобокс</param>
        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("Select IdRole, RoleName " +
                    "From dbo.[Role] Order by IdRole ASC", sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "[Role]");
                comboBox.ItemsSource = dataSet.Tables["[Role]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.Tables["[Role]"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.Tables["[Role]"].Columns["IdRole"].ToString();
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
