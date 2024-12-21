using System.Collections.Generic;
using System.Threading.Tasks;
using TestProgrammasy.DTOs;

namespace TestProgrammasy.Services.TestResultService
{
    public interface ITestResultService
    {
        Task<TestResultDTO> GetTestResultByIdAsync(int id);
        Task<List<TestResultDTO>> GetAllTestResultsAsync();
        Task<List<TestResultDTO>> GetTeacherTestResultsAsync(string userId);
        Task<List<TestResultDTO>> GetStudentTestResultsAsync(string studentId);
        Task<TestResultDTO> GetTestResultWithDetailsAsync(int id);
        Task<TestAnalyticsViewModel> GetTestAnalyticsAsync();

    }
}
