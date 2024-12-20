using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class TestAnalyticsViewModel
    {
        public Dictionary<string, int> GradesDistribution { get; set; }
        public Dictionary<string, double> SubjectsPerformance { get; set; }
    }
}
