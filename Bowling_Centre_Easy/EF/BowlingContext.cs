using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bowling_Centre_Easy.Entities;
using System.Configuration;

namespace Bowling_Centre_Easy.EF
{
    public class BowlingContext : DbContext
    {

        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<BowlingLane> Lanes { get; set; }
        public DbSet<Scorecard> Scorecards { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<PlayerResult> PlayerResults { get; set; }

        // Adding these because of an abstract class problem
        public DbSet<RegisteredMember> RegisteredMembers { get; set; }
        public DbSet<GuestMember> GuestMembers { get; set; }

        // Optionally, you can have a parameterless constructor.
        public BowlingContext(DbContextOptions<BowlingContext> options)
            : base(options)
        {
        }

        // Parameterless constructor for design-time tools.
        public BowlingContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fully qualify the reference to System.Configuration
                var connString =
                    global::System.Configuration.ConfigurationManager
                        .ConnectionStrings["BowlingDB"].ConnectionString;

                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure custom relationships, keys, etc., if needed.
        }
    }
}
