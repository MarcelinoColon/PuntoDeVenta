using Application.Abstractions;
using Application.Brand.UseCases;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Application.Product.DTOs;
using Application.Product.UseCases;
using Application.Product.Mappers;
using Application.Sale.UseCases;
using Application.Sale.DTOs;
using Application.Sale.Mappers;
using Backend.Endpoints;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IRepository<BrandEntity>, BrandRepository>();
builder.Services.AddTransient<IReadRepository<ProductEntity>, ProductRepository>();
builder.Services.AddTransient<ICreateRepository<ProductEntity>, ProductRepository>();
builder.Services.AddTransient<IUpdateRepository<ProductEntity>, ProductRepository>();
builder.Services.AddTransient<IDeleteRepository, ProductRepository>();
builder.Services.AddTransient<ICreateRepository<SaleEntity>, CreateSaleRepository>();
builder.Services.AddTransient<IReadRepository<SaleEntity>, ReadSaleRepositoy>();

builder.Services.AddTransient<IUseCase<BrandEntity>, BrandUseCase>();
builder.Services.AddTransient<IReadUseCase<ProductDto, ProductEntity>, ProductUseCase>();
builder.Services.AddTransient<ICreateUseCase<ProductDto, ProductEntity>, CreateProductUseCase>();
builder.Services.AddTransient<IUpdateUseCase<ProductDto, ProductEntity>, UpdateProductUseCase>();
builder.Services.AddTransient<IDeleteUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<ICreateUseCase<SaleDto, SaleEntity>, CreateSaleUseCase>();
builder.Services.AddTransient<IReadUseCase<SaleDto, SaleEntity>, ReadSaleUseCase>();

builder.Services.AddTransient<IMapper<ProductEntity, ProductDto>, ProductEntityToDtoMapper>();
builder.Services.AddTransient<IMapper<ProductDto, ProductEntity>, ProductDtoToEntityMapper>();
builder.Services.AddTransient<IMapper<SaleDto, SaleEntity>, SaleDtoToEntityMapper>();
builder.Services.AddTransient<IMapper<SaleEntity, SaleDto>, SaleEntityToDtoMapper>();

//swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string FrontendPolicy = "FrontendPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: FrontendPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(FrontendPolicy);

//Endpoints de Ventas
app.MapSaleEndpoints();


// Brand Endpoints

app.MapGet("brand", async (IUseCase<BrandEntity> useCase) =>
{
    return await useCase.GetAllAsync();
}).WithName("getbrand");

app.MapPost("brand", async (IUseCase<BrandEntity> useCase, BrandEntity brand) =>
{
    try
    {
        await useCase.AddAsync(brand);
        return Results.Created();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

}).Produces(StatusCodes.Status201Created)
    .WithName("addbrand");

app.MapPut("brand/{id}", async (int id, BrandEntity brand, IUseCase<BrandEntity> useCase) =>
{
    try
    {
        var brandEntity = new BrandEntity(id, brand.Name);

        await useCase.UpdateAsync(brandEntity);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

    return Results.NoContent();

}).Produces(StatusCodes.Status204NoContent)
    .WithName("updatebrand");

app.MapDelete("brand/{id}", async (int id, IUseCase<BrandEntity> useCase) =>
{
    try
    {
        await useCase.DeleteAsync(id);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

    return Results.NoContent();
}).Produces(StatusCodes.Status204NoContent)
    .WithName("deletebrand");


// Product Endpoints

app.MapGet("/product", async (IReadUseCase<ProductDto, ProductEntity> useCase) =>
{
    return await useCase.GetAllAsync();
}).WithName("getproduct");

app.MapGet("/product/{id}", async (int id, IReadUseCase<ProductDto, ProductEntity> useCase) =>
{
    try
    {
        var product = await useCase.GetByIdAsync(id);
        if (product == null)
            return Results.NotFound();

        return Results.Ok(product);
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
}).Produces<ProductDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithName("getproductbyid");

app.MapPost("/product", async (ProductDto productDto, ICreateUseCase<ProductDto, ProductEntity> useCase) =>
{
    try
    {
        await useCase.AddAsync(productDto);
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
.WithName("addproduct");

app.MapPut("/product/{id}", async (int id, ProductDto productDto, IUpdateUseCase<ProductDto, ProductEntity> useCase) =>
{
    try
    {
        await useCase.UpdateAsync(productDto, id);
        return Results.NoContent();
    }
    catch (ArgumentException argEX)
    {
        return Results.BadRequest(argEX.Message);
    }
    catch (KeyNotFoundException notFoundEx)
    {
        return Results.NotFound(notFoundEx.Message);
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(ex.Message);
    }
}).Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError)
    .WithName("updateproduct");

app.MapDelete("/product/{id}", async (int id, IDeleteUseCase useCase) =>
{
    try
    {
        await useCase.DeleteAsync(id);
        return Results.NoContent();
    }
    catch (KeyNotFoundException notFoundEx)
    {
        return Results.NotFound(notFoundEx.Message);
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(ex.Message);
    }
}).Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status500InternalServerError)
.WithName("deleteproduct");


app.MapGet("/test", () =>
{
    return "online";
}).WithName("test");

app.Run();




