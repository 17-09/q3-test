using System;
using ApplicationCore.Constants;

namespace ApplicationCore.Entities
{
    public class Job : BaseEntity
    {
        public int? ContractorID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Floor { get; set; }
        public int? Room { get; set; }
        public string DelayReason { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateDelayed { get; set; }
        public int? StatusNum { get; set; }
        public int? RJobID { get; set; }

        public Guid? RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public bool AbleToMarkAsComplete => Status == JobStatus.Delayed || Status == JobStatus.InProgress;
    }
}