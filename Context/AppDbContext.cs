using API_WEB_FINAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_WEB_FINAL.Context
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<PropertyDetails> PropertyDetails { get; set; }
        public virtual DbSet<ProductDetailPropertyDetails> ProductDetailPropertyDetails { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDetails>()
                .HasOne(pd => pd.ParentProductDetail)
                .WithMany(pd => pd.ChildProductDetails)
                .HasForeignKey(pd => pd.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = THUOCLE\\THUOCLE; Database = QLSANPHAM; Trusted_Connection = True; TrustServerCertificate=True");
        }
        
    }
}
