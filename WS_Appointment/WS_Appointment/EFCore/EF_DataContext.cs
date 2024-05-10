using Microsoft.EntityFrameworkCore;

namespace WS_Appointment.EFCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<appointments>()
            .Property(e => e.id)
            .UseIdentityColumn(); // This configures Id as auto-increment

            modelBuilder.Entity<appointments>()
            .Property(e => e.appointment_date)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<appointments>()
            .Property(e => e.status)
            .HasColumnType("character varying");

            modelBuilder.Entity<appointments>()
            .Property(e => e.serviceType)
            .HasColumnType("character varying");

            modelBuilder.Entity<appointments>()
           .Property(e => e.created_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<appointments>()
           .Property(e => e.updated_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<appointments>()
            .Property(e => e.created_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<appointments>()
            .Property(e => e.updated_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<configuration>()
           .Property(e => e.id)
           .UseIdentityColumn();

            modelBuilder.Entity<configuration>()
            .Property(e => e.config_value)
            .HasColumnType("character varying");

            modelBuilder.Entity<configuration>()
            .Property(e => e.created_by)
            .HasColumnType("character varying");

            modelBuilder.Entity<configuration>()
           .Property(e => e.updated_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<configuration>()
            .Property(e => e.created_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<configuration>()
            .Property(e => e.updated_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<customers>()
            .Property(e => e.id)
            .UseIdentityColumn();

            modelBuilder.Entity<customers>()
           .Property(e => e.name)
           .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
            .Property(e => e.email)
            .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
            .Property(e => e.phone)
            .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
            .Property(e => e.address)
            .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
            .Property(e => e.registration_date)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<customers>()
           .Property(e => e.created_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
           .Property(e => e.updated_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<customers>()
            .Property(e => e.created_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<customers>()
            .Property(e => e.updated_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<public_holidays>()
            .Property(e => e.id)
            .UseIdentityColumn();

            modelBuilder.Entity<public_holidays>()
           .Property(e => e.holiday_date)
           .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<public_holidays>()
            .Property(e => e.description)
            .HasColumnType("character varying");

            modelBuilder.Entity<public_holidays>()
           .Property(e => e.created_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<public_holidays>()
           .Property(e => e.updated_by)
           .HasColumnType("character varying");

            modelBuilder.Entity<public_holidays>()
            .Property(e => e.created_on)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<public_holidays>()
            .Property(e => e.updated_on)
            .HasColumnType("timestamp without time zone");
        }
        public DbSet<appointments> Appointments { get; set; }
        public DbSet<configuration> Configurations { get; set; }
        public DbSet<customers> Customers { get; set; }
        public DbSet<public_holidays> Public_Holidays { get; set; }
    }
}
