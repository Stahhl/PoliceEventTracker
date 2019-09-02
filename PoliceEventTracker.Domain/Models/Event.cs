using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceEventTracker.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Coordinate { get; set; }

        public Location Location { get; set; }
    }
}
