using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for CreateProjectPage.xaml
    /// </summary>
    public partial class CreateProjectPage : Page
    {
        public CreateProjectPage()
        {
            InitializeComponent();
            RoomRepository.InitInstance();

        }


        private void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {

            string[] docLocations = GetSheetPaths();
            List<Course> courses = SheetParser.Parse(docLocations, RoomRepository.GetInstance());
            if (courses.Count == 0)
            {
                OnNewProjectCreationError();
                return;
            }

            var fileName = "original.bin";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, courses);
            stream.Close();

            CourseRepository.InitInstance(courses);


            NextPage(courses);

        }

        private void OnNewProjectCreationError()
        {
            ProjectCreationErrorTextBlock.Text = "Unable able to use selected folder to create new project.";
        }

        private string[] GetSheetPaths()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            var result = folderBrowser.ShowDialog();

            string[] docLocations = null;
            if (result == DialogResult.OK)
            {
                var pathToDocs = folderBrowser.SelectedPath;
                docLocations = Directory.GetFiles(pathToDocs);
            }

            return docLocations;
        }

        private void ExistingProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = GetFilePath();
            var courses = GetCourses(filePath);

            if (courses == null)
            {
                OnExistingProjectError();
            }
            else
            {
                CourseRepository.InitInstance(courses);
                NextPage(courses);
            }
           
        }

        private void OnExistingProjectError()
        {
            ProjectCreationErrorTextBlock.Text = "Selected file was not valid.";
        }

        private void NextPage(List<Course> courses)
        {
            if (courses.FindAll(m => m.AmbiguousState).Count > 0)
            {
                NavigationService.Navigate(new Uri(@"Windows/AmbiguityResolverPage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri(@"Windows/MainPage.xaml", UriKind.Relative));
            }
        }

        private List<Course> GetCourses(string filePath)
        {
            List<Course> courses = null;
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    courses = formatter.Deserialize(stream) as List<Course>;
                }
            }
            catch (Exception e)
            {

            }

            return courses;
        }

        private string GetFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Assignment File | *.agn";
            var result = dialog.ShowDialog();

            return result == DialogResult.OK ? dialog.FileName : null;
        }

        //private void InitCrossListedCourses(List<Course> courses)
        //{
        //    foreach (var course in courses)
        //    {
        //        if (!string.IsNullOrEmpty(course.CrossListings))
        //        {
        //            List<Course> crossListedCourses = new List<Course>();

        //            var regex = new Regex(@"\s([A-Z]+)\s(\d+)-(\d+)");
        //            var matches = regex.Matches(course.CrossListings);

        //            for (int i = 0; i < matches.Count; i++)
        //            {
        //                var subjectCode = matches[i].Groups[1].Value;
        //                var catalogNumber = matches[i].Groups[2].Value.TrimStart(new char[] { '0' });
        //                var sectionNumber = matches[i].Groups[3].Value.TrimStart(new char[] { '0' });
        //                var c = courses.Find(x => x.SubjectCode == subjectCode && x.CatalogNumber == catalogNumber && x.SectionNumber == sectionNumber);

        //                if (c != null) crossListedCourses.Add(c);
        //            }
        //        }


        //    }
        //}


    }
}

