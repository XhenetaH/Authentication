using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.EntityFramework.Configurations
{
    public class UserConfiguration : AuditColumnsConfiguration ,IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);

            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.PasswordSalt).IsRequired();

            builder.ToTable<User>("User");
        }
    }
}
