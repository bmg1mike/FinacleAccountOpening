namespace StanbicIBTC.AccountOpening.Domain;
public class ChessAccountRequestValidator : AbstractValidator<ChessAccountRequest>
{
    public ChessAccountRequestValidator()
    {
       RuleFor(x => x.Bvn).Length(11).NotEmpty().NotNull();
       RuleFor(x => x.ChildBirthCertificate).NotEmpty().NotNull();
       RuleFor(x => x.ChildFirstName).NotEmpty().NotNull();
       RuleFor(x => x.ChildLastName).NotEmpty().NotNull();
       RuleFor(x => x.Platform).IsInEnum();
       RuleFor(x => x.Photograph).NotEmpty().NotNull();
       RuleFor(x => x.ChildDateOfBirth).NotEmpty().NotNull(); 
    }
}
