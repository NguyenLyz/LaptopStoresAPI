using LaptopStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Configurations
{
    public class JWTTokenConfiguration : IEntityTypeConfiguration<JwTToken>
    {
        public void Configure(EntityTypeBuilder<JwTToken> builder)
        {
            builder.ToTable("JWTToken");
            builder.HasKey(x => x.UserId);
        }
    }
}
