using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PickGroup.OrderService.API.Extensions;
using PickGroup.OrderService.API.Response;
using PickGroup.OrderService.Application.Common.Pagination;
using PickGroup.OrderService.Application.OrderAg.Order.Commands.CreateOrder;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrderById;
using PickGroup.OrderService.Application.OrderAg.Order.Queries.GetOrders;
using PickGroup.OrderService.Domain.Orders;

namespace PickGroup.OrderService.API.Controllers;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateOrder(
        [FromServices] CreateOrderHandler handler,
        [FromBody] CreateOrderRequest request,
        CancellationToken ct = default)
    {
        var order = await handler.CreateOrderAsync(request, ct);
        
        return order.ToResponse();
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult> GetOrderById(
        [FromServices] GetOrderByIdHandler handler,
        [FromRoute] Guid orderId,
        CancellationToken ct = default)
    {
        var order = await handler.GetOrderByIdAsync(orderId, ct);
        
        return order.ToResponse();
    }

    [HttpGet("orders")]
    public async Task<ActionResult> GetOrders(
        [FromServices] GetOrdersHandler handler,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken ct = default)
    {
        var orders = await handler.GetOrdersAsync(page, pageSize, ct);
        
        return orders.ToResponse();
    }
}