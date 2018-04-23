using ClassroomAssignment.Model;
using ClassroomAssignment.Visual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ClassroomAssignment.Views.RoomSchedule
{
    /// <summary>
    /// Interaction logic for RoomScheduleControl.xaml
    /// </summary>
    public partial class RoomScheduleControl : UserControl
    {

        private const int COLUMN_WIDTH = 50;
        private const int TIME_DURATION_UNIT_IN_MINUTES = 15;
        private static readonly TimeSpan FIRST_TIME_SLOT = new TimeSpan(7, 0, 0);
        private static readonly TimeSpan LAST_TIME_SLOT = new TimeSpan(22, 0, 0);
        private const DayOfWeek FIRST_DAY_OF_SCHEDULE = DayOfWeek.Sunday;
        private const DayOfWeek LAST_DAY_OF_SCHEDULE = DayOfWeek.Saturday;
        private const string SCHEDULE_ITEM_TAG = "scheduleItem";

        private ScheduleGridLayout gridLayout;

        //public static readonly DependencyProperty CoursesInRoomProperty =
        //    DependencyProperty.Register(nameof(CoursesInRoom), typeof(ObservableCollection<Course>), typeof(RoomScheduleControl), new PropertyMetadata(default(ObservableCollection<Course>)));

        //[Bindable(true)]
        //public ObservableCollection<Course> CoursesInRoom
        //{
        //    get { return (ObservableCollection<Course>)GetValue(CoursesInRoomProperty); }
        //    set { SetValue(CoursesInRoomProperty, value); }
        //}

        public RoomScheduleControl()
        {
            InitializeComponent();


            gridLayout = new ScheduleGridLayout(
                FIRST_TIME_SLOT,
                LAST_TIME_SLOT,
                FIRST_DAY_OF_SCHEDULE, 
                LAST_DAY_OF_SCHEDULE, 
                TIME_DURATION_UNIT_IN_MINUTES
                );
            SetupScheduleGrid();
        }


        #region Setup

        private void SetupScheduleGrid()
        {
            SetupStructure();
            PopulateHeaders();
        }

        private void SetupStructure()
        {
            AddTimeColumn();
            AddEmptyColumn();
            AddDayOfWeekColumns();
            AddEmptyColumn();

            AddDayOfWeekRow();
            AddTimeRows();
            AddBorders();
        }

        private void PopulateHeaders()
        {
            SetupTimeRowHeaders();
            SetupDayOfWeekColumnHeaders();
        }

        private void AddTimeRows()
        {
            foreach (var slot in gridLayout.TimeSlotsInSchedule())
            {
                ScheduleGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void AddDayOfWeekRow()
        {
            ScheduleGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void AddDayOfWeekColumns()
        {
            var column = ScheduleGrid.ColumnDefinitions.Count;
            foreach (var day in gridLayout.DaysOfWeekInGrid())
            {
                var columnDef = new ColumnDefinition();
                columnDef.Width = new GridLength(1, GridUnitType.Star);
                ScheduleGrid.ColumnDefinitions.Add(columnDef);
            }
        }

        private void AddTimeColumn()
        {
            ScheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void AddEmptyColumn()
        {
            var columnDef = new ColumnDefinition();
            columnDef.Width = new GridLength(COLUMN_WIDTH, GridUnitType.Pixel);
            ScheduleGrid.ColumnDefinitions.Add(columnDef);
        }


        private void SetupTimeRowHeaders()
        {
            foreach (var slot in gridLayout.TimeSlotsInSchedule())
            {
                var textblock = GetTextBlockForTime(slot);
                ScheduleGrid.Children.Add(textblock);
                Grid.SetColumn(textblock, 0);
                Grid.SetRow(textblock, gridLayout.GetRowForTime(slot));
            }
        }

        private void SetupDayOfWeekColumnHeaders()
        {
            const int firstRow = 0;
            for (DayOfWeek day = FIRST_DAY_OF_SCHEDULE; day <= LAST_DAY_OF_SCHEDULE; day++)
            {
                var textBlock = GetTextBlockForDay(day);
                ScheduleGrid.Children.Add(textBlock);
                Grid.SetRow(textBlock, firstRow);
                Grid.SetColumn(textBlock, gridLayout.GetColumnForDay(day));
            }
        }

        private TextBlock GetTextBlockForDay(DayOfWeek day)
        {
            var textBlock = new TextBlock();
            textBlock.Style = FindResource("DayOfWeekHeaderStyle") as Style;
            textBlock.Text = day.ToString();

            return textBlock;
        }

        private TextBlock GetTextBlockForTime(TimeSpan time)
        {
            var textblock = new TextBlock();
            textblock.Text = new DateTime().Add(time).ToString("h tt");
            if (time.Minutes != 0) textblock.Visibility = Visibility.Hidden;

            return textblock;
        }


        private void AddBorders()
        {
            AddDayOfWeekColumnBorders();
            AddHrlyRowBorders();
        }

        private void AddDayOfWeekColumnBorders()
        {

            foreach (var day in gridLayout.DaysOfWeekInGrid())
            {
                Border border = new Border();
                if (day == FIRST_DAY_OF_SCHEDULE)
                {
                    border.Style = FindResource("LeftMostColumnBorder") as Style;
                }
                else
                {
                    border.Style = FindResource("ColumnBorder") as Style;
                }

                ScheduleGrid.Children.Add(border);
                Grid.SetColumn(border, gridLayout.GetColumnForDay(day));
                Grid.SetRow(border, 1);
                Grid.SetRowSpan(border, ScheduleGrid.RowDefinitions.Count - 1);
            }
        }

        private void AddHrlyRowBorders()
        {
            var thickness = new Thickness(0, 1, 0, 0);

            foreach (var time in gridLayout.TimeSlotsInSchedule())
            {
                if (time.Minutes == 0)
                {
                    var border = new Border();
                    border.BorderThickness = thickness;
                    border.BorderBrush = Brushes.Gray;
                    ScheduleGrid.Children.Add(border);

                    Grid.SetRow(border,gridLayout.GetRowForTime(time));
                    Grid.SetColumnSpan(border, 10);
                }
            }
        }

        #endregion

        #region Public Methods

        public void SetRoom(Room room)
        {
            RoomNameTextBlock.Text = room.RoomName;
            RoomCapacityTextBlock.Text = room.Capacity.ToString();
        }

        public void SetCoursesForRoom(IEnumerable<Course> courses)
        {
            RemoveOldScheduleItems();

            foreach (var course in courses)
            {
                foreach (var day in course.MeetingDays)
                {
                    var textBlock = GetTextBlock(day, course.StartTime.Value);
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Text = LabelForCourse(course);
                    Grid.SetRowSpan(textBlock, RowSpanForCourse(course));
                }
            }
        }

        public void ShowAvailableSlot(DayOfWeek meetingDay, TimeSpan startTime, TimeSpan endTime)
        {
            int row = gridLayout.GetRowForTime(startTime);
            int span = gridLayout.SpanForDurationInMinutes((int)(endTime - startTime).TotalMinutes);

            var textblock = new TextBlock();
            textblock.Background = Brushes.Green;
            textblock.Tag = "Available";
            var start = new DateTime().Add(startTime);
            var end = new DateTime().Add(endTime);
            textblock.Text = string.Format("{0}{1}{2:t}-{3:t}", "Available", Environment.NewLine, start, end);
            ScheduleGrid.Children.Add(textblock);
            Grid.SetRow(textblock, gridLayout.GetRowForTime(startTime));
            Grid.SetColumn(textblock, gridLayout.GetColumnForDay(meetingDay));
            Grid.SetRowSpan(textblock, span);
        }

        #endregion

        #region Private Helper Methods

        private void RemoveOldScheduleItems()
        {
            var textblocksToRemove = new List<TextBlock>();
            foreach (var child in ScheduleGrid.Children)
            {
                TextBlock textBlock = child as TextBlock;
                if (textBlock != null)
                {
                    if ((textBlock.Tag as string) == SCHEDULE_ITEM_TAG)
                    {
                        textblocksToRemove.Add(textBlock);
                    }
                }
            }

            foreach (var textblock in textblocksToRemove)
            {
                ScheduleGrid.Children.Remove(textblock);
            }
        }

        private TextBlock GetTextBlock(DayOfWeek day, TimeSpan time)
        {
            var textBlock = new TextBlock();
            textBlock.Tag = SCHEDULE_ITEM_TAG;
            var color = Brushes.LightSlateGray.Color;
            color.A = 100;
            textBlock.Background = new SolidColorBrush(color);
            textBlock.Margin = new Thickness(5, 0, 5, 0);
            ScheduleGrid.Children.Add(textBlock);
            Grid.SetRow(textBlock, gridLayout.GetRowForTime(time));
            Grid.SetColumn(textBlock, gridLayout.GetColumnForDay(day));

            return textBlock;
        }

        private string LabelForCourse(Course course)
        {
            return course.CourseName;
        }


        private int RowSpanForCourse(Course course)
        {
            TimeSpan courseLength = course.EndTime.Value - course.StartTime.Value;

            return gridLayout.SpanForDurationInMinutes((int) courseLength.TotalMinutes);
        }

        #endregion
    }
}
