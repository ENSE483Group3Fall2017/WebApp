using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebApp.Features.Pet;
using WebApp.Features.Tracking;
using System.Net;
using WebApp.Infrastructure;

namespace WebApp.Features.Tracking
{
    public class TrackingController : Controller
    {
        private readonly IMediator _mediator;

        public TrackingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<ActionResult> Index(Tracking.Index.Query query)
        {
            var getPetQuery = new Pet.Details.Query { Id = query.Id };
            var petOption = await _mediator.Send(getPetQuery);

            if (!petOption.HasValue)
                return HttpStatusCode.NotFound.Result();

            ViewBag.PetName = petOption.ValueOr(() => new Pet.Details.Model()).Name;

            var items = await _mediator.Send(query);

            return View(items);
        }
        
        public ActionResult Details(Guid id)
        {
            return View();
        }


    }
}