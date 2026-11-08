namespace QueueContracts;

/// <summary>
/// Задача в очереди tasks.
/// </summary>
public class JobTaskMessage
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
