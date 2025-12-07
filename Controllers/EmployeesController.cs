using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = context.Employees.ToList();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary
            };
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployees(Guid id, UpdateEmployeeDto employeeDto)
        {
            var employee = context.Employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }

            if(employeeDto.Name != null)
            {
                employee.Name = employeeDto.Name;
            }

            if(employeeDto.Email != null)
            {
                employee.Email = employeeDto.Email;
            }   

            if(employeeDto.Phone != null)
            {
                employee.Phone = employeeDto.Phone;
            }

            if(employeeDto.Salary != null)
            {
                employee.Salary = employeeDto.Salary.Value;
            }
           
            context.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = context.Employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok(employee);
        }
    }
}
