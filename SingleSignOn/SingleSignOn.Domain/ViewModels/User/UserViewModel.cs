using Newtonsoft.Json;
using System.Collections.Generic;

namespace SingleSignOn.Domain.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordHash { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> RefreshTokens { get; set; }
    }
}
