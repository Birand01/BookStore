
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
                new Book{Id=1,Title="Etika",Price=100},
                new Book{Id=2,Title="Teolojik Politik İnceleme",Price=200},
                new Book{Id=3,Title="Mektuplar",Price=120},
                new Book{Id=4,Title="Aklın Islahı Üzerine",Price=145}
            );
        }
    }
}