using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.DTOs
{
    public class SuperHeroRequest
    {
        [Required]
        [StringLength( 32, ErrorMessage = "Name must not contain more than 32 chars." )]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength( 32, ErrorMessage = "FirstName must not contain more than 32 chars." )]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength( 32, ErrorMessage = "LastName must not contain more than 32 chars." )]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "TeamID must not be 0.")]
        public int TeamID { get; set; }

        [Required]
        [StringLength( 32, ErrorMessage = "Place must not contain more than 32 chars." )]
        public string Place { get; set; } = string.Empty;

        [Required]
        [Range( 1900, 2500, ErrorMessage = "Debut must be between 1900 and 2500." )]
        public short Debut { get; set; } = 1900;
    }
}
