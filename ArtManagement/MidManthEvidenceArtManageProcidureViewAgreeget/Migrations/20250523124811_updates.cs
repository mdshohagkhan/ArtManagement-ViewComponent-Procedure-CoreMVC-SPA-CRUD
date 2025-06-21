using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidManthEvidenceArtManageProcidureViewAgreeget.Migrations
{
    /// <inheritdoc />
    public partial class updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE EditParamModuleTypes AS TABLE(
             ArtName NVARCHAR(50),
             Quantity INT
         );
        GO
        CREATE OR ALTER PROCEDURE dbo.spUpdateInvoice

            @CustomerName NVARCHAR(50),
            @InvoiceDate DATETIME,
            @InvoiceNo NVARCHAR(15),
            @CustomerTypeId INT,                
            @IsPaid BIT,
            @PaidAmount DECIMAL(18,4),
            @ImageUrl NVARCHAR(100),
            @SaleDetails EditParamModuleTypes READONLY,
            @SaleId INT
         AS
         BEGIN
         BEGIN TRY
        
 
        UPDATE dbo.Sales
         SET
             CustomerName = @CustomerName,
             InvoiceDate = @InvoiceDate,
             InvoiceNo = @InvoiceNo,
             CustomerTypeId = @CustomerTypeId,
             IsPaid = @IsPaid,
             PaidAmount = @PaidAmount,
             ImageUrl = @ImageUrl
         WHERE
          SaleId = @SaleId;
         DELETE FROM dbo.SaleDetails
         WHERE SaleId = @SaleId;

        INSERT INTO dbo.SaleDetails(ArtName, Quantity, SaleId)
         SELECT ArtName, Quantity, @SaleId
         FROM @SaleDetails;

         END TRY
         BEGIN CATCH
             Throw;
         END CATCH
         END
     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spUpdateInvoice");
            migrationBuilder.Sql("DROP TYPE IF EXISTS EditParamModuleTypes");
        }
    }
}
