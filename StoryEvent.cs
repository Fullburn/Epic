using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicProto
{
    public enum EventType
    {
        Book,
        Chapter,
        Scene,
    }

    public class StoryEvent : Article
    {
        public EventType EventType { get; set; }
        public int Position { get; set; }
        public CalendarTime StartTime { get; set; }
        public CalendarTime EndTime { get; set; }

        public uint ParentEvent { get; set; }
        public List<uint> ChildEvents { get; set; }

        public uint Perspective {
            get { return _perspective; }
            set { _perspective = value; }
        }

        private uint _perspective;

        /// <summary>
        /// Constructor used for deserialization.
        /// </summary>
        public StoryEvent() : base()
        {
            //StateManager.Current.AllStoryEvents.Add(this);
        }
    }
}
