using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping7
{
    class Person
    {
        public string Title { get; set; }
    }

    class PersonInfo
    {
        public string Title { get; set; }
    }
    class Source<T>
    {
        public T Value { get; set; }
    }

    class Destination<T>
    {
        public T Value { get; set; }
    }


    public class CalendarEvent
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }

    public class CalendarEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Title { get; set; }
    }
}
