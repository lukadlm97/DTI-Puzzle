using DTI.Puzzle.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Domain.Entities.Action>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Action> builder)
        {
            builder
                 .HasData(Enum.GetValues(typeof(ActionEnum))
                    .Cast<ActionEnum>()
                    .Select(e =>
                        new Domain.Entities.Action()
                        {
                            Id = (short)e,
                            Name = e.ToString()
                        }));
        }
    }
}
