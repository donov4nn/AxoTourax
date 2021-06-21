using Microsoft.EntityFrameworkCore;
using AxoTourax.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace AxoTourax.Data
{
    public partial class AxoTouraxContext : IdentityDbContext
    {
        public AxoTouraxContext()
        {
        }

        public AxoTouraxContext(DbContextOptions<AxoTouraxContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Bobine> Bobines { get; set; }
        public virtual DbSet<Calcul> Calculs { get; set; }
        public virtual DbSet<MatiereBobine> MatiereBobines { get; set; }
        public virtual DbSet<TechniqueBobine> TechniqueBobines { get; set; }
        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bobine>(entity =>
            {
                entity.HasKey(e => e.IdBobine);

                entity.ToTable("Bobine");

                entity.Property(e => e.Photo).HasMaxLength(50);

                entity.Property(e => e.Prix).HasColumnType("money");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMatiereNavigation)
                    .WithMany(p => p.Bobines)
                    .HasForeignKey(d => d.IdMatiere)
                    .HasConstraintName("FK_MatiereBobine_TO_Bobine");

                entity.HasOne(d => d.IdTechniqueNavigation)
                    .WithMany(p => p.Bobines)
                    .HasForeignKey(d => d.IdTechnique)
                    .HasConstraintName("FK_TechniqueBobine_TO_Bobine");
            });

            modelBuilder.Entity<Calcul>(entity =>
            {
                entity.HasKey(e => e.IdCalcul);

                entity.ToTable("Calcul");

                entity.Property(e => e.DateCalcul).HasColumnType("date");

                entity.Property(e => e.TypeCable)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdBobineNavigation)
                    .WithMany(p => p.Calculs)
                    .HasForeignKey(d => d.IdBobine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bobine_TO_Calcul");
            });

            modelBuilder.Entity<MatiereBobine>(entity =>
            {
                entity.HasKey(e => e.IdMatiere);

                entity.ToTable("MatiereBobine");

                entity.Property(e => e.IdMatiere).ValueGeneratedNever();

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TechniqueBobine>(entity =>
            {
                entity.HasKey(e => e.IdTechnique);

                entity.ToTable("TechniqueBobine");

                entity.Property(e => e.IdTechnique).ValueGeneratedNever();

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
