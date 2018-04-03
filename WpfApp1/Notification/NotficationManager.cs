using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Notification
{
    class NotificationManager
    {
        private static NotificationManager _instance;
        public static RoomConflictFinder RoomConflictFinder { get; set; }
        public static CourseRepository

        public static event EventHandler<CourseConflictEventArgs> OnCourseConflict;

        private NotificationManager()
        {
            CourseRepository.GetInstance().CourseModified += NotificationManager_CourseModified;

        }

        private void NotificationManager_CourseModified(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Course course = sender as Course;

            if ()

        }

        public NotificationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationManager();
                }

                return _instance;
            }
        }

       
    }
}
