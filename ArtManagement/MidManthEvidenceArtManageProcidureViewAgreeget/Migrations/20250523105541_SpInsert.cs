using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidManthEvidenceArtManageProcidureViewAgreeget.Migrations
{
    /// <inheritdoc />
    public partial class SpInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE ParamModuleType AS TABLE(
                ArtName NVARCHAR(50),
                Quantity INT
            );
           GO
           CREATE OR ALTER PROCEDURE dbo.spInsertInvoice
            @CustomerName NVARCHAR(50),
            @InvoiceDate DATETIME,
            @InvoiceNo NVARCHAR(15),
            @CustomerTypeId INT,                
            @IsPaid BIT,
            @PaidAmount DECIMAL(18,4),
            @ImageUrl NVARCHAR(100),
            @SaleDetails ParamModuleType READONLY
            AS
            BEGIN
            BEGIN TRY
            DECLARE @LocalModules TABLE(
               ArtName NVARCHAR(50),
               Quantity INT,
               SaleId INT
             );
             DECLARE @SaleId INT;
            INSERT INTO dbo.Sales(CustomerName, InvoiceDate,InvoiceNo,CustomerTypeId,IsPaid,PaidAmount,ImageUrl) VALUES (@CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@PaidAmount, @ImageUrl);
            SET @SaleId=SCOPE_IDENTITY();

            INSERT INTO @LocalModules(ArtName, Quantity, SaleId)
            SELECT ArtName, Quantity, @SaleId
            FROM @SaleDetails

            INSERT INTO dbo.SaleDetails(ArtName, Quantity, SaleId)
            SELECT ArtName, Quantity, @SaleId
            FROM @LocalModules

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
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spInsertInvoice");
            migrationBuilder.Sql("DROP TYPE IF EXISTS ParamModuleType");
        }
    }
}
