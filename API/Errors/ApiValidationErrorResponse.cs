namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {

        }

        public System.Collections.Generic.IEnumerable<string> Errors { get; set; }
    }
}