using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidManthEvidenceArtManageProcidureViewAgreeget.Models;

namespace MidManthEvidenceArtManageProcidureViewAgreeget.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public HeadCountViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int customerTypeId)
        {
            var customerTypeCounts = await _db.Sales.Include(s => s.CustomerType).GroupBy(s => new { s.CustomerType.CustomerTypeId, s.CustomerType.CustomerTypeName }).Select(g => new CustomerTypeHeadCount
            {
                CustomerTypeId = g.Key.CustomerTypeId,
                CustomerTypeName = g.Key.CustomerTypeName,
                Count = g.Count()
            }).ToListAsync();
            return View(customerTypeCounts?? new List<CustomerTypeHeadCount>());
        }
    }
}
