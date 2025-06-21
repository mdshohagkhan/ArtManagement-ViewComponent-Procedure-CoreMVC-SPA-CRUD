namespace MidManthEvidenceArtManageProcidureViewAgreeget.Models
{
    public class CustomerTypeHeadCount
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public int Count { get; set; }
       
       
    }
}
