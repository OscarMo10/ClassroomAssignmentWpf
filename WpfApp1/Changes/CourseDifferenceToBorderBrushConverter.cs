using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ClassroomAssignment.Changes
{
    public class CourseDifferenceToBorderBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dataGridCell = values[0] as DataGridCell;
            var courseDiff = values[1] as CourseDifference;


            Brush NoChangeBrush = Brushes.Transparent;
            Brush ChangeBrush = Brushes.Red;

            if (dataGridCell == null || courseDiff == null) return NoChangeBrush;

            switch (dataGridCell.Column.Header)
            {
                case "Class ID":
                    if (courseDiff.OriginalCourse.ClassID == courseDiff.NewestCourse.ClassID) return NoChangeBrush;
                    else return ChangeBrush;
                case "Room Assignment":
                    if (courseDiff.OriginalCourse.RoomAssignment == courseDiff.NewestCourse.RoomAssignment) return NoChangeBrush;
                    else return ChangeBrush;

                default:
                    return NoChangeBrush;
            }

        }

      
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
