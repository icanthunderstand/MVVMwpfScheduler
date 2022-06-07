using System;
using System.Windows.Media;
using advancedWpfProject.Core;
using System.Collections.Generic;

namespace advancedWpfProject.MVVM.Model
{
    public class Event : ObserverableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
        public string LoginId { get; set; }
    }

    public class MergeList
    {
        public Event Event { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int OverlapCount { get; set; }

        public MergeList(Event ev, int left, int OverlapCount,int width)
        {
            Event = ev;
            this.Left = left;
            this.Width = width;
            this.OverlapCount = OverlapCount;
        }
    }

    public class MonthList
    {
        public List<Event> List { get; set; }
        public int Index { get; set; }

        public MonthList()
        {
            List = new List<Event>();
        }

    }

}

