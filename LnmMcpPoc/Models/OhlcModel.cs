using System.Text.Json.Serialization;

public class OhlcModel
{
    [JsonPropertyName("time")]
    public long Time { get; set; }

    [JsonPropertyName("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("high")]
    public decimal High { get; set; }

    [JsonPropertyName("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("close")]
    public decimal Close { get; set; }

    [JsonPropertyName("volume")]
    public decimal Volume { get; set; }

    [JsonIgnore]
    public DateTime TimeAsDateTime
    {
        get
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(Time).UtcDateTime;
        }
    }
}
