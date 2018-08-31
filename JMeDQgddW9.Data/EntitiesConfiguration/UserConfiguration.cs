using JMeDQgddW9.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMeDQgddW9.Data.EntitiesConfiguration
{
    /// <summary>
    /// User configuration
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configure database options
        /// </summary>
        /// <param name="builder">User builder</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            //Properties
            builder.Property(user => user.Name)                
                .IsRequired()
                .HasMaxLength(150);
            builder.HasIndex(user => user.Email)
                .IsUnique();
            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.HasIndex(user => user.Login)
                .IsUnique();
            builder.Property(user => user.Login)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(user => user.Phone)
                .HasMaxLength(14);
            builder.Property(user => user.Password)
                .IsRequired();

            //Relationships
            //builder.HasOne(user => user.Token).WithOne(token => token.User).HasForeignKey("Token", "UserId");
        }
    }
}
