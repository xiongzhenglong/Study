using OfficeOpenXml;
using ShenQi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ShenQi.Excel
{
    public class ExcelHelper
    {
        public static MemoryStream Export(ICollection<Xwowmall_Good_Model> xgmlst, ICollection<Xwowmall_Good_Model> xgmout)
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage package = new ExcelPackage(stream);
            foreach (var item in xgmout)
            {
                package.Workbook.Worksheets.Add(item.shortdate);
                ExcelWorksheet sheet = package.Workbook.Worksheets[xgmout.ToList().IndexOf(item)+1];

                #region write header
                sheet.Cells[1, 1].Value = "商品名";
                sheet.Cells[1, 2].Value = "一级分类";
                sheet.Cells[1, 3].Value = "二级分类";
                sheet.Cells[1, 4].Value = "三级分类";
                sheet.Cells[1, 5].Value = "单价";
                sheet.Cells[1, 6].Value = "剩余库存";
                sheet.Cells[1, 7].Value = "采集时间";

                using (ExcelRange range = sheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    range.AutoFitColumns(4);
                }
                #endregion

                #region write content
                int pos = 2;
                var exprot = xgmlst.Where(x => x.shortdate == item.shortdate).ToList();
                foreach (var s in exprot)
                {
                    sheet.Cells[pos, 1].Value = s.productname;
                    sheet.Cells[pos, 2].Value = s.firstname;
                    sheet.Cells[pos, 3].Value = s.secondname;
                    sheet.Cells[pos, 4].Value = s.thirdname;
                    sheet.Cells[pos, 5].Value = s.price;
                    sheet.Cells[pos, 6].Value = s.stock;
                    sheet.Cells[pos, 7].Value = s.shortdate;



                    using (ExcelRange range = sheet.Cells[pos, 1, pos, 7])
                    {
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    }

                    pos++;
                }
                #endregion
            }



            package.Save();

            return stream;
        }
    }
}