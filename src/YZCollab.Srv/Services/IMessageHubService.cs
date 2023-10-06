namespace YZCollab.Srv.Services
{
    public interface IMessageHubService
    {
        Task RegisterLogAsync(string message);        
    }
}