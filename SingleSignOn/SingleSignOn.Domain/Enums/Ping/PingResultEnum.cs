using System.ComponentModel;

namespace SingleSignOn.Domain.Enums.Ping
{
    public enum ServiceStatusEnum
    {
        [Description("OK")]
        Ok = 1,
        [Description("Down")]
        Shutdown = 2
    }
}
