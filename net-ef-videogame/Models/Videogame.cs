using Microsoft.EntityFrameworkCore;
using net_ef_videogame.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_ef_videogame.Helpers
{
    [Table("videogames")]
    [Index (nameof(Id), IsUnique =true)]
    public class Videogame
    {
        [Key]
        public long Id { get;set; }
        public string Name { get; set; }

        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }

        public long SoftwareHouseId { get; set; }
        public SoftwareHouse? SoftwareHouse { get; set; }

        public override string ToString()
        {
            return $"{Name} - Descrizione: {Overview} \n Rilasciato: {(ReleaseDate.ToString("dd-MM-yyyy"))} da - {(SoftwareHouse != null ? SoftwareHouse.Name : "Non disponibile")}";
        }

    }
}
