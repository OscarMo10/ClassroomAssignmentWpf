using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.ViewModel
{
    class AssignmentViewModel
    {
        public ObservableCollection<Course> CoursesBeingAssigned { get; } = new ObservableCollection<Course>();

        public AssignmentViewModel(IList<Course> courses)
        {
            foreach(var course in courses)
            {
                CoursesBeingAssigned.Add(course);
            }
        }
    }
}
