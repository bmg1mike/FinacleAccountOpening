namespace StanbicIBTC.AccountOpening.Domain;

public class CreateVirtualAccountValidator : AbstractValidator<CreateVirtualAccountDto>
{
    public CreateVirtualAccountValidator()
    {
        
        RuleFor(x => x.PhoneNumber).NotEmpty()
            .NotNull()
            .Length(11);

        RuleFor(x => x.SecretWord).NotEmpty()
            .NotNull();
    }
}

