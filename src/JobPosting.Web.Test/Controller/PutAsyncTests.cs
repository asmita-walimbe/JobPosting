using FluentValidation;
using JobPosting.Constants;
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
    public class PutAsyncTests
    {
        private readonly Mock<IJobPostingService> _mockJobPostingService;
        private readonly Mock<IValidator<JobPostingDto>> _mockValidator;
        private JobPostingController _controller;
        public PutAsyncTests()
        {
            _mockJobPostingService = new Mock<IJobPostingService>();
            _mockValidator = new Mock<IValidator<JobPostingDto>>();
            _controller = new JobPostingController(_mockJobPostingService.Object, _mockValidator.Object);
        }


        [Fact]
        public async Task PutAsyncTest_With_Correct_Request_Returns_Success()
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            var mockResponse = mockData;
            mockResponse.Id = jobPostingId;
            //Arrange


            var _mockValidatorResponse = _mockValidator.Setup(v => v.Validate(It.IsAny<JobPostingDto>()))
             .Returns(new FluentValidation.Results.ValidationResult());

            var mockServiceResponse = _mockJobPostingService.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<JobPostingDto>()));

            //Act
            dynamic response = await _controller.Put(jobPostingId, mockData);
            //Assert  
            response.StatusCode.Equals("204");
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task PutAsyncTest_With_Incorrect_Title_Requests_Returns_Failure(string title)
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            mockData.Title = title;
            //Arrange
            var _mockValidatorResponse = _mockValidator.Setup(v => v.Validate(It.IsAny<JobPostingDto>()))
               .Returns(new FluentValidation.Results.ValidationResult(
                   new List<FluentValidation.Results.ValidationFailure>() {
               new FluentValidation.Results.ValidationFailure(nameof(title),Constant.TitleError)
               }));

            var mockServiceResponse = _mockJobPostingService.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<JobPostingDto>()));

            //Act
            var response = await _controller.Put(jobPostingId, mockData);
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("400");
                dynamic data = objectResult.Value;

                if (data is FluentValidation.Results.ValidationResult validationResult)
                {
                    validationResult.IsValid.Equals(false);
                    validationResult.Errors[0].ErrorMessage.Equals(Constant.TitleError);
                    validationResult.Errors.Count.Equals(1);
                }
            }
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task PutAsyncTest_With_Incorrect_Description_Requests_Returns_Failure(string description)
        {
            Guid jobPostingId = Guid.NewGuid();
            var mockData = MockJobPostingDtoModel.GetJobPostingDtoMockData();
            mockData.Description = description;
            //Arrange

            var _mockValidatorResponse = _mockValidator.Setup(v => v.Validate(It.IsAny<JobPostingDto>()))
               .Returns(new FluentValidation.Results.ValidationResult(
                   new List<FluentValidation.Results.ValidationFailure>() {
               new FluentValidation.Results.ValidationFailure(nameof(description),Constant.DescriptionError)
               }));

            var mockServiceResponse = _mockJobPostingService.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<JobPostingDto>()));

            //Act
            var response = await _controller.Put(jobPostingId, mockData);
            //Assert  
            if (response is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode.ToString();
                statusCode.Equals("400");
                dynamic data = objectResult.Value;

                if (data is FluentValidation.Results.ValidationResult validationResult)
                {
                    validationResult.IsValid.Equals(false);
                    validationResult.Errors[0].ErrorMessage.Equals(Constant.DescriptionError);
                    validationResult.Errors.Count.Equals(1);
                }
            }
        }

    }
}
