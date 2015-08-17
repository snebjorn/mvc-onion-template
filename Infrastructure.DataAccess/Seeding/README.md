
Seeding with Faker.net:

This is a framework that exposes some very simple functions generate fake data, which easily can be used to seed a database. In Configuration.cs is a example of how to seed a database. The usage of Faker.Net is implemented in the SeedingHelper class.

Executing >Update-Database in the package-manager-console will trigger pending migrations, aswell as the Seed() function.