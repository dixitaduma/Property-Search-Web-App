using Microsoft.EntityFrameworkCore;
using Property_Search_Web_App.Models.Entity;

namespace Property_Search_Web_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Space> Spaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Property>()
                .HasMany(p => p.Spaces)
                .WithOne(s => s.Property)
                .HasForeignKey(s => s.PropertyId);

            
            modelBuilder.Entity<Property>()
                .HasIndex(p => p.Type)
                .HasDatabaseName("IX_Property_Type");

            modelBuilder.Entity<Property>()
                .HasIndex(p => p.Price)
                .HasDatabaseName("IX_Property_Price");

            modelBuilder.Entity<Space>()
                .HasIndex(s => s.Type)
                .HasDatabaseName("IX_Space_Type");

            modelBuilder.Entity<Space>()
                .HasIndex(s => s.Size)
                .HasDatabaseName("IX_Space_Size");

            // Seed data for Properties
            modelBuilder.Entity<Property>().HasData(
                new Property { Id = 1, Address = "123 Main St, City, State", Type = "house", Price = 300000, Description = "A beautiful family house" },
                new Property { Id = 2, Address = "456 Oak St, City, State", Type = "apartment", Price = 150000, Description = "Cozy apartment in the city center" },
                new Property { Id = 3, Address = "789 Pine St, City, State", Type = "condo", Price = 250000, Description = "Modern condo with great amenities" },
                new Property { Id = 4, Address = "101 Maple Ave, City, State", Type = "house", Price = 400000, Description = "Spacious house with a large yard" },
                new Property { Id = 5, Address = "202 Birch Rd, City, State", Type = "apartment", Price = 120000, Description = "Affordable apartment with great location" },
                new Property { Id = 6, Address = "303 Cedar Dr, City, State", Type = "condo", Price = 220000, Description = "Nice condo near the beach" },
                new Property { Id = 7, Address = "404 Elm St, City, State", Type = "house", Price = 350000, Description = "House with open-plan living space" },
                new Property { Id = 8, Address = "505 Walnut Ln, City, State", Type = "apartment", Price = 170000, Description = "Bright apartment with excellent views" },
                new Property { Id = 9, Address = "606 Ash Blvd, City, State", Type = "condo", Price = 280000, Description = "Luxury condo with pool and gym" },
                new Property { Id = 10, Address = "707 Maple Dr, City, State", Type = "house", Price = 320000, Description = "Large family house with 5 bedrooms" }
            );

            // Seed data for Spaces
            modelBuilder.Entity<Space>().HasData(
                new Space { Id = 1, PropertyId = 1, Type = "bedroom", Size = 150, Description = "Master bedroom" },
                new Space { Id = 2, PropertyId = 1, Type = "kitchen", Size = 100, Description = "Modern kitchen" },
                new Space { Id = 3, PropertyId = 1, Type = "living room", Size = 200, Description = "Spacious living room" },
                new Space { Id = 4, PropertyId = 2, Type = "bedroom", Size = 120, Description = "Cozy bedroom" },
                new Space { Id = 5, PropertyId = 2, Type = "bathroom", Size = 60, Description = "Full bathroom" },
                new Space { Id = 6, PropertyId = 3, Type = "bedroom", Size = 140, Description = "Luxury bedroom with ensuite" },
                new Space { Id = 7, PropertyId = 3, Type = "kitchen", Size = 110, Description = "State-of-the-art kitchen" },
                new Space { Id = 8, PropertyId = 4, Type = "living room", Size = 220, Description = "Open-plan living room" },
                new Space { Id = 9, PropertyId = 4, Type = "bedroom", Size = 130, Description = "Large bedroom" },
                new Space { Id = 10, PropertyId = 5, Type = "kitchen", Size = 90, Description = "Compact kitchen with modern appliances" }
            );
        }
    }
}
