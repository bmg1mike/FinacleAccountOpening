namespace StanbicIBTC.AccountOpening.Domain;
public partial class OutboundLogDtoValidator : AbstractValidator<OutboundLogDto>
{
    public OutboundLogDtoValidator()
    {
       RuleFor(x => x.SystemCalledName)
       .MaximumLength(10)
       ;
       RuleFor(x => x.APICalled)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.APIMethod)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       RuleFor(x => x.LogDate)
       .NotNull()
       .NotEmpty()
       ;
       RuleFor(x => x.RequestDetails)
       .NotNull()
       .NotEmpty()
       .MaximumLength(100)
       ;
       RuleFor(x => x.RequestDateTime)
       .NotNull()
       .NotEmpty()
       ;
       RuleFor(x => x.ResponseDetails)
       .NotNull()
       .NotEmpty()
       .MaximumLength(100)
       ;
       RuleFor(x => x.ResponseDateTIme)
       .NotNull()
       .NotEmpty()
       ;
       RuleFor(x => x.ExceptionDetails)
       .NotNull()
       .NotEmpty()
       .MaximumLength(30)
       ;
       
    }
}
