using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoutubeClone.Application.Errors;
using YoutubeClone.Application.Interfaces;
using YoutubeClone.Core.IRepositories;
using YoutubeClone.Infrastructure.FileUpload;

namespace YoutubeClone.Application.Videos
{
    public class UploadVideoService : IUploadVideoService
    {
        private readonly IMapper _mapper;
        private readonly IFileStorage _storage;
        private readonly IOptions<VideoSettings> _settings;
        private readonly IHostingEnvironment _host;

        public UploadVideoService(IFileStorage storage, IOptions<VideoSettings> settings, IHostingEnvironment host, IMapper mapper)
        {
            _storage = storage;
            _settings = settings;
            _host = host;
            _mapper = mapper;
        }

        public async Task<string> UploadVideo(IFormFile file)
        {
            if (file == null) throw new RestException(HttpStatusCode.BadRequest, new { Files = "No files" });
            if (file.Length == 0) throw new RestException(HttpStatusCode.BadRequest, new { Files = "Empty file" });
            if (!_settings.Value.IsSupported(file.FileName)) throw new RestException(HttpStatusCode.BadRequest, new { Files = "Empty file" });

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");

            return await _storage.StoreFile(uploadsFolderPath, file);

        }

        public void DeleteVideo(string fileName)
        {
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            File.Delete(Path.Combine(uploadsFolderPath, fileName));
        }
    }
}
