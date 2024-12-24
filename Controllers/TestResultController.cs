using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProgrammasy.DTOs;
using TestProgrammasy.Services.PdfService;
using TestProgrammasy.Services.TestResultService;
using TestProgrammasy.Services.UserService;

public class TestResultController : Controller
{
    private readonly ITestResultService _testResultService;
    private readonly IUserService _userService;
    private readonly IPdfService _pdfService;

    public TestResultController(
        ITestResultService testResultService,
        IUserService userService,
        IPdfService pdfService)
    {
        _testResultService = testResultService;
        _userService = userService;
        _pdfService = pdfService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var userRole = await _userService.GetUserRole(User.Identity.Name);
        var result = await _testResultService.GetTestResultByIdAsync(id);

        if (result == null) return NotFound();
        result.StudentName = await _userService.GetUserFullName(result.UserId);
        // Admin we mugallymlarähli netijeleri görüp bilýär
        // Studentler diňe öz netijelerini görüp bilýär
        if (userRole == "Student" && result.UserId != User.Identity.Name)
            return Forbid();

        return View(result);
    }

    public async Task<IActionResult> List()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userRole = await _userService.GetUserRole(userId);

        var results = new List<TestResultDTO>();

        switch (userRole)
        {
            case "Admin":
                results = await _testResultService.GetAllTestResultsAsync();
                break;
            case "Teacher":
                results = await _testResultService.GetTeacherTestResultsAsync(userId);
                break;
            case "Student":
                results = await _testResultService.GetStudentTestResultsAsync(userId);
                break;
        }
        return View(results);
    }

    [HttpGet]
    public async Task<IActionResult> ExportPdf(int id)
    {
        var result = await _testResultService.GetTestResultWithDetailsAsync(id);
        if (result == null) return NotFound();

        var pdf = await _pdfService.GenerateTestResultPdfAsync(result);
        return File(pdf, "application/pdf", $"{result.StudentName}.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> Analytics()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userRole = await _userService.GetUserRole(userId);
        if (userRole != "Admin" && userRole != "Teacher")
            return Forbid();

        var analytics = await _testResultService.GetTestAnalyticsAsync();
        return View(analytics);
    }
}