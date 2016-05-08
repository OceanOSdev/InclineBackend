using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TodoListWebApp.Models;

namespace TodoListWebApp.DAL
{
   /// <summary>
   /// The class that holds the database connections and sets
   /// </summary>
   /// <remarks>
   /// The class is named as it is because I was following a tutorial and when i tried
   /// to change the class name, the whole thing broke on me.
   /// </remarks>
    public class TodoListWebAppContext : DbContext
    {
        public TodoListWebAppContext() 
            : base("TodoListWebAppContext")
        { }

        public DbSet<Todo> Todoes { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        // Health Related DatabaseSets
        public DbSet<Flexibility> Flexibilities { get; set; }
        public DbSet<BodyComposition> BodyComps { get; set; }
        public DbSet<CardiovascularFitness> Cardios { get; set; }
        public DbSet<MuscularStrengthAndEndurance> MuscularStrengthsAndEndurances { get; set; }

        // Skill Related DatabaseSets
        public DbSet<ReactionTime> ReactionTimes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}