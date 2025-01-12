using FootballStatisticsEngine.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FootballStatisticsEngine.Contexts
{
    public class MatchReportDbContext : DbContext
    {
        private string _connectionString;

        public MatchReportDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure MatchReports as a keyless entity
            modelBuilder.Entity<MatchReports>().HasNoKey();
        }

        public List<MatchReports> GetReportsFromYear(string tableName)
        {
            return this.MatchReports.FromSqlRaw($"SELECT * FROM {tableName}").ToList();
        }

        public DbSet<MatchReports> MatchReports { get; set; } // Temporary table for raw SQL
    }
}