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
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                Initial Catalog=ConstructionAndInstallationWorksBayramov;
                Integrated Security=True");
        SqlDataAdapter dataAdapter;
        DataSet dataSet;

        public void CBLoad(ComboBox comboBox, string TableName, string IdColumn, string NameColumn)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter($"Select {IdColumn}, {NameColumn} " +
                    $"From dbo.[{TableName}] Order by {IdColumn} ASC", sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, $"{TableName}");
                comboBox.ItemsSource = dataSet.Tables[$"{TableName}"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.Tables[$"{TableName}"].Columns[$"{NameColumn}"].ToString();
                comboBox.SelectedValuePath = dataSet.Tables[$"{TableName}"].Columns[$"{IdColumn}"].ToString();
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
