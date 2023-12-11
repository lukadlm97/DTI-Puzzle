using DTI.Puzzle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence
{
    public class GlossaryDbContext : DbContext
    {
        public GlossaryDbContext()
        {
            
        }
        public GlossaryDbContext(DbContextOptions options)  : base(options) { }
        public DbSet<Domain.Entities.Action> Actions { get; set; }
        public DbSet<GlossaryItem> GlossaryItems { get; set; }
        public DbSet<HistoryChange> HistoryChanges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GlossaryDbContext).Assembly);
        }
    }
}
