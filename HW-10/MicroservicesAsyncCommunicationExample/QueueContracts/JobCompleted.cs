namespace QueueContracts;

/// <summary>
/// Факт «задача выполнена» в очереди results.
/// </summary>
public class JobCompleted
{
    public Guid Id { get; set; }
    public required string Result { get; set; }
    public DateTime CompletedAt { get; set; }
}
