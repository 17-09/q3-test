using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Task<IEnumerable<Job>> GetAll(CancellationToken cancellationToken) =>
            _jobRepository.GetAll(cancellationToken);

        public async Task Complete(Guid id, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.Get(id, cancellationToken);

            if (!job.AbleToMarkAsComplete)
                throw new ApplicationException("Only Delayed and In Progress jobs are allowed to mark as Completed");

            job.Status = JobStatus.Complete;
            await _jobRepository.Update(job, cancellationToken);
        }

        public Task<IEnumerable<RoomTypeSummaryDto>> GetRoomTypeSummary(CancellationToken cancellationToken) =>
            _jobRepository.GetRoomTypeSummary(cancellationToken);
    }
}