namespace StanbicIBTC.AccountOpening.Domain;
public partial class CIFRequestDtoValidator : AbstractValidator<CIFRequestDto>
{
    public CIFRequestDtoValidator()
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
       RuleFor(x => x.Email)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.PhoneNumber)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.CustomerAddress)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.CustomerBVN)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.DateOfBirthInY_M_D_Format)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.Gender)
       .NotNull()
       .NotEmpty()
       .MaximumLength(100)
       ;
       RuleFor(x => x.StateOfResidence)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.LgaOfResidence)
       .NotNull()
       .NotEmpty()
       .MaximumLength(100)
       ;
       
    }
}
