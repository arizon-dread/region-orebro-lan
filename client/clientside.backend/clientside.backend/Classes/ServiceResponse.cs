using clientside.backend.Enums;

namespace clientside.backend.Classes
{
    public class ServiceResponse<T>
    {
        public T? Data { get;  }
        public string Message { get; } 
        public ServiceResponseEnum Status { get; }

        public ServiceResponse(string message, ServiceResponseEnum status, T? data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
    }
}
