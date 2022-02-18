using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using outubroRosa.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Models.Mapper
{
    public class ParticipanteMapper : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.Property(c => c.IsAtivo)
             .HasDefaultValue(true);

            builder.Property(c => c.IsElite)
             .HasDefaultValue(false);

            builder.Property(c => c.DataCad)
              .HasDefaultValueSql("(getdate())");

        }
    }
}
