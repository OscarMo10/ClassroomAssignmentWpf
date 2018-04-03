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

        public event EventHandler<CourseCollectionEventArgs> CourseModified;
        public event EventHandler<CourseCollectionEventArgs> CourseAdded;

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<Course> Courses {
            get => _courses;
        }

        public static CourseRepository GetInstance()
        {
            return _instance;
        }
        public static void initInstance(ICollection<Course> courses)
        {
            _instance = new CourseRepository(courses);
        }

        private CourseRepository(ICollection<Course> courses)
        {
            _courses = new ObservableCollection<Course>(courses);
            _courses.CollectionChanged += _courses_CollectionChanged;
        }

        private void _courses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var courses = e.NewItems.OfType<Course>().ToList();
            CourseCollectionEventArgs eventArgs = new CourseCollectionEventArgs(CourseCollectionEventArgs.EventType.Added, courses);
        }

        public class CourseCollectionEventArgs : EventArgs
        {
            public enum EventType { Modified, Added, Deleted };
            public readonly EventType Type;
            public readonly IList<Course> CoursesInvolved;

            public CourseCollectionEventArgs(EventType eventType, IList<Course> courses)
            {
                Type = eventType;
                CoursesInvolved = new List<Course>(courses);
            }
        }



    }
}
