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
        public async Task<List<JobPostingDto>> GetAllAsync()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return _mapper.Map<List<JobPostingDto>>(jobPostings);
        }

        public async Task<JobPostingDto> GetAsync(Guid id)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(id);
            return _mapper.Map<JobPostingDto>(jobPosting);
        }
        public async Task<JobPostingDto> CreateAsync(JobPostingDto jobPostingDto)
        {
            var jobPositing = _mapper.Map<JobPostingModel>(jobPostingDto);
            var response = await _jobPostingRepository.CreateAsync(jobPositing);
            var responseModel = _mapper.Map<JobPostingDto>(response);

            return responseModel;
        }
        public async Task UpdateAsync(Guid id, JobPostingDto jobPostingDto)
        {
            var jobPositing = _mapper.Map<JobPostingModel>(jobPostingDto);
            await _jobPostingRepository.UpdateAsync(id, jobPositing);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _jobPostingRepository.DeleteAsync(id);
        }
    }
}
