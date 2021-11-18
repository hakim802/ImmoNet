using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbAccess.Configuration
{
    class ImmoConfiguration : IEntityTypeConfiguration<Immo>
    {
        public void Configure(EntityTypeBuilder<Immo> builder)
        {
            builder.HasData(
                new Immo
                {
                    ImmoId = 1,
                    Street = "Zwaluwstraat",
                    HouseNumber = "4",
                    PostalCode = 2800,
                    City = "Mechelen",
                    Province = "Antwerpen",
                    FullName = "Sam",
                    Email = "sam@com",
                    Phone = "0468595",
                    Description = "description hier"
                },
                new Immo
                {
                    ImmoId = 2,
                    Street = "kleinstraat",
                    HouseNumber = "14",
                    PostalCode = 2800,
                    City = "Mechelen",
                    Province = "Antwerpen",
                    FullName = "Sami",
                    Email = "sami@com",
                    Phone = "0458595",
                    Description = "description hier"
                }
                );
        }
    }
}
