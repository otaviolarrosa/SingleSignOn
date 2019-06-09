using Newtonsoft.Json;
using System.Collections.Generic;

namespace SingleSignOn.Domain.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            RefreshTokens = new List<string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordHash { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RefreshTokens { get; set; }
    }
}
