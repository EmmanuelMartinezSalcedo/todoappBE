namespace todoAppBE.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  private readonly List<TaskItem> _tasks = new();
  private readonly List<Tag> _tags = new();

  public User(string name, string email, string passwordHash)
  {
    UpdateName(name);
    UpdateEmail(email);
    PasswordHash = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
    CreatedAt = DateTime.UtcNow;
  }

  public string Name { get; private set; } = default!;
  public string Email { get; private set; } = default!;
  public string PasswordHash { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; }

  public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
  public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

  
  public User UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    return this;
  }

  public User UpdateEmail(string newEmail)
  {
    Email = Guard.Against.NullOrEmpty(newEmail, nameof(newEmail));
    return this;
  }

  public TaskItem AddTask(string title, string description, DateTime? dueDate = null)
  {
    var task = new TaskItem(title, description, dueDate);
    _tasks.Add(task);
    return task;
  }

  public Tag AddTag(string name)
  {
    var tag = new Tag(name);
    _tags.Add(tag);
    return tag;
  }

  public void AssignTagToTask(int taskId, int tagId)
  {
    var task = _tasks.FirstOrDefault(t => t.Id == taskId);
    var tag = _tags.FirstOrDefault(t => t.Id == tagId);

    if (task is null) throw new InvalidOperationException("Task not found");
    if (tag is null) throw new InvalidOperationException("Tag not found");

    task.AddTag(tag.Id);
  }
}
