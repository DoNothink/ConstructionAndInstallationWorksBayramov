using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConstructionAndInstallationWorksBayramov.ClassFolder
{
    class DGClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(GlobalClass.SqlConnection);
        SqlDataAdapter dataAdapter;
        DataGrid dataGrid;
        DataTable dataTable;

        public DGClass(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }
        public void LoadDG(string sqlCommand)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, sqlConnection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;
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

        public string SelectId()
        {
            object[] mass = null;
            string id = "";
            try
            {
                if (dataGrid != null)
                {
                    DataRowView dataRowView = dataGrid.SelectedItem as DataRowView;
                    if (dataRowView != null)
                    {
                        DataRow dataRow = (DataRow)dataRowView.Row;
                        mass = dataRow.ItemArray;
                    }
                }
                id = mass[0].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            return id;
        }
    }
}
