

namespace TechFix.TransportModels.Core
{
    public class ResponseModel
    {
        public string ErrorCode { get; set; }
        public bool? Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public ResponseModel()
        {
            Success = true;
        }

        public ResponseModel(object data)
        {
            Success = true;
            Data = data;
        }

        public ResponseModel(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
