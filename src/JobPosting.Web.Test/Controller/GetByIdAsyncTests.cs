using FluentValidation;
using JobPosting.Controllers;
using JobPosting.Dto;
using JobPosting.Interface;
using JobPosting.Web.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JobPosting.Web.Test.Controller
{
    public class JobPostingControllerTests
    {
        private readonly Mock<IJobPostingService> _mockJobPostingService;
        private readonly Mock<IValidator<JobPostingDto>> _mockValidator;
        private JobPostingController _controller;
        public JobPostingControllerTests()
        {
            _mockJobPostingService = new Mock<IJobPostingService>();
            _mockValidator = new Mock<IValidator<JobPostingDto>>();
            _controller = new JobPostingController(_mockJobPostingService.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task GetByIdTest_WithEmptyResponse()
        {
            Guid jobPostingId = Guid.NewGuid();
            //Arrange
            var mockServiceResponse = _mockJobPostingService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new JobPostingDto());
            //Act
            var response = await _controller.GetById(jobPostingId);
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("200");
                dynamic data = objectResult.Value;
                var resultSet = data.Result;
                resultSet.Equals(new JobPostingDto());
            }
        }

        [Fact]
        public async Task GetByIdTest_WithResponseBody()
        {
            var mockData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            //Arrange
            var mockServiceResponse = _mockJobPostingService.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(mockData);
            //Act
            var response = await _controller.GetById(mockData.Id ?? default);
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("200");
                dynamic data = objectResult.Value;
                var resultSet = data.Result;
                resultSet.Id.Equals(mockData.Id);
                resultSet.Title.Equals(mockData.Title);
                resultSet.Description.Equals(mockData.Description);
            }
        }
    }
}
