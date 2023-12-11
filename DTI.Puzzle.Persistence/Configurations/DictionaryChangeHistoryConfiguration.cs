using DTI.Puzzle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Configurations
{
    public class DictionaryChangeHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.HistoryChange>
    {
        public void Configure(EntityTypeBuilder<HistoryChange> builder)
        {
        }
    }
}
