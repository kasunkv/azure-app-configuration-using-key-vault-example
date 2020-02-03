using MusicStore.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MusicStore.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        public List<Album> GetAll()
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "album-data.json");
            var jsonFile = File.ReadAllText(filePath);
            var albums = JsonConvert.DeserializeObject<List<Album>>(jsonFile);
            return albums;
        }
    }
}
