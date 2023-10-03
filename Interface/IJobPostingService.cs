using JobPosting.Dto;

namespace JobPosting.Interface
{
    public interface IJobPostingService
    {
        Task<JobPostingDto> GetJobPostingAsync(Guid id);
        Task<List<JobPostingDto>> GetAllJobPostingAsync();
        Task CreateJobPostingAsync(JobPostingDto jobPostingDto);
        Task UpdateJobPostingAsync(Guid id, JobPostingDto jobPostingDto);
        Task DeleteJobPostingAsync(Guid id);
    }
}
