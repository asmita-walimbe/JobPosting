using FluentValidation;
using JobPosting.Dto;

namespace JobPosting.Validators
{
    public class JobPostingDtoValidator : AbstractValidator<JobPostingDto>
    {
        public JobPostingDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");
        }
    }
}
