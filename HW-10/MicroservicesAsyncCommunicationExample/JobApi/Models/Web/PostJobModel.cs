namespace JobApi.Models;

public class PostJobModel
{
    /// <summary>
    /// Какую переговорку бронируем.
    /// </summary>
    public Guid RoomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
