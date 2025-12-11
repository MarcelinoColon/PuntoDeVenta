using Application.Abstractions;
using Application.Sale.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Backend.Endpoints
{
    public static class SaleEndpoints
    {
        public static void MapSaleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/sales", async (SaleDto saleDto, ICreateUseCase<SaleDto, SaleEntity> useCase) =>
            {
                try
                {
                    await useCase.AddAsync(saleDto);
                    return Results.Created();
                }
                catch (ArgumentException argEx)
                {
                    return Results.BadRequest(argEx.Message);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("addsale");

            app.MapGet("/sales", async (IReadUseCase<SaleDto, SaleEntity> useCase) =>
            {
                try
                {
                    return Results.Ok(await useCase.GetAllAsync());
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).Produces<IEnumerable<SaleDto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithName("getsales");

            app.MapGet("/sales/{id}", async (int id, IReadUseCase<SaleDto, SaleEntity> useCase) =>
            {
                try
                {
                    var sale = await useCase.GetByIdAsync(id);
                    return Results.Ok(sale);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).Produces<SaleDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError).
            WithName("getsalebyid");
        }
    }
}
