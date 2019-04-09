using DotNetCoreArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreArchitecture.Database
{
    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users", "User");

            builder.HasKey(x => x.UserId);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Roles).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.OwnsOne(x => x.FullName, y =>
            {
                y.Property(x => x.Name).IsRequired().HasMaxLength(100);
                y.Property(x => x.Surname).IsRequired().HasMaxLength(200);
            });

            builder.OwnsOne(x => x.SignIn, y =>
            {
                y.Property(x => x.Login).IsRequired().HasMaxLength(100);
                y.Property(x => x.Password).IsRequired().HasMaxLength(500);
                y.HasIndex(x => x.Login).IsUnique();
            });

            builder.HasMany(x => x.UsersLogs).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
