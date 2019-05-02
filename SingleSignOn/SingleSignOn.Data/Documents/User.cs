using System.Collections.Generic;

namespace SingleSignOn.Data.Documents
{
    public sealed class User : BaseDocument
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> RefreshTokens { get; set; }
    }
}
