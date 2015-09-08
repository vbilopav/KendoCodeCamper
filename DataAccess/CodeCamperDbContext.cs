using System;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Infrastructure;

namespace DataAccess
{
    public class CodeCamperDbContext : DbContext 
    {
        private readonly StringBuilder _sql;

        public string GetSql()
        {
            return _sql.ToString();
        }
               
        public CodeCamperDbContext(string nameOrConnectionString)            
            : base(nameOrConnectionString)           
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;

            Database.SetInitializer(new SampleData.CodeCamperDatabaseInitializer());

            if (App.Config.IsDebugConfiguration)
            {
                _sql = new StringBuilder();
                this.Database.Log = s => 
                {
                    try
                    {
                        _sql.Append(s);
                    }
                    catch (Exception e) //exceeded max capacity or out of memory
                    {
                        _sql.Clear();
                        _sql.AppendFormat("Sql log truncated due exception: {0}\n", e.Message);
                        _sql.Append(s);
                    }
                };
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<Person>().Property(t => t.Id)
                  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Session>().Property(t => t.Id)      
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<TimeSlot>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Room>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Track>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<SpeakerRating>().HasKey(a => new { a.UserId, a.PersonId });
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Person> PersonSpeaker { get; set; }
        public DbSet<Attendance> Attendance { get; set; }    
        public DbSet<SpeakerRating> SpeakerRating { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
