using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Etika",
                    Author = "Baruch Spinoza",
                    ISBN = "9789758717144",
                    Price = 100,
                    PublicationDate = new DateTime(1677, 1, 1),
                    Description = "Spinoza'nın en önemli eseri",
                    PageCount = 320,
                    Language = "Türkçe",
                    Category = "Felsefe",
                    StockQuantity = 50,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Teolojik Politik İnceleme",
                    Author = "Baruch Spinoza",
                    ISBN = "9789758717145",
                    Price = 200,
                    PublicationDate = new DateTime(1670, 1, 1),
                    Description = "Spinoza'nın din ve siyaset üzerine görüşleri",
                    PageCount = 280,
                    Language = "Türkçe",
                    Category = "Felsefe",
                    StockQuantity = 30,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Mektuplar",
                    Author = "Baruch Spinoza",
                    ISBN = "9789758717146",
                    Price = 120,
                    PublicationDate = new DateTime(1677, 1, 1),
                    Description = "Spinoza'nın mektupları",
                    PageCount = 240,
                    Language = "Türkçe",
                    Category = "Felsefe",
                    StockQuantity = 25,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Aklın Islahı Üzerine",
                    Author = "Baruch Spinoza",
                    ISBN = "9789758717147",
                    Price = 145,
                    PublicationDate = new DateTime(1662, 1, 1),
                    Description = "Spinoza'nın metodolojik eseri",
                    PageCount = 180,
                    Language = "Türkçe",
                    Category = "Felsefe",
                    StockQuantity = 35,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}