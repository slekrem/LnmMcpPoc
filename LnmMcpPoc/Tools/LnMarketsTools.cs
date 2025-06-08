using System.ComponentModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Server;

[McpServerToolType]
public class LnMarketsTools(IOptions<LnMarketsOptions> opt)
{
    [McpServerTool, Description("Add margin to a active futures trade")]
    public async Task<string> Add_margin_to_futures_trade(string id, int amount)
    {
        var path = "/v2/futures/add-margin";
        var @params = $"{{\"id\":\"{id}\",\"amount\":{amount}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Cancel all pending/open future orders")]
    public async Task<string> Cancel_all_pending_orders()
    {
        var path = "/v2/futures/all/cancel";
        var httpClient = opt.Value.GetLnmClient("DELETE", path);
        var response = await httpClient.DeleteAsync($"https://api.lnmarkets.com{path}");
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Cancel an pending/open future order")]
    public async Task<string> Cancel_an_open_future_oder(string id)
    {
        var path = "/v2/futures/cancel";
        var @params = $"{{\"id\":\"{id}\"}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Cash-in (i.e. “remove money”) from a trade. Funds are first removed from the trade’s PL (if any), then from the trade’s margin. Note that cashing-in increases the trade’s leverage ; the whole margin hence isn’t available since leverage is bounded.")]
    public async Task<string> Cash_in(string id, int amount)
    {
        var path = "/v2/futures/cash-in";
        var @params = $"{{\"id\":\"{id}\",\"amount\":{amount}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Close all active/running positions")]
    public async Task<string> Close_all_active_positions()
    {
        var path = "/v2/futures/all/close";
        var httpClient = opt.Value.GetLnmClient("DELETE", path);
        var response = await httpClient.DeleteAsync($"https://api.lnmarkets.com{path}");
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get users futures trades. Type Allowed values: running, open, closed")]
    public async Task<string> Get_users_futures_trades(string type)
    {
        var path = "/v2/futures";
        var @params = $"type={type}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        var data = await httpClient.GetFromJsonAsync<List<TradeModel>>($"https://api.lnmarkets.com/v2/futures?{@params}");
        data = data.Where(x => x.Canceled == false).ToList();
        return JsonSerializer.Serialize(data);
    }

    [McpServerTool, Description("Create a limit buy order")]
    public async Task<string> Create_a_limit_buy_order(int leverage, int quantity, int price, int takeprofit = 0)
    {
        var path = "/v2/futures";
        var @params = $"{{\"side\":\"b\",\"type\":\"l\",\"price\":{price},\"takeprofit\":{takeprofit},\"leverage\":{leverage},\"quantity\":{quantity}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a limit sell order")]
    public async Task<string> Create_a_limit_sell_order(int leverage, int quantity, int price, int takeprofit = 0)
    {
        var path = "/v2/futures";
        var @params = $"{{\"side\":\"s\",\"type\":\"l\",\"price\":{price},\"takeprofit\":{takeprofit},\"leverage\":{leverage},\"quantity\":{quantity}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a market buy order")]
    public async Task<string> Create_a_market_buy_order(int leverage, int quantity, int takeprofit = 0)
    {
        var path = "/v2/futures";
        var @params = $"{{\"side\":\"b\",\"type\":\"m\",\"takeprofit\":{takeprofit},\"leverage\":{leverage},\"quantity\":{quantity}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a market sell order")]
    public async Task<string> Create_a_market_sell_order(int leverage, int quantity, int takeprofit = 0)
    {
        var path = "/v2/futures";
        var @params = $"{{\"side\":\"s\",\"type\":\"m\",\"takeprofit\":{takeprofit},\"leverage\":{leverage},\"quantity\":{quantity}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path, @params);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update stoploss on a trade")]
    public async Task<string> Update_stoploss_on_a_trade(string id, int value)
    {
        var path = "/v2/futures";
        var @params = $"{{\"id\":\"{id}\",\"type\":\"stoploss\",\"value\":{value}}}";
        var httpClient = opt.Value.GetLnmClient("PUT", path, @params);
        var response = await httpClient.PutAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update takeprofit on a trade")]
    public async Task<string> Update_takeprofit_on_a_trade(string id, int value)
    {
        var path = "/v2/futures";
        var @params = $"{{\"id\":\"{id}\",\"type\":\"takeprofit\",\"value\":{value}}}";
        var httpClient = opt.Value.GetLnmClient("PUT", path, @params);
        var response = await httpClient.PutAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Carry fees history. Retrieves carry fees history for user")]
    public async Task<string> Carry_fees_history(string from, string to, int limit = 100)
    {
        var fromMs = new DateTimeOffset(from.ToDateTime()).ToUnixTimeMilliseconds();
        var toMs = new DateTimeOffset(to.ToDateTime()).ToUnixTimeMilliseconds();

        var path = "/v2/futures/carry-fees";
        var @params = $"from={fromMs}&to={toMs}&limit={limit}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Retrieve fixing history at most 1000 entries between two given timestamps")]
    public async Task<string> Fixing_history(string from, string to, int limit = 1000)
    {
        var fromMs = new DateTimeOffset(from.ToDateTime()).ToUnixTimeMilliseconds();
        var toMs = new DateTimeOffset(to.ToDateTime()).ToUnixTimeMilliseconds();

        var path = "/v2/futures/history/fixing";
        var @params = $"from={fromMs}&to={toMs}&limit={limit}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Retrieve index history at most 1000 entries between two given timestamps")]
    public async Task<string> Index_history(string from, string to, int limit = 1000)
    {
        var fromMs = new DateTimeOffset(from.ToDateTime()).ToUnixTimeMilliseconds();
        var toMs = new DateTimeOffset(to.ToDateTime()).ToUnixTimeMilliseconds();

        var path = "/v2/futures/history/index";
        var @params = $"from={fromMs}&to={toMs}&limit={limit}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Retrieve index history at most 1000 entries between two given timestamps")]
    public async Task<string> Price_history(string from, string to, int limit = 1000)
    {
        var fromMs = new DateTimeOffset(from.ToDateTime()).ToUnixTimeMilliseconds();
        var toMs = new DateTimeOffset(to.ToDateTime()).ToUnixTimeMilliseconds();

        var path = "/v2/futures/history/price";
        var @params = $"from={fromMs}&to={toMs}&limit={limit}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Get the 10 first users by P&L broke down by day/week/month/all-time")]
    public async Task<string> Leaderboard()
    {
        var path = "/v2/futures/leaderboard";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get the futures market details")]
    public async Task<string> Futures_market()
    {
        var path = "/v2/futures/market";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get the futures ticker")]
    public async Task<string> Ticker()
    {
        var path = "/v2/futures/ticker";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get a futures trade by id")]
    public async Task<string> Get_futures_trade(string id)
    {
        var path = $"/v2/futures/trades/{id}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Retrieve OHLCs history at most 1000 entries between two given timestamps.")]
    public async Task<string> OhlcHistory(string from, string to)
    {
        var fromDt = from.ToDateTime();
        var toDt = to.ToDateTime();
        var totalMinutes = (int)(toDt - fromDt).TotalMinutes;

        var ranges = new (string Name, int Minutes)[]
        {
            ("1", 1), ("3", 3), ("5", 5), ("10", 10), ("15", 15), ("30", 30), ("45", 45),
            ("60", 60), ("120", 120), ("180", 180), ("240", 240),
            ("1D", 1440), ("1W", 10080), ("1M", 43200), ("3M", 129600)
        };

        (string Name, int Minutes) selected = ranges[0];
        int limit = 100;
        foreach (var r in ranges)
        {
            int l = (int)Math.Ceiling((double)totalMinutes / r.Minutes);
            if (l <= 100)
            {
                selected = r;
                limit = l;
            }
            else
            {
                break;
            }
        }

        var fromMs = new DateTimeOffset(fromDt).ToUnixTimeMilliseconds();
        var toMs = new DateTimeOffset(toDt).ToUnixTimeMilliseconds();

        var path = "/v2/futures/ohlcs";
        var @params = $"from={fromMs}&to={toMs}&range={selected.Name}&limit={limit}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Get LN Markets ApiKey, Secret, Passphrase and BaseUrl")]
    public string Get_Ln_Markets_API_Keys()
    {
        return JsonSerializer.Serialize(opt);
    }

    [McpServerTool, Description("Close all options trades")]
    public async Task<string> Close_all_options_trades()
    {
        var path = "/v2/options/all/close";
        var httpClient = opt.Value.GetLnmClient("DELETE", path);
        var response = await httpClient.DeleteAsync($"https://api.lnmarkets.com{path}");
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get user’s vanilla options trades")]
    public async Task<string> Get_users_vanilla_options_trades()
    {
        var path = "/v2/options";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    //[McpServerTool, Description("Allows user to update settlement parameter in running option trade")]
    public async Task<string> Update_option_trade(string id, string settlement)
    {
        throw new NotImplementedException();
    }

    [McpServerTool, Description("Create a new options trade")]
    public async Task<string> Create_a_new_options_trade(string side, int quantity, string settlement, string instrument_name)
    {
        var path = "/v2/options";
        var @params = $"{{\"side\":\"{side}\",\"quantity\":{quantity},\"settlement\":\"{settlement}\",\"instrument_name\":\"{instrument_name}\"}}";
        var httpClient = opt.Value.GetLnmClient("PUT", path, @params);
        var response = await httpClient.PutAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Close an option trade")]
    public async Task<string> Close_an_option_trade(string id)
    {
        var path = "/v2/options";
        var @params = $"id={id}";
        var httpClient = opt.Value.GetLnmClient("DELETE", path, @params);
        var response = await httpClient.DeleteAsync($"https://api.lnmarkets.com{path}?{@params}");
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Returns the volatility of the given instrument")]
    public async Task<string> Get_instrument(string instrument_name)
    {
        var path = "/v2/options/instrument";
        var @params = $"instrument_name={instrument_name}";
        var httpClient = opt.Value.GetLnmClient("GET", path, @params);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}?{@params}");
    }

    [McpServerTool, Description("Return the list of available instruments")]
    public async Task<string> Get_instruments()
    {
        var path = "/v2/options/instruments";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get a option trade by id")]
    public async Task<string> Get_a_option_trade_by_id(string id)
    {
        var path = $"/v2/options/trades/{id}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Volatility index")]
    public async Task<string> Volatility_index()
    {
        var path = "/v2/options/volatility-index";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get the options market details")]
    public async Task<string> Options_market()
    {
        var path = "/v2/options/market";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get users swaps")]
    public async Task<string> Get_users_swaps()
    {
        var path = "/v2/swap";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Create a new swap")]
    public async Task<string> Internal_transfer(string in_asset, string out_asset, int in_amount)
    {
        var path = "/v2/swap";
        var @params = $"{{\"in_amount\": {in_amount}, \"in_asset\": \"{in_asset}\", \"out_asset\": \"{out_asset}\"}}";
        var httpClient = opt.Value.GetLnmClient("POST", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get swap by id")]
    public async Task<string> Get_swap_by_id(string id)
    {
        var path = $"/v2/swap/{id}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get swap by id")]
    public async Task<string> Get_swap_by_sourceId(string sourceId)
    {
        var path = $"/v2/swap/source/{sourceId}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get user information")]
    public async Task<string> Get_User()
    {
        var path = "/v2/user";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    //[McpServerTool, Description("Update user")]
    public async Task<string> Update_User()
    {
        throw new NotImplementedException();
    }

    [McpServerTool, Description("Get bitcoin addresses")]
    public async Task<string> Get_Bitcoin_Addresses()
    {
        var path = "/v2/user/bitcoin/address";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("New Bitcoin address")]
    public async Task<string> New_Bitcoin_Address()
    {
        var path = "/v2/user/bitcoin/address";
        var @params = $"{{\"format\":\"p2wpkh\"}}";
        var httpClient = opt.Value.GetLnmClient("PUT", path, @params);
        var response = await httpClient.PutAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get deposits")]
    public async Task<string> Get_Deposits()
    {
        var path = "/v2/user/deposit";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Get deposit by id")]
    public async Task<string> Get_Deposit_By_Id(string id)
    {
        var path = $"/v2/user/deposit/{id}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Create a new deposit")]
    public async Task<string> Create_a_new_deposit(int amount)
    {
        var path = "/v2/user/deposit";
        var @params = $"{{\"amount\":{amount}}}";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Deposit to the synthetic USD balance")]
    public async Task<string> Deposit_to_the_synthetic_USD_balance(int amount, string currency = "usd")
    {
        var path = "/v2/user/deposit/susd";
        var @params = $"{{\"amount\":{amount},\"currency\":\"{currency}\"}}";
        var httpClient = opt.Value.GetLnmClient("POST", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get withdrawals")]
    public async Task<string> Get_withdrawals()
    {
        var path = "/v2/user/withdraw";
        var httpClient = opt.Value.GetLnmClient("GET", path);
        return await httpClient.GetStringAsync($"https://api.lnmarkets.com{path}");
    }

    [McpServerTool, Description("Create a new withdrawal")]
    public async Task<string> Create_a_new_withdrawal(string invoice)
    {
        var path = "/v2/user/withdraw";
        var @params = $"{{\"invoice\":{invoice}}}";
        var httpClient = opt.Value.GetLnmClient("POST", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Withdraw synthetic USD")]
    public async Task<string> Withdraw_synthetic_USD(int amount, string currency = "btc")
    {
        var path = "/v2/user/withdraw/susd";
        var @params = $"{{\"amount\":{amount},\"currency\":\"{currency}\"}}";
        var httpClient = opt.Value.GetLnmClient("POST", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Internal transfer, transfer sats to another LN Markets user")]
    public async Task<string> Internal_transfer(int amount, string toUsername)
    {
        var path = "/v2/user/transfer";
        var @params = $"{{\"amount\":{amount},\"toUsername\":\"{toUsername}\"}}";
        var httpClient = opt.Value.GetLnmClient("POST", path);
        var response = await httpClient.PostAsync($"https://api.lnmarkets.com{path}", new StringContent(@params, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get the current Time")]
    public string CurrentTime()
    {
        return DateTime.Now.ToString();
    }
}
