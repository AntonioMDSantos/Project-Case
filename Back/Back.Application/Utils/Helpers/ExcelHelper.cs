using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;

namespace CaseBack.Application.Utils
{
    public class ExcelHelper
    {

        static public string GenerateExcel(dynamic list)
        {
            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(list), (typeof(DataTable)));
            string FileName = $@"wwwroot/Export/Aniversarios";
            
            if (!Directory.Exists(FileName))
            {
                Directory.CreateDirectory(FileName);
                FileName = FileName + $"/Aniverarios-export{DateTime.Now.ToString("fff")}.xlsx";
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Sheet1");

            List<String> columns = new();
            IRow row = excelSheet.CreateRow(0);
            int columnIndex = 0;

            foreach (System.Data.DataColumn column in table.Columns)
            {
                columns.Add(column.ColumnName);
                row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                columnIndex++;
            }

            int rowIndex = 1;
            foreach (DataRow dsrow in table.Rows)
            {
                row = excelSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (String col in columns)
                {
                    row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                    cellIndex++;
                }

                rowIndex++;
            }

            ByteArrayOutputStream bos = new ByteArrayOutputStream();
            try
            {
                workbook.Write(bos);
            }
            finally
            {
                bos.Close();
            }
            byte[] bytes = bos.ToByteArray();
            return Convert.ToBase64String(bytes);
        }
    }
}