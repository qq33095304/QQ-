using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQBatchSend.IR.Common
{
    public class DataGridViewHelper
    {
        public static void WriteXml(DataGridView dataGridView, string path)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            //定义列
            foreach(DataGridViewColumn item in dataGridView.Columns)
            {
                DataColumn dataColumn = new DataColumn(item.DataPropertyName, Type.GetType("System.String"));
                dataTable.Columns.Add(dataColumn);
            }
            //写数据
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    dataRow[dataColumn.ColumnName] = item.Cells[dataColumn.ColumnName].Value;
                }
                dataTable.Rows.Add(dataRow);
            }
            dataSet.Tables.Add(dataTable);
            dataSet.WriteXml(path);
        }

        public static void ReadXml(DataGridView dataGridView, string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            if (dataSet.Tables.Count > 0)
            {
                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow item in dataTable.Rows)
                {
                    List<object> fieldValues = new List<object>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        fieldValues.Add(item[dataColumn.ColumnName]);
                    }
                    dataGridView.Rows.Add(fieldValues.ToArray());
                }
            }
            
        }
    }
}
