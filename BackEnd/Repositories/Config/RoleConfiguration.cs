using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Repositories.Config
{
    public class RoleConfiguration:IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name="Administrator",
                    NormalizedName="ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"
                },
                new IdentityRole
                {
                    Name="Editor",
                    NormalizedName="EDITOR"
                }
                
            );
        }
    }
}