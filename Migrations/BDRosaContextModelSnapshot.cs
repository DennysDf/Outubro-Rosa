// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using outubroRosa.Models.Context;
using System;

namespace outubroRosa.Migrations
{
    [DbContext(typeof(BDRosaContext))]
    partial class BDRosaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("outubroRosa.Models.Entidades.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF")
                        .IsRequired();

                    b.Property<string>("Camiseta")
                        .IsRequired();

                    b.Property<string>("Cidade")
                        .IsRequired();

                    b.Property<DateTime>("DataCad")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DataNasc")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Endereco")
                        .IsRequired();

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsElite")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("Numero");

                    b.Property<string>("Sexo")
                        .IsRequired();

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.Property<string>("TipoProva")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Participantes");
                });
#pragma warning restore 612, 618
        }
    }
}
