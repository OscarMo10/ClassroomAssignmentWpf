using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Model.Visual;
using ClassroomAssignmentWpf.Notification;
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

namespace ClassroomAssignment.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool ContinueButtonEnabled { get; } = false;
        public ObservableCollection<Course> Courses { get; set; }
        public List<Conflict> Conflicts { get; set; }
        CourseConflictDetector detector;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            CourseRepository courseRepo = CourseRepository.GetInstance();
            List<Course> courses = courseRepo.Courses;
            courses.Sort(CompareCourses);
            Courses = new ObservableCollection<Course>(courses);

            detector = new CourseConflictDetector(courseRepo);
            Conflicts = detector.AllConflicts();

            courseRepo.CourseModified += CourseRepo_CourseModified;
        }

        private void CourseRepo_CourseModified(object sender, PropertyChangedEventArgs e)
        {
            Conflicts = detector.AllConflicts();
            
        }

        private void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
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
