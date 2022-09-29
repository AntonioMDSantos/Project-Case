namespace Back.Application.Models
{
    public class GetTokenModel
    {
        public GetTokenModel(string token, object user, DateTime validate, int statusCode, string message)
        {
            Token = token;
            User = user;
            ValidTo = validate;
            StatusCode = statusCode;
            Message = message;
        }

        public GetTokenModel(string token, object user, DateTime validate, int statusCode)
        {
            Token = token;
            User = user;
            ValidTo = validate;
            StatusCode = statusCode;
        }

        public string Token { get; set; }
        public string Message { get; set; } = "";
        public int StatusCode { get; set; }
        public object User { get; set; }
        public DateTime ValidTo { get; set; }
    }

    public class ResetPasswordModel
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public int Code { get; set; }
    }
}
