using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MidManthEvidenceArtManageProcidureViewAgreeget.Models.ViewModel
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; } = null!;
        [Required, Display(Name = "Invoice Date"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public bool IsPaid { get; set; }
        public string? ImageUrl { get; set; }
        public int CustomerTypeId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PaidAmount { get; set; }
        public virtual CustomerType? CustomerType { get; set; } 
        public virtual IList<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
        public virtual IList<CustomerType> CustomerTypes { get; set; } = new List<CustomerType>();
        public IFormFile? ProfileFile { get; set; } 
    }
}
