using MusicStore.Shared.Models;
using System.Collections.Generic;

namespace MusicStore.Web.Models
{
    public class HomeViewModel
    {
        public string LeadMessage { get; set; }
        public List<Album> Featured { get; set; }
        public List<Album> Suggestions { get; set; }
    }
}
