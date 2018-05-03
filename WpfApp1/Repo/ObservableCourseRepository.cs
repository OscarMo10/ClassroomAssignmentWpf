using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Repo
{
    class ObservableCourseRepository : ICourseRepository
    {
        protected ObservableCollection<Course> _courses;
        public IList<Course> Courses => _courses;

        public event PropertyChangedEventHandler CourseModified;
        public event NotifyCollectionChangedEventHandler CourseCollectionModified;

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Create _courses object and call RegisterPropertyListeners method.
        /// </summary>
        /// <param name="courses"></param>
        public ObservableCourseRepository(ICollection<Course> courses)
        {
            _courses = new ObservableCollection<Course>(courses);
            _courses.CollectionChanged += _courses_CollectionChanged;
            RegisterPropertyListeners();// call the method.
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
