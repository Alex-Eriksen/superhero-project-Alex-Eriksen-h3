using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI.Database.Entities
{
    public class SuperHero
    {
        [Key]
        public int SuperHeroID { get; set; }

        [Column( TypeName = "nvarchar(32)" )]
        public string Name { get; set; } = string.Empty;

        [Column( TypeName = "nvarchar(32)" )]
        public string FirstName { get; set; } = string.Empty;

        [Column( TypeName = "nvarchar(32)" )]
        public string LastName { get; set; } = string.Empty;

        [Column( TypeName = "nvarchar(32)" )]
        public string Place { get; set; } = string.Empty;

        [ForeignKey("Team.TeamID")]
        public int TeamID { get; set; }
        public Team Team { get; set; }

        [Column( TypeName = "smallint" )]
        public short Debut { get; set; } = 0;
    }
}
