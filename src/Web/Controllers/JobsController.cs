using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var viewModels = (await _jobService.GetAll(cancellationToken)).Select(JobViewModel.ConvertFrom);
            return View(viewModels);
        }

        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            await _jobService.Complete(id, cancellationToken);

            return RedirectToAction("Index");
        }
    }
}