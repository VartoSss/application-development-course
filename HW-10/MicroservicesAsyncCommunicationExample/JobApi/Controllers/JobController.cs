using JobApi.Infra;
using JobApi.Models;
using JobApi.Models.Constants;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using QueueContracts;

namespace JobApi.Controllers;

[ApiController]
[Route("jobs")]
public class JobController(ISendEndpointProvider sendEndpointProvider, ITasksStorage tasksStorage, ILogger<JobController> logger) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(PostJobAcceptedResponse), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PostJob([FromBody] PostJobModel model, CancellationToken ct)
    {
        var id = Guid.NewGuid();
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.Tasks}"));

        tasksStorage.AddAccepted(id);
        try
        {
            await endpoint.Send(new JobTaskMessage
            {
                Id = id,
                RoomId = model.RoomId,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            }, ct);
        }
        catch
        {
            tasksStorage.Remove(id);
            throw;
        }

        logger.LogInformation("Задача {JobId} принята и отправлена в очередь {Queue}", id, QueueNames.Tasks);

        return Accepted(new PostJobAcceptedResponse
        {
            Id = id,
            Status = PostJobResponseStatus.Accepted
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JobState), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetJob(Guid id)
    {
        var job = tasksStorage.Get(id);
        if (job is null)
            return NotFound();

        return Ok(job);
    }
}
