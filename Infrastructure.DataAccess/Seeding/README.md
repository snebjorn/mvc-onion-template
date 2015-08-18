### Seeding:

Faker.Net is a framework that exposes some very simple functions to generate fake data, which easily can be used to seed a database. There is a example of this in SampleContextInitializer.cs. The usage of Faker.Net is implemented in the `SeedingHelper` class.


When executing `>Update-Database` in the package-manager-console, any pending migrations will be executed on the database aswell as the `Seed()` function, do make sure there aren't any errors in this function.
