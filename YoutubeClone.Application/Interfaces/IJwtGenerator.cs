using YoutubeClone.Core.Entities;

namespace YoutubeClone.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateToken(AppUser user);
    }
}