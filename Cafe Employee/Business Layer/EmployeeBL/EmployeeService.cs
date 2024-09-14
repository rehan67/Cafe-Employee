using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.EmployeeDL;

namespace Cafe_Employee.Business_Layer.EmployeeBL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Get Employee by Cafe 
        public async Task<IEnumerable<EmployeeDto>> GetEmployees(string cafeName)
        {
            var employees = await _employeeRepository.GetEmployees(cafeName);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                Gender = e.Gender,
                PhoneNumber = e.PhoneNumber,
                DaysWorked = e.EmployeeCafes.Any() ? CalculateDaysWorked(e.Id).Result : 0,
                Cafe = e.EmployeeCafes.FirstOrDefault() != null ? new CafeDropdown
                {
                    CafeId = e.EmployeeCafes.FirstOrDefault().Cafe.Id,
                    Cafe = e.EmployeeCafes.FirstOrDefault().Cafe.Name
                } : null
            }).ToList();
        }

        // Get All Employee
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                Gender = e.Gender,
                PhoneNumber = e.PhoneNumber,
                DaysWorked = e.EmployeeCafes.Any() ? CalculateDaysWorked(e.Id).Result : 0,
                Cafe = e.EmployeeCafes.FirstOrDefault() != null ? new CafeDropdown
                {
                    CafeId = e.EmployeeCafes.FirstOrDefault().Cafe.Id,
                    Cafe = e.EmployeeCafes.FirstOrDefault().Cafe.Name
                } : null
            }).ToList();
        }

        // Get Employee by Id
        public async Task<EmployeeDto> GetEmployeeById(string id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                DaysWorked = CalculateDaysWorked(employee.Id).Result,
                Cafe = employee.EmployeeCafes.FirstOrDefault() != null ? new CafeDropdown
                {
                    CafeId = employee.EmployeeCafes.FirstOrDefault().Cafe.Id,
                    Cafe = employee.EmployeeCafes.FirstOrDefault().Cafe.Name
                } : null
            };
        }

        // Add Employee
        public async Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            // Check if the Cafe exists
            bool cafeExists = await _employeeRepository.CafeExistsById(employeeDto.CafeId);
            if (!cafeExists)
            {
                throw new ArgumentException($"Cafe with ID {employeeDto.CafeId} not found.");
            }

            // Generate a unique employee ID
            string newEmployeeId = GenerateEmployeeId();

            // Ensure the employee ID is unique
            while (await _employeeRepository.EmployeeExistsById(newEmployeeId))
            {
                newEmployeeId = GenerateEmployeeId();
            }

            // Create a new employee
            var employee = new Employee
            {
                Id = newEmployeeId,
                Name = employeeDto.Name,
                EmailAddress = employeeDto.EmailAddress,
                PhoneNumber = employeeDto.PhoneNumber,
                Gender = employeeDto.Gender
            };

            // Create a new employee-cafe relationship
            var employeeCafe = new EmployeeCafe
            {
                EmployeeId = employee.Id,
                CafeId = employeeDto.CafeId,
                StartDate = DateTime.Now
            };

            // Add employee and relationship to the database
            await _employeeRepository.AddEmployeeAsync(employee, employeeCafe);

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                DaysWorked = CalculateDaysWorked(employee.Id).Result,
                Cafe = employee.EmployeeCafes.FirstOrDefault() != null ? new CafeDropdown
                {
                    CafeId = employee.EmployeeCafes.FirstOrDefault().Cafe.Id,
                    Cafe = employee.EmployeeCafes.FirstOrDefault().Cafe.Name
                } : null
            };
        }


        // Update Emplopyee
        public async Task<EmployeeDto> UpdateEmployeeAsync(string employeeId, UpdateEmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {employeeId} not found.");
            }

            // Check if the cafe exists before updating
            bool cafeExists = await _employeeRepository.CafeExistsById(employeeDto.CafeId);
            if (!cafeExists)
            {
                throw new ArgumentException($"Cafe with ID {employeeDto.CafeId} not found.");
            }

            // Update employee details
            employee.Name = employeeDto.Name;
            employee.EmailAddress = employeeDto.EmailAddress;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Gender = employeeDto.Gender;

            // Update employee-cafe relation
            var employeeCafe = new EmployeeCafe
            {
                EmployeeId = employeeId,
                CafeId = employeeDto.CafeId,
                //StartDate = employeeDto.StartDate
            };

            // Update employee and relationship in the database
            await _employeeRepository.UpdateEmployeeAsync(employee, employeeCafe);

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                DaysWorked = CalculateDaysWorked(employee.Id).Result,
                Cafe = employee.EmployeeCafes.FirstOrDefault() != null ? new CafeDropdown
                {
                    CafeId = employee.EmployeeCafes.FirstOrDefault().Cafe.Id,
                    Cafe = employee.EmployeeCafes.FirstOrDefault().Cafe.Name
                } : null
            };
        }

        // Delete Employee
        public async Task DeleteEmployee(string id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }


        // Calculate CalculateDaysWorked
        public async Task<int> CalculateDaysWorked(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);
            var startDate = employee.EmployeeCafes.FirstOrDefault()?.StartDate ?? DateTime.Now;
            return (DateTime.Now - startDate).Days;
        }

        // GenerateEmployeeId
        private string GenerateEmployeeId()
        {
            const string prefix = "UI";
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string suffix = new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{prefix}{suffix}";
        }

    
    }

}
