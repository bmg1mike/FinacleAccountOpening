using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain;

public class TierThreeAccountOpeningRequestValidator : AbstractValidator<TierThreeAccountOpeningRequest>
{
    public TierThreeAccountOpeningRequestValidator()
    {
        RuleFor(x => x.Bvn).NotEmpty().NotNull().Length(11);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.ResidenceAddress).NotEmpty().NotNull();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Length(11);
        RuleFor(x => x.DateOfBirth).NotEmpty().NotNull();
        RuleFor(x => x.EmployerAddress).NotEmpty().NotNull();
        RuleFor(x => x.EmployerName).NotEmpty().NotNull();
        RuleFor(x => x.EmploymentStatusCode).NotEmpty().NotNull();
        RuleFor(x => x.OccupationCode).NotEmpty().NotNull();
        RuleFor(x => x.IdentityType).NotEmpty().NotNull();
        RuleFor(x => x.IdExpiryDate).NotEmpty().NotNull();
        RuleFor(x => x.IdImage).NotEmpty().NotNull();
        RuleFor(x => x.IdIssueDate).NotEmpty().NotNull();
        RuleFor(x => x.IdNumber).NotEmpty().NotNull();
        RuleFor(x => x.StateOfResidence).NotEmpty().NotNull();
    }
}

