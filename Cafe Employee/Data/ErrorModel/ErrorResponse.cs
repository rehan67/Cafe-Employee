namespace Cafe_Employee.Data.ErrorModel
{
    /// <summary>
    /// Represents an error response returned by the API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional details about the error. This property is optional.
        /// </summary>
        public string Details { get; set; }
    }
}
