public class LnMarketsOptions
{
    public required string ApiKey { get; init; }
    public required string Secret { get; init; }
    public required string Passphrase { get; init; }
    public required string BaseUrl { get; init; } = "https://api.testnet4.lnmarkets.com/v2";
}
