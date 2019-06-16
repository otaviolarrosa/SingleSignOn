using System.Collections.Generic;
using Newtonsoft.Json;
using SingleSignOn.Domain.ViewModels.Permissions;
using SingleSignOn.Domain.ViewModels.User;

namespace SingleSignOn.Domain.ViewModels.UserGroup
{
    public class UserGroupViewModel : BaseViewModel
    {
        public UserGroupViewModel()
        {
            Users = new List<string>();
            Permissions = new List<string>();
        }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string GroupName { get; set; }
        
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public List<string> Users { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public List<string> Permissions { get; set; }
    }
}