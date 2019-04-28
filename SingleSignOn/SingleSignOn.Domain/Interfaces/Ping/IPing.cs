using SingleSignOn.Domain.ViewModels.Ping;

namespace SingleSignOn.Domain.Interfaces.Ping
{
    public interface IPing
    {
        PingResultViewModel VerifyServiceStatus();
    }
}
