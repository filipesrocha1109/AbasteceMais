namespace AbasteceMais.Domain.Common
{
    public class ResponseSuccess
    {
        public bool Success { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
