namespace JobApi.Models;

public class PostJobAcceptedResponse
{
    /// <summary>
    /// Id бронирования
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Статус задачи
    /// </summary>
    public required string Status { get; set; }
}