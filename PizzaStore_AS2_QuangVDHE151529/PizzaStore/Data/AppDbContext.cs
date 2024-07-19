using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
        //Đại diện cho 1 bảng
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        //Mô hình hóa dữ liệu và quan hệ
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.AccountId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
            });

            modelBuilder.Entity<Product>(entity => { 
                entity.HasKey(p => p.ProductId);
                entity.HasOne(s => s.Supplier).WithMany(p => p.Products).HasForeignKey(p => p.SupplierId);
                entity.HasOne(c => c.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.HasOne(c => c.Customer).WithMany(o => o.Orders).HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();
                entity.HasOne(o => o.Order).WithMany().HasForeignKey(o => o.OrderId);
                entity.HasOne(p => p.Product).WithMany().HasForeignKey(o => o.ProductId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
