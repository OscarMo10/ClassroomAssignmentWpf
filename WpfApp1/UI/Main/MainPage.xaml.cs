using ClassroomAssignment.Changes;
using ClassroomAssignment.Model;
using ClassroomAssignment.Operations;
using ClassroomAssignment.Repo;
using ClassroomAssignment.UI.Assignment;
using ClassroomAssignment.UI.Edit;
using ClassroomAssignment.ViewModel;
using ClassroomAssignment.Visual;
using ClassroomAssignment.Windows;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassroomAssignment.UI.Main
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();

        public MainPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            while (NavigationService.RemoveBackEntry() != null)
            {

            }
        }


        private void Menu_Save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.Filter = "Assignment File | *.agn";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                var fileName = saveFileDialog2.FileName;

                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

                    formatter.Serialize(stream, ViewModel.Courses.ToList());
                    stream.Close();

                }
                catch (SerializationException a)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + a.Message);
                }

            }
        }

        private void Menu_Changes(object sender, EventArgs e)
        {
            NavigationService.Navigate(new ChangesPage());

        }

        private void ConflictsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var conflict = ConflictsListView.SelectedItem as Conflict;
            if (conflict != null)
            {
                var assignmentPage = new AssignmentPage(conflict.ConflictingCourses);
                NavigationService.Navigate(assignmentPage);
            }
        }

        private void AssignMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CoursesDataGrid.CurrentItem as Course;
            if (selectedCourse == null || selectedCourse.State == Course.CourseState.NoRoomRequired) return;


            List<Course> courses = new List<Course>();
            courses.Add(selectedCourse);
            var assignmentPage = new AssignmentPage(courses);
            NavigationService.Navigate(assignmentPage);
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel Worksheets|*.xls";
            if (saveFileDialog.ShowDialog() == true)
            {
                var fileName = saveFileDialog.FileName;
                var templateFile = System.IO.Path.Combine(Environment.CurrentDirectory, "ClassroomGridTemplate.xls");
                using (var fileStream = File.OpenRead(templateFile))
                {
                    IWorkbook workbook = new HSSFWorkbook(fileStream);
                    workbook.RemoveSheetAt(workbook.GetSheetIndex("Sheet1"));

                    workbook.MissingCellPolicy = MissingCellPolicy.CREATE_NULL_AS_BLANK;
                    ExcelSchedulePrinter printer = new ExcelSchedulePrinter(fileName, workbook);
                    ICourseRepository courseRepository = CourseRepository.GetInstance();

                    new ScheduleVisualization(courseRepository, null, printer).PrintSchedule();
                }
            }
        }

        private void CoursesContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var course = CoursesDataGrid.SelectedItem as Course;

            if (course == null) CoursesContextMenu.IsEnabled = false;
            else CoursesContextMenu.IsEnabled = true;

            if (course?.State == Course.CourseState.NoRoomRequired) AssignMenuItem.IsEnabled = false;
            else AssignMenuItem.IsEnabled = true;
        }

        private void CoursesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var course = CoursesDataGrid.SelectedItem as Course;
            if (course == null) return;

            var editPage = new CourseEditPage(course);
            NavigationService.Navigate(editPage);
        }

        private void IgnoreMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var courses = CoursesDataGrid.SelectedItems;

            foreach (Course course in courses)
            {
                course.NeedsRoom = false;
            }
        }

        private void RoomSearchButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomSearchPage());
        }
    }
}

