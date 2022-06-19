# Create the School database by implementing Migrations

- In VS 2022 follow this menu sequence
    > Tools
      > NuGet Package Manager 
        > Package Manager Console

- Then in console to create c# database schema type

    `add-migration CreateSchoolDatabase`

  where `CreateSchoolDatabase` is the migration name

- Then push the migrations to database typing

    `update-database`

## Using T-Script

For the creation of the database and the tables, a Transact SQL script is attached:
      `create_database_and_tables.sql`
open it and run it in your MS SQL Server Management Studio
