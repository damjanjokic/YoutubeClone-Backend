using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Subscriptions.Queries
{
    public class GetSubscriptionsQuery : IRequest<IEnumerable<ProfileDto>>
    {
        public string Username { get; set; }
        public string Predicate { get; set; }

        public GetSubscriptionsQuery(string username, string predicate)
        {
            Username = username;
            Predicate = predicate;
        }
    }
}
