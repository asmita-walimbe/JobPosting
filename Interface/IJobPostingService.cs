using JobPosting.Dto;

namespace JobPosting.Interface
{
    public interface IJobPostingService
    {
        Task<JobPostingDto> GetAsync(Guid id);
        Task<List<JobPostingDto>> GetAllAsync();
        Task<JobPostingDto> CreateAsync(JobPostingDto jobPostingDto);
        Task UpdateAsync(Guid id, JobPostingDto jobPostingDto);
        Task DeleteAsync(Guid id);
    }
}
