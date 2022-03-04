namespace StanbicIBTC.AccountOpening.Domain;

[JsonSerializable(typeof(AccountOpeningAttemptDto))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
   PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class AccountOpeningAttemptDtoJsonContext : JsonSerializerContext
{

}
