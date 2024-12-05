using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedLibrary.Backend.DataAccess;
using SharedLibrary.Backend.BusinessLogic;

namespace registration_system.Controllers;

public class TimeRegistrationController : Controller
{
    private readonly TimeRegistrationService _service;
    private readonly EmployeeService _employeeService;
    private readonly CaseService _caseService;

    public TimeRegistrationController(TimeRegistrationService service, EmployeeService employeeService,
        CaseService caseService)
    {
        _service = service;
        _employeeService = employeeService;
        _caseService = caseService;
    }

    public IActionResult Create()
    {
        ViewBag.Employees = new SelectList(_employeeService.GetAllEmployees(), "EmployeeID", "Name");
        ViewBag.Cases = new SelectList(_caseService.GetAllCases(), "CaseID", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TimeRegistration registration)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _service.AddTimeRegistration(registration);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        ViewBag.Employees = new SelectList(_employeeService.GetAllEmployees(), "EmployeeID", "Name");
        ViewBag.Cases = new SelectList(_caseService.GetAllCases(), "CaseID", "Title");
        return View(registration);
    }
}