Package Manager Console: Entity Framework Core 2.1.1

# ----------------------------
Add-Migration "InitialCreate"
Update-Database 

# ----------------------------
Remove-Migration

# ----------------------------
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False