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
    class InMemoryCourseRepository : ICourseRepository
    {
        private static InMemoryCourseRepository _instance;
        private List<Course> _courses;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Course> Courses {
            get { return _courses; }
        }

        public static InMemoryCourseRepository getInstance()
        {
            return _instance;
        }
        public static void initInstance(List<Course> courses)
        {
            _instance = new InMemoryCourseRepository(courses);
        }

        private InMemoryCourseRepository(List<Course> courses)
        {
            _courses = courses;
        }

        

        

  
    }
}
