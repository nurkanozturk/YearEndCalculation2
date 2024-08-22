using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using YearEndCalculation.Business.Tools;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillTdms
    {
        public static string queryId = "";
        public static decimal transferredPrice =0;
        

        public List<ActionRecord> FillTdmsDatas(string fileName, bool isExit)
        {
            List<ActionRecord> dataList = new List<ActionRecord>();
            DataTable dataTable = new DataTable();
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    
                    DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });

                    dataTable = dataSet.Tables[0];
                   
                }
            }

            int actionIndex = 14;
            int queryIdColumn = 3;
            int borcColumn = 11;
            int docColumn = 5;
            int tdmsFileNumColumn = 4;
            int dateColumn = 2;
            int typeColumn = 6;
            int invoiceColumn = 8;
            int expColumn = 7;


            bool isValiedFile = false;
            var checkValue = dataTable.Rows[14][6].ToString();
            //var checkValueAlt = dataTable.Rows[11][4].ToString();

            
            if (checkValue == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
            {
                isValiedFile = true;
            }
                      

            if (isValiedFile)
            {
                transferredPrice = Convert.ToDecimal(dataTable.Rows[actionIndex - 1][borcColumn].ToString());
                queryId = dataTable.Rows[5][queryIdColumn].ToString() + "-" 
                    + dataTable.Rows[6][queryIdColumn].ToString() + "-" 
                    + dataTable.Rows[10][queryIdColumn].ToString();

                while (dataTable.Rows[actionIndex][dateColumn].ToString() != "")
                {
                    if (Convert.ToDecimal(dataTable.Rows[actionIndex][borcColumn + Convert.ToInt32(isExit)]) > 0)
                    {
                        string idPrefix = isExit ? "tdmsExit-" : "tdmsEntry-";
                        string docNumber = dataTable.Rows[actionIndex][docColumn].ToString();
                        
                        dataList.Add(new ActionRecord
                        {
                            Id = idPrefix + dataTable.Rows[actionIndex][2].ToString() + "-" 
                            + dataTable.Rows[actionIndex][tdmsFileNumColumn].ToString() + "-" 
                            + dataTable.Rows[actionIndex][borcColumn + Convert.ToInt32(isExit)].ToString(),
                            DocNumber = docNumber,
                            DocDate = dataTable.Rows[actionIndex][dateColumn].ToString(),
                            Type = dataTable.Rows[actionIndex][typeColumn].ToString(),
                            InvoiceNumber = dataTable.Rows[actionIndex][invoiceColumn].ToString(),
                            Explanation = dataTable.Rows[actionIndex][expColumn].ToString(),
                            Price = Convert.ToDecimal(dataTable.Rows[actionIndex][borcColumn + Convert.ToInt32(isExit)]),
                            DateBase = Convert.ToDateTime(dataTable.Rows[actionIndex][dateColumn].ToString()),

                        });
                        

                    }
                    actionIndex++;
                }
            }

            else
            {
                throw new Exception("Belge içeriği beklenen şekilde değil. Doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun.");
            }
        

            return dataList;
        }
    }
}
