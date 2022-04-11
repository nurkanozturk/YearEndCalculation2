using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using YearEndCalculation2.Entities.Concrete;
using System.Collections;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillTdms
    {

        public void PreLoadOleDb()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection
(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + path + "PreLoad.xls" + "'; Extended Properties=\"Excel 8.0; HDR=NO;IMEX=1\""))
                {
                    oleDbConnection.Open();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from[Sayfa1$]", oleDbConnection);
                    DataSet dataSet = new DataSet();
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                }
            }
            catch { }

        }

        public List<T> FillTdmsDatas<T>(string fileName, T data,bool isExit) where T : class, new()
        {
            List<T> dataList = new List<T>();
            using (OleDbConnection oleDbConnection = new OleDbConnection
          (@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + fileName + "'; Extended Properties=\"Excel 8.0; HDR=NO;IMEX=1\""))
            {
                oleDbConnection.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from[TDMS$]", oleDbConnection);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                int i = 14;
                int fixColumIndex = 0;
                bool isValiedFile = false;
                var checkValue = dataTable.Rows[14][6].ToString();
                var checkValueAlt = dataTable.Rows[11][4].ToString();
                if (checkValue == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    isValiedFile = true;
                }
                else if (checkValueAlt == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    i = 11;
                    fixColumIndex = 2;
                    isValiedFile = true;
                }

                if (isValiedFile)
                {
                    while (dataTable.Rows[i][2 - fixColumIndex].ToString() != "")
                    {
                        if (Convert.ToDecimal(dataTable.Rows[i][9+Convert.ToInt32(isExit) - fixColumIndex]) > 0)
                        {
                            if (dataTable.Rows[i][7 - fixColumIndex].ToString().Length > 149)
                            {
                                dataTable.Rows[i][7 - fixColumIndex] = dataTable.Rows[i][7 - fixColumIndex].ToString().Remove(149);
                            }
                            data = new T();
                            typeof(T).GetProperty("ReceiptNumber").SetValue(data, dataTable.Rows[i][5 - fixColumIndex].ToString());
                            typeof(T).GetProperty("ReceiptDate").SetValue(data, dataTable.Rows[i][2 - fixColumIndex].ToString());
                            typeof(T).GetProperty("TypeOfProcess").SetValue(data, dataTable.Rows[i][6 - fixColumIndex].ToString());
                            typeof(T).GetProperty("Explanation").SetValue(data, dataTable.Rows[i][7 - fixColumIndex].ToString());
                            typeof(T).GetProperty("Amount").SetValue(data, Convert.ToDecimal(dataTable.Rows[i][9+Convert.ToInt32(isExit) - fixColumIndex]));
                            
                            dataList.Add(data);
                        }
                        i++;
                    }
                }

                else
                {
                    throw new Exception("Belge içeriği beklenen şekilde değil. Doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun.");
                }
            }

            return dataList;
        }
/*
        public List<TdmsEntry> FillEntries(string fileName, List<TdmsEntry> tdmsEntries)
        {

            using (OleDbConnection oleDbConnection = new OleDbConnection
           (@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + fileName + "'; Extended Properties=\"Excel 8.0; HDR=NO;IMEX=1\""))
            {
                oleDbConnection.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from[TDMS$]", oleDbConnection);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                int i = 14;
                int fixColumIndex = 0;
                bool isValiedFile = false;
                var checkValue = dataTable.Rows[14][6].ToString();
                var checkValueAlt = dataTable.Rows[11][4].ToString();
                if (checkValue == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    isValiedFile = true;
                }
                else if (checkValueAlt == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    i = 11; 
                    fixColumIndex = 2;
                    isValiedFile = true;
                }

                if (isValiedFile)
                {
                    while (dataTable.Rows[i][2-fixColumIndex].ToString() != "")
                    {
                        if (Convert.ToDecimal(dataTable.Rows[i][9-fixColumIndex]) > 0)
                        {
                            if (dataTable.Rows[i][7-fixColumIndex].ToString().Length > 149)
                            {
                                dataTable.Rows[i][7-fixColumIndex] = dataTable.Rows[i][7-fixColumIndex].ToString().Remove(149);
                            }
                            tdmsEntries.Add(new TdmsEntry
                            {
                                ReceiptNumber = dataTable.Rows[i][5-fixColumIndex].ToString(),
                                ReceiptDate = dataTable.Rows[i][2-fixColumIndex].ToString(),
                                TypeOfProcess = dataTable.Rows[i][6-fixColumIndex].ToString(),
                                Explanation = dataTable.Rows[i][7-fixColumIndex].ToString(),
                                Amount = Convert.ToDecimal(dataTable.Rows[i][9-fixColumIndex])
                            });
                        }
                        i++;
                    }
                }
                
                else
                {
                    throw new Exception("isimli belge içeriği beklenen şekilde değil. Doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun.");
                }
            }

            return tdmsEntries;
        }

        public List<TdmsExit> FillExits(string fileName, List<TdmsExit> tdmsExits)
        {

            using (OleDbConnection oleDbConnection = new OleDbConnection
                        (@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + fileName + "'; Extended Properties=\"Excel 8.0; HDR=NO;IMEX=1\"")
                    )
            {
                oleDbConnection.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from[TDMS$]", oleDbConnection);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                var i = 14;
                int fixColumIndex = 0;
                bool isValiedFile = false;
                var checkValue = dataTable.Rows[14][6].ToString();
                var checkValueAlt = dataTable.Rows[11][4].ToString();
               
                if (checkValue == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    isValiedFile = true;
                }
                else if (checkValueAlt == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
                {
                    i = 11;
                    fixColumIndex = 2;
                    isValiedFile = true;
                }
                
                if (isValiedFile)
                {
                    
                    while (dataTable.Rows[i][2-fixColumIndex].ToString() != "")
                    {
                        if (Convert.ToDecimal(dataTable.Rows[i][10-fixColumIndex]) > 0)
                        {
                            if (dataTable.Rows[i][7-fixColumIndex].ToString().Length > 149)
                            {
                                dataTable.Rows[i][7-fixColumIndex] = dataTable.Rows[i][7-fixColumIndex].ToString().Remove(149);
                            }
                            tdmsExits.Add(new TdmsExit
                            {
                                ReceiptNumber = dataTable.Rows[i][5-fixColumIndex].ToString(),
                                ReceiptDate = dataTable.Rows[i][2-fixColumIndex].ToString(),
                                TypeOfProcess = dataTable.Rows[i][6-fixColumIndex].ToString(),
                                Explanation = dataTable.Rows[i][7-fixColumIndex].ToString(),
                                Amount = Convert.ToDecimal(dataTable.Rows[i][10-fixColumIndex])
                            });
                        }
                        i++;
                    }
                }
            }
            return tdmsExits;
        }
*/
    }
}
