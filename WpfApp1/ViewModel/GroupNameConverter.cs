using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ClassroomAssignment.Extension;

namespace ClassroomAssignment.ViewModel
{
    
    public class GroupNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Course.CourseState? state = value as Course.CourseState?;

            if (!state.HasValue) return string.Empty;

            switch(state.Value)
            {
                case Course.CourseState.Ambiguous:
                    return Course.CourseState.Ambiguous.GetDescription();
                case Course.CourseState.Assigned:
                    return Course.CourseState.Assigned.GetDescription();
                case Course.CourseState.Unassigned:
                    return Course.CourseState.Unassigned.GetDescription();
                case Course.CourseState.NoRoomRequired:
                    return Course.CourseState.NoRoomRequired.GetDescription();
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
