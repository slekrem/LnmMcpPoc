
using System.Text.Json.Serialization;

public class TradeModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("uid")]
    public string? Uid { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("side")]
    public string? Side { get; set; }

    [JsonPropertyName("opening_fee")]
    public decimal? OpeningFee { get; set; }

    [JsonPropertyName("closing_fee")]
    public decimal? ClosingFee { get; set; }

    [JsonPropertyName("maintenance_margin")]
    public decimal? MaintenanceMargin { get; set; }

    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; set; }

    [JsonPropertyName("margin")]
    public decimal? Margin { get; set; }

    [JsonPropertyName("leverage")]
    public decimal? Leverage { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("liquidation")]
    public decimal? Liquidation { get; set; }

    [JsonPropertyName("stoploss")]
    public decimal? Stoploss { get; set; }

    [JsonPropertyName("takeprofit")]
    public decimal? Takeprofit { get; set; }

    [JsonPropertyName("exit_price")]
    public decimal? ExitPrice { get; set; }

    [JsonPropertyName("pl")]
    public decimal? Pl { get; set; }

    [JsonPropertyName("creation_ts")]
    public long? CreationTs { get; set; }

    [JsonPropertyName("market_filled_ts")]
    public long? MarketFilledTs { get; set; }

    [JsonPropertyName("closed_ts")]
    public long? ClosedTs { get; set; }

    [JsonPropertyName("entry_price")]
    public decimal? EntryPrice { get; set; }

    [JsonPropertyName("entry_margin")]
    public decimal? EntryMargin { get; set; }

    [JsonPropertyName("open")]
    public bool? Open { get; set; }

    [JsonPropertyName("running")]
    public bool? Running { get; set; }

    [JsonPropertyName("canceled")]
    public bool? Canceled { get; set; }

    [JsonPropertyName("closed")]
    public bool? Closed { get; set; }

    [JsonPropertyName("sum_carry_fees")]
    public decimal? SumCarryFees { get; set; }
}
