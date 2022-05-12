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
