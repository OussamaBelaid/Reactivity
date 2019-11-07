namespace Persistence
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="DataContext" />
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Values
        /// </summary>
        public DbSet<Value> Values { get; set; }

        /// <summary>
        /// Gets or sets the Activities
        /// </summary>
        public DbSet<Activity> Activities { get; set; }

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Value>().HasData(new Value { Id = 1, Name = "Value 101" }, new Value { Id = 2, Name = "Value 102" }, new Value { Id = 3, Name = "Value 103" });
        }
    }
}
