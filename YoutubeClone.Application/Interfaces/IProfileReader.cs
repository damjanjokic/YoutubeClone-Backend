using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoutubeClone.Application.Dtos;

namespace YoutubeClone.Application.Interfaces
{
    public interface IProfileReader
    {
        Task<ProfileDto> ReadProfile(string username);
    }
}
