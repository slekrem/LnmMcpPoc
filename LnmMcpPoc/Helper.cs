using System.Globalization;
using System.Security.Cryptography;
using System.Text;

public static class Helper
{
    public static DateTime ToDateTime(this string input)
    {
        var formats = new[]
        {
            "dd.MM.yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "dd.MM.yyyy",
            "yyyyMMddHHmm",
            "yyyy-MM-dd'T'HH:mm:ss",
            "yyyy-MM-dd'T'HH:mm:ssZ",
            "yyyy-MM-dd'T'HH:mm:ss.fffZ",
            "s",
            "yyyy-MM-dd"
        };
        if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out DateTime ts))
            return ts;
        throw new Exception();
    }

    public static HttpClient GetLnmClient(this LnMarketsOptions options, string method, string path, string @params = "")
    {
        var timestamp = GetUtcNowInUnixTimestamp();
        var signature = GetSignature(options.Secret, $"{timestamp}{method}{path}{@params}");

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("LNM-ACCESS-KEY", options.ApiKey);
        httpClient.DefaultRequestHeaders.Add("LNM-ACCESS-PASSPHRASE", options.Passphrase);
        httpClient.DefaultRequestHeaders.Add("LNM-ACCESS-SIGNATURE", signature);
        httpClient.DefaultRequestHeaders.Add("LNM-ACCESS-TIMESTAMP", timestamp.ToString());
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

        return httpClient;
    }

    private static string GetSignature(string secret, string payload)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(hash);
    }

    private static long GetUtcNowInUnixTimestamp() => (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
}
