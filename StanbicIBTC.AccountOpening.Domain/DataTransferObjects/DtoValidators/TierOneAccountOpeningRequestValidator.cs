namespace StanbicIBTC.AccountOpening.Domain;

public class TierOneAccountOpeningRequestValidator : AbstractValidator<TierOneAccountOpeningRequest>
{
    public TierOneAccountOpeningRequestValidator()
    {
        RuleFor(x => x.Bvn).Length(11,11)
                            .NotEmpty()
                            .NotNull();

        RuleFor(x => x.EmploymentStatusCode).NotEmpty()
                                            .NotNull()
                                            .Length(3,3);

        RuleFor(x => x.OccupationCode).NotEmpty()
                                        .NotNull()
                                        .Length(3,3);
        
        RuleFor(x => x.PhoneNumber).NotEmpty()
                                    .NotNull()
                                    .Length(11,11);

        RuleFor(x => x.DateOfBirth).NotEmpty()
                                    .NotNull();
                                    
    }
}
