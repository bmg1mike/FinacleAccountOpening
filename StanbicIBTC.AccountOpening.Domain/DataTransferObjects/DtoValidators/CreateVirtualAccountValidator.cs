using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class CreateVirtualAccountValidator : AbstractValidator<CreateVirtualAccountDto>
{
    public CreateVirtualAccountValidator()
    {
        RuleFor(x=>x.BankVerificationNumber).NotEmpty()
            .NotNull()
            .Length(11,11);
        
        RuleFor(x => x.PhoneNumber).NotEmpty()
            .NotNull()
            .Length(11,11);

        RuleFor(x => x.SecretWord).NotEmpty()
            .NotNull();
    }
}

