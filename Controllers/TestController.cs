using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestProgrammasy.DTOs;
using TestProgrammasy.Services.TestService;
using TestProgrammasy.Services.UserService;

namespace TestProgrammasy.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;

        public TestController(ITestService testService, IUserService userService)
        {
            _testService = testService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var tests = _testService.GetTestList();
            return View(tests);
        }

        public IActionResult TestByUser(string userId)
        {
            var test = _testService.GetTestListByUserId(userId);
            if (test == null)
            {
                return NotFound();
            }
            return View("Index", test);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDTO createTestDTO)
        {
            if (ModelState.IsValid)
            {
                string uId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool isAdmin = await _userService.IsAdmin(uId);
                await _testService.CreateTest(createTestDTO);
                if (isAdmin) { return RedirectToAction("Index"); }
                else
                {
                    return RedirectToAction("TestByUser", "Test", new { userId = uId });
                }
            }
            return View(createTestDTO);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var test = await _testService.GetTestForEditById(id);

            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTestDTO editTestDTO)
        {
            if (ModelState.IsValid)
            {
                string uId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool isAdmin = await _userService.IsAdmin(uId);
                await _testService.EditTest(editTestDTO);
                if (isAdmin) { return RedirectToAction("Index"); }
                else
                {
                    return RedirectToAction("TestByUser", "Test", new { userId = uId });
                }
            }
            return View(editTestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string uId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = await _userService.IsAdmin(uId);
            try
            {
                await _testService.RemoveTest(id);

                TempData["Success"] = "Test üstünlikli pozuldy";
                if (isAdmin) { return RedirectToAction("Index"); }
                else
                {
                    return RedirectToAction("TestByUser", "Test", new { userId = uId });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Test pozulanda ýalňyşlyk ýüze çykdy";
                if (isAdmin) { return RedirectToAction("Index"); }
                else
                {
                    return RedirectToAction("TestByUser", "Test", new { userId = uId });
                }
            }
        }

        public async Task<IActionResult> Preview(int id)
        {
            var test = await _testService.GetTestForPreviewById(id);

            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        public async Task<IActionResult> StartTest(int id)
        {
            var test = await _testService.GetTestForPreviewById(id);
            if (test == null) return NotFound();

            if (test.Questions == null || !test.Questions.Any())
            {
                return NotFound("Bu test üçin soraglar tapylmady");
            }

            var progress = new TestProgressDTO
            {
                TestId = id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartDate = DateTime.Now,
                Answers = new Dictionary<int, int>()
            };

            HttpContext.Session.SetString("TestProgress",
                System.Text.Json.JsonSerializer.Serialize(progress));

            return View(test);
        }

        [HttpPost]
        public IActionResult SaveAnswer([FromBody] SaveAnswerRequestDTO request)
        {
            var progressJson = HttpContext.Session.GetString("TestProgress");
            var progress = System.Text.Json.JsonSerializer.Deserialize<TestProgressDTO>(progressJson);

            progress.Answers[request.questionId] = request.answerId;

            HttpContext.Session.SetString("TestProgress",
                System.Text.Json.JsonSerializer.Serialize(progress));

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> CompleteTest([FromBody] int remainingTime)
        {
            var progressJson = HttpContext.Session.GetString("TestProgress");
            var progress = System.Text.Json.JsonSerializer.Deserialize<TestProgressDTO>(progressJson);

            var result = await _testService.CalculateTestResult(progress, remainingTime);

            HttpContext.Session.Remove("TestProgress");

            return Json(new { redirectUrl = "/TestResult/Index/" + result.Id.ToString() });
        }
    }
}
