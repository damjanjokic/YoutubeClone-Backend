using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.User.Commands;
using YoutubeClone.Core.Entities;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Interfaces;

namespace YoutubeClone.Application.User.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public RegisterHandler(IAuthRepository authRepository, IMapper mapper, IEmailSender emailSender)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _authRepository.UserExistsQueryAsync(x => x.NormalizedEmail == request.Email.ToUpper()))
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

            if (await _authRepository.UserExistsQueryAsync(x => x.NormalizedUserName == request.Username.ToUpper()))
                throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

            var user = _mapper.Map<AppUser>(request);
            var result = await _authRepository.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception("Problem creating user");

            var token = await _authRepository.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var verifyUrl = $"{request.Origin}/user/verifyEmail?token={token}&email={request.Email}";

            var message = $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>{verifyUrl}></a></p>";

            await _emailSender.SendEmailAsync(request.Email, "Please verify email address", message);

            return Unit.Value;
        }
    }
}
