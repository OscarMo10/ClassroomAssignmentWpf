using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Operations;
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

        private ScheduleGridLayout gridLayout;
        static List<SolidColorBrush> BackgroundColors;
        private Dictionary<string, SolidColorBrush> colorMap = new Dictionary<string, SolidColorBrush>();
        private int currentColorIndex = 0;

        #region Dependency Properties
        private readonly DependencyProperty _roomScheduledProperty;
        public DependencyProperty RoomScheduledProperty
        {
            get => _roomScheduledProperty;
        }

        private void OnRoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var currentRoom = e.NewValue as Room;
            SetRoom(currentRoom);
        }

        [Bindable(true)]
        public Room RoomScheduled
        {
            get { return GetValue(RoomScheduledProperty) as Room; }

            set { SetValue(RoomScheduledProperty, value); }
        }

        private ObservableCollection<Course> _coursesForRoom;
        public ObservableCollection<Course> CoursesForRoom
        {
            get => _coursesForRoom;
            set
            {
                _coursesForRoom = value;
                value.CollectionChanged += CoursesForRoom_CollectionChanged;
            }
        }

        private void CoursesForRoom_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                RemoveStaleCourseLabels();
                SetCoursesForRoom(CoursesForRoom);

                foreach (var course in e.NewItems)
                {
                    (course as Course).PropertyChanged += CourseInRoom_PropertyChanged;
                }
            }
        }

        private void CourseInRoom_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var course = sender as Course;

            foreach (var child in ScheduleGrid.Children)
            {
                CourseLabel courseLabel;
                if ((courseLabel = child as CourseLabel) != null)
                {
                    if (courseLabel)
                }
            }
        }

        private ObservableCollection<ScheduleSlot> _availableScheduleSlots;
        public ObservableCollection<ScheduleSlot> AvailableScheduleSlots
        {
            get => _availableScheduleSlots;
            set
            {
                _availableScheduleSlots = value;
                value.CollectionChanged += AvailableSlots_CollectionChanged;
            }
        }

        private void AvailableSlots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                RemoveStaleAvailableItems();
                ShowAvailableSlots();
            }
        }

        #endregion



        static RoomScheduleControl()
        {
            var colors = new List<SolidColorBrush>()
            {
                Brushes.Aqua,
                Brushes.Azure,
                Brushes.Beige,
                Brushes.BurlyWood,
                Brushes.Cornsilk,
                Brushes.Coral,
                Brushes.FloralWhite
            };

            BackgroundColors = colors.ConvertAll(x =>
            {
                var color = x.Color;
                color.A = 200;
                return new SolidColorBrush(color);
            });
        }

        public RoomScheduleControl()
        {
            InitializeComponent();

            _roomScheduledProperty = DependencyProperty.Register("RoomScheduled", typeof(Room), typeof(RoomScheduleControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnRoomChanged)));

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

        private void SetRoom(Room room)
        {
            RoomNameTextBlock.Text = room.RoomName;
            RoomCapacityTextBlock.Text = room.Capacity.ToString();
        }

        private void SetCoursesForRoom(IEnumerable<Course> courses)
        {
            RemoveStaleCourseLabels();

            foreach (var course in courses)
            {
                foreach (var day in course.MeetingDays)
                {
                    var textBlock = GetCourseLabel(day, course);
                    textBlock.Background = GetBackgroundColorForCourse(course);
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Text = course.CourseDescription;
                    Grid.SetRowSpan(textBlock, RowSpanForCourse(course));
                }
            }
        }

        public void RemoveStaleAvailableItems()
        {
            List<TextBlock> staleAvailableItems = new List<TextBlock>();
            foreach (var child in ScheduleGrid.Children)
            {
                AvailableSlotLabel textBlock;
                if ((textBlock = child as AvailableSlotLabel) != null) staleAvailableItems.Add(textBlock);
              
            }

            foreach (var staleItem in staleAvailableItems) ScheduleGrid.Children.Remove(staleItem);
        }

        public void ShowAvailableSlots()
        {
            foreach (var slot in AvailableScheduleSlots)
            {
                foreach (var day in slot.MeetingDays)
                {
                    int row = gridLayout.GetRowForTime(slot.StartTime);
                    int span = gridLayout.SpanForDurationInMinutes((int)(slot.EndTime - slot.StartTime).TotalMinutes);

                    var availableSlot = new AvailableSlotLabel(slot.StartTime, slot.EndTime);

                    ScheduleGrid.Children.Add(availableSlot);
                    Grid.SetRow(availableSlot, gridLayout.GetRowForTime(slot.StartTime));
                    Grid.SetColumn(availableSlot, gridLayout.GetColumnForDay(day));
                    Grid.SetRowSpan(availableSlot, span);
                }
            }
        }

        #endregion

        #region Private Helper Methods

        private SolidColorBrush GetBackgroundColorForCourse(Course course)
        {
            if (colorMap.ContainsKey(course.SubjectCode))
            {
                return colorMap[course.SubjectCode];
            }
            else if (BackgroundColors.Count == currentColorIndex + 1)
            {
                return BackgroundColors.Last();
            }
            else
            {
                currentColorIndex++;
                colorMap[course.SubjectCode] = BackgroundColors[currentColorIndex];
                return colorMap[course.SubjectCode];
            }
        }

        private void RemoveStaleCourseLabels()
        {
            var staleLabels = new List<CourseLabel>();
            foreach (var child in ScheduleGrid.Children)
            {
                CourseLabel availableSlot = child as CourseLabel;
                if (availableSlot != null)
                {
                    staleLabels.Add(availableSlot);
                }
            }

            foreach (var staleLabel in staleLabels)
            {
                ScheduleGrid.Children.Remove(staleLabel);
            }
        }

        private CourseLabel GetCourseLabel(DayOfWeek day, Course course)
        {
            var courseLabel = new CourseLabel(course);
            var brush = BackgroundColors[0];
            courseLabel.Background = brush;

            ScheduleGrid.Children.Add(courseLabel);
            Grid.SetRow(courseLabel, gridLayout.GetRowForTime(course.StartTime.Value));
            Grid.SetColumn(courseLabel, gridLayout.GetColumnForDay(day));

            return courseLabel;
        }

        private int RowSpanForCourse(Course course)
        {
            TimeSpan courseLength = course.EndTime.Value - course.StartTime.Value;

            return gridLayout.SpanForDurationInMinutes((int) courseLength.TotalMinutes);
        }

        #endregion

       
    }
}
