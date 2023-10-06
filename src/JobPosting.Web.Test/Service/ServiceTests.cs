using AutoMapper;
using JobPosting.Dto;
using JobPosting.Repository;
using JobPosting.Repository.Entity;
using JobPosting.Service;
using JobPosting.Web.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JobPosting.Web.Test.Service
{
    public class ServiceTests
    {
        private readonly Mock<IJobPostingRepository> _jobPostingRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly JobPostingService _service;
        public ServiceTests()
        {
            _jobPostingRepository = new Mock<IJobPostingRepository>();
            _mapper = new Mock<IMapper>();
            _service = new JobPostingService(_jobPostingRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Test()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingModel.GetAllJobPostingMockData();
            var mockDtoData = MockJobPostingDtoModel.GetAllJobPostingDtoMockData();
            //Arrange

            var _mockValidatorResponse = _jobPostingRepository.Setup(v => v.GetAllAsync())
             .ReturnsAsync(mockData);

            var mockServiceResponse = _mapper.Setup(x => x.Map<List<JobPostingDto>>(mockData))
            .Returns(mockDtoData);
            //Act
            var response = await _service.GetAllAsync();
            //Assert  
            Assert.NotNull(response);
            response.Equals(mockDtoData);
        }

        [Fact]
        public async Task GetAsync_Test()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingModel.GetJobPostingMockData();
            var mockDtoData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            //Arrange

            var _mockValidatorResponse = _jobPostingRepository.Setup(v => v.GetByIdAsync(jobPostingId))
             .ReturnsAsync(mockData);

            var mockServiceResponse = _mapper.Setup(x => x.Map<JobPostingDto>(mockData))
            .Returns(mockDtoData);
            //Act
            var response = await _service.GetAsync(jobPostingId);
            //Assert  
            Assert.NotNull(response);
            response.Equals(mockDtoData);
        }

        [Fact]
        public async Task CreateAsync_Test()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingModel.GetJobPostingMockData();
            var mockDtoData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            //Arrange

            var mockMapperToModelResponse = _mapper.Setup(x => x.Map<JobPostingModel>(mockDtoData))
            .Returns(mockData);

            var _mockValidatorResponse = _jobPostingRepository.Setup(v => v.CreateAsync(It.IsAny<JobPostingModel>()))
             .ReturnsAsync(mockData);

            var mockMapperToDtoResponse = _mapper.SetupSequence(x => x.Map<JobPostingDto>(mockData))
            .Returns(mockDtoData);
            //Act
            var response = await _service.CreateAsync(mockDtoData);
            //Assert  
            Assert.NotNull(response);
            response.Equals(mockDtoData);
        }

        [Fact]
        public async Task UpdateAsync_Test()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingModel.GetJobPostingMockData();
            var mockDtoData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            //Arrange

            var mockMapperToModelResponse = _mapper.Setup(x => x.Map<JobPostingModel>(mockDtoData))
            .Returns(mockData);

            var _mockValidatorResponse = _jobPostingRepository.Setup(v => v.UpdateAsync(It.IsAny<Guid>(), It.IsAny<JobPostingModel>()));

            //Act and Assert
            Exception exception = null;
            try
            {
                await _service.UpdateAsync(jobPostingId, mockDtoData);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteAsync_Test()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingModel.GetJobPostingMockData();
            var mockDtoData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            //Arrange
            var _mockValidatorResponse = _jobPostingRepository.Setup(v => v.DeleteAsync(It.IsAny<Guid>()));

            //Act and Assert
            Exception exception = null;
            try
            {
                await _service.DeleteAsync(jobPostingId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.Null(exception);
        }
    }
}
