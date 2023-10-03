using FluentValidation;
using JobPosting.Dto;
using JobPosting.Constants;

namespace JobPosting.Validators
{
    public class JobPostingDtoValidator : AbstractValidator<JobPostingDto>
    {
        public JobPostingDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(Constant.TitleError);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(Constant.DescriptionError);
        }
    }
}
