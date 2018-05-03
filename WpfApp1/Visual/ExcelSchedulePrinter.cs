using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using static ClassroomAssignment.Model.ClassScheduleTemplate;
using System.Collections.Specialized;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using ClassroomAssignment.Extension;

namespace ClassroomAssignment.Model.Visual
{
    /// <summary>
    /// Helps with exported visualization 
    /// </summary>
    class ExcelSchedulePrinter : ISchedulePrinter
    {
        static Tuple<int, int> RoomNameLocation = Tuple.Create<int, int>(2, 0);
        static Tuple<int, int> RoomCapacityLocation = Tuple.Create<int, int>(2, 1);
        static Tuple<int,int> TimeHeaderLocation = Tuple.Create<int, int>(4, 1);
        static int CellSpanPerTimeInterval = 2;
        static int startTimeLocationRow = 6;

        static TimeSpan StartTime = new TimeSpan(7, 30, 0);
        static TimeSpan EndTime = new TimeSpan(22, 0, 0);
        static TimeSpan TimeInterval = new TimeSpan(0, 30, 0);
        
        static Dictionary<TimeSpan, int> TimeMap = new Dictionary<TimeSpan, int>();
        static Dictionary<DayOfWeek, int> DayMap = new Dictionary<DayOfWeek, int>();

        private string _outputFile;

        private IWorkbook _workbook;

        private ISheet _scheduleTemplate;

        public ExcelSchedulePrinter(string outputFile, IWorkbook workbook)
        {
            _outputFile = outputFile;
            _workbook = workbook;
            _scheduleTemplate = _workbook.GetSheet(ClassScheduleTemplate.SCHEDULE_TEMPLATE_NAME);
        }

        static ExcelSchedulePrinter()
        {

            // initialize TimeMap, maps times to row location
            TimeMap.Add(StartTime, startTimeLocationRow);
            var currTime = StartTime;
            var currRow = startTimeLocationRow - 1;
            while(currTime.CompareTo(EndTime) < 0)
            {
                currTime = currTime.Add(TimeInterval);
                currRow += CellSpanPerTimeInterval;
                TimeMap.Add(currTime, currRow);
            }

            // initialize DayMap: Maps days to column locations
            int i = 1;
            DayMap.Add(DayOfWeek.Sunday, i++);
            DayMap.Add(DayOfWeek.Monday, i++);
            DayMap.Add(DayOfWeek.Tuesday, i++);
            DayMap.Add(DayOfWeek.Wednesday, i++);
            DayMap.Add(DayOfWeek.Thursday, i++);
            DayMap.Add(DayOfWeek.Friday, i++);
            DayMap.Add(DayOfWeek.Saturday, i++);

        }
        
        /// <summary>
        /// Writes course schedules for each room in the excel file
        /// </summary>
        /// <param name="courseRepo"></param>
        /// <param name="roomRepo"></param>
        public void Print(ICourseRepository courseRepo, IRoomRepository roomRepo)
        {
            List<Course> courses = courseRepo.Courses.ToList();

            var coursesInRoom = from course in courses
                                where course.AlreadyAssignedRoom && course.MeetingDays != null
                                group course by course.RoomAssignment;


            foreach (var courseGroup in coursesInRoom)
            {
                string room = courseGroup.Key;
                ISheet sheet = _workbook.CloneSheet(_workbook.GetSheetIndex(_scheduleTemplate));
                var sheetIndex = _workbook.GetSheetIndex(sheet);
                _workbook.SetSheetName(sheetIndex, room);
                _workbook.SetSheetHidden(sheetIndex, SheetState.Visible);

                ICell cell = sheet.GetRow(RoomNameLocation.Item1).GetCell(RoomNameLocation.Item2);
                cell.SetCellValue(room);

                PrintCourses(sheet, courseGroup.ToList());
                printLegend(sheet);
            }

            _workbook.SortWorksheets();

            _workbook.WriteToFile(_outputFile);
        }

        private void printLegend(ISheet sheet)
        {
            CellReference cellReference = new CellReference("J5");
            int rowIndex = cellReference.Row;
            int cellIndex = cellReference.Col;

            OrderedDictionary subjectColorMap =  ClassScheduleTemplate.GetSubjectColorMap();
            foreach(DictionaryEntry entry in subjectColorMap)
            {
                IRow row = sheet.GetRow(rowIndex);
                ICell cell = row.GetCell(cellIndex);
                cell.CellStyle = ClassScheduleTemplate.GetCellStyle(_workbook, (short) entry.Value);
                cell.SetCellValue((string) entry.Key);
                rowIndex++;
            }
        }

        private Dictionary<string, List<Course>> getRoomNameToCoursesMap(IEnumerable<Course> courses) 
        {

            Dictionary<string, List<Course>> roomCourseMap = new Dictionary<string, List<Course>>();

            foreach (Course course in courses)
            {
                if (roomCourseMap.ContainsKey(course.RoomAssignment))
                {
                    roomCourseMap[course.RoomAssignment].Add(course);
                }
                else
                {
                    roomCourseMap.Add(course.RoomAssignment, new List<Course>() { course });
                }
            }

            return roomCourseMap;
        }

        private void PrintCourses(ISheet sheet, List<Course> courses)
        {
            foreach (Course course in courses)
            {
                foreach (DayOfWeek meetingDay in course.MeetingDays)
                {
                    int column = DayMap[meetingDay];
                    int startRow = GetRowForTime(course.StartTime.Value);
                    int endRow = GetRowForTime(course.EndTime.Value);

                    
                    //Get cell
                    var row = sheet.GetRow(startRow);
                    var cell = row.GetCell(column);

                    // Style cell
                    cell.CellStyle = ClassScheduleTemplate.GetCellStyle(_workbook, course.Color());

                    var cellValue = getCourseLabel(course);
                    cell.SetCellValue(cellValue);
                    sheet.AutoSizeColumn(column, true);

                    CellRangeAddress cellRange = new CellRangeAddress(startRow , endRow, column, column);
                    var regionIndex = sheet.AddMergedRegion(cellRange);
                }
            }

        }

        private string getCourseLabel(Course course)
        {
            return course.CourseTitle 
                + Environment.NewLine
                + string.Format("Sect. {0}", course.SectionNumber)
                + Environment.NewLine
                + course.Instructor 
                + Environment.NewLine 
                + course.MeetingPattern;
        }

        private int GetRowForTime(TimeSpan time)
        {
            int minutes = time.Minutes;
            if (minutes % 30 == 0)
            {
                return TimeMap[time];
            }
            else
            {
                minutes = (minutes / 30) * 30;
                return TimeMap[new TimeSpan(time.Hours, minutes, 0)];
            }
        }

        
    }

    
}
