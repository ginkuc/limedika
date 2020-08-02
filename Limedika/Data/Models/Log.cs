using System;

namespace Limedika.Data.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public LocationActionEnum Action { get; set; }
    }
}