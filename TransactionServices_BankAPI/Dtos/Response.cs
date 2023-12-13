namespace TransactionServices_BankAPI.Dtos;

public class Response
{
    public string RequestId => $"{Guid.NewGuid().ToString()}";
    public string ResponCode { get; set; }
    public string ResponseMessage { get; set; }
    public object Data { get; set; }
}