using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace ClassroomAssignment
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

            List<Course> courses = SheetParser.Parse(docLocations, new InMemoryRoomRepository());
            CourseRepository.initInstance(courses);


            if (courses.FindAll(m =>m.AmbiguousState).Count > 0)
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
    }
}
