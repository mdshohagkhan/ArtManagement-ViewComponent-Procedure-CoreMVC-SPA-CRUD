using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MidManthEvidenceArtManageProcidureViewAgreeget.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; } = null!;
        [Required, Display(Name = "Invoice Date"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Invoice No")]
        [Required]
        public string InvoiceNo { get; set; } = null!;
        [Display(Name = "Is Paid")]
       
        public bool IsPaid { get; set; }
        [Display(Name = "Upload ImageFile")]
        [Required]
        public string? ImageUrl { get; set; }
        public int CustomerTypeId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "Paid Amount ")]
        [Required]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Customer Type ")]
        [Required]
        public virtual CustomerType CustomerType { get; set; } = null!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }

    public class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public string ArtName { get; set; } = null!;
        public int Quantity { get; set; }
        public int SaleId { get; set; }
        public virtual Sale? Sale { get; set; } = null!;
    }

    public class CustomerType
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        { }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerType>().HasData(
              new CustomerType { CustomerTypeId = 1, CustomerTypeName = "Exclusive" },
                new CustomerType { CustomerTypeId = 2, CustomerTypeName = "Reglura" },
                new CustomerType { CustomerTypeId = 3, CustomerTypeName = "Premium" }
            );
        }
    }
}
