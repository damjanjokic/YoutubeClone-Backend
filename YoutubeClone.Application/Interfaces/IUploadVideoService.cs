using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeClone.Application.Interfaces
{
    public interface IUploadVideoService
    {
        Task<string> UploadVideo(IFormFile File);
        void DeleteVideo(string fileName);
    }
}
