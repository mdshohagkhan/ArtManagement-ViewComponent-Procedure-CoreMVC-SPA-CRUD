using Microsoft.AspNetCore.Mvc;
using MidManthEvidenceArtManageProcidureViewAgreeget.Models.ViewModel;
using MidManthEvidenceArtManageProcidureViewAgreeget.Models;
using static MidManthEvidenceArtManageProcidureViewAgreeget.Models.ViewModel.AggregateViewModel;

namespace MidManthEvidenceArtManageProcidureViewAgreeget.ViewComponents
{
    public class AggregateInfoViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public AggregateInfoViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _db.Sales.Join(_db.CustomerTypes,
            sale => sale.CustomerTypeId,
            customerType => customerType.CustomerTypeId,
            (sale, customerType) => new { Sale = sale, CustomerType = customerType })
        .ToList();
            if (data.Count > 0)
            {
                var min = data.Min(s => s.Sale.PaidAmount);
                var max = data.Max(s => s.Sale.PaidAmount);
                var sum = data.Sum(s => s.Sale.PaidAmount);
                var avg = data.Average(s => s.Sale.PaidAmount);
                var count = data.Count();

                var groupByResult = data
                    .GroupBy(s => new { s.Sale.CustomerTypeId, s.CustomerType.CustomerTypeName }).Select(c => new GroupByViewModel
                    {
                        CustomerTypeId = c.Key.CustomerTypeId,
                        CustomerTypeName = c.Key.CustomerTypeName,
                        MaxValue = c.Max(s => s.Sale.PaidAmount),
                        MinValue = c.Min(s => s.Sale.PaidAmount),
                        SumValue = c.Sum(s => s.Sale.PaidAmount),
                        AvgValue = c.Average(s => s.Sale.PaidAmount),
                        Count = c.Count()
                    }).ToList();

                var aggregateViewModel = new AggregateViewModel
                {
                    MinValue = min,
                    MaxValue = max,
                    SumValue = sum,
                    AvgValue = avg,
                    GroupByResult = groupByResult
                };
                return View(aggregateViewModel);
            }
            return View(new AggregateViewModel
            {
                MinValue = 0,
                MaxValue = 0,
                SumValue = 0,
                AvgValue = 0,
                GroupByResult = new List<GroupByViewModel>()

            });

        }
    }
}
