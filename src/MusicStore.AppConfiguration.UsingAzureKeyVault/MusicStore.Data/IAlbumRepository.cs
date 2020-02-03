using MusicStore.Shared.Models;
using System.Collections.Generic;

namespace MusicStore.Data
{
    public interface IAlbumRepository
    {
        List<Album> GetAll();
    }
}
