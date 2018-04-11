using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClassroomAssignment.ViewModel
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Assign = new RoutedUICommand("Assign", "Assign", typeof(CustomCommands));

    }
}
