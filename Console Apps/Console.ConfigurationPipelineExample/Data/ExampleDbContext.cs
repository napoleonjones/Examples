using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using ConfigurationPipelineExample.Data.Entities;

namespace ConfigurationPipelineExample.Data
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.AccountId)
                .HasMaxLength(1);
            });
        }
    }
}
