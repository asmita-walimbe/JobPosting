using FluentValidation;
using FluentValidation.Results;
using JobPosting.Dto;
using JobPosting.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JobPosting.Controllers
{
    [Route("api/JobPosting")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        IJobPostingService _jobPostingService;
        IValidator<JobPostingDto> _validator;
        public JobPostingController(IJobPostingService jobPostingService, IValidator<JobPostingDto> validator)
        {
            _jobPostingService = jobPostingService;
            _validator = validator;
        }

        /// <summary>
        /// Get All Job Postings
        /// </summary>
        /// <returns>Returns all job postings</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = _jobPostingService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get JobPosting by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns particular job posting entry by Id</returns>
        /// 
        [Route("getById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById([Required]Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var response = _jobPostingService.GetAsync(id);
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        /// <summary>
        /// Insert new entry for JobPosting
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns>Retursns Created StatusCode</returns>
       // [Route("insert")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobPostingDto jobPostingDto)
        {
            var validationResult = _validator.Validate(jobPostingDto);
            if (validationResult.IsValid)
            {
                var response = await _jobPostingService.CreateAsync(jobPostingDto);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            return BadRequest(validationResult);
        }

        /// <summary>
        /// Update an existing JobPosting entry 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        //[Route("update/{id}")]
        [HttpPut()]
        public async Task<IActionResult> Put(Guid id, [FromBody] JobPostingDto jobPostingDto)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var validationResult = _validator.Validate(jobPostingDto);
            if (validationResult.IsValid)
            {
                await _jobPostingService.UpdateAsync(id, jobPostingDto);
                return NoContent();
            }
            return BadRequest(validationResult);
        }

        /// <summary>
        /// Delete JobPost by Id
        /// </summary>
        /// <param name="id"></param>
       // [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            await _jobPostingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
