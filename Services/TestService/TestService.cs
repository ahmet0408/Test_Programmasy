using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    }
}
