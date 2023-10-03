using AutoMapper;
using JobPosting.Dto;
using JobPosting.Repository.Entity;

namespace JobPosting.Utility
{
    public class JobPostingMappingProfile : Profile
    {
        public JobPostingMappingProfile()
        {
            CreateMap<JobPostingModel, JobPostingDto>().ReverseMap();
        }
    }
}
