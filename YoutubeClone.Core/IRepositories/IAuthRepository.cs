using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoutubeClone.Core.Entities;

namespace YoutubeClone.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<SignInResult> CheckPasswordSignInAsync(AppUser User, string password, bool lockoutOnFailure);
        Task<AppUser> FindByUsernameAsync(string username);
        Task<AppUser> FindByEmailAsync(string username);
        Task UpdateAsync(AppUser user);
        Task<bool> UserExistsQueryAsync(Expression<Func<AppUser, bool>> filter = null);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string decodedToken);
    }
}
