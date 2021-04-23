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

        private bool Equals(Job other)
        {
            return ContractorID == other.ContractorID && Name == other.Name && Status == other.Status &&
                   Floor == other.Floor && Room == other.Room && DelayReason == other.DelayReason &&
                   Nullable.Equals(DateCreated, other.DateCreated) &&
                   Nullable.Equals(DateCompleted, other.DateCompleted) &&
                   Nullable.Equals(DateDelayed, other.DateDelayed) && StatusNum == other.StatusNum &&
                   RJobID == other.RJobID && Nullable.Equals(RoomTypeId, other.RoomTypeId) &&
                   Equals(RoomType, other.RoomType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Job) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ContractorID);
            hashCode.Add(Name);
            hashCode.Add(Status);
            hashCode.Add(Floor);
            hashCode.Add(Room);
            hashCode.Add(DelayReason);
            hashCode.Add(DateCreated);
            hashCode.Add(DateCompleted);
            hashCode.Add(DateDelayed);
            hashCode.Add(StatusNum);
            hashCode.Add(RJobID);
            hashCode.Add(RoomTypeId);
            hashCode.Add(RoomType);
            return hashCode.ToHashCode();
        }

        public static bool operator ==(Job left, Job right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Job left, Job right)
        {
            return !Equals(left, right);
        }
    }
}