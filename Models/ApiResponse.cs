namespace AzureDocumentIntelligence.Models
{
    public class ApiResponse
    {
        public ApiStatus Status { get; set; } = new ApiStatus();
        public List<ApiError> Errors { get; set; }

        public ApiResponse(ApiError error)
        {
            Status = new ApiStatus { Code = 4001 };
            Errors = new List<ApiError> { error };
        }
    }
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public ApiStatus Status { get; set; } = new ApiStatus();
        public List<ApiError> Errors { get; set; }
        public ApiResponse(T data)
        {
            Data = data;
            Status = Status;
        }
        public ApiResponse(ApiError error)
        {
            Status = new ApiStatus{ Code = 4001 };
            Errors = new List<ApiError> { error };
        }
    }

    public class ApiStatus
    {
        public int Code { get; set; } = 2000;
        public string Version { get; set; } = "AzureDocumentIntelligence 1.0";
        public DateTime Timestampz { get => DateTime.UtcNow; }
    }

    public class ApiError
    {
        public int Code { get; set; } = 4001;
        public string Message { get; set; } = "invalid request";
    }
}
