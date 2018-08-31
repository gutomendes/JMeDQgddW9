using JMeDQgddW9.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMeDQgddW9.Data.EntitiesConfiguration
{
    /// <summary>
    /// Token configuration
    /// </summary>
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        /// <summary>
        /// Configure database options
        /// </summary>
        /// <param name="builder">Token builder</param>
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            //Table
            builder.ToTable("Tokens");
            builder.HasKey(token => token.Id);

            //Properties
            builder.Property(token => token.ExpirationDate)
                .IsRequired();
            builder.Property(token => token.TokenValue)
                .IsRequired();
            builder.Property(token => token.UserId)
                .IsRequired();

            //Relationships
            builder.HasOne(token => token.User)
                .WithOne(user => user.Token)
                .HasForeignKey("Token", "UserId")
                .IsRequired();
        }
    }
}