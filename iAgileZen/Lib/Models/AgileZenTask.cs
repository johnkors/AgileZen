using System;

namespace AgileZen.Lib
{
    public class AgileZenTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Status { get; set; }
        public AgileZenUser FinishedBy { get; set; }
    }
}