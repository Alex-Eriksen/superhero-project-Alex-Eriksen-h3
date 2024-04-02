using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI.Database.Entities
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Column( TypeName = "nvarchar(32)" )]
        public string TeamName { get; set; } = string.Empty;

        public ICollection<SuperHero> ?SuperHeroes { get; set; }
    }
}
