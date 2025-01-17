namespace Chilaqueria_API.Models
{
    public class Globals
    {
        public class GlobalResponse<T>
        {
            public T? GlobalData { get; set; }
            public int? ResponseCode { get; set; }
            public int? ResultQnty { get; set; }
            public string? UserMessage { get; set; }
            public double ElapsedTime { get; set; }

            public GlobalResponse()
            {
                ResponseCode = 0;
                GlobalData = default;
            }
        }

    }
}
