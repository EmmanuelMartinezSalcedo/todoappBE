namespace todoAppBE.Core.UserAggregate;

public class Tag : EntityBase
{
  internal Tag(string name)
  {
    UpdateName(name);
  }

  public string Name { get; private set; } = default!;

  public Tag UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    return this;
  }
}
