namespace todoappBE.Core.Models;

public class TaskTag
{
  public int TaskId { get; private set; }
  public int TagId { get; private set; }

  private TaskTag() { } // EF Core

  public TaskTag(int taskId, int tagId)
  {
    TaskId = taskId;
    TagId = tagId;
  }
}
