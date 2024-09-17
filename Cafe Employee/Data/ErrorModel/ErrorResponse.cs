namespace Cafe_Employee.Data.ErrorModel
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; } // Optional: for additional information
    }
}
