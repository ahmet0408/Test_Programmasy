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
            Test test = _mapper.Map<Test>(createTestDTO);
            await _dbContext.Tests.AddAsync(test);
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
        public List<TestDTO> GetTestList()
        {
            var test = _dbContext.Tests.Include(p => p.Questions).ThenInclude(p => p.Answers).ToList();
            var result = _mapper.Map<List<TestDTO>>(test);
            return result;
        }
    }
}
