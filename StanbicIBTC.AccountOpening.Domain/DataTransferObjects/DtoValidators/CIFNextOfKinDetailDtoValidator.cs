namespace StanbicIBTC.AccountOpening.Domain;
public partial class CIFNextOfKinDetailDtoValidator : AbstractValidator<CIFNextOfKinDetailDto>
{
    public CIFNextOfKinDetailDtoValidator()
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
       RuleFor(x => x.MiddleName)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.DateOfBirthInY_M_DFormat)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.Address)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.PhoneNumber)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.Email)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       
    }
}
