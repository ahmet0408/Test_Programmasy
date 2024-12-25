using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TestProgrammasy.Data;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;

namespace TestProgrammasy.Services.TestService
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public TestService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateTest(CreateTestDTO createTestDTO)
        {
            if (createTestDTO != null)
            {
                // Soraglaryň her biri üçin dogry jogaby bellemek
                foreach (var question in createTestDTO.Questions)
                {
                    if (question.CorrectAnswerIndex >= 0 &&
                        question.CorrectAnswerIndex < question.Answers.Count)
                    {
                        foreach (var answer in question.Answers)
                        {
                            answer.IsCorrect = false;
                        }
                        question.Answers[question.CorrectAnswerIndex].IsCorrect = true;
                    }
                }
                Test test = _mapper.Map<Test>(createTestDTO);
                await _dbContext.Tests.AddAsync(test);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task EditTest(EditTestDTO editTestDTO)
        {
            if (editTestDTO != null)
            {
                // Soraglaryň her biri üçin dogry jogaby bellemek
                foreach (var question in editTestDTO.Questions)
                {
                    if (question.CorrectAnswerIndex >= 0 &&
                        question.CorrectAnswerIndex < question.Answers.Count)
                    {
                        foreach (var answer in question.Answers)
                        {
                            answer.IsCorrect = false;
                        }
                        question.Answers[question.CorrectAnswerIndex].IsCorrect = true;
                    }
                }
                Test test = _mapper.Map<Test>(editTestDTO);
                _dbContext.Tests.Update(test);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveTest(int id)
        {
            Test test = await _dbContext.Tests.FindAsync(id);
            _dbContext.Tests.Remove(test);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditTestDTO> GetTestForEditById(int id)
        {
            Test test = await _dbContext.Tests.Include(p => p.Questions).ThenInclude(p => p.Answers).FirstOrDefaultAsync(p => p.Id == id);
            var result = _mapper.Map<EditTestDTO>(test);
            return result;
        }
        public async Task<TestDTO> GetTestForPreviewById(int id)
        {
            Test test = await _dbContext.Tests.Include(p => p.Questions).ThenInclude(p => p.Answers).FirstOrDefaultAsync(p => p.Id == id);
            var result = _mapper.Map<TestDTO>(test);
            return result;
        }

        public IEnumerable<TestDTO> GetTestListByUserId(string userId)
        {
            var testList = _dbContext.Tests.Include(p => p.Questions).ThenInclude(p => p.Answers).Where(p => p.UserId == userId);
            var result = _mapper.Map<IEnumerable<TestDTO>>(testList);
            return result;
        }
        public List<TestDTO> GetTestList()
        {
            var test = _dbContext.Tests.Include(p => p.Questions).ThenInclude(p => p.Answers).ToList();
            var result = _mapper.Map<List<TestDTO>>(test);
            return result;
        }
        public async Task<TestResult> CreateTestResult(TestResultDTO testResultDTO)
        {
            if (testResultDTO != null)
            {
                TestResult testResult = _mapper.Map<TestResult>(testResultDTO);
                await _dbContext.TestResults.AddAsync(testResult);
                await _dbContext.SaveChangesAsync();
                return testResult;
            } else 
            return null;                
        }

        public async Task<TestResultDTO> CalculateTestResult(TestProgressDTO progress, int remainingTime)
        {
            // Testi almak
            var test = _dbContext.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(t => t.Id == progress.TestId);

            if (test == null)
                throw new Exception("Test tapylmady");

            int totalPoints = test.Questions.Count;
            int earnedPoints = 0;

            // Her sorag üçin jogaplary barlamak
            foreach (var question in test.Questions)
            {
                if (progress.Answers.TryGetValue(question.Id, out int answerId))
                {
                    var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                    if (correctAnswer != null && correctAnswer.Id == answerId)
                    {
                        earnedPoints++;
                    }
                }
            }

            // Netijäni hasaplamak
            double percentage = (double)earnedPoints / totalPoints * 100;
            string grade = CalculateGrade(percentage);
            int spentTime = (remainingTime >= 0) ? test.TimeLimit *60 - remainingTime : 0;
            // TestResult döretmek we ýatda saklamak
            var result = new TestResultDTO
            {
                TestId = test.Id,
                Name = test.Name,
                UserId = progress.UserId,
                Score = earnedPoints,
                TotalPoints = totalPoints,
                Percentage = Math.Round(percentage, 2),
                Grade = grade,
                CompletedAt = DateTime.Now,
                StartDate = progress.StartDate,
                TimeSpent = spentTime
            };

            var testResult = await CreateTestResult(result);

            // DTO-a öwürmek
            return new TestResultDTO
            {
                Id = testResult.Id,
                TestId = result.TestId,
                Name = result.Name,
                UserId = result.UserId,
                Score = result.Score,
                TotalPoints = result.TotalPoints,
                Percentage = result.Percentage,
                Grade = result.Grade,
                CompletedAt = result.CompletedAt,
                Description = result.Description
            };
        }

        private string CalculateGrade(double percentage)
        {
            if (percentage >= 90) return "5";
            if (percentage >= 70) return "4";
            if (percentage >= 50) return "3";
            return "2";
        }

    }
}
