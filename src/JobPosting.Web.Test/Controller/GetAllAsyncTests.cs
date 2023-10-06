using FluentValidation;
using JobPosting.Controllers;
using JobPosting.Dto;
using JobPosting.Interface;
using JobPosting.Web.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JobPosting.Web.Test.Controller
{
    public class GetAllAsyncTests
    {
        private readonly Mock<IJobPostingService> _mockJobPostingService;
        private readonly Mock<IValidator<JobPostingDto>> _mockValidator;
        private JobPostingController _controller;
        public GetAllAsyncTests()
        {
            _mockJobPostingService = new Mock<IJobPostingService>();
            _mockValidator = new Mock<IValidator<JobPostingDto>>();
            _controller = new JobPostingController(_mockJobPostingService.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task GetById_WithoutResponseBody()
        {
            Guid jobPostingId = Guid.NewGuid();
            //Arrange
            var mockServiceResponse = _mockJobPostingService.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<JobPostingDto>());
            //Act
            var response = await _controller.Get();
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("200");
                dynamic data = objectResult.Value;
                var resultSet = data.Result;
                resultSet.Equals(new List<JobPostingDto>());
            }
        }

        [Fact]
        public async Task GetAllTest_WithResponseBody()
        {
            var mockData = MockJobPostingDtoModel.GetAllJobPostingDtoMockData();
            //Arrange
            var mockServiceResponse = _mockJobPostingService.Setup(x => x.GetAllAsync())
                .ReturnsAsync(mockData);
            //Act
            var response = await _controller.Get();
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("200");
                dynamic data = objectResult.Value;
                var resultSet = data.Result;
                resultSet[0].Id.Equals(mockData[0].Id);
                resultSet[0].Title.Equals(mockData[0].Title);
                resultSet[0].Description.Equals(mockData[0].Description);
            }
        }
    }
}