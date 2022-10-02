using Microsoft.EntityFrameworkCore;

namespace invoice_project.Models
{
    public class Entity:DbContext
    {
        public Entity() { }
        public Entity(DbContextOptions<Entity> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceItem>()
                .HasKey(o => new { o.invoice_id,o.item_id });
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
    }
}
