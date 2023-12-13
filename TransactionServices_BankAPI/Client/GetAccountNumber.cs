namespace TransactionServices_BankAPI.Client; 


public class GetAccountNumber //0124557517 gtb
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GetAccountNumber(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    /*public async Task<IReadOnlyCollection<Account>> GetAccountNumberAsync(string userServiceBaseUrl, string accountId)
    {
        var httpClient = _httpClientFactory.CreateClient();

        string accountNumberEndpoint = $"api/accounts/get_account_by_Id/{accountId}";

        try
        {
            string requestUri = userServiceBaseUrl + accountNumberEndpoint;

            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                //Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                //Desirialize the JSON string to an account object using System.Text.Json
                var account = JsonSerializer.Deserialize<Account>(responseContent);
                return (IReadOnlyCollection<Account>)account;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve account details. Status code: {response.StatusCode}");

            }
        }
        finally
        {
            httpClient.Dispose();
        }
    }*/
}