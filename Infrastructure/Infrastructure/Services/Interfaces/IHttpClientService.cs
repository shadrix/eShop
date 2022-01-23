namespace Infrastructure.Services.Interfaces;

public interface IHttpClientService
{
    Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content);
}