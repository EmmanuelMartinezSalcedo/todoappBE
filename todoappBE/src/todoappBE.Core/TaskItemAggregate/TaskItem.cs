namespace todoAppBE.Core.UserAggregate;

public class TaskItem : EntityBase
{
  private readonly List<int> _tagIds = new();

  internal TaskItem(string title, string description, DateTime? dueDate)
  {
    UpdateTitle(title);
    Description = description;
    CreatedAt = DateTime.UtcNow;
    DueDate = dueDate;
    Status = TaskStatus.Pending;
  }

  public string Title { get; private set; } = default!;
  public string Description { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; }
  public DateTime? DueDate { get; private set; }
  public TaskStatus Status { get; private set; }

  public IReadOnlyCollection<int> TagIds => _tagIds.AsReadOnly();

  public TaskItem UpdateTitle(string newTitle)
  {
    Title = Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
    return this;
  }

  public TaskItem SetDueDate(DateTime? dueDate)
  {
    DueDate = dueDate;
    return this;
  }

  public TaskItem SetStatus(TaskStatus status)
  {
    Status = status;
    return this;
  }

  internal void AddTag(int tagId)
  {
    if (!_tagIds.Contains(tagId))
      _tagIds.Add(tagId);
  }
}

public enum TaskStatus
{
  Pending = 0,
  Completed = 1,
  Archived = 2
}
