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
                .NotNull()
                .WithMessage(Constant.TitleError);

            RuleFor(x => x.Description)
                .NotEmpty()
                 .NotNull()
                .WithMessage(Constant.DescriptionError);
        }
    }
}
