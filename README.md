# Car Store
The car store was the final project from my second course regarding Web Apps. It's an ASP.NET project that provides a decent framework for a storefront where you order cars online. It depends on an MSSQL Server instance
to provide the list of products and categories available to view/"purchase."

## Setting up
Provide a Database Connection String and an admin login in your appconfig.json and install EF Core. Once done, you can run `Update-Database` and have the models migrated to your database.
Afterwards, navigate to `localhost:[port]/admin/` to access the admin portal. From there you can start adding Products and Categories.
