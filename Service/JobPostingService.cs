using AutoMapper;
using JobPosting.Dto;
using JobPosting.Interface;
using JobPosting.Repository;
using JobPosting.Repository.Entity;

namespace JobPosting.Service
{
    public class JobPostingService : IJobPostingService
    {
        IJobPostingRepository _jobPostingRepository;
        IMapper _mapper;
        public JobPostingService(IJobPostingRepository jobPostingRepository, IMapper mapper)
        {
            _mapper = mapper;
            _jobPostingRepository = jobPostingRepository;
        }
        public async Task<List<JobPostingDto>> GetAllJobPostingAsync()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return _mapper.Map<List<JobPostingDto>>(jobPostings);
        }

        public async Task<JobPostingDto> GetJobPostingAsync(Guid id)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(id);
            return _mapper.Map<JobPostingDto>(jobPosting);
        }
        public async Task CreateJobPostingAsync(JobPostingDto jobPostingDto)
        {
            var jobPositing = _mapper.Map<JobPostingModel>(jobPostingDto);
            await _jobPostingRepository.CreateAsync(jobPositing);
        }
        public async Task UpdateJobPostingAsync(Guid id, JobPostingDto jobPostingDto)
        {
            var jobPositing = _mapper.Map<JobPostingModel>(jobPostingDto);
            await _jobPostingRepository.UpdateAsync(id, jobPositing);
        }
        public async Task DeleteJobPostingAsync(Guid id)
        {
            await _jobPostingRepository.DeleteAsync(id);
        }
    }
}
