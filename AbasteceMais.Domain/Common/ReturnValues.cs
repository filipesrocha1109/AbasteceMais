using AbasteceMais.Domain.Enums;

namespace AbasteceMais.Domain.Common
{
    public class ReturnValues
    {
        public bool Error { get; set; }

        public ErrorCodes Code { get; set; }

        public string Message { get; set; }

        public void SetReturnValues(bool error, ErrorCodes code, string message)
        {
            this.Error = error;
            this.Code = code;
            this.Message = message;
        }
    }
}
