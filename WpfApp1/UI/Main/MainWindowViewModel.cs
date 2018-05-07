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

namespace ClassroomAssignment.UI.Main
{
    /// <summary>
    /// Main Window
    /// </summary>
    [Serializable]
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool ContinueButtonEnabled { get; } = false;


        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            }
        }

        public ObservableCollection<Conflict> Conflicts { get; } = new ObservableCollection<Conflict>();

        
        public event PropertyChangedEventHandler PropertyChanged;

        public Course.CourseState Assigned { get; } = Course.CourseState.Assigned;

        /// <summary>
        /// initializes main window
        /// </summary>
        public MainWindowViewModel()
        {
            CourseRepository courseRepo = CourseRepository.GetInstance();
            Courses = new ObservableCollection<Course>(courseRepo.Courses);
            
            foreach (var conflict in courseRepo.GetConflicts())
            {
                Conflicts.Add(conflict);
            }

            courseRepo.ChangeInConflicts += CourseRepo_ChangeInConflicts;
        }

        private void CourseRepo_ChangeInConflicts(object sender, CourseRepository.ChangeInConflictsEventArgs e)
        {
            Conflicts.Clear();
            foreach (var conflict in e.Conflicts)
            {
                Conflicts.Add(conflict);
            }
            
        }

              
    }
    
}
