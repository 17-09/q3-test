using System.Collections.Generic;

namespace ApplicationCore.Dtos
{
    public class RoomTypeSummaryDto
    {
        public string RoomTypeName { get; set; }
        public IEnumerable<RoomTypeStatusSummaryDto> RoomTypeStatusSummaries { get; set; }
    }
}