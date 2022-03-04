namespace StanbicIBTC.AccountOpening.Domain;
public partial class AccountOpeningAttemptDtoValidator : AbstractValidator<AccountOpeningAttemptDto>
{
    public AccountOpeningAttemptDtoValidator()
    {
       RuleFor(x => x.FirstName)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.LastName)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.Bvn)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.Response)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.IsSuccessful)
       .NotNull()
       .NotEmpty()
       ;
       RuleFor(x => x.AttemptedDate)
       .NotNull()
       .NotEmpty()
       ;
       RuleFor(x => x.PhoneNumber)
       .NotNull()
       .NotEmpty()
       ;
       
    }
}
