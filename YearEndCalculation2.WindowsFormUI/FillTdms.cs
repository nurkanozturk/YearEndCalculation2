using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillTdms
    {
        public static string queryId = "";
        public static decimal transferredPrice = 0;
        

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
                            UseHeaderRow = true
                        }
                    });

                    dataTable = dataSet.Tables[0];
                   
                }
            }

            int i = 14;
            bool isValiedFile = false;
            var checkValue = dataTable.Rows[14][6].ToString();
            var checkValueAlt = dataTable.Rows[11][4].ToString();
            if (checkValue == "Ödeme Emri" || checkValue == "Muhasebe İşlem")
            {
                isValiedFile = true;
            }


            if (isValiedFile)
            {
                transferredPrice = Convert.ToDecimal(dataTable.Rows[i - 1][9].ToString());
                queryId = dataTable.Rows[5][3].ToString() + "-" + dataTable.Rows[6][3].ToString() + "-" + dataTable.Rows[10][3].ToString();

                while (dataTable.Rows[i][2].ToString() != "")
                {
                    if (Convert.ToDecimal(dataTable.Rows[i][9 + Convert.ToInt32(isExit)]) > 0)
                    {
                        string idPrefix = isExit ? "tdmsExit-" : "tdmsEntry-";

                        dataList.Add(new ActionRecord
                        {
                            Id = idPrefix + dataTable.Rows[i][2].ToString() + "-" + dataTable.Rows[i][4].ToString() + "-" + dataTable.Rows[i][9 + Convert.ToInt32(isExit)].ToString(),
                            DocNumber = dataTable.Rows[i][5].ToString(),
                            DocDate = dataTable.Rows[i][2].ToString(),
                            Type = dataTable.Rows[i][6].ToString(),
                            Explanation = dataTable.Rows[i][7].ToString(),
                            Price = Convert.ToDecimal(dataTable.Rows[i][9 + Convert.ToInt32(isExit)])
                        });

                    }
                    i++;
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
