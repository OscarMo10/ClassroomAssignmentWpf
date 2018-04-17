using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Extension
{
    static class WorkbookExtension
    {
        public static void SortWorksheets(this IWorkbook workbook) 
        {
            var listOfNames = new List<string>();

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                listOfNames.Add(workbook.GetSheetName(i));
            }

            listOfNames.Sort();

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                workbook.SetSheetOrder(listOfNames[i], i);
            }
        }

        public static void WriteToFile(this IWorkbook workbook, string outputFileName)
        {
            using (var fileStream = File.OpenWrite(outputFileName))
            {
                workbook.Write(fileStream);
            }
        }
    }
}
