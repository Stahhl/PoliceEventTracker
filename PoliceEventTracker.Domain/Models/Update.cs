using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceEventTracker.Domain.Models
{
    public class Update
    {
        public Update()
        {
            Events = new List<Event>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        public List<Event> Events { get; set; }
    }
}
