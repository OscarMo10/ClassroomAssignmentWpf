using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace ClassroomAssignmentWpf.Notification
{
    class NotificationManager
    {
        private static NotificationManager _instance;
        private static CourseRepository _courseRepository;
        public static RoomConflictFinder _roomConflictFinder { get; set; }

        public static event EventHandler<CourseConflictEventArgs> OnCourseConflict;

        private NotificationManager(CourseRepository courseRepository, RoomConflictFinder roomConflictFinder)
        {
            _courseRepository = courseRepository;
            _roomConflictFinder = roomConflictFinder;

            _courseRepository.CourseModified += NotificationManager_CourseModified;
            _courseRepository.CourseCollectionModified += _courseRepository_CourseCollectionModified;

        }

        private void _courseRepository_CourseCollectionModified(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                List<Conflict> conflicts = _roomConflictFinder.ConflictsInvolvingCourses(e.NewItems.Cast<Course>().ToList());

                if (conflicts.Count != 0)
                {
                    OnCourseConflict?.Invoke(_instance, new CourseConflictEventArgs(conflicts));
                }
            }
            
        }

        private void NotificationManager_CourseModified(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Course course = sender as Course;

            List<Conflict> conflicts = _roomConflictFinder.ConflictsInvolvingCourse(course);

            if (conflicts.Count != 0)
            {
                OnCourseConflict?.Invoke(_instance, new CourseConflictEventArgs(conflicts));
            }
        }

        public NotificationManager Instance
        {
            get
            {
                if (Instance == null) throw new Exception("Notification must be initialized before getting instance.");

                return _instance;
            }
        }

       
        
    }
}
