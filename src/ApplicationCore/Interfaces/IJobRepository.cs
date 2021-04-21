using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Dtos;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IJobRepository : IAsyncRepository<Job>
    {
        Task<IEnumerable<RoomTypeSummaryDto>> GetRoomTypeSummary(CancellationToken cancellationToken);
    }
}