using SingleSignOn.Domain.Enums.Ping;

namespace SingleSignOn.Domain.ViewModels.Ping
{
    public class PingResultViewModel : BaseViewModel
    {
        public PingResultViewModel() { }

        public string DatabaseStatus { get; set; }
        public string CacheServiceStatus { get; set; }
    }
}
