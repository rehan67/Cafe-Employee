namespace Cafe_Employee.CustomException
{
    public class EmployeeAlreadyExistsException : Exception
    {
        public EmployeeAlreadyExistsException(string message) : base(message) { }
    }
}
