## UnitTests:

This folder contains examples of how to use xUnit, nSubstitute and faking a db context with fake data.
There will be a file with some short examples of how they are used, and how they can be used together. 

### xUnit:
This testing framework and works much like NUnit. They are both used much in the same way, although you implement your tests different.

#### Notable differences: 
- They use different attribute and assertion naming. A comparison can be found here (http://xunit.github.io/docs/comparisons.html), but do not expect it to be up to date.

- xUnit uses the class constructor as as `[Setup]`.

- If needed, IDisposable can be implemented for `[TearDown]`.

- Tests uses either the `[Fact]` or `[Theory]` attribute to define which functions are tests.
    
- NUnit instantiates one class and runs all its tests. xUnit will instantiate and dispose the class each time a test is run.

### nSubstitute:
When testing, stubs and mocks are a good way to substitute your dependencies, but when writing a test you would rather spend time testing, than trying to determine whether you should be using a stub or mock. You also want your tests to remain clean and clear for anyone who might edit them later. This framework saves you the trouble by only using a single 'substitute' type, which can be derived from any interface or class. 


### Fake db context:
When testing controllers, it is preferable not to use the real database as the tests could potentially fill the database with invalid data, if not used correct. The small example shows how to substitute the database context and provide it with some fake data with Faker.Net. All tests are then run on the data provided, and only persists in the current memory.
