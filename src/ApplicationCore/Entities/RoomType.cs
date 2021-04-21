using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}