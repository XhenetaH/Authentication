using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.EntityFramework.Configurations
{
    public class AuditColumnsConfiguration 
    {
        public void Configure(EntityTypeBuilder<AuditColumns> builder)
        {
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();

            builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();

            builder.Property(x => x.InsertBy).IsRequired();

            builder.Property(x => x.InsertDate).HasDefaultValue(DateTime.Now);
        }
    }
}
