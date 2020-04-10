using System.Collections.Generic;

namespace SingleSignOn.Data.Documents
{
    public class UserGroup : BaseDocument
    {
        public string  GroupName { get; set; }
        public List<string> Users { get; set; }
        public List<string> Permissions { get; set; }
    }
}