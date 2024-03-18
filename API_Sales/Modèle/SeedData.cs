using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API_Sales.Modèle; // Assurez-vous que ce namespace correspond à celui de vos modèles et de votre contexte de base de données.
using System;
using System.Linq;
using API_Sales.Data;

namespace API_Sales.Modèle
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new API_SalesContext( // Remplacez par le nom réel de votre DbContext
                serviceProvider.GetRequiredService<
                    DbContextOptions<API_SalesContext>>()))
            {
                
                context.Database.EnsureCreated();

                
                if (!context.Consolec.Any())
                {
                    // Créez et ajoutez des fabricants
                    var manufacturer1 = new Manufacturer { Name = "Microsoft", OriginCountry = "USA" };
                    var manufacturer2 = new Manufacturer { Name = "Nintendo", OriginCountry = "Japan" };
                    context.Manufacturer.AddRange(manufacturer1, manufacturer2);

                    context.SaveChanges();

                    // Créez et ajoutez des consoles liées aux fabricants ci-dessus
                    var console1 = new Consolec { Name = "Xbox 360", ReleaseYear = 2013, ManufacturerId = manufacturer1.ManufacturerId };
                    var console2 = new Consolec { Name = "Nintendo Switch", ReleaseYear = 2020, ManufacturerId = manufacturer2.ManufacturerId };
                    context.Consolec.AddRange(console1, console2);

                    // Enregistrez les modifications pour obtenir les IDs générés
                    context.SaveChanges();

                    // Créez et ajoutez des données de ventes associées aux consoles ci-dessus
                    context.Sale.AddRange(
                        new Sale { Year = 2015, UnitsSold = 1.5, ConsoleId = console1.ConsoleId },
                        new Sale { Year = 2021, UnitsSold = 1.2, ConsoleId = console2.ConsoleId }
                    );

                    // Enregistrez toutes les modifications dans la base de données
                    context.SaveChanges();
                }
            }
        }
    }
}

