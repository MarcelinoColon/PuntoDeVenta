using Application.Abstractions;
using Application.Brand.UseCases;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

//conexion
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var services = new ServiceCollection();

services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(connectionString));

services.AddTransient<IRepository<BrandEntity>, BrandRepository>();
services.AddTransient<IUseCase<BrandEntity>, BrandUseCase>();

var serviceProvider = services.BuildServiceProvider();

var brandUseCase = serviceProvider.GetRequiredService<IUseCase<BrandEntity>>();


while (true)
{
    try
    {
        Console.WriteLine("\n Seleccione una opción:");
        Console.WriteLine("1.- Agregar una marca");
        Console.WriteLine("2.- Mostrar marcas almacenadas");
        Console.WriteLine("3.- Salir");
        Console.WriteLine("Opcion: ");

        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Ingrese una marca: ");
                string name = Console.ReadLine();
                await brandUseCase.AddAsync(new BrandEntity(name));
                break;
            case "2":
                Console.WriteLine("\n Marcas almacenadas");
                var brands = await brandUseCase.GetAllAsync();
                foreach(var brand in brands)
                {
                    Console.WriteLine($"Marca: {brand.Name}");
                }
                break;
            case "3":
                Console.WriteLine("Saliendo del sistema...");
                break;
            default:
                Console.WriteLine("Opcion invalida");
                break;
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}