using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for CourseEditPage.xaml
    /// </summary>
    public partial class CourseEditPage : Page
    {
        private Course originalCourse;
        private Course copyCourse;
        private List<PropertyInfo> propertiesChanged = new List<PropertyInfo>();
        public CourseEditPage(Course course)
        {
            InitializeComponent();

            originalCourse = course;
            var stream = new MemoryStream();
            IFormatter f = new BinaryFormatter();
            f.Serialize(stream, course);
            stream.Seek(0, SeekOrigin.Begin);
            copyCourse = f.Deserialize(stream) as Course;
            stream.Close();

            copyCourse.PropertyChanged += CopyCourse_PropertyChanged;
            CourseDetail.DataContext = copyCourse;
        }

        private void CopyCourse_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Type type = originalCourse.GetType();
            PropertyInfo propertyInfo = type.GetProperty(e.PropertyName);
            if (propertyInfo.SetMethod != null)
            {
                propertiesChanged.Add(propertyInfo);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var property in propertiesChanged)
            {
                var newValue = property.GetValue(copyCourse);
                property.SetValue(originalCourse, newValue);
            }

            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
