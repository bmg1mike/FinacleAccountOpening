using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Domain.DataTransferObjects.DtoValidators
{
    public class TierOneUgradeValidator : AbstractValidator<TierOneUgrade>
    {
        public TierOneUgradeValidator()
        {
            RuleFor(x => x.NextOfKin.Address).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.PhoneNumber).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.LastName).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.Relationship).NotNull().NotEmpty();
            RuleFor(x => x.NextOfKin.DateOfBirth).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.Gender).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.Title).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.Town).NotEmpty().NotNull();
            RuleFor(x => x.NextOfKin.StateOfResidence).NotEmpty().NotNull();

            RuleFor(x => x.Platform).NotEmpty().NotNull().IsInEnum();
            RuleFor(x => x.AccountNumber).NotEmpty().NotNull().Length(10);
            RuleFor(x => x.MonthlyIncome).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.EmployerAddress).NotEmpty().NotNull();
            RuleFor(x => x.EmployerName).NotNull().NotEmpty();
            RuleFor(x => x.IdentityType).NotEmpty().NotNull();
            RuleFor(x => x.IdNumber).NotEmpty().NotNull();
            RuleFor(x => x.IdIssueDate).NotEmpty().NotNull();
            RuleFor(x => x.IdImage).NotEmpty().NotNull();
            RuleFor(x => x.Signature).NotNull().NotEmpty();
            RuleFor(x => x.PassportPhotograph).NotEmpty().NotNull();
        }
    }
}
