using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Täze test döretmek (Create)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDTO test)
        {
            if (ModelState.IsValid)
            {
                await _testService.CreateTest(test);
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }
        // Testi üýtgetmek (Update)
        public async Task<IActionResult> Edit(int id)
        {
            var test = await _testService.GetTestForEditById(id);

            if (test == null)
            {
                return NotFound();
            }

            var viewModel = new EditTestDTO
            {
                Id = test.Id,
                Name = test.Name,
                Description = test.Description,
                Questions = test.Questions.Select(q => new EditQuestionDTO
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    //Points = q.Points,
                    Answers = q.Answers.Select(a => new EditAnswerDTO
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(EditTestViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var test = await _context.Tests
        //            .Include(t => t.Questions)
        //                .ThenInclude(q => q.Answers)
        //            .FirstOrDefaultAsync(t => t.Id == model.Id);

        //        if (test == null)
        //        {
        //            return NotFound();
        //        }

        //        test.Title = model.Title;
        //        test.Subject = model.Subject;

        //        // Soraglary täzelemek
        //        foreach (var questionModel in model.Questions)
        //        {
        //            var question = test.Questions.FirstOrDefault(q => q.Id == questionModel.Id);
        //            if (question == null)
        //            {
        //                // Täze sorag
        //                question = new Question
        //                {
        //                    QuestionText = questionModel.QuestionText,
        //                    Points = questionModel.Points
        //                };
        //                test.Questions.Add(question);
        //            }
        //            else
        //            {
        //                // Bar bolan sораgy üýtgetmek
        //                question.QuestionText = questionModel.QuestionText;
        //                question.Points = questionModel.Points;
        //            }

        //            // Jogaplary täzelemek
        //            foreach (var answerModel in questionModel.Answers)
        //            {
        //                var answer = question.Answers.FirstOrDefault(a => a.Id == answerModel.Id);
        //                if (answer == null)
        //                {
        //                    // Täze jogap
        //                    answer = new Answer
        //                    {
        //                        AnswerText = answerModel.AnswerText,
        //                        IsCorrect = answerModel.IsCorrect
        //                    };
        //                    question.Answers.Add(answer);
        //                }
        //                else
        //                {
        //                    // Bar bolan jogaby üýtgetmek
        //                    answer.AnswerText = answerModel.AnswerText;
        //                    answer.IsCorrect = answerModel.IsCorrect;
        //                }
        //            }
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(model);
        //}

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
                    Answers =  q.Answers.Select(a => a.AnswerText).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> StartTest(int id)
        {
            var test = await _testService.GetTestForPreviewById(id);

            if (test == null)
            {
                return NotFound();
            }           

            var viewModel = new TestDTO
            {
                Id = test.Id,
                Name = test.Name,
                Description = test.Description,
                TimeLimit = 60, // minutda
                Questions = test.Questions.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Points = q.Points,
                    Answers = q.Answers.Select(a => new AnswerDTO
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText
                    }).ToList() ?? new List<AnswerDTO>()
                }).ToList()
            };

            return View(viewModel);
        }

        
    }
}
