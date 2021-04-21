using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class JobRepository : IJobRepository
    {
        private readonly RxContext _context;

        public JobRepository(RxContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAll(CancellationToken cancellationToken) =>
            await _context.Jobs.AsNoTracking()
                .Include(j => j.RoomType)
                .OrderByDescending(j => j.DateCreated)
                .ToListAsync(cancellationToken);

        public Task<Job> Get(Guid id, CancellationToken cancellationToken) =>
            _context.Jobs.AsNoTracking()
                .SingleAsync(j => j.Id == id, cancellationToken);

        public async Task Update(Job entity, CancellationToken cancellationToken)
        {
            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<RoomTypeSummaryDto>> GetRoomTypeSummary(CancellationToken cancellationToken)
        {
            var source = await _context.Jobs.AsNoTracking()
                .Select(j => new
                {
                    RoomTypeName = j.RoomType.Name,
                    j.Status
                })
                .ToArrayAsync(cancellationToken);

            var summary = source.GroupBy(j => j.RoomTypeName)
                .Select(j => new RoomTypeSummaryDto
                {
                    RoomTypeName = j.Key,
                    RoomTypeStatusSummaries = j.GroupBy(js => js.Status).Select(js => new RoomTypeStatusSummaryDto
                    {
                        Status = js.Key,
                        NumberOfJobs = js.Count()
                    })
                });

            return summary;
        }
    }
}