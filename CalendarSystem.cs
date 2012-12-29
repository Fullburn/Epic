using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EpicProto
{
    public class CalendarSystem
    {
        public const int MinutesPerHour = 60;

        [XmlIgnore]
        public int MinutesPerDay { get { return MinutesPerHour * this.HoursPerDay; } }

        [XmlIgnore]
        public int MinutesPerYear { get { return this.MinutesPerDay * this.DaysPerYear; } }

        public int HoursPerDay { get; set; }

        public List<string> NamesOfDays { get; set; }

        public List<int> DaysPerMonth { get; set; }

        public List<string> NamesOfMonths;

        [XmlIgnore]
        public List<int> MinutesPerMonth { get { return this.DaysPerMonth.Select(days => this.MinutesPerDay * days).ToList(); } }

        [XmlIgnore]
        public int DaysPerWeek { get { return NamesOfDays.Count; } }

        [XmlIgnore]
        public int DaysPerYear { get { return DaysPerMonth.Sum(); } }

        public int BaseDayOfWeek { get; set; }

        public CalendarSystem()
        {
            this.NamesOfDays = new List<string>();
            this.NamesOfMonths = new List<string>();
            this.DaysPerMonth = new List<int>();
        }

        public CalendarSystem(int hoursPerDay, List<string> namesOfDays, List<int> daysPerMonth, List<string> namesOfMonths)
        {
            this.HoursPerDay = hoursPerDay;
            this.NamesOfDays = namesOfDays;
            this.NamesOfMonths = namesOfMonths;
            this.DaysPerMonth = daysPerMonth;
        }

    }

    /// <summary>
    /// Represents a date and time according to a selected CalendarSystem.
    /// </summary>
    public class CalendarTime
    {
        /// <summary>
        /// Deserialization constructor.
        /// </summary>
        public CalendarTime()
        {

        }

        public CalendarTime(int minute, int hour, int dayOfMonth, int month, int year)
        {
            minutes += year * (long)StateManager.Current.Calendar.DaysPerYear;
            minutes += StateManager.Current.Calendar.DaysPerMonth.Take(month - 1).Sum() * StateManager.Current.Calendar.MinutesPerDay;
            minutes += (long)(dayOfMonth - 1 * StateManager.Current.Calendar.MinutesPerDay);
            minutes += (long)(hour * CalendarSystem.MinutesPerHour);
            minutes += (long)(minute);
        }

        public int Minute
        {
            get
            {
                return (int)(minutes % CalendarSystem.MinutesPerHour);
            }
        }

        public int Hour
        {
            get
            {
                long hours = minutes / CalendarSystem.MinutesPerHour;
                return (int)(hours % StateManager.Current.Calendar.HoursPerDay);
            }
        }

        public int Day
        {
            get
            {
                long days = minutes / StateManager.Current.Calendar.MinutesPerDay;
                return 1;
            }
        }

        public int Month
        {
            get
            {
                long days = minutes / StateManager.Current.Calendar.MinutesPerDay;
                return 1;
            }
        }

        public int Year
        {
            get
            {
                return (int)(minutes / StateManager.Current.Calendar.MinutesPerYear);
            }
        }

        public int DayOfWeek
        {
            get
            {
                long days = minutes / StateManager.Current.Calendar.MinutesPerDay;
                return (int)((days + StateManager.Current.Calendar.BaseDayOfWeek) % StateManager.Current.Calendar.DaysPerWeek);
            }
        }

        private long minutes;
    }
}