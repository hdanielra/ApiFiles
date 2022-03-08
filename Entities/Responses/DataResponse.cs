namespace Entities.Responses
{
    public class DataResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object? MostSold { get; set; }
        public object? Data { get; set; }

        public DataResponse()
        {
            Status = "";
            Message = "";
        }
    }
}
    