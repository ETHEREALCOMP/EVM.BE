namespace EVM.Services.Features.EventTask.Models.Requests;

public class UpdateTaskRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public Data.Enums.TaskStatus? Status { get; set; }
}
