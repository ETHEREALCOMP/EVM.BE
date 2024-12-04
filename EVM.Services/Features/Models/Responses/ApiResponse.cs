using System.Net;

namespace EVM.Services.Features.Models.Responses;

public class ApiResponse
{
    public ApiResponseNoData? Data { get; set; }

    public List<ApiError>? Errors { get; set; }

    public List<ApiError>? Warnings { get; set; }

    public ApiResponse(List<ApiError>? warnings = null)
    {
        Data = new() { Success = true };
        Warnings = warnings;
    }

    public ApiResponse(HttpStatusCode errorCode, string message = "Something went wrong", List<ApiError>? warnings = null)
    {
        Errors = [
            new()
            {
                Message = message,
                Code = ((int)errorCode).ToString(),
            },
        ];
        Warnings = warnings;
    }

    public ApiResponse Happify()
    {
        Data = new() { Success = true };

        return this;
    }

    public class ApiResponseNoData
    {
        public bool Success { get; set; }
    }
}