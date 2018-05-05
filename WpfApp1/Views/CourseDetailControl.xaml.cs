using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
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

namespace ClassroomAssignment.Views
{
    /// <summary>
    /// Interaction logic for CourseDetailControl.xaml
    /// </summary>
    public partial class CourseDetailControl : UserControl
    {
        public static readonly DependencyProperty CourseShownProperty = DependencyProperty.Register("CourseShown", typeof(Course), typeof(CourseDetailControl));

       

        public Course CourseShown
        {
            get
            {
                return (Course)GetValue(CourseShownProperty);
            }

            set
            {
                SetValue(CourseShownProperty, value);
            }
        }
        

        public CourseDetailControl()
        {
            InitializeComponent();
        }

    }
}
