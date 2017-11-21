using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApp.Infrastructure;

namespace WebApp.Features.Pet
{
    public class PetController : Controller
    {
        private readonly IMediator _mediator;

        public PetController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: Pets
        public async Task<ActionResult> Index()
        {
            var query = new Index.Query();
            var items = await _mediator.Send(query);

            return View(items);
        }

        // GET: Pet/Details/5
        public async Task<ActionResult> Details(Details.Query query)
        {
            var option = await _mediator.Send(query);

            return option.Match(some => View(some), () => HttpStatusCode.NotFound.Result());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Create.Command command)
        {
            await _mediator.Send(command);

            return RedirectToAction(nameof(Details), new { id = command.Beacon.ToString() });
        }

        // POST: Pet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Delete.Command command)
        {
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}