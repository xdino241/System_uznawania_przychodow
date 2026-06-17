using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.ClientId);
        builder.Property(c => c.Email).HasMaxLength(50);
        builder.Property(c => c.Address).HasMaxLength(100);
        builder.Property(c => c.Phone).HasMaxLength(9);

        builder.HasData(new List<Client>()
        {
            new Client { ClientId = 1, Address = "ul. Kowalska 1, Warszawa", Email = "jan.kowalski@gmail.com", Phone = "500600700", IsDeleted = false },
            new Client { ClientId = 2, Address = "ul. Nowaka 5, Kraków", Email = "anna.nowak@gmail.com", Phone = "501601701", IsDeleted = false },
            new Client { ClientId = 3, Address = "ul. Wiśniewska 3, Gdańsk", Email = "piotr.wisniewski@gmail.com", Phone = "502602702", IsDeleted = false },
            new Client { ClientId = 4, Address = "ul. Wójcika 7, Poznań", Email = "maria.wojcik@gmail.com", Phone = "503603703", IsDeleted = false },
            new Client { ClientId = 5, Address = "ul. Kamińska 9, Wrocław", Email = "tomasz.kaminski@gmail.com", Phone = "504604704", IsDeleted = false },
            new Client { ClientId = 6, Address = "ul. Firmowa 1, Warszawa", Email = "kontakt@abcsp.pl", Phone = "600700800", IsDeleted = false },
            new Client { ClientId = 7, Address = "ul. Biznesowa 2, Kraków", Email = "biuro@xyzsa.pl", Phone = "601701801", IsDeleted = false },
            new Client { ClientId = 8, Address = "ul. Handlowa 3, Gdańsk", Email = "office@techsolutions.pl", Phone = "602702802", IsDeleted = false },
            new Client { ClientId = 9, Address = "ul. Przemysłowa 4, Poznań", Email = "info@megacorp.pl", Phone = "603703803", IsDeleted = false },
            new Client { ClientId = 10, Address = "ul. Korporacyjna 5, Wrocław", Email = "kontakt@globaltech.pl", Phone = "604704804", IsDeleted = false }
        });
    }
}