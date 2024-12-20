using System.Threading.Tasks;
using TestProgrammasy.DTOs;

namespace TestProgrammasy.Services.PdfService
{
    public interface IPdfService
    {
        Task<byte[]> GenerateTestResultPdfAsync(TestResultDTO result);
    }
}
