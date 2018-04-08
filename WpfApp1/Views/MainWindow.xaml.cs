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
    }
}
