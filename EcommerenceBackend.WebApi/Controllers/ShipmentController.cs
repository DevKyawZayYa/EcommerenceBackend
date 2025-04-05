using EcommerenceBackend.Application.UseCases.Reviews.Commands.CreateReview;
using EcommerenceBackend.Application.UseCases.Reviews.Queries.GetReviewByProductIdQuery;
using EcommerenceBackend.Application.UseCases.Shipment.Commands;
using EcommerenceBackend.Application.UseCases.Shipment.Queries.GetShipmentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ShipmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShipmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] CreateCategoryCommand command)
        {
            var shipmentid = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(CreateShipment), new { id = shipmentid }, shipmentid));
        }

        [HttpGet("{shipmentId}")]
        public async Task<IActionResult> GetShipmentById(Guid shipmentId)
        {
            var shipment = await _mediator.Send(new GetShipmentByIdQuery { ShipmentID = shipmentId });
            return shipment != null ? Ok(shipment) : NotFound();
        }
    }
}
