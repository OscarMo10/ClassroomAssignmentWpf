using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassroomAssignment.Model.Utils
{
    public class DateUtil
    {
        private static Dictionary<string, DayOfWeek> DayNameMatcher = new Dictionary<string, DayOfWeek>();

        static DateUtil()
        {
            DayNameMatcher.Add("Sn", DayOfWeek.Sunday);
            DayNameMatcher.Add("M", DayOfWeek.Monday);
            DayNameMatcher.Add("T", DayOfWeek.Tuesday);
            DayNameMatcher.Add("W", DayOfWeek.Wednesday);
            DayNameMatcher.Add("Th", DayOfWeek.Thursday);
            DayNameMatcher.Add("F", DayOfWeek.Friday);
            DayNameMatcher.Add("Sa", DayOfWeek.Saturday);
        }

        public static string ShortToLongDayName(string dayAbbreviation)
        {
            try
            {
                return DayNameMatcher[dayAbbreviation].ToString();
            }
            catch (Exception e)
            {
                //LogUtil.Debug(e.Message);
                return null;
            }
        }

        public static DayOfWeek AbbreviationToDayOfWeek(string dayAbbreviation)
        {
            return DayNameMatcher[dayAbbreviation];
        }
    }
}
