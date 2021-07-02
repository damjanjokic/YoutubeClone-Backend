using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.User.Commands;
using YoutubeClone.Core.IRepositories;

namespace YoutubeClone.Application.User.Handlers
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, IdentityResult>
    {
        private readonly IAuthRepository _authRepository;

        public ConfirmEmailHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

        }
        public async Task<IdentityResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.FindByEmailAsync(request.Email);
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            return await _authRepository.ConfirmEmailAsync(user, decodedToken);
        }
    }
}
