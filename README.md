# 🚀ArtManagement-ViewComponent-Procedure-CoreMVC-SPA-CRUD

**ArtManagement-ViewComponent-Procedure-CoreMVC-SPA-CRUD** is an ASP.NET Core MVC project designed for managing sales data and generating reports.  
It uses View Components and Stored Procedures for data processing and analysis.

---

## 🎯 Project Purpose

- Manage sales invoices and details  
- Perform data grouping and aggregation (Min, Max, Sum, Avg) based on customer type  
- Upload and manage profile images  
- Use Stored Procedures for inserting and updating data in the database  
- Modularize UI using Partial Views and View Components

---

## ✅ Features Implemented

- **Create Sale**  
  - Create new invoices with multiple sales details  
  - Upload profile images  
  - Save data using Stored Procedures  

- **Edit Sale**  
  - Edit existing invoices and details  
  - Change or retain profile images  
  - Update data using Stored Procedures  

- **Delete Sale**  
  - Delete sales records  

- **Aggregate Reporting**  
  - Calculate min, max, sum, and average of sales values  
  - Group reports by customer type  
  - Display reports using View Components  

- **File Upload**  
  - Save images to `wwwroot/images` folder  
  - Generate unique filenames using GUID  

- **Use of Partial Views and View Components**  
  - Modularize the UI for better maintainability and reuse  

---

## 🛠 Technologies Used

| Technology              | Purpose                        |
|-------------------------|-------------------------------|
| ASP.NET Core MVC        | Web application framework      |
| Entity Framework Core   | ORM (Code First approach)      |
| Microsoft SQL Server    | Database                      |
| Razor Views             | UI templating                  |
| Bootstrap               | Responsive design              |
| Stored Procedures       | Data processing                |
| IFormFile               | File upload handling           |

---

## 📂 Folder Structure (Summary)

ArtManagement-ViewComponent-Procedure-CoreMVC-SPA-CRUD/
├── Controllers/
│ └── SalesController.cs
├── Models/
│ ├── Sale.cs
│ ├── SaleDetail.cs
│ └── ViewModels/
├── Views/
│ └── Sales/
│ ├── Index.cshtml
│ ├── _CreateSalePartial.cshtml
│ └── _EditSalePartial.cshtml
├── wwwroot/
│ └── images/
├── Scripts/
│ └── DbSchema.sql
├── appsettings.json
└── README.md


---

## ⚙️ Installation & Running Instructions

### 1. Clone the repository

```bash
git clone https://github.com/mdshohagkhan/SArtManagement-ViewComponent-Procedure-CoreMVC-SPA-CRUD.git
cd ArtManagement-ViewComponent-Procedure-CoreMVC-SPA-CRUD

2. Create the database and run the scripts
.Create a new database in SQL Server (e.g., SalesDb)

Execute the script file located at Scripts/DbSchema.sql (this contains table types and stored procedures)

3. Update the connection string
.Open appsettings.json and update the connection string to match your database setup:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SalesDb;Trusted_Connection=True;"
}
4. Run the application
dotnet restore
dotnet build
dotnet run
📊 Reporting & Analytics
Fast data aggregation using stored procedures

.Sales statistics grouped by customer type

.Beautiful report display via View Components

🔮 Future Plans
.User authentication and role-based access control

.Export reports as PDF or Excel

.Interactive charts and dashboards

.Advanced search and filtering features

