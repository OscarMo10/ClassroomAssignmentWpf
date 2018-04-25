using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for CourseChangesWindow.xaml
    /// </summary>
    public partial class CourseChangesWindow : Window
    {
        public CourseChangesWindow()
        {
            InitializeComponent();
            List<Course> originalCourses = GetOriginalCourses();
            List<Course> upToDateCourses = GetUpToDateCourses();
            List<CourseDifference> courseDifferences = GetDifferences(originalCourses, upToDateCourses);
            DataContext = courseDifferences;
        }

        private List<Course> GetOriginalCourses()
        {
            string fileName = "original.bin";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = File.OpenRead(fileName);

            List<Course> originalCourses = formatter.Deserialize(stream) as List<Course>;
            originalCourses = originalCourses.OrderBy(x => int.Parse(x.ClassID)).ToList();
            stream.Close();

            return originalCourses;
        }

        private List<Course> GetUpToDateCourses()
        {
            return CourseRepository.GetInstance().Courses.OrderBy(x => int.Parse(x.ClassID)).ToList();
        }

        private List<CourseDifference> GetDifferences(List<Course> originalCourses, List<Course> newCourses)
        {
            List<CourseDifference> differences = new List<CourseDifference>();
            for (int i = 0; i < originalCourses.Count; i++)
            {
                for (int j = 0; j < newCourses.Count; j++)
                {
                    var difference = new CourseDifference();
                    if (originalCourses[i].ClassID_AsInt == newCourses[j].ClassID_AsInt)
                    {
                        if (CoursesAreDifferent(originalCourses[i], newCourses[j]))
                        {
                            difference.DifferenceType = "Modified";
                            difference.MostUpToDateCourse = newCourses[j];
                        }
                    }

                   // TODO: Finish Implementing differences
                }
            }

            return differences;
        }

        private bool CoursesAreDifferent(Course a, Course b)
        {
            return a.RoomAssignment != b.RoomAssignment;
        }

        private class CourseDifference
        {
            public string DifferenceType { get; set; }
            public Course MostUpToDateCourse { get; set; }
        }
    }
}
