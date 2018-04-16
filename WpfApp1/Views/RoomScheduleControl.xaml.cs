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
    /// Interaction logic for RoomScheduleControl.xaml
    /// </summary>
    public partial class RoomScheduleControl : UserControl
    {

        TextBlockCache textBlockCache = new TextBlockCache();
        public RoomScheduleControl()
        {
            InitializeComponent();
            AddRowDefinitions();
            AddColumnDefinitions();
            SetHrColumn();
            SetDayOfWeekRow();
            AddFormatting();
        }

        public void SetCourses(List<Course> coursesForRoom)
        {
            foreach (var course in coursesForRoom)
            {
                foreach (var day in course.MeetingDays)
                {
                    var textBlock = textBlockCache.GetTextBlock(day, course.StartTime.Value);
                    if (textBlockCache == null)
                    {
                        textBlock = new TextBlock();
                        ScheduleGrid.Children.Add(textBlock);
                        textBlockCache.AddTextBlock(day, course.StartTime.Value, textBlock);
                    }


                }
            }
        }

        private void AddRowDefinitions()
        {
            for (int i = 0; i < 90; i++)
            {
                var rowDef = new RowDefinition();
                ScheduleGrid.RowDefinitions.Add(rowDef);
            }
        }

        private void AddColumnDefinitions()
        {
            ScheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
            var columnDef = new ColumnDefinition();
            columnDef.Width = new GridLength(50, GridUnitType.Pixel);
            ScheduleGrid.ColumnDefinitions.Add(columnDef);


            for (int i = 0; i < 10; i++)
            {
                columnDef = new ColumnDefinition();
                ScheduleGrid.ColumnDefinitions.Add(columnDef);
            }
        }

        private void SetHrColumn()
        {
            var date = new DateTime(1, 1, 1, 7, 0, 0);

            for (int i = 0; i < 62; i++)
            {
                var textblock = new TextBlock();
                textblock.Text = date.ToString("h tt");
                if (date.Minute == 0) textblock.Visibility = Visibility.Hidden;
                ScheduleGrid.Children.Add(textblock);

                Grid.SetColumn(textblock, 0);
                Grid.SetRow(textblock, i + 3);

                date = date.AddMinutes(15);
            }
        }

        private void SetDayOfWeekRow()
        {
            
            int i = 0;
            for (DayOfWeek day = DayOfWeek.Monday; day < DayOfWeek.Saturday; day++)
            {
                var textblock = new TextBlock();
                textblock.Margin = new Thickness(0, 0, 5, 0);
                textblock.Width = 100;
                textblock.Text = day.ToString();
                textblock.TextAlignment = TextAlignment.Center;

                ScheduleGrid.Children.Add(textblock);
                Grid.SetColumn(textblock, 3 + i++);
                Grid.SetRow(textblock, 0);
            }

        }

        private void AddFormatting()
        {
            for (int i = 0; i < 5; i++)
            {
                Border border = new Border();
                if (i == 0)
                {
                    border.BorderThickness = new Thickness(2, 0, 1, 0);
                }
                else if (i == 4)
                {
                    border.BorderThickness = new Thickness(1, 0, 2, 0);
                }
                else
                {
                    border.BorderThickness = new Thickness(1, 0, 1, 0);
                } 

                border.BorderBrush = Brushes.Black;
                ScheduleGrid.Children.Add(border);
                Grid.SetColumn(border, 3 + i);
                Grid.SetRow(border, 1);
                Grid.SetRowSpan(border, ScheduleGrid.RowDefinitions.Count - 1);
            }
            
        }

        private class TextBlockCache
        {
            private Dictionary<DayOfWeek, Dictionary<TimeSpan, TextBlock>> textblockFinder = new Dictionary<DayOfWeek, Dictionary<TimeSpan, TextBlock>>();

            public TextBlockCache()
            {
                for (DayOfWeek day = DayOfWeek.Monday; day <= DayOfWeek.Friday; day++)
                {
                    textblockFinder.Add(day, new Dictionary<TimeSpan, TextBlock>());
                }
            }

            public TextBlock GetTextBlock(DayOfWeek day, TimeSpan timeSpan)
            {
                var textblockDict = textblockFinder[day];
                if (textblockDict.ContainsKey(timeSpan)) return textblockDict[timeSpan];

                return null;
            }

            public void AddTextBlock(DayOfWeek day, TimeSpan time, TextBlock textBlock)
            {
                textblockFinder[day].Add(time, textBlock);
            }
        }
    }

   
}
