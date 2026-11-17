using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PickGroup.API.Extensions;
using PickGroup.Application.OrderAg.Order.CreateOrder;
using PickGroup.Application.OrderAg.Order.GetOrder;
using PickGroup.Domain.Orders;

namespace PickGroup.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateOrder(
        [FromServices] CreateOrderHandler handler,
        [FromBody] CreateOrderRequest request,
        CancellationToken ct = default)
    {
        var order = await handler.CreateOrderAsync(request, ct);
        
        return order.ToResponse();
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<GetOrderByIdResponse>> GetOrderById(
        [FromServices] GetOrderByIdHandler handler,
        [FromRoute] Guid orderId,
        CancellationToken ct = default)
    {
        var order = await handler.GetOrderByIdAsync(orderId, ct);
        return order.ToResponse();
    }
}