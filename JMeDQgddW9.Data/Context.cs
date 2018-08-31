using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMeDQgddW9.Data
{
    /// <summary>
    /// Data access context
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Constructor with context options
        /// </summary>
        /// <param name="options">Options of database</param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        /// <summary>
        /// Users DbSet 
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Tokens DbSet
        /// </summary>
        public DbSet<Token> Tokens { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuration classes
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
        }
    }
}