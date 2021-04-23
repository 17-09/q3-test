using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Job> Jobs { get; set; }

        private bool Equals(RoomType other)
        {
            return Name == other.Name && Description == other.Description && Equals(Jobs, other.Jobs);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RoomType) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Jobs);
        }

        public static bool operator ==(RoomType left, RoomType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RoomType left, RoomType right)
        {
            return !Equals(left, right);
        }
    }
}