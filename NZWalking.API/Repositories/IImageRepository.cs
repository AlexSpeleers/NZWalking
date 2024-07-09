using NZWalking.API.Models.Domain;
using System.Net;

namespace NZWalking.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
