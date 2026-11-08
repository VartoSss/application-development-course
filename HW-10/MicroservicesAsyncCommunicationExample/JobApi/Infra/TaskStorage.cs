using System.Collections.Concurrent;
using JobApi.Models.Constants;

namespace JobApi.Infra;

public class TaskStorage : ITasksStorage
{
    private readonly ConcurrentDictionary<Guid, JobState> _jobs = new();

    public void AddAccepted(Guid id)
    {
        _jobs.TryAdd(id, new JobState
        {
            Id = id,
            Status = PostJobResponseStatus.Accepted
        });
    }

    public void Remove(Guid id) => _jobs.TryRemove(id, out _);

    public JobState? Get(Guid id) => _jobs.GetValueOrDefault(id);

    public void MarkDone(Guid id, string result, DateTime completedAt)
    {
        _jobs.AddOrUpdate(
            id,
            _ => new JobState
            {
                Id = id,
                Status = PostJobResponseStatus.Done,
                Result = result,
                CompletedAt = completedAt
            },
            (_, existing) =>
            {
                if (existing.Status == PostJobResponseStatus.Done)
                    return existing;

                existing.Status = PostJobResponseStatus.Done;
                existing.Result = result;
                existing.CompletedAt = completedAt;
                return existing;
            });
    }
}
