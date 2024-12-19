using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TestProgrammasy.Data;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;
using TestProgrammasy.Services.TestService;

namespace TestProgrammasy.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ApplicationDbContext _dbContext;

        public TestController(ITestService testService, ApplicationDbContext dbContext)
        {
            _testService = testService;
            _dbContext = dbContext;
        }

        // Test soraglaryny görkezmek (Read)
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

        // Täze test döretmek (Create)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDTO createTestDTO)
        {
            if (ModelState.IsValid)
            {
                await _testService.CreateTest(createTestDTO);
                return RedirectToAction("TestByUser", "Test", new { userId = @User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            return View(createTestDTO);
        }
        // Testi üýtgetmek (Update)
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
                await _testService.EditTest(editTestDTO);
                return RedirectToAction("TestByUser", "Test", new { userId = @User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            return View(editTestDTO);            
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _testService.RemoveTest(id);

                TempData["Success"] = "Test üstünlikli pozuldy";
                return RedirectToAction("TestByUser", "Test", new { userId = @User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Test pozulanda ýalňyşlyk ýüze çykdy";
                return RedirectToAction("TestByUser", "Test", new { userId = @User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
        }

        
        public async Task<IActionResult> Preview(int id)
        {
            var test = await _testService.GetTestForPreviewById(id);

            if (test == null)
            {
                return NotFound();
            }

            var viewModel = new TestPreviewDTO
            {
                Id = test.Id,
                Name = test.Name,
                Description = test.Description,
                TotalQuestions = test.Questions.Count,
                TotalPoints = test.Questions.Sum(q => q.Points),
                Questions = test.Questions.Select(q => new QuestionPreviewDTO
                {
                    QuestionText = q.QuestionText,
                    Points = q.Points,
                    Answers = q.Answers.Select(a => a.AnswerText).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

    }
}
