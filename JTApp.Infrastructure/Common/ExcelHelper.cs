﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Infrastructure.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 读取excel到datatable中
        /// </summary>
        /// <param name="excelPath">excel地址</param>
        /// <param name="sheetIndex">sheet索引</param>
        /// <returns>成功返回datatable，失败返回null</returns>
        public static DataTable ImportExcel(string excelPath, int sheetIndex)
        {

            IWorkbook workbook = null;//全局workbook
            ISheet sheet;//sheet
            DataTable table = null;
            try
            {
                FileInfo fileInfo = new FileInfo(excelPath);//判断文件是否存在
                if (fileInfo.Exists)
                {
                    FileStream fileStream = fileInfo.OpenRead();//打开文件，得到文件流
                    switch (fileInfo.Extension)
                    {
                        //xls是03，用HSSFWorkbook打开，.xlsx是07或者10用XSSFWorkbook打开
                        case ".xls": workbook = new HSSFWorkbook(fileStream); break;
                        case ".xlsx": workbook = new XSSFWorkbook(fileStream); break;
                        default: break;
                    }
                    fileStream.Close();//关闭文件流
                }
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(sheetIndex);//读取到指定的sheet
                    table = new DataTable();//初始化一个table

                    IRow headerRow = sheet.GetRow(0);//获取第一行，一般为表头
                    int cellCount = headerRow.LastCellNum;//得到列数

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);//初始化table的列
                        table.Columns.Add(column);
                    }
                    //遍历读取cell
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);//得到一行
                        DataRow dataRow = table.NewRow();//新建一个行

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);//得到cell
                            if (cell == null)//如果cell为null，则赋值为空
                            {
                                dataRow[j] = "";
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();//否则赋值
                            }
                        }

                        table.Rows.Add(dataRow);//把行 加入到table中

                    }
                }
                return table;

            }
            catch
            {
                return table;
            }
            finally
            {
                //释放资源
                if (table != null) { table.Dispose(); }
                workbook = null;
                sheet = null;
            }
        }

        public static DataTable ImportExcel(MemoryStream ms)
        {

            IWorkbook workbook = null;//全局workbook
            ISheet sheet;//sheet
            DataTable table = null;
            try
            {
                workbook = new HSSFWorkbook(ms);
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取到指定的sheet
                    table = new DataTable();//初始化一个table

                    IRow headerRow = sheet.GetRow(0);//获取第一行，一般为表头
                    int cellCount = headerRow.LastCellNum;//得到列数

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);//初始化table的列
                        table.Columns.Add(column);
                    }
                    //遍历读取cell
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);//得到一行
                        DataRow dataRow = table.NewRow();//新建一个行

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);//得到cell
                            if (cell == null)//如果cell为null，则赋值为空
                            {
                                dataRow[j] = "";
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();//否则赋值
                            }
                        }

                        table.Rows.Add(dataRow);//把行 加入到table中

                    }
                }
                return table;

            }
            catch
            {
                return table;
            }
            finally
            {
                //释放资源
                if (table != null) { table.Dispose(); }
                workbook = null;
                sheet = null;
            }
        }


        public static byte[] ModifyTemplete(string fileName, string[] deptList, string[] dutiesList)
        {
            IWorkbook workbook = null;//全局workbook
            ISheet sheet;//sheet
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);//判断文件是否存在                
                if (fileInfo.Exists)
                {
                    FileStream fileStream = fileInfo.OpenRead();//打开文件，得到文件流
                    switch (fileInfo.Extension)
                    {
                        //xls是03，用HSSFWorkbook打开，.xlsx是07或者10用XSSFWorkbook打开
                        case ".xls": workbook = new HSSFWorkbook(fileStream); break;
                        case ".xlsx": workbook = new XSSFWorkbook(fileStream); break;
                        default: break;
                    }
                    fileStream.Close();//关闭文件流
                }
                if (workbook == null)
                    return null;
                sheet = workbook.GetSheetAt(0);

                if (deptList != null && deptList.Length > 0)
                {
                    CellRangeAddressList regions = new CellRangeAddressList(0, 65535, 2, 2);
                    DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(deptList);
                    HSSFDataValidation validate = new HSSFDataValidation(regions, constraint);
                    sheet.AddValidationData(validate);
                }
                if (dutiesList != null && dutiesList.Length > 0)
                {
                    CellRangeAddressList regions = new CellRangeAddressList(0, 65535, 4, 4);
                    DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(dutiesList);
                    HSSFDataValidation validate = new HSSFDataValidation(regions, constraint);
                    sheet.AddValidationData(validate);
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                byte[] buf = ms.ToArray();
                return buf;
            }
            catch { }
            return null;
        }
        public static byte[] ExportToExcel(DataTable dt, string sheetName)
        {
            IWorkbook workbook = null;//全局workbook
            ISheet sheet;//sheet
            workbook = new HSSFWorkbook();
            sheet = workbook.CreateSheet(sheetName);
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;

            ICellStyle headStyle = workbook.CreateCellStyle();
            headStyle.BorderBottom = BorderStyle.Thin;
            headStyle.BorderLeft = BorderStyle.Thin;
            headStyle.BorderRight = BorderStyle.Thin;
            headStyle.BorderTop = BorderStyle.Thin;
            headStyle.VerticalAlignment = VerticalAlignment.Center;
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.Boldweight = 700;
            headStyle.SetFont(font);

            IRow header = sheet.CreateRow(0);
            header.HeightInPoints = 18.75f;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                cell.CellStyle = headStyle;
            }
            int index = 1;
            foreach (DataRow row in dt.Rows)
            {
                IRow workRow = sheet.CreateRow(index++);
                workRow.HeightInPoints = 18.75f;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = workRow.CreateCell(i);
                    if (i > 1)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(double.Parse(row[i].ToString()));
                    }
                    else
                        cell.SetCellValue(row[i].ToString());
                    cell.CellStyle = cellStyle;
                }

            }
            for (int columnNum = 0; columnNum < dt.Columns.Count; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)//在这一列上循环行  
                {
                    IRow currentRow = sheet.GetRow(rowNum);
                    ICell currentCell = currentRow.GetCell(columnNum);
                    int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                    if (columnWidth < length + 1)
                    {
                        columnWidth = length + 1;
                    }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            byte[] buf = ms.ToArray();
            ms.Flush();
            return buf;
        }
    }
}
