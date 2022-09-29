using Back.Domain.Entities;

namespace CaseBack.Application.Models
{
    public class ResponseModel
    {
        public List<Product> content;

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Content { get; set; }

        public ResponseModel(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ResponseModel(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ResponseModel(int statusCode, object? content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public ResponseModel(int statusCode, string message, object? content)
        {
            StatusCode = statusCode;
            Message = message;
            Content = content;
        }
    }
}
