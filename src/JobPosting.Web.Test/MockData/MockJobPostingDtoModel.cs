using JobPosting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPosting.Web.Test.MockData
{
    public static class MockJobPostingDtoModel
    {
        public static JobPostingDto GetJobPostingDtoMockData()
        {
            return new JobPostingDto()
            {
                Id = Guid.NewGuid(),
                Title = "QA",
                Description = "Testing",
            };

        }

        public static List<JobPostingDto> GetAllJobPostingDtoMockData()
        {
            return new List<JobPostingDto>()
            {
                GetJobPostingDtoMockData(),
                new JobPostingDto()
                {
                    Id = Guid.NewGuid(),
                    Title = "Dev",
                    Description = "Development",
                }
            };
        }
    }
}
