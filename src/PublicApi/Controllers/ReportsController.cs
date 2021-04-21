using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ILogger<ReportsController> logger,
            IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        [Route(nameof(RoomTypeSummary))]
        [HttpGet]
        public async Task<IActionResult> RoomTypeSummary(CancellationToken cancellationToken)
        {
            return Ok(await _jobService.GetRoomTypeSummary(cancellationToken));
        }
    }
}