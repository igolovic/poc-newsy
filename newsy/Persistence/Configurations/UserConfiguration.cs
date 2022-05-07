﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(user => user.Id);

            builder.Property(account => account.Id).ValueGeneratedOnAdd();

            builder.Property(user => user.FirstName).HasMaxLength(60).IsRequired();

            builder.Property(user => user.LastName).HasMaxLength(60).IsRequired();

            builder.Property(user => user.UserName).HasMaxLength(100).IsRequired();

            //builder.HasMany(user => user.)
            //    .WithOne()
            //    .HasForeignKey(account => account.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
