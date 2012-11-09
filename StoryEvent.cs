using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicProto
{
    public class StoryEvent : Article
    {
        int Position { get; set; }
        CalendarTime startTime { get; set; }
        CalendarTime endTime { get; set; }

        /// <summary>
        /// Constructor used for deserialization.
        /// </summary>
        public StoryEvent()
        {
            //StateManager.Current.AllStoryEvents.Add(this);
        }
    }

    public class Book : StoryEvent
    {
        public List<uint> Chapters { get; set; }
    }

    public class Chapter : StoryEvent
    {
        public uint Perspective { get; set; }
        public uint Book { get; set; }
        public List<uint> Scenes { get; set; }
    }

    public class Scene : StoryEvent
    {
        public uint Perspective { get; set; }
        public uint Chapter { get; set; }
    }
}
