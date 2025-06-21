using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MidManthEvidenceArtManageProcidureViewAgreeget.Models;
using MidManthEvidenceArtManageProcidureViewAgreeget.Models.ViewModel;
using System.Data;
using System.Threading.Tasks;

namespace MidManthEvidenceArtManageProcidureViewAgreeget.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _web;

        public SalesController(AppDbContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }

        public IActionResult Index()
        {
            var sales = _db.Sales.Include(s => s.CustomerType).Include(s => s.SaleDetails).ToList();
            return View(sales);

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sale = _db.Sales.Find(id);
            if (sale != null)
            {
                _db.Sales.Remove(sale);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreatePartial()
        {
            SaleViewModel sale = new SaleViewModel();
            sale.CustomerTypes = _db.CustomerTypes.ToList();
            sale.SaleDetails.Add(new SaleDetail() { SaleDetailId = 1 });
            return PartialView("_CreateSalePartial", sale);

        }
        [HttpPost]


        public async Task< ActionResult> CreateSale(SaleViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.CustomerTypes = _db.CustomerTypes.ToList();
                return View();
            }
            Sale sale = new Sale
            {
                CustomerName = vobj.CustomerName,
                InvoiceDate = vobj.InvoiceDate,
                InvoiceNo = vobj.InvoiceNo,
                CustomerTypeId = vobj.CustomerTypeId,
                IsPaid = vobj.IsPaid,
                PaidAmount = vobj.PaidAmount,
                SaleDetails = vobj.SaleDetails
            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName =await GetFileName(vobj.ProfileFile);
                sale.ImageUrl = uniqueFileName;
            }
            else
            {
                sale.ImageUrl = "noimage.png";
            }

            //_db.Sales.Add(sale);
            //_db.SaveChanges();
            DataTable moduleTable = new DataTable();
            moduleTable.Columns.Add("ArtName", typeof(string));
            moduleTable.Columns.Add("Quantity", typeof(int));
            if (vobj.SaleDetails != null && vobj.SaleDetails.Any())
            {
                foreach (var m in vobj.SaleDetails)
                {
                    moduleTable.Rows.Add(m.ArtName, m.Quantity);
                }
            }
            var parameters = new[]
            {
                new SqlParameter("@CustomerName",sale.CustomerName),
                new SqlParameter("@InvoiceDate",sale.InvoiceDate),
                new SqlParameter("@InvoiceNo",sale.InvoiceNo),
                new SqlParameter("@CustomerTypeId",sale.CustomerTypeId),
                new SqlParameter("@IsPaid",sale.IsPaid),
                new SqlParameter("@PaidAmount",sale.PaidAmount),
                new SqlParameter("@ImageUrl",sale.ImageUrl??(object)DBNull.Value),
                new SqlParameter
                {
                    ParameterName="@SaleDetails",
                    SqlDbType= SqlDbType.Structured,
                    TypeName="dbo.ParamModuleType",
                    Value=moduleTable
                }
            };
            await _db.Database.ExecuteSqlRawAsync("EXEC dbo.spInsertInvoice @CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@PaidAmount,@ImageUrl,@SaleDetails", parameters);
            return RedirectToAction("Index");
            

        }

        private async Task<string> GetFileName(IFormFile profileFile)
        {
            string uniqueFileName = "";
            if (profileFile != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName); ;
                string uploadFolder = Path.Combine(_web.WebRootPath, "images");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                  await  profileFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public ActionResult EditPartial(int id)
        {
            var sale = _db.Sales
                .Include(a => a.SaleDetails)
                .FirstOrDefault(x => x.SaleId == id);
            var vObj = new SaleViewModel
            {
                CustomerName = sale.CustomerName,
                SaleId = sale.SaleId,
                InvoiceDate = sale.InvoiceDate,
                InvoiceNo = sale.InvoiceNo,
                CustomerTypeId = sale.CustomerTypeId,
                IsPaid = sale.IsPaid,
                ImageUrl = sale.ImageUrl,
                PaidAmount = sale.PaidAmount,
                SaleDetails = sale.SaleDetails.ToList(),
                CustomerTypes = _db.CustomerTypes.ToList()
            };
            return PartialView("_EditSalePartial", vObj);
        }

        [HttpPost]
        
        public async Task<  ActionResult> EditSale(SaleViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.CustomerTypes = _db.CustomerTypes.ToList();
                return Json(new { success = false });
            }
            Sale sale = _db.Sales.Find(vobj.SaleId);
            if (sale != null)
            {
                sale.CustomerName = vobj.CustomerName;
                sale.CustomerTypeId = vobj.CustomerTypeId;
                sale.InvoiceNo = vobj.InvoiceNo;
                
                sale.IsPaid = vobj.IsPaid;
                sale.InvoiceDate = vobj.InvoiceDate;
                
                sale.PaidAmount = vobj.PaidAmount;
                if (vobj.ProfileFile != null)
                {
                    string uniqueFileName =await GetFileName(vobj.ProfileFile);
                    sale.ImageUrl = uniqueFileName;
                }
                else
                {
                    sale.ImageUrl = OldImageUrl;
                }
                var modules = _db.SaleDetails.Where(m => m.SaleId == vobj.SaleId).ToList();
                DataTable moduleTable = new DataTable();
                moduleTable.Columns.Add("ArtName", typeof(string));
                moduleTable.Columns.Add("Quantity", typeof(int));
                if (vobj.SaleDetails != null && vobj.SaleDetails.Any())
                {
                    foreach (var m in vobj.SaleDetails)
                    {
                        moduleTable.Rows.Add(m.ArtName, m.Quantity);
                    }
                }
                var parameters = new[]
                {
                new SqlParameter("@CustomerName",sale.CustomerName),
                new SqlParameter("@InvoiceDate",sale.InvoiceDate),
                new SqlParameter("@InvoiceNo",sale.InvoiceNo),
                new SqlParameter("@CustomerTypeId",sale.CustomerTypeId),
                new SqlParameter("@IsPaid",sale.IsPaid),
                new SqlParameter("@PaidAmount",sale.PaidAmount),
                new SqlParameter("@ImageUrl",sale.ImageUrl??(object)DBNull.Value),
                new SqlParameter
                {
                    ParameterName="@SaleDetails",
                    SqlDbType= SqlDbType.Structured,
                    TypeName="dbo.EditParamModuleTypes",
                    Value=moduleTable
                },
                new SqlParameter("@SaleId",vobj.SaleId),
            };
                await _db.Database.ExecuteSqlRawAsync("EXEC dbo.spUpdateInvoice @CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@PaidAmount,@ImageUrl,@SaleDetails,@SaleId", parameters);
                return RedirectToAction("Index");
            }
               
               
                
                vobj.CustomerTypes = _db.CustomerTypes.ToList();
            return View(vobj);
        }
    }
}