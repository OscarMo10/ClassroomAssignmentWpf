using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClassroomAssignment.Model.Visual;
using ClassroomAssignment.ViewModel;
using System.Diagnostics;
using System.Windows.Input;
using ClassroomAssignment.Views;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using ClassroomAssignment.Repo;
using System.Collections.ObjectModel;

namespace ClassroomAssignment
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void Menu_Save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.Filter = "Binary File |*.bin";

            if(saveFileDialog2.ShowDialog() == true)
            {
                var fileName = saveFileDialog2.FileName;

                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

                    formatter.Serialize(stream, ViewModel.Courses.ToList());
                    stream.Close();

                }
                catch(SerializationException a)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + a.Message);
                }

            }
        }


        private void Menu_Export(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Worksheets|*.xls";
            if (saveFileDialog.ShowDialog() == true)
            {
                var fileName = saveFileDialog.FileName;
                var templateFile = Path.Combine(Environment.CurrentDirectory, "ClassroomGridTemplate.xls");
                using (var fileStream = File.OpenRead(templateFile))
                {
                    IWorkbook workbook = new HSSFWorkbook(fileStream);
                    workbook.RemoveSheetAt(workbook.GetSheetIndex("Sheet1"));

                    workbook.MissingCellPolicy = MissingCellPolicy.CREATE_NULL_AS_BLANK;
                    ExcelSchedulePrinter printer = new ExcelSchedulePrinter(fileName, workbook);
                    ICourseRepository courseRepository = CourseRepository.GetInstance();
                    //IRoomRepository roomRepository = InMemoryRoomRepository.getInstance();

                    new ScheduleVisualization(courseRepository, null, printer).PrintSchedule();
                }
            }

        }

        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
        }

        private void AssignMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var course = CoursesDataGrid.CurrentItem as Course;
         }

        private void ReassignMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AssignClassCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Course selectedCourse = CoursesDataGrid.CurrentItem as Course;

            if (selectedCourse == null) e.CanExecute = false;
            else e.CanExecute = true;
        }

        private void AssignClassCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Course selectedCourse = CoursesDataGrid.CurrentItem as Course;

            List<Course> courses = new List<Course>();
            courses.Add(selectedCourse);
            var assignmentWindow = new AssignmentWindow(courses);
            assignmentWindow.Show();

            Close();
        }

        private void Menu_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary File |*.bin";

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                try
                {
                    IFormatter format = new BinaryFormatter();
                    Stream stream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite);

                    ViewModel.Courses = new ObservableCollection<Course>(format.Deserialize(stream) as List<Course>); 
                    stream.Close();

                }
                catch (SerializationException f)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + f.Message);
                }
                

            }

        }
    }
}
