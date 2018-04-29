using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
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
using WinForms = System.Windows.Forms;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();


        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderBrowser = new WinForms.FolderBrowserDialog();
            var result = folderBrowser.ShowDialog();

            string[] docLocations;
            if (result == WinForms.DialogResult.OK)
            {
                var pathToDocs = folderBrowser.SelectedPath;
                docLocations = Directory.GetFiles(pathToDocs);
            }
            else
            {
                return;
            }

            RoomRepository.InitInstance();
            List<Course> courses = SheetParser.Parse(docLocations, RoomRepository.GetInstance());
            var fileName = "original.bin";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, courses);
            stream.Close();

            CourseRepository.initInstance(courses);


            if (courses.FindAll(m => m.AmbiguousState).Count > 0)
            {
                AmbiguityResolverWindow mainWindow = new AmbiguityResolverWindow();
                mainWindow.Show();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }

            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Binary File |*.bin";

            if (openFileDialog2.ShowDialog() == true)
            {
                var fileName = openFileDialog2.FileName;

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
