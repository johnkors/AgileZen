using System.Collections.Generic;

namespace AgileZen.Lib
{
    public class AgileZenStory
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public string Priority { get; set; }
        public AgileZenPhase Phase { get; set; }
        public AgileZenUser Creator { get; set; }
        public AgileZenUser Owner { get; set; }
        public IEnumerable<AgileZenTask> Tasks { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}