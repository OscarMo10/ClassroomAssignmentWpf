using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClassroomAssignment.Views.RoomSchedule
{
    class CourseLabel : TextBlock
    {
        private readonly Course _boundCourse;
        public Course BoundCourse => _boundCourse;

        public CourseLabel(Course course)
        {
            _boundCourse = course;
            TextWrapping = TextWrapping.Wrap;
            Margin = new Thickness(5, 0, 5, 0);
            Text = course.CourseDescription;
        }

       
    }
}
