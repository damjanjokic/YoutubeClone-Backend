using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.User.Queries;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.User.Handlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(IAuthRepository authRepository, IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _authRepository = authRepository;

        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
                var user = await _authRepository.FindByUsernameAsync(request.Username);

            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            if (!user.EmailConfirmed) 
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email is not confirmed" });

            var result = await _authRepository
                .CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                return new UserDto(user, _jwtGenerator);
            }

            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
