using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassroomAssignment.Views
{
    class RoomScheduleView : StackPanel
    {

        private TextBlock roomNameTextBlock;
        private TextBlock roomCapacityTextBlock;

        public RoomScheduleView()
        {
            Orientation = Orientation.Vertical;
            
            AddHeaderViews();
            AddGrid();
            SetHrColumn();
        }

        private void AddHeaderViews()
        {
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            roomNameTextBlock = new TextBlock();
            roomNameTextBlock.Margin = new System.Windows.Thickness(0, 0, 5, 0);
            roomCapacityTextBlock = new TextBlock();
            stackPanel.Children.Add(roomNameTextBlock);
            stackPanel.Children.Add(roomCapacityTextBlock);

            Children.Add(stackPanel);
        }

        public void SetRoomName(string roomName)
        {
            roomNameTextBlock.Text = roomName;
        }

        public void SetRoomCapacity(string roomCapacity)
        {
            roomCapacityTextBlock.Text = roomCapacity;
        }

        public void AddGrid()
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            Grid grid = new Grid();
            
            for (int i = 0; i < 90; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            scrollViewer.Content = grid;
            scrollViewer.CanContentScroll = true;


            Children.Add(scrollViewer);
        }

        public void SetHrColumn()
        {
            var date = new DateTime(1, 1, 1, 7, 0, 0);

             for (int i = 0; i < 89; i++)
            {
                var textblock = new TextBlock();
                if (date.Minute == 0) textblock.Text = date.ToString("hh:mm tt");
                Children.Add(textblock);

                Grid.SetColumn(textblock, 0);
                Grid.SetRow(textblock, i + 2);

                date = date.AddMinutes(15);
            }
        }
    }
}
