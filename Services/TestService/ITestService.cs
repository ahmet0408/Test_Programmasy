﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestProgrammasy.DTOs;

namespace TestProgrammasy.Services.TestService
{
    public interface ITestService
    {
        Task CreateTest(CreateTestDTO createTestDTO);
        Task EditTest(EditTestDTO editTestDTO);
        Task RemoveTest(int id);
        Task<EditTestDTO> GetTestForEditById(int id);
        Task<TestDTO> GetTestForPreviewById(int id);
        List<TestDTO> GetTestList();
    }
}