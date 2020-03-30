using Newtonsoft.Json;

namespace SingleSignOn.Domain.ViewModels.Permissions
{
    public class PermissionsViewModel : BaseViewModel
    {
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string  PermissionDescription { get; set; }
    }
}