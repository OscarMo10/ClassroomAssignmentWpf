using ClassroomAssignment.Model;
using ClassroomAssignment.Repo;
using ClassroomAssignment.Visual;
using ClassroomAssignment.Notification;
using ClassroomAssignment.Operations;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ClassroomAssignment.ViewModel
{
    [Serializable]
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool ContinueButtonEnabled { get; } = false;

        private IEnumerable<Course> _courses;
        public IEnumerable<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            }
        }
        public IEnumerable<Conflict> Conflicts { get; set; }

        
        public event PropertyChangedEventHandler PropertyChanged;

        public Course.CourseState Assigned { get; } = Course.CourseState.Assigned;

        public MainWindowViewModel()
        {
            CourseRepository courseRepo = CourseRepository.GetInstance();
            Courses = courseRepo.Courses;
            
            Conflicts = courseRepo.GetConflicts();
            courseRepo.ChangeInConflicts += CourseRepo_ChangeInConflicts;
        }

        private void CourseRepo_ChangeInConflicts(object sender, CourseRepository.ChangeInConflictsEventArgs e)
        {
            Conflicts = e.Conflicts;
        }

        
        private int CompareCourses(Course c1, Course c2)
        {
            var value1 = CourseValue(c1);
            var value2 = CourseValue(c2);
            if (value1 == value2)
            {
                var val1 = int.Parse(c1.ClassID);
                var val2 = int.Parse(c2.ClassID);
                return val1 - val2;
            }
            else return value2 - value1;
        }

        private static int CourseValue(Course course)
        {
            if (course.NeedsRoom)
            {
                if (!course.AlreadyAssignedRoom) return 4;
                else return 3;
            }
            else return 2;
        }

       
    }
    
}
