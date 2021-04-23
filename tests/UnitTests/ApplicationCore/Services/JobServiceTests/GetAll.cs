using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace UnitTests.ApplicationCore.Services.JobServiceTests
{
    public class GetAll
    {
        private readonly Mock<IJobRepository> _jobRepositoryMock;

        private readonly List<Job> _dataJobs = new()
        {
            new Job
            {
                Id = new Guid("9CEE9C39-81A2-4567-A76D-8359C601F576"),
                Name = "Job1",
                Status = "Not Started",
                Floor = 19,
                Room = 20,
                DateCreated = DateTime.Now,
                StatusNum = 2,
                RJobID = 37,
                RoomTypeId = new Guid("BA020BF0-B205-4B9A-9390-FF7823D2CC0F")
            },
            new Job
            {
                Id = new Guid("B60F79AD-8517-4F9D-897C-DE3967E47A00"),
                Name = "Job2",
                Status = "Complete",
                Floor = 6,
                Room = 9,
                DateCreated = DateTime.Now,
                StatusNum = 1,
                RJobID = 36,
                RoomTypeId = new Guid("86F23884-1971-4BDF-90DB-629F887ED76A")
            },
            new Job
            {
                Id = new Guid("266E2B0C-BB42-416B-A9A4-D60B53391E89"),
                Name = "Job3",
                Status = "In Progress",
                Floor = 1,
                Room = 2,
                DateCreated = DateTime.Now,
                StatusNum = 1,
                RJobID = 34,
                RoomTypeId = new Guid("911C7AF6-2D20-4B06-AD06-BD835A3871F1")
            }
        };

        public GetAll()
        {
            _jobRepositoryMock = new Mock<IJobRepository>();
        }

        [Fact]
        public async Task GetAll_WithAllJobs_ShouldReturnAll()
        {
            // Arrange
            _jobRepositoryMock.Setup(x => x.GetAll(CancellationToken.None))
                .ReturnsAsync(_dataJobs);

            // Act
            var jobService = new JobService(_jobRepositoryMock.Object);
            var jobs = (await jobService.GetAll(CancellationToken.None)).ToList();

            // Assert
            Assert.NotEmpty(jobs);
            Assert.Equal(3, jobs.Count);
            _jobRepositoryMock.Verify(j => j.GetAll(CancellationToken.None), Times.Once);

            foreach (var job in jobs)
            {
                var source = _dataJobs.FirstOrDefault(j => j.Name == job.Name);
                Assert.NotNull(source);
                Assert.Equal(source, job);
            }
        }

        [Fact]
        public async Task GetAll_WithEmptyJobs_ShouldReturnEmpty()
        {
            // Arrange
            _jobRepositoryMock.Setup(x => x.GetAll(CancellationToken.None))
                .ReturnsAsync(new List<Job>());

            // Act
            var jobService = new JobService(_jobRepositoryMock.Object);
            var jobs = (await jobService.GetAll(CancellationToken.None)).ToList();

            // Assert
            Assert.Empty(jobs);
            _jobRepositoryMock.Verify(j => j.GetAll(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetAll_WithCancelEvent_ShouldCancel()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            _jobRepositoryMock.Setup(x => x.GetAll(cts.Token))
                .Callback(() => { cts.CancelAfter(100); })
                .ReturnsAsync(() =>
                {
                    Task.Delay(500, cts.Token).Wait(cts.Token);
                    return _dataJobs;
                });

            // Act
            var jobService = new JobService(_jobRepositoryMock.Object);

            // Assert
            await Assert.ThrowsAsync<OperationCanceledException>(() => jobService.GetAll(cts.Token));
            _jobRepositoryMock.Verify(j => j.GetAll(CancellationToken.None), Times.Never);
        }
    }
}