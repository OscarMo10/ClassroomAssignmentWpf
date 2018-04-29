using ClassroomAssignment.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for CourseEditPage.xaml
    /// </summary>
    public partial class CourseEditPage : Page
    {
        public CourseEditPage()
        {
            InitializeComponent();

            Course copyOfCourse = new Course();

            var stream = new MemoryStream();
            IFormatter f = new BinaryFormatter();
            f.Serialize(stream, )
        }
    }
}
