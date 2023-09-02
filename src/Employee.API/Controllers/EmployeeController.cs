using Employee.API.Application.Inputs;
using Employee.API.Application.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Employee.API.Infrastructure.Repositories;

namespace Employee.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<CreateEmployeeInput> _createEmployeeInputValidator;
    private readonly IValidator<UpdateEmployeeInput> _updateEmployeeInputValidator;
    public EmployeeController(
        IEmployeeService employeeService,
        IEmployeeRepository employeeRepository,
        IValidator<CreateEmployeeInput> createUserInputValidable,
        IValidator<UpdateEmployeeInput> updateEmployeeInputValidator)
    {
        _employeeService = employeeService;
        _employeeRepository = employeeRepository;
        _createEmployeeInputValidator = createUserInputValidable;
        _updateEmployeeInputValidator = updateEmployeeInputValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeInput input)
    {
        var validate = _createEmployeeInputValidator.Validate(input);

        if (!validate.IsValid)
            return BadRequest("Invalid body");

        var currentEmployee = await _employeeRepository.FirstOrDefaultByDocumentAsync(input.Document!);
        if (currentEmployee is not null)
            return Conflict("Already exists employee with this document");

        var employee = await _employeeService.Create(input);

        var output = new CreateEmployeeOutput(
            employee.Id, employee.Name, employee.Document,
            employee.BirthedAt, employee.CreatedAt, employee.UpdatedAt);

        return Created(employee.Id.ToString(), output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var employee = await _employeeRepository.FirstOrDefaultByIdAsync(id);

        if (employee is null)
            return NotFound("Cannot found employee");

        var output = new ReadEmployeeOutput(
            employee.Id, employee.Name, employee.Document,
            employee.BirthedAt, employee.CreatedAt, employee.UpdatedAt);

        return Ok(output);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateEmployeeInput input)
    {
        var validate = _updateEmployeeInputValidator.Validate(input);

        if (!validate.IsValid)
            return BadRequest("Invalid");

        var currentEmployee = await _employeeService.Update(id, input);

        if (currentEmployee is null)
            return NotFound("Cannot found employee");

        return Ok(currentEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var currentEmployee = await _employeeRepository.FirstOrDefaultByIdAsync(id);

        if (currentEmployee is null)
            return NotFound("Cannot found employee");

        await _employeeRepository.DeleteByIdAsync(id);

        return NoContent();
    }
}
