using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model.Repo
{
    class CourseRepository : ICourseRepository
    {
        private static CourseRepository _instance;
        private ObservableCollection<Course> _courses;

        public event PropertyChangedEventHandler CourseModified;
        public event NotifyCollectionChangedEventHandler CourseCollectionModified;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Course> Courses {
            get => _courses.ToList();
        }

        public static CourseRepository GetInstance()
        {
            return _instance;
        }
        public static void initInstance(ICollection<Course> courses)
        {
            if (courses == null) throw new ArgumentNullException();

            _instance = new CourseRepository(courses);
        }

        private CourseRepository(ICollection<Course> courses)
        {
            _courses = new ObservableCollection<Course>(courses);
            _courses.CollectionChanged += _courses_CollectionChanged;
            RegisterPropertyListeners();
        }

        private void RegisterPropertyListeners()
        {
            foreach (var course in _courses)
            {
                course.PropertyChanged += Course_PropertyChanged;
            }
        }

        private void Course_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CourseModified?.Invoke(sender, e);
        }

        private void _courses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CourseCollectionModified?.Invoke(sender, e);
        }
    }
}
