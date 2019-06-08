using SingleSignOn.Domain.Interfaces.Authentication;
using SingleSignOn.Domain.Interfaces.Jwt;
using SingleSignOn.Domain.ViewModels.User;
using System;

namespace SingleSignOn.Domain.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly IAuthenticationValidator authenticationValidator;
        private readonly ITokenGenerator tokenGenerator;

        public Authentication(IAuthenticationValidator authenticationValidator, ITokenGenerator tokenGenerator)
        {
            this.authenticationValidator = authenticationValidator;
            this.tokenGenerator = tokenGenerator;
        }

        public UserViewModel AuthenticateUser(UserViewModel userViewModel)
        {
            authenticationValidator.ValidateToAuthenticateUser(ref userViewModel);
            if (userViewModel.Invalid)
                return userViewModel;

            userViewModel.Token = tokenGenerator.GenerateToken(userViewModel.UserName);
            string refreshToken = tokenGenerator.GenerateRefreshToken();
            userViewModel.RefreshTokens.Add(refreshToken);
            return userViewModel;
        }
    }
}
