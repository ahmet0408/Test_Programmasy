using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammasy.Data;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;
using TestProgrammasy.Services.UserService;

namespace TestProgrammasy.Services.TestResultService
{
    public class TestResultService: ITestResultService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TestResultService(ApplicationDbContext dbContext, IMapper mapper, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<TestResultDTO> GetTestResultByIdAsync(int id)
        {
            var testResult = await _dbContext.TestResults.Include(tr => tr.User).Include(tr => tr.Test).FirstOrDefaultAsync(tr => tr.Id == id);
            var result = _mapper.Map<TestResultDTO>(testResult);
            return result;
        }

        public async Task<List<TestResultDTO>> GetAllTestResultsAsync()
        {
            return await _dbContext.TestResults
                .Include(tr => tr.User)
                .OrderByDescending(tr => tr.CompletedAt)
                .Select(tr => new TestResultDTO
                {
                    Id = tr.Id,
                    TestId = tr.TestId,
                    Name = tr.Name,
                    UserId = tr.UserId,
                    Score = tr.Score,
                    StudentName = tr.User.FirstName + " " + tr.User.LastName,
                    Description = tr.Description,
                    TotalPoints = tr.TotalPoints,
                    Percentage = tr.Percentage,
                    Grade = tr.Grade,
                    CompletedAt = tr.CompletedAt
                })
                .ToListAsync();
        }

        public async Task<List<TestResultDTO>> GetTeacherTestResultsAsync(string userId)
        {
            var testResults = await _dbContext.TestResults.Include(p => p.Test).Include(p => p.User).Where(p => p.Test.UserId == userId).OrderByDescending(p => p.CompletedAt).ToListAsync();
            var result = _mapper.Map<List<TestResultDTO>>(testResults);
            return result;
        }

        public async Task<List<TestResultDTO>> GetStudentTestResultsAsync(string studentId)
        {
            return await _dbContext.TestResults
                .Where(tr => tr.UserId == studentId)
                .OrderByDescending(tr => tr.CompletedAt)
                .Select(tr => new TestResultDTO
                {
                    Id = tr.Id,
                    TestId = tr.TestId,
                    Name = tr.Name,
                    UserId = tr.UserId,
                    Score = tr.Score,                    
                    Description = tr.Description,
                    TotalPoints = tr.TotalPoints,
                    Percentage = tr.Percentage,
                    Grade = tr.Grade,
                    CompletedAt = tr.CompletedAt
                })
                .ToListAsync();
        }

        public async Task<TestResultDTO> GetTestResultWithDetailsAsync(int id)
        {
            var testResult = await _dbContext.TestResults.Include(tr => tr.User).Include(tr => tr.Test)
                    .ThenInclude(t => t.Questions)
                        .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(tr => tr.Id == id);
            var result = _mapper.Map<TestResultDTO>(testResult);

            return result;
        }

        public async Task<TestAnalyticsViewModel> GetTestAnalyticsAsync()
        {
            var results = await _dbContext.TestResults.ToListAsync();

            var gradesDistribution = results
                .GroupBy(r => r.Grade)
                .ToDictionary(g => g.Key, g => g.Count());

            var subjectsPerformance = results
                .GroupBy(r => r.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(r => r.Percentage)
                );

            return new TestAnalyticsViewModel
            {
                GradesDistribution = gradesDistribution,
                SubjectsPerformance = subjectsPerformance
            };
        }
    }
}

