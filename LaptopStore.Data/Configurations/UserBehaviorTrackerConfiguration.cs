using LaptopStore.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Configurations
{
    public class UserBehaviorTrackerConfiguration : IEntityTypeConfiguration<UserBehaviorTracker>
    {
        public void Configure(EntityTypeBuilder<UserBehaviorTracker> builder)
        {
            builder.ToTable("UserBehaviorTracker");
            builder.HasKey(x => new { x.UserId, x.TimeStamp });
        }
    }
}
