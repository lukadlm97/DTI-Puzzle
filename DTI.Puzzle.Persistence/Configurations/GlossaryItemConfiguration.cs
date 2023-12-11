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
    public class GlossaryItemConfiguration : IEntityTypeConfiguration<Domain.Entities.GlossaryItem>
    {
        public void Configure(EntityTypeBuilder<GlossaryItem> builder)
        {
        }
    }
}
