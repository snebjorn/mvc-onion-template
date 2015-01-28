Core -> Domain Model
==================
The very center. If this library references any other libraries (other than native C#), then YOU'RE DOING IT WRONG!

This is where you place your domain model. Also known as the Plain Old CLR Objects (POCO) for Entity Framework (Code First) to work with.

The most important thing to remember here is to abide the conventions made by Entity Framework.

Here's a few of them:

* Always use auto properties.
* Properties named **Id** or *ClassName***Id** are marked as primary key.
* Navigation properties with cardinality MANY uses `ICollection<T>` as their type. Remember to instantiate collections to avoid `NullReferenceException`s

```csharp
public class MyPoco
{
    public MyPoco()
    {
        OtherPocos = new HashSet<OtherPoco>();
    }

    public ICollection<OtherPoco> OtherPocos {get; set;}
}
```

* `virtual` navigation properties are lazy loaded. (**ALWAYS DO THIS**, unless you have an insanely good reason not to)

```csharp
public class MyPoco
{
    public virtual ICollection<OtherPoco> OtherPocos {get; set;}
}
```