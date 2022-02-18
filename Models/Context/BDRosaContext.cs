using Microsoft.EntityFrameworkCore;
using outubroRosa.Models.Entidades;
using outubroRosa.Models.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace outubroRosa.Models.Context
{
    public class BDRosaContext : DbContext
    {
        public BDRosaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParticipanteMapper());
        }
    }
}
