using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Dtos;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAll(CancellationToken cancellationToken);
        Task Complete(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<RoomTypeSummaryDto>> GetRoomTypeSummary(CancellationToken cancellationToken);
    }
}