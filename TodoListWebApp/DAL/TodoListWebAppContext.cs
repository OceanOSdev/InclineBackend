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

        // Health Related DatabaseSets [Deprecated]
        public DbSet<Flexibility> Flexibilities { get; set; }
        public DbSet<BodyComposition> BodyComps { get; set; }
        public DbSet<CardiovascularFitness> Cardios { get; set; }
        public DbSet<MuscularStrengthAndEndurance> MuscularStrengthsAndEndurances { get; set; }

        // New Health Related DatabaseSets
        public DbSet<SitAndReachModel> SitAndReaches { get; set; }
        public DbSet<ArmAndShoulderModel> ArmAndShoulders { get; set; }
        public DbSet<TrunkLiftModel> TrunkLifts { get; set; }
        public DbSet<HeightModel> Heights { get; set; }
        public DbSet<WeightModel> Weights { get; set; }
        public DbSet<PercentBodyFatModel> PercentBodyFats { get; set; }
        public DbSet<HalfMileTimeModel> HalfMileTimes { get; set; }
        public DbSet<PacerModel> Pacers { get; set; }
        public DbSet<MileTimeModel> MileTimes { get; set; }
        public DbSet<StepTestModel> StepTests { get; set; }
        public DbSet<CurlUpModel> CurlUps { get; set; }
        public DbSet<RightAnglePushUpModel> RightAnglePushUps { get; set; }
        public DbSet<MaxBenchModel> MaxBenches { get; set; }
        public DbSet<MaxLegPressModel> MaxLegPresses { get; set; }
        public DbSet<PullUpModel> PullUps { get; set; }
        public DbSet<FlexedArmHangModel> FlexedArmHangs { get; set; }

        // Skill Related DatabaseSets
        public DbSet<ReactionTime> ReactionTimes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}