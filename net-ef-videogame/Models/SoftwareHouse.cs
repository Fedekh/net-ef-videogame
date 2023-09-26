using net_ef_videogame.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_ef_videogame.Classes
{
    public class SoftwareHouse
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<Videogame> Videogames { get; set; }

    }
}
