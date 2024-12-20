using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammasy.Data;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;

namespace TestProgrammasy.Services.TestResultService
{
    public class TestResultService: ITestResultService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TestResultService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TestResultDTO> GetTestResultByIdAsync(int id)
        {
            var result = await _dbContext.TestResults
                .Include(tr => tr.User)
                .FirstOrDefaultAsync(tr => tr.Id == id);

            if (result == null) return null;

            return new TestResultDTO
            {
                Id = result.Id,
                TestId = result.TestId,
                TestTitle = result.TestTitle,
                UserId = result.UserId,
                Score = result.Score,
                //Subject = result.Subject,
                TotalPoints = result.TotalPoints,
                //EarnedPoints = result.EarnedPoints,
                Percentage = result.Percentage,
                Grade = result.Grade,
                //CompletedDate = result.CompletedDate
            };
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
                    TestTitle = tr.TestTitle,
                    UserId = tr.UserId,
                    Score = tr.Score,
                    //Subject = tr.Subject,
                    TotalPoints = tr.TotalPoints,
                    //EarnedPoints = tr.EarnedPoints,
                    Percentage = tr.Percentage,
                    Grade = tr.Grade,
                    CompletedDate = tr.CompletedAt
                })
                .ToListAsync();
        }

        //public async Task<List<TestResultDTO>> GetTeacherTestResultsAsync(string teacherId)
        //{
        //    var teacherSubjects = await _context.TeacherSubjects
        //        .Where(ts => ts.TeacherId == teacherId)
        //        .Select(ts => ts.Subject)
        //        .ToListAsync();

        //    return await _context.TestResults
        //        .Include(tr => tr.User)
        //        .Where(tr => teacherSubjects.Contains(tr.Subject))
        //        .OrderByDescending(tr => tr.CompletedDate)
        //        .Select(tr => new TestResultDTO
        //        {
        //            Id = tr.Id,
        //            TestId = tr.TestId,
        //            TestTitle = tr.TestTitle,
        //            UserId = tr.UserId,
        //            Score = tr.Score,
        //            Subject = tr.Subject,
        //            TotalPoints = tr.TotalPoints,
        //            EarnedPoints = tr.EarnedPoints,
        //            Percentage = tr.Percentage,
        //            Grade = tr.Grade,
        //            CompletedDate = tr.CompletedDate
        //        })
        //        .ToListAsync();
        //}

        public async Task<List<TestResultDTO>> GetStudentTestResultsAsync(string studentId)
        {
            return await _dbContext.TestResults
                .Where(tr => tr.UserId == studentId)
                .OrderByDescending(tr => tr.CompletedAt)
                .Select(tr => new TestResultDTO
                {
                    Id = tr.Id,
                    TestId = tr.TestId,
                    TestTitle = tr.TestTitle,
                    UserId = tr.UserId,
                    Score = tr.Score,
                    //Subject = tr.Subject,
                    TotalPoints = tr.TotalPoints,
                    //EarnedPoints = tr.EarnedPoints,
                    Percentage = tr.Percentage,
                    Grade = tr.Grade,
                    CompletedDate = tr.CompletedAt
                })
                .ToListAsync();
        }

        public async Task<TestResultDTO> GetTestResultWithDetailsAsync(int id)
        {
            var result = await _dbContext.TestResults
                .Include(tr => tr.User)
                .Include(tr => tr.Test)
                    .ThenInclude(t => t.Questions)
                        .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(tr => tr.Id == id);

            if (result == null) return null;

            return new TestResultDTO
            {
                Id = result.Id,
                TestId = result.TestId,
                TestTitle = result.TestTitle,
                UserId = result.UserId,
                Score = result.Score,
                //Subject = result.Subject,
                TotalPoints = result.TotalPoints,
                //EarnedPoints = result.EarnedPoints,
                Percentage = result.Percentage,
                Grade = result.Grade,
                CompletedDate = result.CompletedAt
            };
        }

        public async Task<TestAnalyticsViewModel> GetTestAnalyticsAsync()
        {
            var results = await _dbContext.TestResults.ToListAsync();

            var gradesDistribution = results
                .GroupBy(r => r.Grade)
                .ToDictionary(g => g.Key, g => g.Count());

            var subjectsPerformance = results
                .GroupBy(r => r.TestTitle)
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

