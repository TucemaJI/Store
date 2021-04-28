﻿using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Models.Users;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<TokenModel> RefreshAsync(TokenModel model);
        public Task<TokenModel> SignInAsync(SignInModel model);
        public Task<TokenModel> SignInByGoogleAsync(SignInByGoogleModel model);
        public Task<TokenModel> SignInByFacebookAsync(SignInByFacebookModel model);
        public Task<IdentityResult> RegistrationAsync(UserModel model);
        public Task<IdentityResult> ConfirmEmailAsync(ConfirmModel model);
        public Task<IdentityResult> SignOutAsync(string accessToken);
        public Task RecoveryPasswordAsync(string email);
    }
}
