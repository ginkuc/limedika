using Limedika.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimedikaTests.Setup
{
    public static class LocationSeeder
    {
        public static async Task SeedLocations(DbSet<Location> locations)
        {
            var newLocations = new List<Location>
            {
                new Location {Id = 1, Name = "Vaistine nr. 1", Address = "Vilniaus g. 220, Šiauliai" },
                new Location {Id = 2, Name = "Vaistine nr. 2", Address = "Baltų pr. 7A-1, Kaunas" },
                new Location {Id = 3, Name = "Vaistine nr. 3", Address = "Vytenio g. 16, Prienai" },
                new Location {Id = 4, Name = "Vaistine nr. 4", Address = "Livonijos g. 5, Joniškis" }
            };

            await locations.AddRangeAsync(newLocations);
        }
    }
}