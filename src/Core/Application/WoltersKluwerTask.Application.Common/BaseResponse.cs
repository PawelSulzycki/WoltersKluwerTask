namespace WoltersKluwerTask.Application.Common
{
    public abstract class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public bool Success { get => Status == ResponseStatus.Success ? true : false; }

        protected BaseResponse()
        {
            Status = ResponseStatus.Success;
        }


        protected BaseResponse(ResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
