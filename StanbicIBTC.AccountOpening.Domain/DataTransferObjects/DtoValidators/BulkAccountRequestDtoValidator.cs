namespace StanbicIBTC.AccountOpening.Domain;

public class BulkAccountRequestDtoValidator : AbstractValidator<BulkAccountRequestDto>
{
    public BulkAccountRequestDtoValidator()
    {
        RuleFor(x => x.BranchId).NotEmpty().NotNull();
        RuleFor(x => x.CreatedBy).NotEmpty().NotNull();
        RuleFor(x => x.File).NotEmpty().NotNull();
    }
}
