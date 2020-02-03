using MusicStore.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public interface IAlbumService
    {
        Task<List<Album>> PromotionalAlbumsAsync(int albumCount = 6);
        List<Album> UserPreferenceAlbums(int albumCount = 4);
        List<Album> AllAlbums();
    }
}
