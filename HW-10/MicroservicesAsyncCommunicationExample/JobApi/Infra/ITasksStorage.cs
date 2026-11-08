namespace JobApi.Infra;

public interface ITasksStorage
{
    void AddAccepted(Guid id);
    void Remove(Guid id);
    JobState? Get(Guid id);
    void MarkDone(Guid id, string result, DateTime completedAt);
}

public class JobState
{
    public Guid Id { get; set; }
    public required string Status { get; set; }
    public string? Result { get; set; }
    public DateTime? CompletedAt { get; set; }
}
