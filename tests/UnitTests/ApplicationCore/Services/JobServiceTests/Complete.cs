using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace UnitTests.ApplicationCore.Services.JobServiceTests
{
    public class Complete
    {
        private readonly Mock<IJobRepository> _jobRepositoryMock;

        public Complete()
        {
            _jobRepositoryMock = new Mock<IJobRepository>();
        }

        [Fact]
        public async Task Complete_WithInProgressAndDelayedJob_ShouldWork()
        {
            // Arrange
            var inProgressJobId = Guid.NewGuid();
            var delayedJobId = Guid.NewGuid();

            _jobRepositoryMock.Setup(x => x.Update(It.IsAny<Job>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _jobRepositoryMock.Setup(x => x.Get(inProgressJobId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Job
                {
                    Status = JobStatus.InProgress
                });
            _jobRepositoryMock.Setup(x => x.Get(delayedJobId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Job
                {
                    Status = JobStatus.Delayed
                });

            // Act
            var jobService = new JobService(_jobRepositoryMock.Object);
            await jobService.Complete(inProgressJobId, CancellationToken.None);
            await jobService.Complete(delayedJobId, CancellationToken.None);

            // Assert
            _jobRepositoryMock.Verify(j => j.Update(It.IsAny<Job>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Complete_WithNoInProgressOrDelayedJob_ShouldThrowException()
        {
            // Arrange
            var otherJobId = Guid.NewGuid();
            _jobRepositoryMock.Setup(x => x.Get(otherJobId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Job
                {
                    Status = JobStatus.NotStarted
                });
            var jobService = new JobService(_jobRepositoryMock.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<ApplicationException>(
                () => jobService.Complete(otherJobId, CancellationToken.None));
        }
    }
}