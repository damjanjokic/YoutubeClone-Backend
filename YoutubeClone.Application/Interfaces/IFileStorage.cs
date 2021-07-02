using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeClone.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> StoreFile(string uploadsFolderPath, IFormFile file);
    }
}
