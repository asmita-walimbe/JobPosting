using JobPosting.Repository.Entity;

namespace JobPosting.Web.Test.MockData
{
    public static class MockJobPostingModel
    {
        public static JobPostingModel GetJobPostingMockData()
        {
            return new JobPostingModel()
            {
                Id = Guid.NewGuid(),
                Title = "QA",
                Description = "Testing",
            };

        }

        public static List<JobPostingModel> GetAllJobPostingMockData()
        {
            return new List<JobPostingModel>()
            {
                GetJobPostingMockData(),
                new JobPostingModel()
                {
                    Id = Guid.NewGuid(),
                    Title = "Dev",
                    Description = "Development",
                }
            };
        }
    }
}
